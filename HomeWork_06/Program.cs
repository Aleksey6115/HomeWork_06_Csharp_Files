using System;
using System.IO;
using System.IO.Compression;

namespace HomeWork_06
{
    class Program
    {
        /// <summary>
     /// 1. Программа считыват из файла (путь к которому можно указать) некоторое N, 
     ///для которого нужно подсчитать количество групп
     ///Программа работает с числами N не превосходящими 1 000 000 000
     /// 
     ///2. В ней есть два режима работы:
     ///2.1. Первый - в консоли показывается только количество групп, т е значение M
     ///2.2. Второй - программа получает заполненные группы и записывает их в файл используя один из
     ///вариантов работы с файлами
     ///         
     ///3. После выполения пунктов 2.1 или 2.2 в консоли отображается время, за которое был выдан результат
     ///в секундах и миллисекундах
     ///
     ///4. После выполнения пунта 2.2 программа предлагает заархивировать данные и если пользователь соглашается -
     ///делает это.
     /// </summary>
     /// <param name="args"></param>
        static void Main(string[] args)
        {
            int number = 0; // Число для вычеслений
            int select = 1; // Переменная для выбора в меню
            bool input;  // Проверка ввода
            string path_file; // Путь к файлу
            char yes_no; // Переменная для выбора
            string compression_path_file; // Путь для архива

            while (select != 4)
            {
                Console.WriteLine("Что Вы хотите сделать?\n" +
                    "1. Узнать кол-во групп\n2. Вычеслить и записать группы в файл\n" +
                    "3. Изменить или создать файл с числом\n4. Закрыть программу");

                // Пользователь делает выбор
                do
                {
                    input = int.TryParse(Console.ReadLine(), out select);
                    if (!input ^ select < 1 ^ select > 4)
                    {
                        Console.Write("Введите ещё раз: ");
                        input = false;
                    }
                }
                while (!input);

                switch (select)
                {
                    case 1: // Узнать кол-во групп
                        #region
                        Console.WriteLine("\nДавайте считаем число из файла");
                        Console.Write("Укажите путь к файлу: ");
                        path_file = Console.ReadLine();

                        do
                        {
                            number = GetNumber(path_file);
                            Console.WriteLine();

                            if (number == 0) // Если число оказалось не таким, каким должно было быть
                            {
                                Console.WriteLine("В указаном файле нет числа");
                                Console.WriteLine("Может изменим число в файле или создадим новый?");

                                do // Пользователь делает выбор
                                {
                                    Console.Write("\nВведите н/д: ");
                                    yes_no = Console.ReadKey(false).KeyChar;
                                    yes_no = Char.ToLower(yes_no);
                                }
                                while (yes_no != 'н' && yes_no != 'д');

                                if (yes_no == 'н') // Возврат в главное меню
                                {
                                    Console.WriteLine("\n");
                                    break;
                                }

                                else if (yes_no == 'д') // Изменяем или создаём файл с числом
                                {
                                    Console.Write("\nУкажите путь к файлу: ");
                                    path_file = Console.ReadLine();

                                    ChangeNumber(path_file);
                                }
                            }

                            else GroupCount(number); // Считаем кол-во групп
                            Console.WriteLine();
                        }
                        while (number == 0);
                        #endregion
                        break;

                    case 2: // Вычеслить и записать группы в файл
                        #region
                        Console.WriteLine("\nДавайте считаем число из файла");
                        Console.Write("Укажите путь к файлу: ");
                        path_file = Console.ReadLine();

                        do
                        {
                            number = GetNumber(path_file);
                            Console.WriteLine();

                            if (number == 0) // Если число оказалось не таким, каким должно было быть
                            {
                                Console.WriteLine("В указаном файле нет числа");
                                Console.WriteLine("Может изменим число в файле или создадим новый?");

                                do // Пользователь делает выбор
                                {
                                    Console.Write("\nВведите н/д: ");
                                    yes_no = Console.ReadKey(false).KeyChar;
                                    yes_no = Char.ToLower(yes_no);
                                }
                                while (yes_no != 'н' && yes_no != 'д');

                                if (yes_no == 'н') // Возврат в главное меню
                                {
                                    Console.WriteLine("\n");
                                    break;
                                }

                                else if (yes_no == 'д') // Изменяем или создаём файл с числом
                                {
                                    Console.Write("\nУкажите путь к файлу: ");
                                    path_file = Console.ReadLine();

                                    ChangeNumber(path_file);
                                }
                            }

                            else // Записываем вычесление в файл
                            {
                                Console.Write("Укажите путь к файлу в который мы будем записывать резултат: ");
                                path_file = Console.ReadLine();

                                RecordingGroups(number, path_file);

                                Console.WriteLine("\nМожет поместим файл в архив?");
                                do // Пользователь делает выбор
                                {
                                    Console.Write("\nВведите н/д: ");
                                    yes_no = Console.ReadKey(false).KeyChar;
                                    yes_no = Char.ToLower(yes_no);
                                }
                                while (yes_no != 'н' && yes_no != 'д');

                                if (yes_no == 'д') // Архивируем файл
                                {
                                    Console.Write("\nУкажите путь для архивации: ");
                                    compression_path_file = Console.ReadLine();
                                    CompressionFile(path_file, compression_path_file);
                                }

                                else if (yes_no == 'н') // Не архивиуем файл
                                {
                                    Console.WriteLine("\n");
                                    break;
                                }
                            }
                            Console.WriteLine();
                        }
                        while (number == 0);
                        #endregion
                        break;

                    case 3: // Изменить или создать файл с числом
                        #region
                        Console.Write("\nУкажите путь к файлу: ");
                        path_file = Console.ReadLine();

                        ChangeNumber(path_file);
                        Console.WriteLine();
                        #endregion
                        break;
                }
            }
        }

