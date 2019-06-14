using Microsoft.Data.Sqlite;
using System;
using System.IO;

namespace GenerateDatabase
{
    class Program
    {
        const string sqlFilename = "Chinook_Sqlite.sql";
        const string databaseFilename = "Chinook.db";

        static void Main(string[] args)
        {
            // I used this program to create a/an sqlite database to use for the MVC_Test.
            // Initially I wanted to use a memory database and manually create the code, but generating it from DB is more convenient.
            // https://docs.microsoft.com/en-us/ef/ef6/modeling/code-first/workflows/existing-database

            // database gotten from
            // https://raw.githubusercontent.com/lerocha/chinook-database/master/ChinookDatabase/DataSources/Chinook_Sqlite.sql

            // this program will overwrite any existing Chinook.db File in the program directory.
            // since this only has to be ran once, creating a self contained exe is not needed.

            // NOTE: the database can take up to a minute to create. Since I only create it once that will be fine.
            // if needed, some tests can be done using https://stackoverflow.com/questions/1711631/improve-insert-per-second-performance-of-sqlite
            if (File.Exists(sqlFilename))
            {
                // re-create the database
                File.Delete(databaseFilename);

                Console.WriteLine("About to create the database. This will take a bit of time, please wait ...");

                using (var connection = new SqliteConnection(string.Format("Filename={0}", databaseFilename)))
                {
                    try
                    {
                        connection.Open();
                        using (var transaction = connection.BeginTransaction())
                        {
                            var command = connection.CreateCommand();
                            command.CommandText = File.ReadAllText(sqlFilename);
                            command.ExecuteNonQuery();

                            transaction.Commit();
                        }                        
                    }
                    catch (SqliteException exc)
                    {
                        Console.WriteLine(exc.ToString());
                    }

                    // connection will be closed in the dispose of SqliteConnection
                    // connection.Close();
                }
            }
            // "shouldn't happen case".
            else
            {
                Console.WriteLine(string.Format("Cannot find {0}, looking for it in path: {1}",
                    sqlFilename, new FileInfo(sqlFilename).FullName));
            }

            Console.WriteLine("Database created, should be in below directory. Press any key to exit " + Environment.NewLine + new FileInfo(databaseFilename).FullName);

            Console.ReadKey(true);
        }
    }
}
