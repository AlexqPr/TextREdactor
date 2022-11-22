using Newtonsoft.Json;
using System.Reflection;
using System.Xml.Serialization;

namespace NewLastText
{ 
   
    //Есть еще  вопрос, если файл уже создан, то в него нужно добавлять значения с помощью File.AppendAllText или полностью переписывать File.WrtieAllText
    internal class Program
    {

        static string[] reader;
        static Figure priamoyg = new Figure();
        static Figure square = new Figure();
        static Figure newpriamoyg = new Figure();
        static List<Figure> prime = new List<Figure>();
        static int true1; //Переменная которая будет являться маячком и отвечает за  то менялся ли наш текст или нет
        static void Main()
        {
            true1 = 0;
            Console.WriteLine("Введите путь до файла (вместе с названием), который вы хотите открыть");
            Console.WriteLine("-----------------------------------------------------------------------");
            string pyt = Convert.ToString(Console.ReadLine());
            Console.Clear();
            Console.WriteLine("Чтобы перейти к параметрам сохранения нажмите F1\nЧтобы перейти в режим редактирования файла нажмите F2\nЧтобы выйти из  программы  нажмите Escape");
            ConsoleKeyInfo newkey = Console.ReadKey();
            if (newkey.Key == ConsoleKey.F1)
            {

            }
            else if (newkey.Key == ConsoleKey.F2)
            {
                Change(pyt);
            }
            else if(newkey.Key  == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
        

            Console.Clear();

         

           
            Console.WriteLine("Сохранить файл в  одном из трех форматов (txt,json,xml) - F1, выйти из программы - Escape");
            string proverkapyti = Provetochka.Proverka(pyt); //здесь мы проверяем расширение нашего файла

            switch (proverkapyti)
            {
                case ".txt":
                    TXT(pyt);
                    break;
                case ".json":
                    JSON(pyt);
                    break;
                case ".xml":
                    XML(pyt);
                    break;
            }
            ConsoleKeyInfo newkey2 = Console.ReadKey();
            if(newkey2.Key == ConsoleKey.F1)
            {
                Console.Clear();
                Console.WriteLine("Укажите путь, по которому вы хотите сохранить ваш файл");
                string newpyt2 = Convert.ToString(Console.ReadLine());
                string newpytsoxr = Provetochka.Proverka(newpyt2);
                switch(newpytsoxr)
                {
                    case ".txt":
                        NewTXT(newpyt2);
                        break;
                    case ".json":
                        NewJSON(newpyt2);
                        break;
                    case ".xml":
                        NewXML(newpyt2);
                        break;
                }
                Console.Clear();
                Console.WriteLine("Файл успешно сохранен");
            }
            else if(newkey2.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);

            }

            static void TXT(string pyt) //Метод для чтения
            {
                if (true1 == 0)
                {
                    Program.reader = File.ReadAllLines(pyt);//массив в котором содержатся значения из файла
                    priamoyg.Name = Program.reader[0];
                    int newLenght = Convert.ToInt32(reader[1]);
                    int newWidth = Convert.ToInt32(reader[2]);
                    priamoyg.Lenght = newLenght;
                    priamoyg.Widht = newWidth;

                    square.Name = reader[3];
                    int newLenght2 = Convert.ToInt32(reader[4]);                 //Перезапись значений на те что содержаться в файле
                    int newWidth2 = Convert.ToInt32(reader[5]);
                    square.Lenght = newLenght2;
                    square.Widht = newWidth2;

                    newpriamoyg.Name = reader[6];
                    int newLenght3 = Convert.ToInt32(reader[7]);
                    int newWidth3 = Convert.ToInt32(reader[8]);
                    newpriamoyg.Lenght = newLenght3;
                    newpriamoyg.Widht = newWidth3;

                    prime.Add(priamoyg);
                    prime.Add(square);           //добавление новых перезаписанных фигур в наш лист
                    prime.Add(newpriamoyg);

                    foreach (Figure chto in prime)
                    {
                        Console.WriteLine(chto.Name);
                        Console.WriteLine(chto.Lenght);   //Чтение листа
                        Console.WriteLine(chto.Widht);
                    }
                }
                else if(true1  == 1)
                {
                    foreach(Figure pro in prime)
                    {
                        Console.WriteLine(pro.Name);
                        Console.WriteLine(pro.Lenght);
                        Console.WriteLine(pro.Widht);
                    }
                }
               
            
            }

            static void JSON(string pyt) //метод для чтения
            {
                if (true1 == 0)
                {
                    int i = 1;
                
                    string text = File.ReadAllText(pyt);
                    prime = JsonConvert.DeserializeObject<List<Figure>>(text);
                    foreach (Figure f in prime)
                    {
                        if (i  == 1)
                        {
                            priamoyg.Name = f.Name;
                            priamoyg.Lenght = f.Lenght;
                            priamoyg.Widht = f.Widht;

                        }
                        if (i == 2)
                        {
                            square.Name = f.Name;
                            square.Lenght = f.Lenght;
                            square.Widht = f.Widht;
                        }
                        if (i == 3)
                        {
                            newpriamoyg.Name = f.Name;
                            newpriamoyg.Lenght = f.Lenght;
                            newpriamoyg.Widht = f.Widht;
                        }
                        
                        i++;
                       
                    }
                    prime.Clear();
                    prime.Add(priamoyg);
                    prime.Add(square);
                    prime.Add(newpriamoyg);
                    foreach (Figure f in prime)
                    {
                        Console.WriteLine(f.Name);
                        Console.WriteLine(f.Lenght);
                        Console.WriteLine(f.Widht);
                    }
                }
                else if (true1 == 1)
                {
                    foreach(Figure f in prime)
                    {
                        Console.WriteLine(f.Name);
                        Console.WriteLine(f.Lenght);
                        Console.WriteLine(f.Widht);
                    }
                }
            }
            
            static void NewJSON(string pyt) //метод для записи
            {
                string json = JsonConvert.SerializeObject(prime);
                File.WriteAllText(pyt, json);
            }


            static void NewTXT(string pyt) //метод для  записи
            {
                if (File.Exists(pyt))
                {
                    foreach(Figure f in prime)
                    {
                        File.AppendAllText(pyt, f.Name + "\n");
                        string newf = Convert.ToString(f.Lenght);
                        File.AppendAllText(pyt, newf + "\n");
                        string newf2  = Convert.ToString(f.Widht);
                        File.AppendAllText(pyt, newf2 + "\n");
                    }
                }
                else
                {
                    File.WriteAllText(pyt, "");
                    foreach (Figure f in prime)
                    {
                        File.AppendAllText(pyt, f.Name + "\n");
                        string newf = Convert.ToString(f.Lenght);
                        File.AppendAllText(pyt, newf + "\n");
                        string newf2 = Convert.ToString(f.Widht);
                        File.AppendAllText(pyt, newf2 + "\n");
                    }
                }
            }

            static void XML(string pyt)
            {
                if (true1 ==  0)
                {
                    
                    XmlSerializer xml = new XmlSerializer(typeof(Figure));
                    using (FileStream fs = new FileStream(pyt, FileMode.Open))
                    {
                        priamoyg = (Figure)xml.Deserialize(fs);
                    }
              
                    prime.Clear();
                    prime.Add(priamoyg);
                    //prime.Add(square);
                    //prime.Add(newpriamoyg);

                    Console.WriteLine(priamoyg.Name);
                    Console.WriteLine(priamoyg.Lenght);
                    Console.WriteLine(priamoyg.Widht);
                }
                else if(true1  == 1)
                {
                    Console.WriteLine(priamoyg.Name);
                    Console.WriteLine(priamoyg.Lenght);
                    Console.WriteLine(priamoyg.Widht);
                }
           
            }

            static  void NewXML(string  pyt)
            {
                XmlSerializer xml = new XmlSerializer(typeof(Figure));
                using(FileStream fs = new FileStream(pyt, FileMode.OpenOrCreate))
                {
                    xml.Serialize(fs, priamoyg);
                }
            }


            static void Change(string pyt)
            {
                
                Console.Clear();
                Console.WriteLine("Выберите строку, которую  хотите изменить");
                string newpyt = Provetochka.Proverka(pyt);
                switch (newpyt)
                {
                    case ".txt":
                        TXT(pyt); //Вывод содержимого файла в консоль
                        break;
                    case ".json":
                        JSON(pyt);
                        break;
                    case ".xml":
                        XML(pyt);
                        break;
                }

                int position = 1;
                Console.SetCursorPosition(0, position);
                ConsoleKeyInfo key = Console.ReadKey();

                while (key.Key != ConsoleKey.Enter)
                {
                    if (key.Key == ConsoleKey.UpArrow)
                    {
                        position--;
                    }
                    if(key.Key == ConsoleKey.DownArrow)
                    {
                        position++;
                    }
                    if (position > 9)
                    {
                        position = 9;
                        Console.SetCursorPosition(0, position);
                    }
                    if (position < 1)
                    {
                        position = 1;
                        Console.SetCursorPosition(0, position);
                    }
                    Console.Clear();
                    Console.WriteLine("Выберите строку, которую хотите изменить");
                    switch (newpyt)
                    {
                        case ".txt":
                            prime.Clear(); //очищает лист, так как если не очистить то при обращение к методу TXT будет много одних  и тех же значений
                            TXT(pyt);  //Вывод содержимого файла
                            break;
                        case ".json":
                            prime.Clear(); 
                            JSON(pyt);
                            break;
                        case ".xml":
                            prime.Clear();
                            XML(pyt);
                            break;
                    }
                    Console.SetCursorPosition(0, position);
                    key = Console.ReadKey();

                }
                Console.Clear();
                Console.WriteLine("Введите новое значение");
                if ((position == 1) || (position == 4) || (position == 7)) // Меняем имя
                {
                    string newznach = Convert.ToString(Console.ReadLine());
                    switch (position)
                    {
                        case 1:
                            priamoyg.Name = newznach;
                            break;
                        case 4:
                            square.Name = newznach;
                            break;
                        case 7:
                            newpriamoyg.Name = newznach;
                            break;
                    }
                }
                if ((position == 2) || (position == 3) || (position == 5) || (position == 6) || (position == 8)  || (position == 9))  //Меняем длину или ширину
                {
                    int b = Convert.ToInt32(Console.ReadLine());
                    switch (position)
                    {
                        case 2:
                            priamoyg.Lenght = b;
                            break;
                        case 3:
                            priamoyg.Widht = b;
                            break;
                        case 5:
                            square.Lenght = b;
                            break;
                        case 6:
                            square.Widht = b;
                            break;
                        case 8:
                            newpriamoyg.Lenght = b;
                            break;
                        case 9:
                            newpriamoyg.Widht = b;
                            break;
                    }
                }
                prime.Clear();//Очищаем наш лист и записываем в него новые значения
                prime.Add(priamoyg);
                prime.Add(square);
                prime.Add(newpriamoyg);

                true1 = 1;
            }
        }   
    }
}