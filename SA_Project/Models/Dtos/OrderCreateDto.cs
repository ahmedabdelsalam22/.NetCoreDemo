namespace SA_Project_API.Models.Dtos
{
    public class OrderCreateDto
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Street { get; set; }
        public string DeviceName { get; set; }
        public string IssueDescription { get; set; }
    }
}
