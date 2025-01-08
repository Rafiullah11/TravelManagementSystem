namespace TravelManagementSystem.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public string Trade { get; set; }
        public Agent Agent { get; set; }
        public string AgentId { get; set; }
        public bool Status { get; set; }

    }
}
