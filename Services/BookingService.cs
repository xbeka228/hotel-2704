using HotelManagement.Data;
using HotelManagement.Models;

namespace HotelManagement.Services;

public static class BookingService
{
    public static void CreateBooking(int roomId, string guestName, string phone, string roomClass)
    {
        using var conn = Database.GetConnection();
        var cmd = conn.CreateCommand();
        cmd.CommandText = @"INSERT INTO Bookings (RoomId,GuestName,GuestPhone,RoomClass)
                            VALUES (@rid,@n,@ph,@rc)";
        cmd.Parameters.AddWithValue("@rid", roomId);
        cmd.Parameters.AddWithValue("@n", guestName);
        cmd.Parameters.AddWithValue("@ph", phone);
        cmd.Parameters.AddWithValue("@rc", roomClass);
        cmd.ExecuteNonQuery();
    }

    public static List<Booking> GetBookings(string? statusFilter = null)
    {
        var list = new List<Booking>();
        using var conn = Database.GetConnection();
        var cmd = conn.CreateCommand();

        if (statusFilter != null)
        {
            cmd.CommandText = @"SELECT b.Id, b.RoomId, b.GuestName, b.GuestPhone, b.RoomClass,
                                b.Status, b.CreatedAt, r.Number, r.Price
                                FROM Bookings b JOIN Rooms r ON b.RoomId=r.Id
                                WHERE b.Status=@s ORDER BY b.CreatedAt DESC";
            cmd.Parameters.AddWithValue("@s", statusFilter);
        }
        else
        {
            cmd.CommandText = @"SELECT b.Id, b.RoomId, b.GuestName, b.GuestPhone, b.RoomClass,
                                b.Status, b.CreatedAt, r.Number, r.Price
                                FROM Bookings b JOIN Rooms r ON b.RoomId=r.Id
                                ORDER BY b.CreatedAt DESC";
        }

        using var r = cmd.ExecuteReader();
        while (r.Read())
        {
            list.Add(new Booking
            {
                Id = r.GetInt32(0),
                RoomId = r.GetInt32(1),
                GuestName = r.GetString(2),
                GuestPhone = r.GetString(3),
                RoomClass = r.GetString(4),
                Status = r.GetString(5),
                CreatedAt = DateTime.Parse(r.GetString(6)),
                RoomNumber = r.GetInt32(7),
                RoomPrice = (decimal)r.GetDouble(8)
            });
        }
        return list;
    }

    public static bool ConfirmBooking(int bookingId)
    {
        using var conn = Database.GetConnection();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "UPDATE Bookings SET Status='Подтверждено' WHERE Id=@id AND Status='Ожидает'";
        cmd.Parameters.AddWithValue("@id", bookingId);
        int rows = cmd.ExecuteNonQuery();

        if (rows > 0)
        {
            var getRoomCmd = conn.CreateCommand();
            getRoomCmd.CommandText = "SELECT RoomId FROM Bookings WHERE Id=@id";
            getRoomCmd.Parameters.AddWithValue("@id", bookingId);
            var roomId = Convert.ToInt32(getRoomCmd.ExecuteScalar());

            var updateRoom = conn.CreateCommand();
            updateRoom.CommandText = "UPDATE Rooms SET IsAvailable=0 WHERE Id=@rid";
            updateRoom.Parameters.AddWithValue("@rid", roomId);
            updateRoom.ExecuteNonQuery();
        }
        return rows > 0;
    }

    public static bool RejectBooking(int bookingId)
    {
        using var conn = Database.GetConnection();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "UPDATE Bookings SET Status='Отклонено' WHERE Id=@id AND Status='Ожидает'";
        cmd.Parameters.AddWithValue("@id", bookingId);
        return cmd.ExecuteNonQuery() > 0;
    }
}
