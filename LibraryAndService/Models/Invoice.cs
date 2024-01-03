namespace LibraryAndService.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public decimal Total { get; set; }
        public DateOnly Deadline { get; set; }
        public bool IsPayed { get; set; }
        public Booking Booking { get; set; }
    }
}
