using Microsoft.Data.Sqlite;

string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "test2.db");
string connectionString = $"Data Source={dbPath}";

Console.WriteLine($"Chemin de la base de données : {dbPath}");

using (var connection = new SqliteConnection(connectionString))
{
    connection.Open();

    // Créer la table Test
    string createTableQuery = @"
    CREATE TABLE IF NOT EXISTS Test (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        Nom TEXT NOT NULL,
        Age INTEGER
    );";
    using (var createTableCommand = new SqliteCommand(createTableQuery, connection))
    {
        createTableCommand.ExecuteNonQuery();
    }

    // Insérer des valeurs de test
    string insertDataQuery = @"
    INSERT INTO Test (Nom, Age) VALUES 
    ('Alice', 30),
    ('Bob', 25),
    ('Charlie', 35);";
    using (var insertCommand = new SqliteCommand(insertDataQuery, connection))
    {
        insertCommand.ExecuteNonQuery();
    }

    // Lire et afficher les données
    string selectQuery = "SELECT * FROM Test";
    using (var selectCommand = new SqliteCommand(selectQuery, connection))
    using (var reader = selectCommand.ExecuteReader())
    {
        Console.WriteLine("Données insérées :");
        while (reader.Read())
        {
            Console.WriteLine($"ID: {reader["Id"]}, Nom: {reader["Nom"]}, Age: {reader["Age"]}");
        }
    }
}