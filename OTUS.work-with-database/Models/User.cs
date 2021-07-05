using OTUS.work_with_database.Interfaces;

namespace OTUS.work_with_database.Models
{
    public class User : IIdentity, IPerson
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public byte? Age { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return $"{Id} | {Name} | {Surname} | {Age} | {Email}";
        }
    }
}