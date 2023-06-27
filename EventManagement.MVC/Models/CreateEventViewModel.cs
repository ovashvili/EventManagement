using System.ComponentModel.DataAnnotations;

namespace EventManagement.MVC.Models
{
    public class CreateEventViewModel
    {
        [Required(ErrorMessage = "Event name is required.")]
        [StringLength(100, ErrorMessage = "Event name should not exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [StringLength(50, ErrorMessage = "City should not exceed 50 characters.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(200, ErrorMessage = "Address should not exceed 200 characters.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Ticket quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Ticket quantity must be a positive number.")]
        public int TicketQuantity { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(1000, ErrorMessage = "Description should not exceed 1000 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Start date is required.")]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid date format.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required.")]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid date format.")]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Photo")]
        public IFormFile Photo { get; set; }
    }
}
