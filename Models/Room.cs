namespace HotelManagement.Models;

public class Room
{
    public int Id { get; set; }
    public int Number { get; set; }
    public string Class { get; set; } = "";
    public decimal Price { get; set; }
    public string Description { get; set; } = "";
    public bool IsAvailable { get; set; } = true;
    public string Photo1 { get; set; } = "";
    public string Photo2 { get; set; } = "";
    public string Photo3 { get; set; } = "";
}
