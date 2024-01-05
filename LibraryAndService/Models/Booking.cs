using LibraryAndService.Enumeration;

namespace LibraryAndService.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public byte AmountOfGuest { get; set; }
        public AmountOfBed ExtraBed { get; set; }
        public bool IsActive { get; set; }
        public Guest Guest { get; set; }
        public Room Room { get; set; }
        public Invoice Invoice { get; set; }



        public Booking(DateOnly startDate, DateOnly endDate, byte amountOfGuest, AmountOfBed extraBed, bool isActive)
        {
            StartDate = startDate;
            EndDate = endDate;
            AmountOfGuest = amountOfGuest;
            ExtraBed = extraBed;
            IsActive = isActive;
        }
    }
}
