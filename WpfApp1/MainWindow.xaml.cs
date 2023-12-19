using System;
using System.Data.SQLite;
using System.Windows;


namespace WpfApp1
{ 
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            using (SQLiteConnection connection = new SQLiteConnection(DatabaseManager.ConnectionString))
            {
                connection.Open();
                using (SQLiteCommand countCommand = new SQLiteCommand("SELECT COUNT(*) FROM Steklopakety", connection))
                {
                    int rowCount = Convert.ToInt32(countCommand.ExecuteScalar());

                    if (rowCount == 0)
                    {
                        DatabaseManager.CreateDatabase();
                        DatabaseManager.InsertData("6 SG HD Silver Grey 32 ЗАК/16/6 м1/16/6 ЗАК", 2, 50, 18);
                        DatabaseManager.InsertData("4/16/4", 1, 24, 8);
                        DatabaseManager.InsertData("4 TOP-N/14/6_м1", 1, 24, 10);
                        DatabaseManager.InsertData("4 ПлА2/16/4м1", 1, 24, 8);
                        DatabaseManager.InsertData("4 TOP-N/14ar/4 м1/14ar/4_StClBr", 2, 40, 12);
                        DatabaseManager.InsertData("10 м1/22/8 м1", 1, 40, 18);
                        InitializeComponent();
                    }
                    else
                    {
                        // База данных не пуста, выполните необходимые действия (возможно, обработка этой ситуации)
                        MessageBox.Show("База данных не пуста, не добавлены новые данные.");
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection(DatabaseManager.ConnectionString))
            {
                connection.Open();

                string selectDataSql = @"
                SELECT Artikul, Kamernost, TolshinaSP, TolshinaStekla
                FROM Steklopakety
                WHERE Artikul = @Artikul;
            ";

                using (SQLiteCommand command = new SQLiteCommand(selectDataSql, connection))
                {
                    command.Parameters.AddWithValue("@Artikul", artikul.Text);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            OutputText.Text = $"Камерность: {reader["Kamernost"]}, \n" +
                                $"Толщина стеклопакета: {reader["TolshinaSP"]}, \n" +
                                $"Толщина стекла: {reader["TolshinaStekla"]}";
                        }
                    }
                }
            }
        }
    }
}
