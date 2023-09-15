using System.ComponentModel.DataAnnotations;

namespace SA_Project.Models.Order
{
    public class OrderCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Region { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string DeviceName { get; set; }
        [Required]
        public string IssueDescription { get; set; }
    }
}
