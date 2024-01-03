namespace LibraryAndService.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public bool IsActive { get; set; }
        public Guest Guest { get; set; }
        public List<Room> Room { get; set; }
    }
}
