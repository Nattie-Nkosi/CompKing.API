namespace CompKing.API.Models
{
    public class ComputerDto
    {
        

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int NumberOfComponents
        {
            get
            {
                return Components.Count;
            }
        }

        public ICollection<ComponentDto> Components { get; set; } = new List<ComponentDto>();
    }
}
