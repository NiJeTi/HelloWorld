namespace HelloWorld.Api.Services
{
    public class NameHolder
    {
        public NameHolder(string name)
        {
            Name = name;
        }
        
        public string Name { get; set; }
        
        public string? LastProposedName { get; set; }
    }
}