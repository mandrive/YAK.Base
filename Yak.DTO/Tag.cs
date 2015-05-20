namespace Yak.DTO
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Tag()
        {
        }

        public Tag(string name)
        {
            Name = name;
        }

        public Tag(Database.Entities.Tag tag)
        {
            Id = tag.Id;
            Name = tag.Name;
        }
    }
}
