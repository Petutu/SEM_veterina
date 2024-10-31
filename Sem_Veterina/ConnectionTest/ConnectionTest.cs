namespace Sem_Veterina.ConnectionTest
{
    using System;
    using Oracle.ManagedDataAccess.Client;

    
        class Program
        {
            static void Main(string[] args)
            {
                string connectionString = "User Id=myUsername;Password=myPassword;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=XE)))";

                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        Console.WriteLine("Připojení k databázi bylo úspěšné!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Připojení k databázi se nezdařilo.");
                        Console.WriteLine($"Chyba: {ex.Message}");
                    }
                    finally
                    {
                        if (connection.State == System.Data.ConnectionState.Open)
                        {
                            connection.Close();
                            Console.WriteLine("Připojení bylo uzavřeno.");
                        }
                    }
                }
            }
        }
    }

