using LibraryAndService.Enumeration;
using System.ComponentModel.DataAnnotations;

namespace LibraryAndService.Models
{
    public class Room
    {
        public int Id { get; set; }
        [MaxLength(150)]
        public string RoomName { get; set; }
        [Range(0, 10000000000)]
        public decimal Price { get; set; }
        public RoomType RoomType { get; set; }
        [Range(0, 100)]
        public byte RoomSize { get; set; }
        public bool IsActive { get; set; }

        public Room(string roomName, decimal price, RoomType roomType, byte roomSize, bool isActive)
        {
            RoomName = roomName;
            Price = price;
            RoomType = roomType;
            RoomSize = roomSize;
            IsActive = isActive;
        }
    }
}
