using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mat_log
{
    public class Expression
    {
        public string inputString { get; set; } // вводимый пример
        public int K { get; set; } // k-значная логика
        public int Count { get; set; } // кол-во существенных переменных

        public List<int> listOfIndexesOfOpeningBrackets { get; set; }

        public List<int> listOfIndexesOfClosingBrackets { get; set; }

        public List<int> function { get; set; } // список для хранения всех результатов функции

        public List<string> table { get; set; } // список для формирования таблицы вывода

        public Example ex { get; set; } // класс примера

        public string check = "";

        public Expression()
        {

            Console.WriteLine("\nСтудент: Трифонов С.А");
            Console.WriteLine("Группа 4218, номер в списке 10");
            Console.WriteLine("Функция одного аргумента - характеристическая функция первого рода числа 1 : jx");
            Console.WriteLine("Функция двух аргументов - усеченная разность : x - y");
            Console.WriteLine("Первая форма (аналог СДНФ) : 3&J(0)(x)\n");
            Console.Write("Введите функцию: ");

            bool run = false;

            while (run == false) // проверка примера, который я ввел
            {
                inputString = Console.ReadLine();

                for (var i = 0; i < inputString.Length; i++)
                {
                    if (inputString[i] != '(' && inputString[i] != ')' && inputString[i] != ' ' && inputString[i] != 'x'
                                              && inputString[i] != 'y' && inputString[i] != 'j' && inputString[i] != '-'
                                              && Char.IsNumber(inputString[i]) == false)
                    {
                        Console.WriteLine("Неправильно введеная функция..");

                        Console.Write("Введите функцию: ");

                        break;
                    }
                    else if (checkBrackets(inputString) == false)
                    {
                        Console.WriteLine("Неправильно введеная функция..");

                        Console.Write("Введите функцию: ");

                        break;
                    }
                    else if (i == inputString.Length - 1)
                    {
                        run = true;
                    }
                }
            }

            Console.Write($"\nВведите логику, в которой хотите вычислить функцию : ");
            K = Convert.ToInt32(Console.ReadLine());

            Console.Write($"\nВведите количество используемых переменных : ");
            Count = Convert.ToInt32(Console.ReadLine());

            listOfIndexesOfOpeningBrackets = new List<int>();
            listOfIndexesOfClosingBrackets = new List<int>();

            function = new List<int>();
            table = new List<string>();

            table.Add(" _______________________________________________");
        }

        public void RemoveSpace() // удаление всех пробелов из примера, чтобы легче работать со строкой
        {
            var str = "";

            for (var i = 0; i < inputString.Length; i++)
            {
                if (inputString[i] != ' ')
                {
                    str += inputString[i];
                }
            }

            inputString = str;

            Console.WriteLine($"\nЗапись функции после удаления пробелов: {inputString}\n");

            if (Count == 1) // заполнение таблицы для вывода для 1 переменной
            {
                table[0] = " _______________________";

                var note = "";

                for (var k = 0; k < inputString.Length; k++)
                {
                    if (inputString[k] == 'x')
                    {
                        check = "x";
                    }
                    else if (inputString[k] == 'y')
                    {
                        check = "y";
                    }
                }

                if (check == "x")
                {
                    note = "|\tx\tf(x)\t|";
                }
                else
                {
                    note = "|\ty\tf(y)\t|";
                }

                table.Add(note);

                for (var j = 0; j < K; j++)
                {
                    ex = new Example(inputString, j, j, K, Count);

                    var res = ex.ShowMomentResult();

                    function.Add(res);

                    note = $"|\t{j}\t {res}\t|";

                    table.Add(note);
                }

                table.Add("|_______________________|");
            }
            else // заполнение таблицы для вывода для 2 переменных
            {
                var note = "";

                note = "|\tx\t\ty\t\tf(x, y)\t|";

                table.Add(note);

                for (var i = 0; i < K; i++)
                {
                    for (var j = 0; j < K; j++)
                    {
                        ex = new Example(inputString, i, j, K, Count);

                        var res = ex.Show();

                        function.Add(res);

                        note = $"|\t{i}\t\t{j}\t\t{res}\t|";

                        table.Add(note);
                    }
                }

                table.Add("|_______________________________________________|");
            }

            foreach (var el in table)
            {
                Console.WriteLine(el);
            }

            Console.Write("Первая форма : "); // первая форма(аналог СДНФ)
            if (Count == 1)
            {
                var i = 0;

                var ss = "";

                foreach (var el in function)
                {
                    if (el != 0)
                    {
                        ss += $"{el}&J({i})({check}) v "; 
                    }

                    i++;
                }

                for (var w = 0; w < ss.Length - 2; w++)
                {
                    Console.Write(ss[w]);
                }
            }
            else
            {
                var i = 0;
                var j = 0;

                var ss = "";

                foreach (var el in function)
                {
                    if (el != 0)
                    {
                        if (el > 1)
                        {
                            ss += $"{el}&J({i})(x)&J({j})(y) v ";
                        }
                        else
                        {
                            ss += $"J({i})(x)&J({j})(y) v ";
                        }
                    }

                    j++;

                    if (j == K)
                    {
                        j = 0;
                        i++;
                    }
                }

                for (var w = 0; w < ss.Length - 2; w++)
                {
                    Console.Write(ss[w]);
                }
            }

            Proverka();
        }

        public void Proverka() // функция проверки принадлежности классу T(E) (сохраняет или не сохраняет мн-во E)
        {
            Console.Write($"\n\nВведите кол-во элементов множества E от 1 до {K} : ");
            var x = Convert.ToInt32(Console.ReadLine());

            while (x <= 0 || x > K)
            {
                Console.Write("\nНеверное значение, введите снова : ");
                x = Convert.ToInt32(Console.ReadLine());
            }

            var E = new List<int>();

            for (var i = 0; i < x; i++)
            {
                Console.Write($"\n{i}-й элемент множества = ");
                var el = Convert.ToInt32(Console.ReadLine());

                while (el < 0 || el >= K || ch(E, el) == false)
                {
                    Console.Write("\nНеверное значение элемента множества.. Введите снова : ");
                    el = Convert.ToInt32(Console.ReadLine());
                }

                E.Add(el);
            }

            Console.Write("\nМножество E = { ");
            foreach (var el in E)
            {
                Console.Write($"{el} ");
            }
            Console.WriteLine("}");

            if (Count == 2)
            {
                var CountFunction = 0;

                for (var ii = 0; ii < K; ii++)
                {
                    for (var jj = 0; jj < K; jj++)
                    {
                        if (ch(E, ii) == false && ch(E, jj) == false && ch(E, function[CountFunction]) == true)
                        {
                            Console.Write("\nФункция не принадлежит классу T(E)");
                            Console.Write($"\nПример набора, из-за которого функция не принадлежит классу T(E) : x = {ii}, y == {jj}, f(x, y) = {function[CountFunction]}");

                            return;
                        }

                        CountFunction++;
                    }
                }

                Console.WriteLine("\nФункция принадлежит классу T(E)");
            }
            else
            {
                var CountFunction = 0;

                for (var ii = 0; ii < K; ii++)
                {
                    if (ch(E, ii) == false && ch(E, function[CountFunction]) == true)
                    {
                        Console.Write("\nФункция не принадлежит классу T(E)");
                        Console.Write($"\nПример набора, из-за которого функция не принадлежит классу T(E) : переменная = {ii}, f(x, y) = {function[CountFunction]}");

                        return;
                    }

                    CountFunction++;
                }

                Console.WriteLine("\nФункция принадлежит классу T(E)");
            }
        }

        public bool ch(List<int> l, int el) // проверка принадлежности значения массиву (например массиву со значениеми функции)
        {
            foreach (var element in l)
            {
                if (element == el)
                {
                    return false;
                }
            }

            return true;
        }

        public bool checkBrackets(string s) // проверка : кол-во открытых и закрытых скобок должно быть равно друг другу
        {
            var CountCB = 0;
            var CountOB = 0;

            for (var i = 0; i < s.Length; i++)
            {
                if (s[i] == '(')
                {
                    CountOB++;
                }
                else if (s[i] == ')')
                {
                    CountCB++;
                }
            }

            if (CountCB != CountOB)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
