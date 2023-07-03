using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;

public class SQLiteConnectionScript : MonoBehaviour
{
    // SQLite database path
    private string dbPath;

    // Start is called before the first frame update
    void Start()
    {
        // Set the SQLite database path
        dbPath = "URI=file:" + Application.dataPath + "/Database/2DDungeon.db";

        // Create a new SQLite connection
        using (var connection = new SqliteConnection(dbPath))
        {
            try
            {
                // Open the connection
                connection.Open();

                // Connection established, perform SQL queries or other operations here

                // Example: Execute a SQL insert statement
                string query = "INSERT INTO Items (CharacterID, ItemID, ItemName) VALUES (@charID, @itemID, @itemName)";
                using (var command = new SqliteCommand(query, connection))
                {
                    // Set the parameter values
                    command.Parameters.AddWithValue("@charID", 2);
                    command.Parameters.AddWithValue("@itemID", 2);
                    command.Parameters.AddWithValue("@itemName", "Bow");

                    // Execute the insert statement
                    command.ExecuteNonQuery();

                    Debug.Log("Data inserted successfully.");
                }
            }
            catch (SqliteException e)
            {
                // Handle any exceptions that occur during the connection or SQL operations
                Debug.LogError("SQLite Error: " + e.Message);
            }
        }
    }
}
