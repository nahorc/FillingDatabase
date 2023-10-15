using Microsoft.VisualBasic;

namespace FillingDatabase
{
    static internal class Randomizer
    {
        static private Random random = new Random();

        static internal DateTime Date(Int32 Year, Int32 EndYear)
        {
            DateTime Date = new DateTime(
                        random.Next(Year, EndYear),
                        random.Next(1, 12),
                        random.Next(1, 28)
                    );
            return Date;
        }

        static internal Boolean Boolean()
        {
            return Convert.ToBoolean(Int(0, 1));
        }

        static internal String Degit(String result = "", Int32 count = 1)
        {
            if(count == 1)
            {
                return result + random.Next(0, 9);
            }
            return Degit(result + random.Next(0, 9), count - 1);
        }

        static internal String AdressNoTown()
        {
            String[] towns = File.ReadAllLines("files/towns.txt");
            return "ул. " + Street() + " д." + Int(1, 200);
        }

        static internal String Street()
        {
            String[] towns = File.ReadAllLines("files/towns.txt");
            return towns[random.Next(0, towns.Length - 1)] + "ская";
        }

        static internal String Region()
        {
            String[] towns = File.ReadAllLines("files/towns.txt");
            return towns[random.Next(0, towns.Length - 1)] + "ский";
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
            return "9" + Int(1000, 9999) + Int(10000, 99999);
        }

        static internal String CarPlate()
        {
            return Convert.ToString((char)random.Next('A', 'Z')) + Degit(count:3) + Convert.ToString((char)random.Next('A', 'Z')) + Convert.ToString((char)random.Next('A', 'Z')) + Degit(count:3);
        }

        static internal String CarBrand()
        {
            String[] brends = File.ReadAllLines("files/brand_car.txt");
            return brends[random.Next(0, brends.Length - 1)];
        }

        static internal DateTime DateTime(Int32 year)
        {
            while(true)
            {
                try
                {
                    return (new DateTime(year, Int(1, 12), Int(1, 31), Int(0, 23), Int(0, 60), Int(0, 60)));
                } catch { }
            }
        }

        static internal DateTime NextDateTime(DateTime dateTime, Int32 maxCountMinute)
        {
            return dateTime.AddMinutes(Int(30, maxCountMinute));
        }

        static internal String CarModel()
        {
            String[] cars = File.ReadAllLines("files/model_car.txt");
            return cars[random.Next(0, cars.Length - 1)];
        }

        static internal String Surname()
        {
            String[] surnames = File.ReadAllLines("files/surnames.txt");
            String surname = surnames[random.Next(0, surnames.Length - 1)];
            surname = surname.Substring(0, 1).ToUpper() + 
                surname.Substring(1, surname.Length - 1).ToLower();
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
