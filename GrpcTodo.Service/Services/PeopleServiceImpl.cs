using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using People.Service.Models;
using Microsoft.EntityFrameworkCore;
using People.Protos;

namespace People.Service.Services
{
    public class PersonServiceImpl : PersonService.PersonServiceBase
    {
        private readonly PeopleContext _db;

        public PersonServiceImpl(PeopleContext db)
        {
            _db = db;
        }

        public override async Task<Person> Create(Person request, ServerCallContext context)
        {
            var entity = new PersonEntity
            {
                Name = request.Name,
                Surname = request.Surname,
                Age = request.Age,
                DateOfBirth = DateTime.Parse(request.DateOfBirth)
            };

            _db.People.Add(entity);
            await _db.SaveChangesAsync();

            request.Id = entity.Id;
            return request;
        }

        public override async Task<Person> Read(Id request, ServerCallContext context)
        {
            var entity = await _db.People.FindAsync(request.Id_);  // <- use Id_ here

            if (entity == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Person not found"));

            return new Person
            {
                Id = entity.Id,
                Name = entity.Name,
                Surname = entity.Surname,
                Age = entity.Age,
                DateOfBirth = entity.DateOfBirth.ToString("yyyy-MM-dd")
            };
        }

        public override async Task<OperationResult> Update(Person request, ServerCallContext context)
        {
            var entity = await _db.People.FindAsync(request.Id);  // Person.Id is fine

            if (entity == null)
                return new OperationResult { Success = false, Message = "Person not found" };

            entity.Name = request.Name;
            entity.Surname = request.Surname;
            entity.Age = request.Age;
            entity.DateOfBirth = DateTime.Parse(request.DateOfBirth);

            await _db.SaveChangesAsync();

            return new OperationResult { Success = true, Message = "Person updated successfully" };
        }

        public override async Task<OperationResult> Delete(Id request, ServerCallContext context)
        {
            var entity = await _db.People.FindAsync(request.Id_);  // <- use Id_ here too

            if (entity == null)
                return new OperationResult { Success = false, Message = "Person not found" };

            _db.People.Remove(entity);
            await _db.SaveChangesAsync();

            return new OperationResult { Success = true, Message = "Person deleted successfully" };
        }

        public override async Task<PersonList> List(People.Protos.Empty request, ServerCallContext context)
        {
            var entities = await _db.People.ToListAsync();

            var response = new PersonList();
            response.People.AddRange(entities.Select(e => new Person
            {
                Id = e.Id,
                Name = e.Name,
                Surname = e.Surname,
                Age = e.Age,
                DateOfBirth = e.DateOfBirth.ToString("yyyy-MM-dd")
            }));

            return response;
        }
    }
}
