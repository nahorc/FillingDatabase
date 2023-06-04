using System.Diagnostics;
using System.Net;

namespace database
{
    static internal class Randomizer
    {
        static private Random random = new Random();

        static internal DateTime Date(Int32 Year, Int32 EndYear)
        {
            DateTime Date = new DateTime(
                        random.Next(Year, 2023),
                        random.Next(1, 12),
                        random.Next(1, 28)
                    );
            return Date;
        }

        static internal Boolean Boolean()
        {
            return Convert.ToBoolean(Int(0, 1));
        }

        static internal String AddressNoTown()
        {
            String[] towns = File.ReadAllLines("files/towns.txt");
            return "ул. " + towns[random.Next(0, towns.Length - 1)] + "ская д." + Int(1, 200);
        }

        static internal String Town()
        {
            String[] towns = File.ReadAllLines("files/towns.txt");
            return towns[random.Next(0, towns.Length - 1)];
        }

        static internal String WorkBook()
        {
            String[] domens = new String[]
            {
                "I", "II", "III", "IV", "V"
            };
            return domens[random.Next(0, domens.Length)] + " " + Int(1000000, 9999999);
        }

        static internal String Phone()
        {
            return "+7(9" + Int(10, 99) + ")" +
                Int(100, 999) + "-" + Int(10, 99) 
                + "-" + Int(10, 99);
        }

        static internal String Surname()
        {
            String[] surnames = File.ReadAllLines("files/surnames.txt");
            String surname = surnames[random.Next(0, surnames.Length - 1)];
            surname = surname.Substring(0, 1).ToUpper() + 
                surname.Substring(1, surname.Length - 1).ToLower() + " ";
            return surname;
        }

        static internal String Name()
        {
            String[] names = File.ReadAllLines("files/names.txt");
            return names[random.Next(0, names.Length - 1)];
        }

        static internal String SNILS()
        {
            return Int(100, 999) + "-" + 
                Int(100, 999) + "-" +
                Int(100, 999) + "-" +
                Int(100, 999) + " " +
                Int(10, 99);
        }

        static internal String Passport()
        {
            return Int(100000, 999999) + " " + Int(1000, 9999);
        }

        static internal Int32 Int(Int32 start, Int32 end)
        {
            return random.Next(start, end);
        }

        static internal String Email(Int32 count)
        {
            return Email("", count);
        }

        static private String Email(String address, Int32 count)
        {
            address += Convert.ToString((char)random.Next('a', 'z'));
            String[] domens = new String[]
            {
                "@gmail.com", "@mail.ru", "@email.ru", "@rambler.ru"
            };
            if (count <= 1)
            {
                return address + domens[random.Next(0, domens.Length)];
            }
            return Email(address, count - 1);
        }
    }
}
