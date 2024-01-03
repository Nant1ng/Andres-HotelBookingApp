namespace LibraryAndService.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string RoomName { get; set; }
        public decimal Price { get; set; }
        public int RoomType { get; set; }
        public int RoomSize { get; set; }
        public bool IsActive { get; set; }
    }
}
