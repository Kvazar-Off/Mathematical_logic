using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mat_log
{
    public class Example
    {
        /********************************************
        
        "j" - характеристическая функция первого рода числа 1

        "-" - усеченная разность

        *********************************************/

        public Example leftEx { get; set; } // левая часть примера
        public Example rightEx { get; set; } // правая часть примера
        public string Left { get; set; }
        public string Right { get; set; }
        public char Operation { get; set; } // операция между правой и левой частью
        public string data { get; set; } // строка для хранения примера
        public int result { get; set; } // результат
        public int X { get; set; } // значение X
        public int Y { get; set; } // значение Y
        public int K { get; set; } // значение K (K-значная логика)
        public bool HarFunc { get; set; } // проверка на наличие характеристической функции первого рода числа 1
        public int Count { get; set; } // кол-во существенных переменных

        public List<int> listOfIndexesOfOpeningBrackets { get; set; } // массив открывающихся скобок
        public List<int> listOfIndexesOfClosingBrackets { get; set; } // массив закрывающихся скобок

        public Example(string s, int x, int y, int k, int c)
        {
            data = s;
            leftEx = null;
            rightEx = null;
            X = x;
            Y = y;
            K = k;
            Count = c;
            HarFunc = false;

            listOfIndexesOfOpeningBrackets = new List<int>();
            listOfIndexesOfClosingBrackets = new List<int>();

            // это нужно для вычисления моментальных значений, если, например, ввели просто x или y или 3x или 13y и т.д.
            result = -99999;
            if (data[0] != 'j') // если нет характеристической функции первого рода числа 1
            {
                if (data.Length == 1)
                {
                    if (data == "x")
                    {
                        result = X;
                    }
                    else if (data == "y")
                    {
                        result = Y;
                    }
                }
                else if (data.Length == 2)
                {
                    if (data[1] == 'x')
                    {
                        result = (Convert.ToInt32(Convert.ToString(data[0])) * X) % K;
                    }
                    else if (data[1] == 'y')
                    {
                        result = (Convert.ToInt32(Convert.ToString(data[0])) * Y) % K;
                    }
                }
                else if (data.Length == 3)
                {
                    if (data[1] != '-')
                    {
                        if (data[2] == 'x')
                        {
                            result = (Convert.ToInt32(Convert.ToString(data[0]) + Convert.ToString(data[1])) * X) % K;
                        }
                        else if (data[2] == 'y')
                        {
                            result = (Convert.ToInt32(Convert.ToString(data[0]) + Convert.ToString(data[1])) * Y) % K;
                        }
                    }
                }
            }
            else if (data[0] == 'j') // если есть характеристическая функция первого рода числа 1
            {
                if (data.Length == 2)
                {
                    if (data[1] == 'x')
                    {
                        result = Har(X);
                    }
                    else if (data[1] == 'y')
                    {
                        result = Har(Y);
                    }
                }
                else if (data.Length == 3)
                {
                    if (data[2] == 'x')
                    {
                        result = Har((Convert.ToInt32(Convert.ToString(data[1])) * X) % K);
                    }
                    else if (data[2] == 'y')
                    {
                        result = Har((Convert.ToInt32(Convert.ToString(data[1])) * Y) % K);
                    }
                }
                else if (data.Length == 4)
                {
                    if (data[2] != '-')
                    {
                        if (data[3] == 'x')
                        {
                            result = Har((Convert.ToInt32(Convert.ToString(data[1]) + Convert.ToString(data[2])) * X) % K);
                        }
                        else if (data[3] == 'y')
                        {
                            result = Har((Convert.ToInt32(Convert.ToString(data[1]) + Convert.ToString(data[2])) * Y) % K);
                        }
                    }
                }
            }

            if (Count == 2) // если 2 переменных
            {
                GetIndexes();
            }
        }

        public int UsechRaz(int x, int y) // усеченная разность
        {
            if (x >= y)
            {
                return x - y; 
            }
            else
            {
                return 0;
            }
        }

        public int Har(int x) // характеристическая функция первого рода числа 1
        {
            if (x == 1)
            {
                return 1;  
            }
            else
            {
                return 0;
            } 
        }

        public int Show() // вызывает метод вычисления значения функции + возвращает её результат
        {
            Solve();

            return result % K;
        }

        public int ShowMomentResult() // просто возвращает результат (используется в основном для функции с 1 переменной)
        {

            return result % K;
        }

        public void GetIndexes() // высчитывает индексы открытых и закрытых скобок и удаляет внешние ( ненужные ) скобки, если надо
        {
            if (data[0] == 'j')
            {
                var str = "";

                for (var p = 1; p < data.Length; p++)
                {
                    str += data[p];
                }

                HarFunc = true;
                data = str;
            }

            for (var i = 0; i < data.Length; i++)
            {
                if (data[i] == '(')
                {
                    listOfIndexesOfOpeningBrackets.Add(i);
                }
                else if (data[i] == ')')
                {
                    listOfIndexesOfClosingBrackets.Add(i);
                }
            }

            RemoveOuterBrackets(); // вызывает функцию, которая удаляет внешние ( ненужные ) скобки

            var check = SplitIntoTwoExpressions(); // делит пример на 2 части (на левый от операции и на правый от операции)

            if (check == true) // рекурсия дальнейшего разбиения примера
            {
                leftEx = new Example(Left, X, Y, K, Count); // левая часть примера
                rightEx = new Example(Right, X, Y, K, Count); // правая

                // в этой рекурсии делается всё тоже самое вначале делится левая часть примера, потом правая
            }
        }

        public void RemoveOuterBrackets() // удаление 1 пары внешних ( ненужных ) скобок
        {
            var OpeningBracketsCount = listOfIndexesOfOpeningBrackets.Count;
            var ClosingBracketsCount = listOfIndexesOfClosingBrackets.Count;

            var IndexOfOpeningBracket = -1;
            var IndexOfClosingBracket = -1;

            for (var i = OpeningBracketsCount - 1; i >= 0; i--)
            {
                for (var j = 0; j < ClosingBracketsCount; j++)
                {
                    if (listOfIndexesOfOpeningBrackets[i] < listOfIndexesOfClosingBrackets[j])
                    {
                        IndexOfOpeningBracket = listOfIndexesOfOpeningBrackets[i];
                        IndexOfClosingBracket = listOfIndexesOfClosingBrackets[j];

                        listOfIndexesOfOpeningBrackets.RemoveAt(i);
                        listOfIndexesOfClosingBrackets.RemoveAt(j);

                        OpeningBracketsCount--;
                        ClosingBracketsCount--;

                        break;
                    }
                }
            }

            if (IndexOfOpeningBracket == 0 && IndexOfClosingBracket == (data.Length - 1))
            {
                var str = "";

                for (var k = 1; k < data.Length - 1; k++)
                {
                    str += data[k];
                }

                data = str;

                GetIndexes(); // снова вызов функции, мало ли еще остались внешние ( ненужные ) скобки
            }
        }

        public bool SplitIntoTwoExpressions() // функции деления примера на 2 части
        {

            /**********************************************************
             
                Делит пример так:
                       1. Находит операцию, которая лежит вне каких-либо скобок, то есть, которая связывает левую и правую часть примера
                       2. Берет левую часть примера
                       3. Берет правую часть примера
                       4. Сохраняет операцию между ними для вычисления результата
             
            **********************************************************/

            var OpeningBracketsCount = 0;
            var ClosingBracketsCount = 0;

            for (var k = 0; k < data.Length; k++)
            {
                if (data[k] == '(')
                {
                    OpeningBracketsCount++;
                }
                else if (data[k] == ')')
                {
                    ClosingBracketsCount++;
                }

                if (data[k] == '-')
                {
                    if (OpeningBracketsCount == ClosingBracketsCount)
                    {
                        var str = "";

                        for (var v = 0; v < k; v++)
                        {
                            str += data[v];
                        }

                        var exL = str;
                        var op = data[k];

                        str = "";

                        for (var v = k + 1; v < data.Length; v++)
                        {
                            str += data[v];
                        }

                        Left = exL;
                        Right = str;
                        Operation = op;

                        return true;
                    }
                }
            }

            return false;
        }

        public void Solve() // функция вычисляет рекурсивно значение функции
        {
            var exL = leftEx;
            var exR = rightEx;

            if (exL.leftEx != null) // если можем идти влево, то идём влева до конца
            {
                exL.Solve();
            }

            if (exR.rightEx != null) // потом вправо
            {
                exR.Solve();
            }

            if (exL.result == -99999 || exR.result == -99999) // если result = -99999, то значение еще не вычислено, поэтому вычисляем со всеми возможными условиями
            {
                if (exL.result == -99999 && exR.result != -99999)
                {
                    int resL = -99999;

                    if (exL.data == "x")
                    {
                        resL = X;
                    }
                    else if (exL.data == "y")
                    {
                        resL = Y;
                    }

                    if (Operation == '-')
                    {
                        result = UsechRaz(resL, exR.result);

                        if (HarFunc == true)
                        {
                            result = Har(result);
                        }
                    }
                }
                else if (exR.result == -99999 && exL.result != -99999)
                {
                    int resR = -99999;

                    if (exL.data == "x")
                    {
                        resR = X;
                    }
                    else if (exL.data == "y")
                    {
                        resR = Y;
                    }

                    if (Operation == '-')
                    {
                        result = UsechRaz(exL.result, resR);

                        if (HarFunc == true)
                        {
                            result = Har(result);
                        }
                    }
                }
                else if (exL.result == -99999 && exR.result == -99999)
                {
                    int resL = -99999;
                    int resR = -99999;

                    if (exL.data == "x")
                    {
                        resL = X;
                    }
                    else if (exL.data == "y")
                    {
                        resL = Y;
                    }

                    if (exR.data == "x")
                    {
                        resR = X;
                    }
                    else if (exR.data == "y")
                    {
                        resR = Y;
                    }

                    if (Operation == '-')
                    {
                        result = UsechRaz(resL, resR);

                        if (HarFunc == true)
                        {
                            result = Har(result);
                        }
                    }
                }
            }
            else
            {
                if (Operation == '-')
                {
                    result = UsechRaz(exL.result, exR.result);

                    if (HarFunc == true)
                    {
                        result = Har(result);
                    }
                }
            }
        }
    }
}
