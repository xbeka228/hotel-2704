namespace HotelManagement.Models;

public class Booking
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public string GuestName { get; set; } = "";
    public string GuestPhone { get; set; } = "";
    public string RoomClass { get; set; } = "";
    public string Status { get; set; } = "Ожидает";
    public DateTime CreatedAt { get; set; }
    public int RoomNumber { get; set; }
    public decimal RoomPrice { get; set; }
}
