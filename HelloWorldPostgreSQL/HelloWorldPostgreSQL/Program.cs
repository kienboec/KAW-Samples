using System;
using Npgsql;

namespace HelloWorldPostgreSQL
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * cleanup
             * select 'drop table if exists "' || tablename || '" cascade;'  from pg_tables where schemaname = 'public';
             *
             * see \dt
             */

            NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder();
            builder.ApplicationName = "HelloWorldPostgreSQL";
            builder.Database = "postgres";
            builder.Host = "localhost";
            builder.Port = 5432;
            builder.Username = "postgres";

            // Console.WriteLine(builder.ToString());
            // Application Name=HelloWorldPostgreSQL;Database=postgres;Host=localhost;Port=5432;Username=postgres

            using NpgsqlConnection connection = new NpgsqlConnection(builder.ToString());
            connection.Open();

            // create table (see list in prompt like \dt )
            using (var cmd = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS data (x varchar(100))", connection))
            {
                cmd.ExecuteNonQuery();
            }

            // Insert some data / SQL Injection
            using (var cmd = new NpgsqlCommand("INSERT INTO data VALUES (@p)", connection))
            {
                cmd.Parameters.AddWithValue("p", "Hello world");
                cmd.ExecuteNonQuery();
            }

            // Retrieve all rows
            using (var cmd = new NpgsqlCommand("SELECT x FROM data", connection))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader.GetString(0));
                    }
                }
            }

            connection.Close();
        }
    }
}
