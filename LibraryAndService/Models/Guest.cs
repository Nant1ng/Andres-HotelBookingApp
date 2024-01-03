using System.ComponentModel.DataAnnotations;

namespace LibraryAndService.Models
{
    public class Guest
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string FirstName { get; set; }
        [MaxLength(100)]
        public string LastName { get; set; }
        [MaxLength(100)]
        public string PhoneNumber { get; set; }
        [MaxLength(250)]
        public string Email { get; set; }
        [Range(0, 100)]
        public int? Age { get; set; }
        public List<Booking>? Booking { get; set; } = new List<Booking>();
        public bool IsActive { get; set; }
    }
}
