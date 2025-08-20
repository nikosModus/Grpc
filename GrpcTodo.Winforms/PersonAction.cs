namespace People.Winforms
{
    public enum ActionType
    {
        Create,
        Update,
        Delete
    }

    public class PersonAction
    {
        public ActionType ActionType { get; set; }
        public People.Protos.Person PersonBefore { get; set; }
        public People.Protos.Person PersonAfter { get; set; }
    }
}
