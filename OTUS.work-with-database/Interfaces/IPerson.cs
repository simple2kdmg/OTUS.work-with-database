namespace OTUS.work_with_database.Interfaces
{
    public interface IPerson
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public byte? Age { get; set; }
    }
}