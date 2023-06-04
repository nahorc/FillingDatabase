using System.ComponentModel;
using System.Xml;
using Npgsql;


namespace database
{
    internal static class Program
    {
        static public void Main(string[] args)
        {
            while (true)
            {
                Database connection = ConnectionDatabase();

                String table = Input("Таблица");
                Int32 countAttributes = Int32.Parse(Input("Кол-во атрибутов"));
                Dictionary<String, String> attributes = new Dictionary<String, String>();
                Console.WriteLine("\nВ дальнейшем вам будет предложено заполнить \n" +
                    "имена и типы атрибутов, которые необходимо выбрать из скобок. \n" +
                    "Если вашего типа там нет, то допустимые значения для поля \n" +
                    "атрибута следует указать через запятую (Строковые значения).\n " +
                    "Также существуют особые типы, требующие уточнения диапозона через запятую.\n" +
                    "Обычные типы: name, surname, patronymic, phone, email, passport, SNILS, workbook, town, address, boolean" +
                    "Особые типы и их вид записи:\n" +
                    "(Рандомное число) int,1,1502\n" +
                    "(Дата на рандом год) date,2020,2023\n" +
                    "(Выбрать из списка) list,Текст1,Текст2,Текст3\n" +
                    "(Первичный ключ из другой таблицы) table,nametable\n");

                for (Int32 i = 0; i < countAttributes; i += 1)
                {
                    String nameAttribute = Input("\nИмя колонки");
                    String typeAttribute = Input("Тип колонки");
                    attributes.Add(nameAttribute, typeAttribute);
                }
                Int32 countRowGenerate = Int32.Parse(Input("Кол-во записей для генерации"));

                for (Int32 i = 0; i <= countRowGenerate; i += 1)
                {
                    Console.WriteLine($"Запись {i}");
                    connection.Generate(table, attributes);
                }
                Console.ReadKey();
                Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
            }
            
        }

        static private String Input(String nameData)
        {
            Console.Write($"{nameData}: ");
            return Console.ReadLine();
        }

        static internal void WriteLineColor(String line, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(line);
            Console.ForegroundColor = ConsoleColor.White;
        }

        static public Database ConnectionDatabase()
        {
            Dictionary<String, String> connectionData = ReaderXML.GetData();
            Database connection = new Database();
            try
            {
                Console.Write("Подключиться ли к сохраненной базе? (0 - нет; 1 - да)\nВвод: ");
                String check = Console.ReadLine();
                if (check == "1")
                {
                    connection = new Database(connectionData);
                    WriteLineColor("Успешный вход", ConsoleColor.Green);
                }
                else
                {
                    throw new Exception("Исключение");
                }
            }
            catch
            {
                WriteLineColor("Неудачный вход", ConsoleColor.Red);
                while (true)
                {
                    connectionData["host"] = Input("Хост");
                    connectionData["username"] = Input("Логин");
                    connectionData["password"] = Input("Пароль");
                    connectionData["database"] = Input("База");

                    try
                    {
                        connection = new Database(connectionData);
                        ReaderXML.Write(connectionData);
                        WriteLineColor("Успешный вход", ConsoleColor.Green);
                        break;
                    }
                    catch
                    {
                        WriteLineColor("Неудачный вход", ConsoleColor.Red);
                    }
                }
            }
            return connection;
        }
    }
}