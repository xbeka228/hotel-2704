using Microsoft.Data.Sqlite;

namespace HotelManagement.Data;

public static class Database
{
    private static string _dbPath = Path.Combine(AppContext.BaseDirectory, "hotel.db");
    private static string ConnectionString => $"Data Source={_dbPath}";

    public static SqliteConnection GetConnection()
    {
        var conn = new SqliteConnection(ConnectionString);
        conn.Open();
        return conn;
    }

    public static void Initialize()
    {
        using var conn = GetConnection();
        var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            CREATE TABLE IF NOT EXISTS Users (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Login TEXT NOT NULL UNIQUE,
                Password TEXT NOT NULL,
                Role TEXT NOT NULL
            );
            CREATE TABLE IF NOT EXISTS Rooms (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Number INTEGER NOT NULL UNIQUE,
                Class TEXT NOT NULL,
                Price REAL NOT NULL,
                Description TEXT,
                IsAvailable INTEGER DEFAULT 1,
                Photo1 TEXT DEFAULT '',
                Photo2 TEXT DEFAULT '',
                Photo3 TEXT DEFAULT ''
            );
            CREATE TABLE IF NOT EXISTS Bookings (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                RoomId INTEGER NOT NULL,
                GuestName TEXT NOT NULL,
                GuestPhone TEXT NOT NULL,
                RoomClass TEXT NOT NULL,
                Status TEXT DEFAULT 'Ожидает',
                CreatedAt TEXT DEFAULT (datetime('now','localtime')),
                FOREIGN KEY (RoomId) REFERENCES Rooms(Id)
            );
        ";
        cmd.ExecuteNonQuery();
        SeedUsers(conn);
        SeedRooms(conn);
    }

    private static void SeedUsers(SqliteConnection conn)
    {
        var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT COUNT(*) FROM Users";
        if (Convert.ToInt64(cmd.ExecuteScalar()) > 0) return;

        var insert = conn.CreateCommand();
        insert.CommandText = @"
            INSERT INTO Users (Login, Password, Role) VALUES ('admin', 'admin123', 'Админ');
            INSERT INTO Users (Login, Password, Role) VALUES ('staff', 'staff123', 'Персонал');
        ";
        insert.ExecuteNonQuery();
    }

    private static void SeedRooms(SqliteConnection conn)
    {
        var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT COUNT(*) FROM Rooms";
        if (Convert.ToInt64(cmd.ExecuteScalar()) > 0) return;

        var insert = conn.CreateCommand();
        insert.CommandText = @"
            INSERT INTO Rooms (Number, Class, Price, Description, Photo1, Photo2, Photo3) VALUES
            (101, 'Standart', 7500, 'Уютный номер с одной кроватью, ТВ, Wi-Fi', 's101_1.jpg', 's101_2.jpg', 's101_3.jpg'),
            (102, 'Standart', 8000, 'Номер с двумя кроватями, ТВ, Wi-Fi', 's102_1.jpg', 's102_2.jpg', 's102_3.jpg'),
            (103, 'Standart', 7000, 'Экономный номер с одной кроватью, Wi-Fi', 's103_1.jpg', 's103_2.jpg', 's103_3.jpg'),
            (201, 'Comfort', 14000, 'Просторный номер с кроватью king-size, мини-бар, ТВ', 'c201_1.jpg', 'c201_2.jpg', 'c201_3.jpg'),
            (202, 'Comfort', 15000, 'Номер с балконом, кроватью king-size, мини-бар', 'c202_1.jpg', 'c202_2.jpg', 'c202_3.jpg'),
            (203, 'Comfort', 16000, 'Номер с видом на город, джакузи, мини-бар', 'c203_1.jpg', 'c203_2.jpg', 'c203_3.jpg'),
            (301, 'Luxe', 30000, 'Люкс с гостиной, спальней, джакузи, панорамный вид', 'l301_1.jpg', 'l301_2.jpg', 'l301_3.jpg'),
            (302, 'Luxe', 38000, 'Президентский люкс: 2 спальни, гостиная, терраса', 'l302_1.jpg', 'l302_2.jpg', 'l302_3.jpg'),
            (303, 'Luxe', 45000, 'Королевский люкс: 3 комнаты, сауна, панорамный вид', 'l303_1.jpg', 'l303_2.jpg', 'l303_3.jpg');
        ";
        insert.ExecuteNonQuery();
    }
}
