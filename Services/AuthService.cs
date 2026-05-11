using HotelManagement.Data;

namespace HotelManagement.Services;

public static class AuthService
{
    public static bool ValidateLogin(string login, string password, string role)
    {
        using var conn = Database.GetConnection();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT COUNT(*) FROM Users WHERE Login=@l AND Password=@p AND Role=@r";
        cmd.Parameters.AddWithValue("@l", login);
        cmd.Parameters.AddWithValue("@p", password);
        cmd.Parameters.AddWithValue("@r", role);
        return Convert.ToInt64(cmd.ExecuteScalar()) > 0;
    }
}
