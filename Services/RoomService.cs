using HotelManagement.Data;
using HotelManagement.Models;

namespace HotelManagement.Services;

public static class RoomService
{
    public static List<Room> GetRoomsByClass(string roomClass)
    {
        var rooms = new List<Room>();
        using var conn = Database.GetConnection();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT * FROM Rooms WHERE Class=@c ORDER BY Number";
        cmd.Parameters.AddWithValue("@c", roomClass);
        using var r = cmd.ExecuteReader();
        while (r.Read()) rooms.Add(ReadRoom(r));
        return rooms;
    }

    public static List<Room> GetAllRooms()
    {
        var rooms = new List<Room>();
        using var conn = Database.GetConnection();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT * FROM Rooms ORDER BY Class, Number";
        using var r = cmd.ExecuteReader();
        while (r.Read()) rooms.Add(ReadRoom(r));
        return rooms;
    }

    public static Room? GetRoomById(int id)
    {
        using var conn = Database.GetConnection();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT * FROM Rooms WHERE Id=@id";
        cmd.Parameters.AddWithValue("@id", id);
        using var r = cmd.ExecuteReader();
        return r.Read() ? ReadRoom(r) : null;
    }

    public static void AddRoom(Room room)
    {
        using var conn = Database.GetConnection();
        var cmd = conn.CreateCommand();
        cmd.CommandText = @"INSERT INTO Rooms (Number,Class,Price,Description,Photo1,Photo2,Photo3)
                            VALUES (@n,@c,@p,@d,@p1,@p2,@p3)";
        cmd.Parameters.AddWithValue("@n", room.Number);
        cmd.Parameters.AddWithValue("@c", room.Class);
        cmd.Parameters.AddWithValue("@p", (double)room.Price);
        cmd.Parameters.AddWithValue("@d", room.Description);
        cmd.Parameters.AddWithValue("@p1", room.Photo1);
        cmd.Parameters.AddWithValue("@p2", room.Photo2);
        cmd.Parameters.AddWithValue("@p3", room.Photo3);
        cmd.ExecuteNonQuery();
    }

    public static void UpdateRoom(Room room)
    {
        using var conn = Database.GetConnection();
        var cmd = conn.CreateCommand();
        cmd.CommandText = @"UPDATE Rooms SET Number=@n,Class=@c,Price=@p,Description=@d,
                            IsAvailable=@a,Photo1=@p1,Photo2=@p2,Photo3=@p3 WHERE Id=@id";
        cmd.Parameters.AddWithValue("@n", room.Number);
        cmd.Parameters.AddWithValue("@c", room.Class);
        cmd.Parameters.AddWithValue("@p", (double)room.Price);
        cmd.Parameters.AddWithValue("@d", room.Description);
        cmd.Parameters.AddWithValue("@a", room.IsAvailable ? 1 : 0);
        cmd.Parameters.AddWithValue("@p1", room.Photo1);
        cmd.Parameters.AddWithValue("@p2", room.Photo2);
        cmd.Parameters.AddWithValue("@p3", room.Photo3);
        cmd.Parameters.AddWithValue("@id", room.Id);
        cmd.ExecuteNonQuery();
    }

    public static void DeleteRoom(int id)
    {
        using var conn = Database.GetConnection();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "DELETE FROM Rooms WHERE Id=@id";
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }

    public static void ToggleAvailability(int id)
    {
        using var conn = Database.GetConnection();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "UPDATE Rooms SET IsAvailable = CASE WHEN IsAvailable=1 THEN 0 ELSE 1 END WHERE Id=@id";
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }

    private static Room ReadRoom(Microsoft.Data.Sqlite.SqliteDataReader r)
    {
        return new Room
        {
            Id = r.GetInt32(0),
            Number = r.GetInt32(1),
            Class = r.GetString(2),
            Price = (decimal)r.GetDouble(3),
            Description = r.IsDBNull(4) ? "" : r.GetString(4),
            IsAvailable = r.GetInt32(5) == 1,
            Photo1 = r.IsDBNull(6) ? "" : r.GetString(6),
            Photo2 = r.IsDBNull(7) ? "" : r.GetString(7),
            Photo3 = r.IsDBNull(8) ? "" : r.GetString(8),
        };
    }
}
