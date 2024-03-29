﻿namespace FillingDatabase
{
    internal static class Program
    {
        static public void Main(string[] args)
        {
            Console.WriteLine(Randomizer.CarPlate());

            while (true)
            {
                Database connection = ConnectionDatabase();

                string table = Input("Таблица");
                string schema = Input("Схема");
                int countAttributes = int.Parse(Input("Кол-во атрибутов"));
                Dictionary<string, string> attributes = new Dictionary<string, string>();
                Console.WriteLine("\nВ дальнейшем вам будет предложено заполнить \n" +
                    "имена и типы атрибутов, которые необходимо выбрать из скобок. \n" +
                    "Если вашего типа там нет, то допустимые значения для поля \n" +
                    "атрибута следует указать через запятую (Строковые значения).\n " +
                    "Также существуют особые типы, требующие уточнения диапозона через запятую.\n" +
                    "Обычные типы: name, surname, patronymic, phone, int_random, email, passport, region,\nSNILS, workbook, town, adress, street, boolean, car_plate, car_brand, car_model\n" +
                    "Особые типы и их вид записи:\n" +
                    "(Рандомное число) int,1,1502\n" +
                    "(Рандомные числа с нулем) int_random,4\n" + 
                    "(Дата на рандом год) date,2020,2023\n" +
                    "(Выбрать из списка) list,Текст1,Текст2,Текст3\n" +
                    "(Первичный ключ из другой таблицы) table,nametable\n");

                for (int i = 0; i < countAttributes; i += 1)
                {
                    string nameAttribute = Input("\nИмя колонки");
                    string typeAttribute = Input("Тип колонки");
                    attributes.Add(nameAttribute, typeAttribute);
                }
                int countRowGenerate = int.Parse(Input("Кол-во записей для генерации"));

                for (int i = 1; i <= countRowGenerate; i += 1)
                {
                    Console.WriteLine($"Запись {i}");
                    connection.Generate(table, schema, attributes);
                }
                Console.ReadKey();
                Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
            }

        }

        static private string Input(string nameData)
        {
            Console.Write($"{nameData}: ");
            return Console.ReadLine();
        }

        static internal void WriteLineColor(string line, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(line);
            Console.ForegroundColor = ConsoleColor.White;
        }

        static public Database ConnectionDatabase()
        {
            Dictionary<string, string> connectionData = ReaderXML.GetData();
            Database connection = new Database();
            try
            {
                Console.Write("Подключиться ли к сохраненной базе? (0 - нет; 1 - да)\nВвод: ");
                string check = Console.ReadLine();
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