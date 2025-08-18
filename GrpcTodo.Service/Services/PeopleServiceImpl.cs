using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using People.Protos;
using People.Service.Models;

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
            bool duplicateExists = await _db.People.AnyAsync(p =>
                p.Name == request.Name &&
                p.Surname == request.Surname &&
                p.Age == request.Age &&
                p.DateOfBirth == DateTime.Parse(request.DateOfBirth));

            if (duplicateExists)
                throw new RpcException(new Status(StatusCode.AlreadyExists, "Person with same details already exists"));

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
            var entity = await _db.People.FindAsync(request.Id_);

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
            var entity = await _db.People.FindAsync(request.Id);

            if (entity == null)
                return new OperationResult { Success = false, Message = "Person not found" };

            bool isUnchanged = string.Equals(entity.Name, request.Name, StringComparison.OrdinalIgnoreCase)
                               && string.Equals(entity.Surname, request.Surname, StringComparison.OrdinalIgnoreCase)
                               && entity.Age == request.Age
                               && entity.DateOfBirth == DateTime.Parse(request.DateOfBirth);

            if (isUnchanged)
                return new OperationResult { Success = true, Message = "No changes were made" };

            var peopleList = await _db.People.ToListAsync();

            bool duplicateExists = peopleList
                .Where(p => p.Id != request.Id)
                .Any(p => string.Equals(p.Name, request.Name, StringComparison.OrdinalIgnoreCase) &&
                          string.Equals(p.Surname, request.Surname, StringComparison.OrdinalIgnoreCase) &&
                          p.Age == request.Age &&
                          p.DateOfBirth == DateTime.Parse(request.DateOfBirth));

            if (duplicateExists)
                return new OperationResult { Success = false, Message = "Another person with the same details already exists" };

            entity.Name = request.Name;
            entity.Surname = request.Surname;
            entity.Age = request.Age;
            entity.DateOfBirth = DateTime.Parse(request.DateOfBirth);

            await _db.SaveChangesAsync();

            return new OperationResult { Success = true, Message = "Person updated successfully" };
        }

        public override async Task<OperationResult> Delete(Id request, ServerCallContext context)
        {
            var entity = await _db.People.FindAsync(request.Id_);

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

        public override async Task<PersonList> Search(SearchRequest request, ServerCallContext context)
        {
            Console.WriteLine($"[gRPC Search] Query='{request.Query}', ByName={request.ByName}");

            IQueryable<PersonEntity> query = _db.People;

            if (!string.IsNullOrWhiteSpace(request.Query))
            {
                if (request.ByName)
                    query = query.Where(p => p.Name.Contains(request.Query));
                else
                    query = query.Where(p => p.Surname.Contains(request.Query));
            }

            var results = await query.ToListAsync();

            var response = new PersonList();
            response.People.AddRange(results.Select(e => new Person
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
