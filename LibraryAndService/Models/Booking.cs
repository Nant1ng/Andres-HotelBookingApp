namespace LibraryAndService.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public bool OneExtraBed { get; set; }
        public bool IsActive { get; set; }
        public Guest Guest { get; set; }
        public Room Room { get; set; }
        public Invoice Invoice { get; set; }


        public Booking(DateOnly startDate, DateOnly endDate, bool oneExtraBed, bool isActive)
        {
            StartDate = startDate;
            EndDate = endDate;
            OneExtraBed = oneExtraBed;
            IsActive = isActive;
        }
    }
}
