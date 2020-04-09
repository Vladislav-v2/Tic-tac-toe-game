using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Tic_tac_toe_game
{
    public class Program
    {
        private static Dictionary<char, int> PlayerSymbols = new Dictionary<char, int>();

        public static void Main(string[] args)
        {
            char[,] poligon = new char[3, 3] { { ' ', ' ', ' ' }, { ' ', ' ', ' ' }, { ' ', ' ', ' ' } };

            bool win = false;
            int count = 0; // количество итераций
            char pl_1_symb = ' ', 
                 pl_2_symb = ' ';


            InputSymbols(ref pl_1_symb, ref pl_2_symb);

            PlayerSymbols.Add(pl_1_symb, 1);
            PlayerSymbols.Add(pl_2_symb, 2);

            while (!win)
            {
                win = InputBoxNumber(ref poligon, PlayerSymbols['X'], 'X', ref count);
                if (count == 9)
                {
                    Console.WriteLine(" --A draw-- ");
                    ShowMatrix(poligon);
                    break;
                }
                ShowMatrix(poligon);//вывод таблицы
                if (win) break;

                win = InputBoxNumber(ref poligon, PlayerSymbols['O'], 'O', ref count);
                ShowMatrix(poligon);//вывод таблицы
                if (win) break;
            }
            Console.ReadKey();
        }

        private static void InputSymbols(ref char symb1, ref char symb2)
        {
            Console.WriteLine("Player 1, select your symbol (X or O)");
            string symbol;
            do
            {
                symbol = Console.ReadLine();
                if (symbol != "")
                    switch (symbol[0])
                    {
                        case 'x':
                        case 'X':
                            symb1 = 'X';
                            symb2 = 'O';
                            Console.WriteLine("Player 1, your symbol: X");
                            Console.WriteLine("Player 2, your symbol: O");
                            return;
                        case 'o':
                        case 'O':
                            symb1 = 'O';
                            symb2 = 'X';
                            Console.WriteLine("Player 1, your symbol: O");
                            Console.WriteLine("Player 2, your symbol: X");
                            return;
                        default:
                            Console.WriteLine("\"{0}\" - invalid symbol, enter again", symbol);
                            break;
                    }
                else Console.WriteLine("\"{0}\" - invalid symbol, enter again", symbol);
            }
            while (true);
        }

        private static bool InputBoxNumber(ref char[,] mas, int plyerNumd, char symb, ref int count)
        {
            int BoxNumber, cord_x, cord_y;// количество итераций
            do
            {
                Console.WriteLine("Player {0} enter box number", plyerNumd);

                if (int.TryParse(Console.ReadLine(), out BoxNumber) && (BoxNumber > 0 && BoxNumber < 10))
                {
                    cord_x = (int)Math.Ceiling(BoxNumber / 3.0) - 1; //1
                    cord_y = BoxNumber - cord_x * 3 - 1;//   
                    if (mas[cord_x, cord_y] == ' ')
                    {
                        mas[cord_x, cord_y] = symb;
                        if (count > 2)
                        {
                            //BotPlayer(mas, 1, 'r', ref cord_y);
                            if (CheckVictory(mas, cord_x, cord_y, symb) == plyerNumd)
                            {
                                Console.WriteLine(" --Player {0} wins!-- ", plyerNumd);
                                return true;
                            }
                        }
                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(" --This box is occupied-- ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            } while (true);

            count++;
            return false;
        }

        private static int CheckVictory(char[,] mas, int x, int y, char symb)
        {
            int count = 0; //к-во чимволов подряд
            bool flag;
            flag = true;
            for (int i = 0; i < 3; i++)//проверка совпадения сивола по горизонтали
            {
                if (mas[i, y] == symb) count++;
                else
                {
                    flag = false;
                    break;
                }
            }
            if (flag) //если есть 3 символа подряд
            {
                if (symb == 'O') return PlayerSymbols['O'];
                if (symb == 'X') return PlayerSymbols['X'];
            }
            count = 0;//анулируем счетчик
            flag = true;
            for (int i = 0; i < 3; i++)
            {
                if (mas[x, i] == symb) count++; else break;//проверка совпадения сивола по вертикали
            }
            if (count == 3)//если есть 3 символа подряд
            {
                if (symb == 'O') return PlayerSymbols['O'];
                if (symb == 'X') return PlayerSymbols['X'];
            }

            if ((x == y) || (x + y == 2))
            {
                //проверка, введенный номер ячейки является ли частью одной из диагоналей или нет
                count = 0;
                flag = true;
                for (int i = 0; i < 3; i++)
                {
                    if (mas[i, i] == symb) count++;
                    else
                    {
                        flag = false;
                        break;
                    }//проверка совпадения сивола по главной диагонали
                }
                if (flag)//если есть 3 символа подряд
                {
                    if (symb == 'O') return PlayerSymbols['O'];
                    if (symb == 'X') return PlayerSymbols['X'];
                }
                count = 0;
                flag = true;
                for (int i = 0; i < 3; i++)
                {
                    if (mas[i, 2 - i] == symb) count++;
                    else
                    {
                        flag = false;
                        break;
                    }//проверка совпадения сивола по побочной диагонали
                }
                if (flag)//если есть 3 символа подряд
                {
                    if (symb == 'O') return PlayerSymbols['O'];
                    if (symb == 'X') return PlayerSymbols['X'];
                }
            }
            return -1;
        }

        private static void ShowMatrix(char[,] mas) //Show matrix in console
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write("| ");
                    if (mas[i, j] == 'X') Console.ForegroundColor = ConsoleColor.Red;
                    if (mas[i, j] == 'O') Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(mas[i, j]);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" |");
                }
                Console.WriteLine();
            }
        }

        private static bool BotPlayer(char[,] mas, int plyerNumd, char symb, ref int h)
        {
            List<int> list = new List<int>();

            int count = 0;

            for (int i = 0; i < 3; i++)//проверка совпадения сивола по горизонтали
            {
                for (int j = 0; j < 3; j++)
                {
                    if (mas[i, j] == ' ') list.Add(i + j + 1 + i * 2);
                }
            }
            if (count == 3) //если есть 3 символа подряд
            {
                //if (symb == 'O') return 2;
                //if (symb == 'X') return 1;
            }
            count = 0;//анулируем счетчик
                      //for (int i = 0; i < 3; i++)
                      //{
                      //if (mas[x, i] == symb) count++; else break;//проверка совпадения сивола по вертикали
                      //}
                      //if (count == 3)//если есть 3 символа подряд
                      //{
                      //    if (symb == 'O') return 2;
                      //    if (symb == 'X') return 1;
                      //}
            return true;
        }

    }
}