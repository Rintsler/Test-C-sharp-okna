using System.Data.SQLite;
using Dapper;

public class DatabaseManager
{
    public const string ConnectionString = "Data Source=MyDatabase.db;Version=3;";

    public static void CreateDatabase()
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();

            // Создание таблицы
            connection.Execute(@"
                CREATE TABLE IF NOT EXISTS Steklopakety (
                    Artikul VARCHAR(255) NOT NULL,
                    Kamernost INT NOT NULL,
                    TolshinaSP INT NOT NULL,
                    TolshinaStekla INT NOT NULL,
                    PRIMARY KEY (Artikul));
            ");
        }
    }

    public static void InsertData(string artikul, int kamernost, int tolshinaSP, int tolshinaStekla)
    {
        using (SQLiteConnection connection = new SQLiteConnection(DatabaseManager.ConnectionString))
        {
            connection.Open();
            string insertDataSql = @"
                INSERT INTO Steklopakety (Artikul, Kamernost, TolshinaSP, TolshinaStekla) 
                VALUES (@Artikul, @Kamernost, @TolshinaSP, @TolshinaStekla);";

            using (SQLiteCommand command = new SQLiteCommand(insertDataSql, connection))
            {
                command.Parameters.AddWithValue("@Artikul", artikul);
                command.Parameters.AddWithValue("@Kamernost", kamernost);
                command.Parameters.AddWithValue("@TolshinaSP", tolshinaSP);
                command.Parameters.AddWithValue("@TolshinaStekla", tolshinaStekla);

                command.ExecuteNonQuery();
            }
        }
    }
}