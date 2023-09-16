namespace SA_Project.Web.Models
{
    public class OrderUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Street { get; set; }
        public string DeviceName { get; set; }
        public string IssueDescription { get; set; }
    }
}
