using OTUS.work_with_database.Interfaces;

namespace OTUS.work_with_database.Models
{
    public class Category : IIdentity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"{Id} | {Name} | {Description}";
        }
    }
}