using System.ComponentModel.DataAnnotations;

namespace LibraryAndService.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        [Range(0, 1000000000000)]
        public decimal Total { get; set; }
        public DateOnly Deadline { get; set; }
        public bool IsPayed { get; set; }
    }
}
