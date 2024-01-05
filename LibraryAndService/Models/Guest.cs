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
        public List<Booking?> Booking { get; set; }
        public bool Booked { get; set; } = false; 
        public bool IsActive { get; set; }

        public Guest(string firstName, string lastName, string phoneNumber, string email, int? age, bool isActive)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
            Age = age;
            IsActive = isActive;
        }
    }
}
