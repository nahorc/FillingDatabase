using Npgsql;
using String = System.String;

namespace FillingDatabase
{
    internal class Database
    {
        private readonly NpgsqlConnection CONNECTION;

        internal Database(Dictionary<String, String> connectionData)
        {
            NpgsqlDataSourceBuilder DATA_SOURCE_BUILDER = 
                new NpgsqlDataSourceBuilder(
                    $"Host={connectionData["host"]};" +
                    $"Username={connectionData["username"]};" +
                    $"Password={connectionData["password"]};" +
                    $"Database={connectionData["database"]}"
                );
            CONNECTION = DATA_SOURCE_BUILDER.Build().OpenConnection();
            Close();
        }
        internal Database() {}

        internal void Generate(String table, Dictionary<String, String> attributes)
        {
            try
            {
                Open();
            }
            catch { }

            using (var command = CONNECTION.CreateCommand())
            {
                command.CommandText = "INSERT INTO \"" + table + "\" (";              
                foreach (var key in attributes.Keys)
                {
                    command.CommandText += key + ",";
                }
                command.CommandText = command.CommandText.Substring(
                    0, command.CommandText.Length - 1);
                command.CommandText += ") VALUES (";
                foreach (var key in attributes.Keys)
                {
                    command.CommandText += "@" + key + ",";
                }
                command.CommandText = command.CommandText.Substring(
                    0, command.CommandText.Length - 1);
                command.CommandText += ");";
                foreach (var attribute in attributes)
                {
                    switch (attribute.Value)
                    {
                        case "email":
                            command.Parameters.AddWithValue(
                                "@" + attribute.Key, Randomizer.Email(10));
                            break;
                        case "passport":
                            command.Parameters.AddWithValue(
                                "@" + attribute.Key, Randomizer.Passport());
                            break;
                        case "SNILS":
                            command.Parameters.AddWithValue(
                                "@" + attribute.Key, Randomizer.SNILS());
                            break;
                        case "name":
                            command.Parameters.AddWithValue(
                                "@" + attribute.Key, Randomizer.Name());
                            break;
                        case "surname":
                            command.Parameters.AddWithValue(
                                "@" + attribute.Key, Randomizer.Surname());
                            break;
                        case "patronymic":
                            command.Parameters.AddWithValue(
                                "@" + attribute.Key, Randomizer.Surname() + "ович");
                            break;
                        case "phone":
                            command.Parameters.AddWithValue(
                                "@" + attribute.Key, Randomizer.Phone());
                            break;
                        case "workbook":
                            command.Parameters.AddWithValue(
                                "@" + attribute.Key, Randomizer.WorkBook());
                            break;
                        case "town":
                            command.Parameters.AddWithValue(
                                "@" + attribute.Key, Randomizer.Town());
                            break;
                        case "address":
                            command.Parameters.AddWithValue(
                                "@" + attribute.Key, Randomizer.AddressNoTown());
                            break;
                        case "boolean":
                            command.Parameters.AddWithValue(
                                "@" + attribute.Key, Randomizer.Boolean());
                            break;
                        default:
                            String[] parameters = attribute.Value.Split(",");
                            switch (parameters[0])
                            {
                                case "int":
                                    command.Parameters.AddWithValue(
                                        "@" + attribute.Key, Randomizer.Int(
                                            Int32.Parse(parameters[1]), Int32.Parse(parameters[2])));
                                    break;
                                case "table":
                                    String[] primaryKeys = GetPrimaryKeys(parameters[1]);
                                    String primaryKey = primaryKeys[Randomizer.Int(0, primaryKeys.Length - 1)];
                                    try
                                    {
                                        command.Parameters.AddWithValue(
                                            "@" + attribute.Key, Int32.Parse(primaryKey));
                                    }
                                    catch
                                    {
                                        command.Parameters.AddWithValue(
                                            "@" + attribute.Key, primaryKey);
                                    }
                                    break;
                                case "list":
                                    command.Parameters.AddWithValue(
                                        "@" + attribute.Key, parameters[Randomizer.Int(1, parameters.Length - 1)]);
                                    break;
                                case "date":
                                    command.Parameters.AddWithValue(
                                        "@" + attribute.Key, Randomizer.Date(Int32.Parse(parameters[1]), Int32.Parse(parameters[2])));
                                    break;
                                default:
                                    throw new Exception($"{attribute.Value} неизвестный метод генерации");
                            }
                            break;
                    }
                }
                try
                {
                    command.ExecuteNonQuery();
                    Program.WriteLineColor($"Успешно создана!", ConsoleColor.Green);
                }
                catch (Exception e)
                {
                    Program.WriteLineColor($"Ошибка создания!\n{e}", ConsoleColor.Red);
                }
            }
            Close();
        }

        internal String[] GetPrimaryKeys(String table)
        {
            Int32 countRows;
            using (var command = new NpgsqlCommand($"SELECT Count(*) FROM {table}", CONNECTION))
            using (var reader = command.ExecuteReader())
            {
                reader.Read();
                countRows = Convert.ToInt32(reader[0]);
            }

            String[] primaryKeys = new String[countRows];
            using (var command = new NpgsqlCommand($"SELECT * FROM {table}", CONNECTION))
            using (var reader = command.ExecuteReader())
            {
                for (Int32 i = 0; reader.Read(); i += 1)
                {
                    primaryKeys[i] = Convert.ToString(reader[0]);
                }
            }
            return primaryKeys;
        }

        private void Close() => CONNECTION.Close();
        private void Open() => CONNECTION.Open();
    }
}
