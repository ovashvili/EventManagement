using System.ComponentModel.DataAnnotations;

namespace EventManagement.AdminMVC.Models
{
    public class EditConfigurationValueModel
    {
        [Required]
        public string Key { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Value { get; set; }
    }
}
