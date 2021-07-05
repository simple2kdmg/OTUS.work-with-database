using OTUS.work_with_database.Interfaces;

namespace OTUS.work_with_database.Models
{
    public class Advertisement : IIdentity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public long UserId { get; set; }
        public long CategoryId { get; set; }

        public override string ToString()
        {
            return $"{Id} | {Name} | {Description} | {Price} | {UserId} | {CategoryId}";
        }
    }
}