        /// <summary>
        /// Метод считает колличество групп для числа
        /// </summary>
        /// <param name="number"></param>
        static void GroupCount(int number)
        {
            DateTime start_time = DateTime.Now; // Время начало операции

            int group_count = 1; // Колличество групп
            int power_result = 1; // Результат возведения в степень

            // Считаем колличество групп
            while (power_result < number)
            {
                group_count++;
                power_result = (int) Math.Pow(2, group_count);
            }

            Console.WriteLine($"Колличество груп для числа {number} будет равным {group_count}");

            DateTime finish_time = DateTime.Now; // Время завершения операци
            TimeSpan result_time = start_time.Subtract(finish_time); // Считаем сколько времени было потрачено

            Console.Write("На выполнение расчёта было потрачено времени - ");
            Console.WriteLine($"{result_time.ToString(@"mm")} min : {result_time.ToString(@"ss")} sec : " +
                $"{result_time.ToString(@"fff")} ms");
            Console.WriteLine("Нажмите любую клавишу...");
            Console.ReadKey();
        }


        /// <summary>
        /// Метод разбивает число на группы и записывает результат файл
        /// </summary>
        /// <param name="number">Число, которое нужно разбить на группы</param>
        /// <param name="path_name">Путь к файлу</param>
        static void RecordingGroups(int number, string path_file)
        {
            DateTime start_time = DateTime.Now; // Время начало операции
            Console.WriteLine("Подождите пока процесс не будет завершён...");

            // Открываем поток и работаем с ним
            using (StreamWriter sw = new StreamWriter(path_file, false, System.Text.Encoding.Unicode)) 
            {
                // Записываем данные сразу в файл
                sw.Write("Группа 1: ");
                for (int i = 1, j = 1; i < number + 1; i++)
                {
                    sw.Write($"{i} ");
                    if ((i + 1) == Math.Pow(2, j))
                    {
                        j++;
                        sw.Write($"\nГруппа {j}: "); // Запись в файл
                        Console.Write("|"); // Имитация полосы загрузки
                    }
                }
            }
            Console.WriteLine(" - Готово!!!" +
                "\nЗапись прошла успешно");

            DateTime finish_time = DateTime.Now; // Время завершения операци
            TimeSpan result_time = start_time.Subtract(finish_time); // Считаем сколько времени было потрачено

            Console.Write("Потрачено времени - ");
            Console.WriteLine($"{result_time.ToString(@"mm")} min : {result_time.ToString(@"ss")} sec : " +
                $"{result_time.ToString(@"fff")} ms");
        }


        /// <summary>
        /// Метод считывает число из файла, если число не удовлетворяет требованиям
        /// то метод возвращает 0
        /// </summary>
        /// <param name="path_file"></param>
        /// <returns></returns>
        static int GetNumber(string path_file)
        {
            string reader_str; // Хранит прочитанное из файла
            int number; // Результат чтения
            bool input_number; 

            // Читаем данные из файла
            using (StreamReader sr = new StreamReader(path_file))
            {
                reader_str = sr.ReadToEnd();
            }

            input_number = int.TryParse(reader_str, out number);
            if (input_number && number > 0 && number <= 1000000000) return number;
            else return 0; 
        }


        /// <summary>
        /// Метод либо создаёт новый файл с числом,
        /// либо изменяет существующий
        /// </summary>
        /// <param name="path_file"></param>
        static void ChangeNumber (string path_file)
        {
            bool input_number;
            int number;

            Console.Write("Введите число, которое хотите записать: ");
            // Читаем число с клавиатуры
            do
            {
                input_number = int.TryParse(Console.ReadLine(), out number);
                if (number < 1 ^ number >= 1000000001 ^ !input_number)
                {
                    input_number = false;
                    Console.Write("Введите ещё раз: ");
                }
            }
            while (!input_number);

            // Записываем число в файл
            using(StreamWriter sw = new StreamWriter(path_file, false, System.Text.Encoding.Unicode))
            {
                sw.WriteLine(number);
            }
            Console.WriteLine("Запись прошла успешно!");
        }


        /// <summary>
        /// Метод архивирует файл
        /// </summary>
        /// <param name="original_path_file"></param>
        /// <param name="compression_path_file"></param>
        static void CompressionFile(string original_path_file,string compression_path_file)
        {
            Console.WriteLine($"Архивация {original_path_file}...");
            // Создаём поток с файлом, который будет архивироваться
            using (FileStream original_stream = new FileStream(original_path_file, FileMode.OpenOrCreate))
            {
                // Создаём поток с архивом
                using (FileStream compression_stream = new FileStream(compression_path_file, FileMode.OpenOrCreate))
                {
                    // Поток для архивации
                    using (GZipStream gz = new GZipStream(compression_stream, CompressionMode.Compress))
                    {
                        original_stream.CopyTo(gz); // Архивируем файл
                        Console.WriteLine("Архивация прошла успешно");
                        Console.WriteLine($"Размер файла до сжатия - {original_stream.Length} / Размер файла после сжатия - {compression_stream.Length}");
                    }
                }
                Console.WriteLine("Нажмите любую клавишу...");
                Console.ReadKey();
            }
        }
    }
}
