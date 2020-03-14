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
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            char[,] poligon = new char[3, 3] { { ' ', ' ', ' ' }, { ' ', ' ', ' ' }, { ' ', ' ', ' ' } };

            bool win = false;
            int count = 0; // количество итераций
            while (!win)
            {
                win = inputBoxNumber(ref poligon, 1, 'X', ref count);
                if (count == 9)
                {
                    Console.WriteLine(" --A draw-- ");
                    ShowMatrix(poligon);
                    break;
                }
                ShowMatrix(poligon);//вывод таблицы
                if (win) break;

                win = inputBoxNumber(ref poligon, 2, 'O', ref count);
                ShowMatrix(poligon);//вывод таблицы
                if (win) break;
            }
            Console.ReadKey();
        }

        private static bool inputBoxNumber(ref char[,] mas, int plyerNumd, char symb, ref int count)
        {
            int boxNumber, cord_x, cord_y;// количество итераций
            do
            {
                Console.WriteLine("Player {0} enter box number", plyerNumd);

                if (int.TryParse(Console.ReadLine(), out boxNumber) && (boxNumber > 0 && boxNumber < 10))
                {
                    cord_x = (int)Math.Ceiling(boxNumber / 3.0) - 1; //1
                    cord_y = boxNumber - cord_x * 3 - 1;//   
                    if (mas[cord_x, cord_y] == ' ')
                    {
                        mas[cord_x, cord_y] = symb;
                        if (count > 3)
                        {
                            botPlayer(mas, 1, 'r', ref cord_y);
                            if (checkVictory(mas, cord_x, cord_y, symb) == plyerNumd)
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

        private static int checkVictory(char[,] mas, int x, int y, char symb)
        {
            int count = 0; //к-во чимволов подряд

            for (int i = 0; i < 3; i++)//проверка совпадения сивола по горизонтали
            {
                if (mas[i, y] == symb) count++; else break;
            }
            if (count == 3) //если есть 3 символа подряд
            {
                if (symb == 'O') return 2;
                if (symb == 'X') return 1;
            }
            count = 0;//анулируем счетчик
            for (int i = 0; i < 3; i++)
            {
                if (mas[x, i] == symb) count++; else break;//проверка совпадения сивола по вертикали
            }
            if (count == 3)//если есть 3 символа подряд
            {
                if (symb == 'O') return 2;
                if (symb == 'X') return 1;
            }
            count = 0;

            if ((x == y) || (x + y == 2))
            {
                //проверка, введенный номер ячейки является ли частью одной из диагоналей или нет
                for (int i = 0; i < 3; i++)
                {
                    if (mas[i, i] == symb) count++; else break;//проверка совпадения сивола по главной диагонали
                }
                if (count == 3)//если есть 3 символа подряд
                {
                    if (symb == 'O') return 2;
                    if (symb == 'X') return 1;
                }
                count = 0;
                for (int i = 0; i < 3; i++)
                {
                    if (mas[i, 2 - i] == symb) count++; else break;//проверка совпадения сивола по побочной диагонали
                }
                if (count == 3)//если есть 3 символа подряд
                {
                    if (symb == 'O') return 2;
                    if (symb == 'X') return 1;
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

        private static bool botPlayer(char[,] mas, int plyerNumd, char symb, ref int h)
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