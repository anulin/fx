using ConsoleApp3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        public static long aaa = 0;
        struct dbool
        {
            public bool n;//количество переменных
            public bool p;
        }
        struct dList
        {
            public List<int> pos;
            public List<int> lvl;
        }
        static string nomer(int n)//возвращает номер
        {
            string s = "";
            n += 1;
            for (; n / 2 > 0;)
            {
                if (n % 2 == 1)
                {
                    s = 'l' + s;
                }
                else { s = 'o' + s; }
                n /= 2;
            }
            return (s);
        }
        static string sk(int n)//возвращает skobki
        {
            string s = "";
            for (; n > 0;)
            {
                s = ')' + s;
                n--;
            }
            return (s);
        }
        static int siz(int n)//длина названия функции включая f
        {
            int x = 1;
            for (n++; n / 2 > 0; n /= 2)
            {
                x++;
            }
            return (x);
        }

        static int per(prog a, int m, List<int> skobk, int n, List<dbool> mxnu, int nu, int ra, int fu)//строит все возможные синтаксически верные строки; mxnu.n =tru если 2 переменные и fals если 1, длина это количество функций
        {//m текущее количество запрашиваемых аргументов. xx nn xf) xn nf) f)f) сокращают ее на 1, n максимальная длина; skobk - последний элемент =допустимое кол-во скобок, nu - количество неиспользованных ф-й(NU+1) fu - функция, которая сейчас//!!ДОДЕЛАТЬ изменение m, РАЗОБРАТЬСЯ с поведением nu и ra!!
         //ra(1 -нет знака и 0 - есть) - наличие знака "="
         //if (nu == 0) { nu = -1; }
            List<int> kk = new List<int>();
            int fa = 0;
            if (fu != -1 && !mxnu[fu].p)
            {
                if (mxnu[fu].n) { fa = siz(fu) + 1; }
                else { fa = siz(fu); }
            }
            //if (a.Length > 1 && a[a.Length - 2] == '+' && a.Last() == '1' && a[a.Length-3]=='1') { Convert.ToInt32(Console.ReadLine()); }
            if (a.Last() == 'f')//m>0 
            {
                if (m > 1 || nu - fa == 0 || n - a.Length - nu + fa - 2 > 0)//2 - +NUx или =NUx
                {
                    per(a + 'n', m - 1, skobk, n, mxnu, nu, ra, fu);
                    per(a + 'x', m - 1, skobk, n, mxnu, nu, ra, fu);
                }//СДЕЛАНО
                kk = new List<int>(skobk);
                kk[kk.Count() - 1] += 1;
                for (int i = 0; i < mxnu.Count(); i++)// DONE
                {
                    if (mxnu[i].n)//если 2 переменных и они запрашиваются//DONE
                    {
                        if (!mxnu[i].p && fu != i)//если данная функция(2) не использовалась...DONE
                        {
                            mxnu[i] = new dbool { p = true, n = true };
                            per(a + nomer(i) + "f", m + 1, kk.Concat(new List<int> { 0 }).ToList(), n, mxnu, nu - 1 - siz(i), ra, fu);
                            mxnu[i] = new dbool { n = true }; //bool по умолчанию false
                        }
                        else if (siz(i) + 1 <= n - a.Length - m - nu + fa - ra * 2)// (siz(i)+1 тк f-я двух переменных)DONE
                        {
                            per(a + nomer(i) + 'f', m + 1, kk.Concat(new List<int> { 0 }).ToList(), n, mxnu, nu, ra, fu);
                        }
                    }
                    else//DONE
                    {
                        if (!mxnu[i].p && fu != i)//если данная функция(1) не использовалась...DONE
                        {
                            mxnu[i] = new dbool { p = true };
                            per(a + nomer(i) + 'f', m, kk, n, mxnu, nu - siz(i), ra, fu);
                            mxnu[i] = new dbool();
                        }
                        else if (siz(i) <= n - a.Length - m - nu + fa - ra * 2)//DONE
                        {
                            per(a + nomer(i) + 'f', m, kk, n, mxnu, nu, ra, fu);
                        }
                    }// Возможно будет плохо если функция вызовет сама себя
                }//DONE
                for (int i = 1; ; i++)// 
                {
                    if ((m > 1 || nu - fa == 0) && Convert.ToString(i, 2).Length <= n - a.Length - m + 1 - nu + fa - ra * 2 || Convert.ToString(i, 2).Length <= n - a.Length - m + 1 - nu + fa - 2)//DONE
                    {
                        per(a + Convert.ToString(i, 2), m - 1, skobk, n, mxnu, nu, ra, fu);
                    }
                    else { break; }
                }//DONE
            }//DONE
            if (a.Last() == 'x')
            {
                kk = new List<int>(skobk);
                if (m > 1)
                {
                    kk[kk.Count() - 2] += 1;
                }
                for (int i = 0; i < mxnu.Count(); i++)// DONE
                {
                    if (mxnu[i].n && m > 0)//если 2 переменных и они запрашиваются
                    {
                        if (!mxnu[i].p && fu != i)//если данная функция(2) не использовалась...
                        {
                            mxnu[i] = new dbool { p = true, n = true };
                            per(a + nomer(i) + 'f', m + 1, kk.GetRange(0, skobk.Count() - 1).Concat(new List<int> { 0 }).ToList(), n, mxnu, nu - 1 - siz(i), ra, fu);
                            mxnu[i] = new dbool { n = true }; //bool по умолчанию false
                        }//DONE
                        else if (siz(i) + 1 <= n - a.Length - m - nu + fa - ra * 2)//DONE
                        {
                            per(a + nomer(i) + 'f', m + 1, kk.GetRange(0, skobk.Count() - 1).Concat(new List<int> { 0 }).ToList(), n, mxnu, nu, ra, fu);
                        }
                    }//DONE
                    else if (m > 0)//DONE
                    {
                        if (!mxnu[i].p && fu != i)//если данная функция(1) не использовалась...
                        {
                            mxnu[i] = new dbool { p = true };
                            per(a + nomer(i) + 'f', m, kk.GetRange(0, skobk.Count() - 1), n, mxnu, nu - siz(i), ra, fu);
                            mxnu[i] = new dbool();
                        }
                        else if (siz(i) <= n - a.Length - m - nu + fa - ra * 2)
                        {
                            per(a + nomer(i) + 'f', m, kk.GetRange(0, skobk.Count() - 1), n, mxnu, nu, ra, fu);
                        }
                    }//DONE Возможно будет плохо если функция вызовет сама себя
                    else if (ra == 0) //аргумет не запрашиваются=> новое выражение
                    {
                        if (siz(i) <= n - a.Length - 3 - nu || !mxnu[i].p && 0 <= n - a.Length - 3 - nu)// 3- минимальное число знаков в новом выражении: ...f x= "nu"х
                        {
                            if (mxnu[i].n)//DONE
                            {
                                if (siz(i) <= n - a.Length - 4 - nu || !mxnu[i].p) { per(a * nomer(i) + 'f', 2, new List<int> { 1, 0 }, n, mxnu, nu, 1, i); }    //4-...                            
                            }
                            else { per(a * nomer(i) + 'f', 1, new List<int> { 1 }, n, mxnu, nu, 1, i); }
                        }//DONE
                    }//DONE
                }//DONE (проверка знака = , числа)DONE
                if (m == 0 && ra == 0)//-объявление новой функции если не ожидается аргументов
                {
                    if (siz(mxnu.Count()) <= n - a.Length - 3 - nu)
                    {
                        per(a * nomer(mxnu.Count()) + 'f', 1, new List<int> { 1 }, n, mxnu.Concat(new List<dbool> { new dbool() }).ToList(), nu + siz(mxnu.Count()), 1, mxnu.Count());
                    }
                    if (siz(mxnu.Count()) <= n - 4 - a.Length - nu)//3-...(xn=)
                    {
                        per(a * nomer(mxnu.Count()) + 'f', 2, new List<int> { 1, 0 }, n, mxnu.Concat(new List<dbool> { new dbool { n = true } }).ToList(), nu + siz(mxnu.Count()) + 1, 1, mxnu.Count());
                    }
                }//DONE
                for (int i = 1; ; i++)// 
                {
                    if (m > 0 && ((m > 1 || nu - fa == 0) && Convert.ToString(i, 2).Length <= n - a.Length - m + 1 - nu + fa - ra * 2 || Convert.ToString(i, 2).Length <= n - a.Length - m + 1 - nu + fa - 2))//DONE
                    {
                        per(a + Convert.ToString(i, 2), m - 1, skobk.GetRange(0, skobk.Count() - 1), n, mxnu, nu, ra, fu);
                    }
                    else { break; }
                }//DONE
                kk = new List<int>(skobk);
                for (int i = 0; i <= skobk.Last() && i <= n - a.Length - 2 - m - nu + fa - ra * 2; i++)//2 -...))) +"nu"x... + и х  - два символа
                {
                    per(a + sk(i) + "+", m + 1, kk, n, mxnu, nu, ra, fu);
                    kk[kk.Count() - 1]--;
                }//DONE
                if (m == 0 && n - a.Length - nu + fa - 2 >= 0)//=*-2 символа
                {
                    per(a + '=', 1, new List<int> { 0 }, n, mxnu, nu, 0, fu);
                }//DONE
                if (m > 0)
                {
                    if (m > 1 || nu - fa == 0 || n - a.Length - nu + fa - 2 > 0)
                    {
                        per(a + 'n', m - 1, skobk.GetRange(0, skobk.Count() - 1), n, mxnu, nu, ra, fu);
                        per(a + 'x', m - 1, skobk.GetRange(0, skobk.Count() - 1), n, mxnu, nu, ra, fu);
                    }
                }//верно
                else if (n - a.Length - nu + fa - 2 > 0 && ra == 0)//2 - знак "=*" в новом выражении
                {
                    per(a * "n", m, new List<int> { 0 }, n, mxnu, nu, 1, -1);
                }//DONE
            }//DONE
            if (a.Last() == 'n')
            {
                kk = new List<int>(skobk);
                if (m > 1)
                {
                    kk[kk.Count() - 2] += 1;
                }
                for (int i = 0; i < mxnu.Count(); i++)// DONE
                {
                    if (mxnu[i].n && m > 0)//если 2 переменных и они запрашиваются
                    {
                        if (!mxnu[i].p && fu != i)//если данная функция(2) не использовалась...
                        {
                            mxnu[i] = new dbool { p = true, n = true };
                            per(a + nomer(i) + 'f', m + 1, kk.GetRange(0, skobk.Count() - 1).Concat(new List<int> { 0 }).ToList(), n, mxnu, nu - 1 - siz(i), ra, fu);
                            mxnu[i] = new dbool { n = true }; //bool по умолчанию false
                        }//DONE
                        else if (siz(i) + 1 <= n - a.Length - m - nu + fa - ra * 2)//DONE
                        {
                            per(a + nomer(i) + 'f', m + 1, kk.GetRange(0, skobk.Count() - 1).Concat(new List<int> { 0 }).ToList(), n, mxnu, nu, ra, fu);
                        }
                    }//DONE
                    else if (m > 0)//DONE
                    {
                        if (!mxnu[i].p && fu != i)//если данная функция(1) не использовалась...
                        {
                            mxnu[i] = new dbool { p = true };
                            per(a + nomer(i) + 'f', m, kk.GetRange(0, skobk.Count() - 1), n, mxnu, nu - siz(i), ra, fu);
                            mxnu[i] = new dbool();
                        }
                        else if (siz(i) <= n - a.Length - m - nu + fa - ra * 2)
                        {
                            per(a + nomer(i) + 'f', m, kk.GetRange(0, skobk.Count() - 1), n, mxnu, nu, ra, fu);
                        }
                    }//DONE Возможно будет плохо если функция вызовет сама себя
                    else if (ra == 0) //аргумет не запрашиваются=> новое выражение
                    {
                        if (siz(i) <= n - a.Length - 3 - nu || !mxnu[i].p && 0 <= n - a.Length - 3 - nu)// 3- минимальное число знаков в новом выражении: ...f x= "nu"х
                        {
                            if (mxnu[i].n)//DONE
                            {
                                if (siz(i) <= n - a.Length - 4 - nu || !mxnu[i].p) { per(a * nomer(i) + 'f', 2, new List<int> { 1, 0 }, n, mxnu, nu, 1, i); }    //4-...                            
                            }
                            else { per(a * nomer(i) + 'f', 1, new List<int> { 1 }, n, mxnu, nu, 1, i); }
                        }//DONE
                    }//DONE
                }//DONE (проверка знака = , числа)DONE
                if (m == 0 && ra == 0)//-объявление новой функции если не ожидается аргументов
                {
                    if (siz(mxnu.Count()) <= n - a.Length - 3 - nu)
                    {
                        per(a * nomer(mxnu.Count()) + 'f', 1, new List<int> { 1 }, n, mxnu.Concat(new List<dbool> { new dbool() }).ToList(), nu + siz(mxnu.Count()), 1, mxnu.Count());
                    }
                    if (siz(mxnu.Count()) <= n - 4 - a.Length - nu)//3-...(xn=)
                    {
                        per(a * nomer(mxnu.Count()) + 'f', 2, new List<int> { 1, 0 }, n, mxnu.Concat(new List<dbool> { new dbool { n = true } }).ToList(), nu + siz(mxnu.Count()) + 1, 1, mxnu.Count());
                    }
                }//DONE
                for (int i = 1; ; i++)// 
                {
                    if (m > 0 && ((m > 1 || nu - fa == 0) && Convert.ToString(i, 2).Length <= n - a.Length - m + 1 - nu + fa - ra * 2 || Convert.ToString(i, 2).Length <= n - a.Length - m + 1 - nu + fa - 2))//DONE
                    {
                        per(a + Convert.ToString(i, 2), m - 1, skobk.GetRange(0, skobk.Count() - 1), n, mxnu, nu, ra, fu);
                    }
                    else { break; }
                }//DONE
                kk = new List<int>(skobk);
                for (int i = 0; i <= skobk.Last() && i <= n - a.Length - 2 - m - nu + fa - ra * 2; i++)//2 -...))) +"nu"x... + и х  - два символа
                {

                    per(a + sk(i) + "+", m + 1, kk, n, mxnu, nu, ra, fu);
                    kk[kk.Count() - 1]--;
                }//DONE
                if (m == 0 && n - a.Length - nu + fa - 2 >= 0)//=*-2 символа
                {
                    per(a + '=', 1, new List<int> { 0 }, n, mxnu, nu, 0, fu);
                }//DONE
                if (m > 0)
                {
                    if (m > 1 || nu - fa == 0 || n - a.Length - nu + fa - 2 > 0)
                    {
                        per(a + 'n', m - 1, skobk.GetRange(0, skobk.Count() - 1), n, mxnu, nu, ra, fu);
                        per(a + 'x', m - 1, skobk.GetRange(0, skobk.Count() - 1), n, mxnu, nu, ra, fu);
                    }
                }//верно
                else if (n - a.Length - nu + fa - 2 > 0 && ra == 0)//2 - знак "=*" в новом выражении
                {
                    per(a * "n", m, new List<int> { 0 }, n, mxnu, nu, 1, -1);
                }//DONE
            }//DONE
            if (a.Last() == '+')
            {
                if (m > 1 || nu - fa == 0 || n - a.Length - nu + fa - 2 > 0)//2 - +NUx или =NUx
                {
                    per(a + 'n', m - 1, skobk, n, mxnu, nu, ra, fu);
                    per(a + 'x', m - 1, skobk, n, mxnu, nu, ra, fu);
                }//СДЕЛАНО
                kk = new List<int>(skobk);
                kk[kk.Count() - 1] += 1;
                for (int i = 0; i < mxnu.Count(); i++)// DONE
                {
                    if (mxnu[i].n)//если 2 переменных и они запрашиваются//DONE
                    {
                        if (!mxnu[i].p && fu != i)//если данная функция(2) не использовалась...DONE
                        {
                            mxnu[i] = new dbool { p = true, n = true };
                            per(a + nomer(i) + 'f', m + 1, kk.Concat(new List<int> { 0 }).ToList(), n, mxnu, nu - 1 - siz(i), ra, fu);
                            mxnu[i] = new dbool { n = true }; //bool по умолчанию false
                        }
                        else if (siz(i) + 1 <= n - a.Length - m - nu + fa - ra * 2)// (siz(i)+1 тк f-я двух переменных)DONE
                        {
                            per(a + nomer(i) + 'f', m + 1, kk.Concat(new List<int> { 0 }).ToList(), n, mxnu, nu, ra, fu);
                        }
                    }
                    else//DONE
                    {
                        if (!mxnu[i].p && fu != i)//если данная функция(1) не использовалась...DONE
                        {
                            mxnu[i] = new dbool { p = true };
                            per(a + nomer(i) + 'f', m, kk, n, mxnu, nu - siz(i), ra, fu);
                            mxnu[i] = new dbool();
                        }
                        else if (siz(i) <= n - a.Length - m - nu + fa - ra * 2)//DONE
                        {
                            per(a + nomer(i) + 'f', m, kk, n, mxnu, nu, ra, fu);
                        }
                    }// Возможно будет плохо если функция вызовет сама себя
                }//DONE
                for (int i = 1; ; i++)// 
                {
                    if ((m > 1 || nu - fa == 0) && Convert.ToString(i, 2).Length <= n - a.Length - m + 1 - nu + fa - ra * 2 || Convert.ToString(i, 2).Length <= n - a.Length - m + 1 - nu + fa - 2)//DONE
                    {
                        per(a + Convert.ToString(i, 2), m - 1, skobk, n, mxnu, nu, ra, fu);
                    }
                    else { break; }
                }//DONE
                //per(a + "x",m,skobk, n);
                //per(a + "1",m,skobk, n);
                /*  for (int i = 0; i < mxnu.Count(); i++)
                  {
                      if (mxnu[i])
                      {
                          per(a + nomer(i)+'f', m + 1, n, mxnu);
                      }
                      else { per(a + nomer(i)+'f', m,skobk, n, mxnu); }// Возможно будет плохо если функция вызовет сама себя
                  }
                  if (n - m > 1) { per(a + 'f', m,skobk, n); }
                  per(a + 'n',m,skobk, n);*/
            }//DONE
            if (a.Last() == '=')
            {
                if (nu - fa == 0 || n - a.Length - nu + fa - 2 > 0)//2 - +NUx или =NUx
                {
                    per(a + 'n', 0, skobk, n, mxnu, nu, ra, fu);
                    per(a + 'x', 0, skobk, n, mxnu, nu, ra, fu);
                }//DONE
                kk = new List<int>(skobk);
                kk[kk.Count() - 1] += 1;
                for (int i = 0; i < mxnu.Count(); i++)// DONE
                {
                    if (mxnu[i].n)//если 2 переменных и они запрашиваются//DONE
                    {
                        if (!mxnu[i].p && fu != i)//если данная функция(2) не использовалась...DONE
                        {
                            mxnu[i] = new dbool { p = true, n = true };
                            per(a + nomer(i) + 'f', m + 1, kk.Concat(new List<int> { 0 }).ToList(), n, mxnu, nu - 1 - siz(i), ra, fu);
                            mxnu[i] = new dbool { n = true }; //bool по умолчанию false
                        }
                        else if (siz(i) + 1 <= n - a.Length - 1 - nu + fa)// (siz(i)+1 тк f-я двух переменных) 1=m DONE
                        {
                            per(a + nomer(i) + 'f', m + 1, kk.Concat(new List<int> { 0 }).ToList(), n, mxnu, nu, ra, fu);
                        }
                    }
                    else//DONE
                    {
                        if (!mxnu[i].p && fu != i)//если данная функция(1) не использовалась...DONE
                        {
                            mxnu[i] = new dbool { p = true };
                            per(a + nomer(i) + 'f', m, kk, n, mxnu, nu - siz(i), ra, fu);
                            mxnu[i] = new dbool();
                        }
                        else if (siz(i) <= n - a.Length - m - nu + fa)//DONE
                        {
                            per(a + nomer(i) + 'f', m, kk, n, mxnu, nu, ra, fu);
                        }
                    }// Возможно будет плохо если функция вызовет сама себя
                }//DONE
                for (int i = 1; ; i++)// 
                {
                    if (nu - fa == 0 && Convert.ToString(i, 2).Length <= n - a.Length || Convert.ToString(i, 2).Length <= n - a.Length - nu + fa - 2)//DONE
                    {
                        per(a + Convert.ToString(i, 2), m - 1, skobk, n, mxnu, nu, ra, fu);
                    }
                    else { break; }
                }//DONE
            }//DONE

            if (a.Last() == '1' || a.Last() == '0')
            {
                if (m > 0)
                {
                    if (m > 1 || nu - fa == 0 || n - a.Length - nu + fa - 2 > 0)
                    {
                        per(a + 'n', m - 1, skobk.GetRange(0, skobk.Count() - 1), n, mxnu, nu, ra, fu);
                        per(a + 'x', m - 1, skobk.GetRange(0, skobk.Count() - 1), n, mxnu, nu, ra, fu);
                    }
                }//верно
                else if (n - a.Length - nu + fa - 2 > 0 && ra == 0)//2 - знак "=*" в новом выражении
                {
                    per(a * "n", m, new List<int> { 0 }, n, mxnu, nu, 1, -1);
                }//DONE
                kk = new List<int>(skobk);
                for (int i = 1; i <= skobk.Last() && i <= n - a.Length - 2 - m - nu + fa - ra * 2; i++)//2 -...))) +"nu"x... + и х  - два символа; После числа не может идти '+'
                {
                    kk[kk.Count() - 1]--;
                    per(a + sk(i) + "+", m + 1, kk, n, mxnu, nu, ra, fu);
                }//DONE
                if (m == 0 && n - a.Length - nu + fa - 2 >= 0)//=*-2 символа
                {
                    per(a + '=', 1, new List<int> { 0 }, n, mxnu, nu, 0, fu);
                }//DONE
                if (m > 1)
                {
                    kk = new List<int>(skobk);
                    kk[kk.Count() - 2] += 1;
                }
                for (int i = 0; i < mxnu.Count(); i++)// DONE
                {
                    if (mxnu[i].n && m > 0)//если 2 переменных и они запрашиваются
                    {
                        if (!mxnu[i].p && fu != i)//если данная функция(2) не использовалась...
                        {
                            mxnu[i] = new dbool { p = true, n = true };
                            per(a + nomer(i) + 'f', m + 1, kk.GetRange(0, skobk.Count() - 1).Concat(new List<int> { 0 }).ToList(), n, mxnu, nu - 1 - siz(i), ra, fu);
                            mxnu[i] = new dbool { n = true }; //bool по умолчанию false
                        }//DONE
                        else if (siz(i) + 1 <= n - a.Length - m - nu + fa - ra * 2)//DONE
                        {
                            per(a + nomer(i) + 'f', m + 1, kk.GetRange(0, skobk.Count() - 1).Concat(new List<int> { 0 }).ToList(), n, mxnu, nu, ra, fu);
                        }
                    }//DONE
                    else if (m > 0)//DONE
                    {
                        if (!mxnu[i].p && fu != i)//если данная функция(1) не использовалась...
                        {
                            mxnu[i] = new dbool { p = true };
                            per(a + nomer(i) + 'f', m, kk.GetRange(0, skobk.Count() - 1), n, mxnu, nu - siz(i), ra, fu);
                            mxnu[i] = new dbool();
                        }
                        else if (siz(i) <= n - a.Length - m - nu + fa - ra * 2)
                        {
                            per(a + nomer(i) + 'f', m, kk.GetRange(0, skobk.Count() - 1), n, mxnu, nu, ra, fu);
                        }
                    }//DONE Возможно будет плохо если функция вызовет сама себя
                    else if (ra == 0) //аргумет не запрашиваются=> новое выражение
                    {
                        if (siz(i) <= n - a.Length - 3 - nu || !mxnu[i].p && 0 <= n - a.Length - 3 - nu)// 3- минимальное число знаков в новом выражении: ...f x= "nu"х
                        {
                            if (mxnu[i].n)//DONE
                            {
                                if (siz(i) <= n - a.Length - 4 - nu || !mxnu[i].p) { per(a * nomer(i) + 'f', 2, new List<int> { 1, 0 }, n, mxnu, nu, 1, i); }    //4-...                            
                            }
                            else { per(a * nomer(i) + 'f', 1, new List<int> { 1 }, n, mxnu, nu, 1, i); }
                        }//DONE
                    }//DONE
                }//DONE (проверка знака = , числа)DONE
                if (m == 0 && ra == 0)//-объявление новой функции если не ожидается аргументов
                {
                    if (siz(mxnu.Count()) <= n - a.Length - 3 - nu)
                    {
                        per(a * nomer(mxnu.Count()) + 'f', 1, new List<int> { 1 }, n, mxnu.Concat(new List<dbool> { new dbool() }).ToList(), nu + siz(mxnu.Count()), 1, mxnu.Count());
                    }
                    if (siz(mxnu.Count()) <= n - 4 - a.Length - nu)//3-...(xn=)
                    {
                        per(a * nomer(mxnu.Count()) + 'f', 2, new List<int> { 1, 0 }, n, mxnu.Concat(new List<dbool> { new dbool { n = true } }).ToList(), nu + siz(mxnu.Count()) + 1, 1, mxnu.Count());
                    }
                }//DONE
                for (int i = 1; ; i++)// 
                {
                    if (m > 0 && ((m > 1 || nu - fa == 0) && Convert.ToString(i, 2).Length <= n - a.Length - m + 1 - nu + fa - ra * 2 || Convert.ToString(i, 2).Length <= n - a.Length - m + 1 - nu + fa - 2))//DONE
                    {
                        per(a + Convert.ToString(i, 2), m - 1, skobk.GetRange(0, skobk.Count() - 1), n, mxnu, nu, ra, fu);
                    }
                    else { break; }
                }//DONE
                // (per(a + '+',m,skobk, n); в таком случае могут появиться выражения вида 1+1, что вредно)FIXED

            }//номер для использования n!!!
            if (n - a.Length <= 1 && nu - fa == 0 && m == 0)
            { aaa++; }
            return (0);
        }
       
        static int findend (string a, int n)//находит конец слагаемого начиная c f !!!функции двух переменных??
        {
            n++;
            for (int m = 1; m != 0 && n<a.Length-1;)
            {
                if (a[n] == ')') { m--; }
                if (a[n] == '=') { m = 0; }
                else if (a[n] == 'f') { m++; }
                n++;
            }
            return (n);

        }


        static KeyValuePair<Tree,Dictionary<Tree, int>> ConvertToTree(string stroka, bool end=true)
        {
            int idx = 1, number = 0, C=1;
            Dictionary<Tree, int> Xs = new Dictionary<Tree, int>();
            Tree x;
            Tree function = new Tree { index = -2}, Change=new Tree {index=-2 };
            if (!end && stroka.Contains('=') ){ C = -1; }
            for (int i = 0; i < stroka.Length; i++)
            {
                for (; stroka[i] == 'o' || stroka[i] == 'l'; i++)
                {
                    idx *= 2;
                    if (stroka[i] == 'l')
                    {
                        idx++;
                    }
                }
                if (stroka[i] == 'f')
                {
                    idx -= 1;
                    if (function.index == -2) { function = function.Addchild(C, new Tree { index = idx }); }
                    else { function = function.Addchild(1, new Tree { index = idx }); }
                    idx = 1;
                }
                else if (stroka[i] == '=')
                {
                    while(function.index!=-2)
                    {
                        function = function.parent;
                    }
                    C *= -1;
                }
                else if (stroka[i] == 'x')
                {
                    x = function;
                    if (function.index == -2)
                    {
                        if (Xs.ContainsKey(x)) { Xs[x] += C; }
                        else { Xs[x] = C; }
                    }
                    else
                    {
                        if (Xs.ContainsKey(x)) { Xs[x] += 1; }
                        else { Xs[x] = 1; }
                    }
                }
                else if (stroka[i] == ')')
                {
                    if(function.ends.Count == 0)
                    {
                        function.ends.Add(function);
                        function.ApplyEnds();
                    }
                    function = function.parent;
                }
                else if (stroka[i] == '0' || stroka[i] == '1')
                {
                    for (; i < stroka.Length && (stroka[i] == '0' || stroka[i] == '1'); i++)
                    {
                        number *= 2;
                        if (stroka[i] == '1')
                        { number++; }
                    }
                    if (function.index == -2) { function.number=C * number; }
                    else { function.number= number; }
                    number = 0;
                    i--;
                }
            }
            if (end)
            {
                if (Xs.Count != 0)
                {
                    x = Xs.Keys.First();
                    x = x.ends.First();
                }
                else
                {
                    return new KeyValuePair<Tree, Dictionary<Tree, int>>(function.ends.First(), Xs);
                }
                return new KeyValuePair<Tree, Dictionary<Tree, int>>(x, Xs);
            }
            else
            {
                return new KeyValuePair<Tree, Dictionary<Tree, int>>(function, Xs);
            }
        }
        static KeyValuePair<int, List<int>> SortedFunctions(string a)//функции отсортированы
        {
            int j = 0, m = 0, depth = 0, idx = 1, number = 0, number1=0, result=0;
            bool ready=false, notcomplete=true;//ready- обозначает получения листа максимального уровня( поскольку функции отсортированы то этот лист соответствует первой функции)
            List<int> list1 = new List<int>(), list = new List<int>();//list1 - наибольший набор индексов
            for (int i = 0; i < a.Length; i++)
            {

                for (;notcomplete && a[i] == 'o' || a[i] == 'l'; i++)
                {
                    idx *= 2;
                    if (a[i] == 'l')
                    {
                        idx++;
                    }
                }
                if (notcomplete && a[i] == 'f')
                {
                    idx -= 1;
                    depth++;
                    list1.Add(idx);
                    idx = 1;
                }
                if ((a[i] == ')' || a[i]=='=') && notcomplete)
                {
                    if (!ready)//лист для сравнения(максимальный)
                    {
                        if (a[i - 1] == '0' || a[i - 1] == '1')
                        {
                            for (int ii = 1; a[i - ii] == '0' || a[i - ii] == '1'; ii++)
                            {
                                if (a[i - ii] == '1') { number += (int)Math.Pow(2, ii - 1); }
                            }
                        }
                        for (; j < list1.Count; j = list1.IndexOf(list1.GetRange(j + 1, list1.Count - j - 1).Max()))
                        {
                            list.Add(list1[j]);
                            if (list1.Count == j + 1) { break; }
                        }
                        ready = true;
                    }
                    else//лист который сравниваем
                    {
                        j = 0; m = 0;
                        for (; j < list1.Count; j = list1.IndexOf(list1.GetRange(j + 1, list1.Count - j - 1).Max()))
                        {
                            if (list1[j] != list[m]) { return new KeyValuePair<int, List<int>>(result, list1); }
                            m++;
                            if (list1.Count == j + 1) { break; }
                        }
                        if (a[i - 1] == '0' || a[i - 1] == '1')
                        {
                            for (int ii = 1; a[i - ii] == '0' || a[i - ii] == '1'; ii++)
                            {
                                if (a[i - ii] == '1') { number1 += (int)Math.Pow(2, ii - 1); }
                            }
                        }
                        if (number1 != number) { return new KeyValuePair<int, List<int>>(result, list1); }
                        number1 = 0;
                    }
                    notcomplete = false;
                }
                if (depth == 0) {
                    list1.Clear(); notcomplete = true; result=i; }
                else if (a[i] == ')') { depth--; }
                if (a[i] == '=')
                {
                    depth = 0; list1.Clear();
                    result = i; notcomplete = true;
                }
            }
            return new KeyValuePair<int, List<int>>();
        }      
        static int[] Functions(string a, int n)//функции в строке расположены как угодно
        {
            int j = 0, m = 0, nomer = 1, idx = 1, number1 = 0, number = 0, removeN = 0;
            int[] position=new int[2];
            bool sameOrder=true;
            List<int> list2 = new List<int>(), list1=new List<int>(), list=new List<int>();
           /* for(int i=a.Length; i>=0;i--)
            {
                if(m==0 && a[i] == '1' || a[i] == '0')
                {
                    for (int ii=0; a[i] == '1' || a[i] == '0'; i--)
                    {
                        ii++;
                        if (a[i] == '1') { number += 2 ^ ii; }
                            
                    }
                }

                if (a[i] == 'f')
                {
                    m++;
                    for(nomer=1 ;i>0 && a[i-1]=='o' || a[i - 1] == 'l';i--)
                    {
                        if(a[i-1]=='l')
                        { idx += nomer; }
                        nomer *= 2;
                    }
                    idx +=nomer- 1;
                    if (list1.Last() < idx)
                    {
                        list1.Append(idx);
                        if()
                    }
                }
            } -БРЕД*/
            for (int i=0; i< a.Length;i++)
            {
                for(;a[i]=='o' || a[i]=='l';i++)
                {
                    idx *= 2;
                    if(a[i]=='l')
                    {
                        idx ++;
                    }
                }
                idx -= 1;
                if (a[i] == 'f')
                {
                    m++;
                    list1.Append(idx);                   
                    idx = 1;
                }
                if (a[i] == ')')
                {
                    removeN++;
                    if(a[i-1]=='0'||a[i-1]=='1')
                    {
                        for (int ii= 1; a[i - ii] == '0' || a[i - ii] == '1';ii++)
                        {
                            if (a[i - ii] == '1') { number1 += (int)Math.Pow(2, ii - 1); }
                        }
                    }
                    if (i + 1 == a.Length || a[i + 1] != ')')//eсли скобок много
                    {
                        j = 0;
                        m = 0;
                        for (; j < list1.Count && list1[j] >= list[m]; j = list1.IndexOf(list1.GetRange(j, list1.Count).Max()))
                        {
                            if (list1[j] > list[m])
                            {
                                list.RemoveRange(m, list.Count - m);
                                list.Append(list1[j]);
                                position = new int[]{i, list1.Count};
                                sameOrder = false;
                            }
                            if (m == list.Count()) { list.Append(list1[j]); sameOrder = false; }
                            m++;
                        }
                        if (number1 > number && m==list.Count) { number = number1; sameOrder = false; }
                        if (sameOrder) { }
                        number1 = 0;
                        list1.RemoveRange(removeN,list1.Count() - removeN);
                        removeN = 0;
                    }
                    
                }
                if (a[i] == '=' && a[i-1]!=')')
                {
                    if (a[i - 1] == '0' || a[i - 1] == '1')
                    {
                        for (int ii = 1; a[i - ii] == '0' || a[i - ii] == '1'; ii++)
                        {
                            if (a[i - ii] == '1') { number1 += (int)Math.Pow(2, ii - 1); }
                        }
                    }
                    j = 0;
                    m = 0;
                    for (; j < list1.Count && list1[j] >= list[m]; j = list1.IndexOf(list1.GetRange(j, list1.Count).Max()))
                    {
                        if (list1[j] > list[m])
                        {
                            list.RemoveRange(m, list.Count - m);
                            list.Append(list1[j]);
                            position = new int[] { i, list1.Count };
                        }
                        if (m == list.Count()) { list.Append(list1[j]); position = new int[] { i, list1.Count }; }
                        m++;
                    }
                    if (number1 > number && j == list1.Count && m==list.Count()) { number = number1; position = new int[] { i, list1.Count }; }
                    number1 = 0;
                    list1.Clear();
                    removeN = 0;
                }
            }
            
            return (position);
            
        }
        static int Exe(prog proga, int x, string stroka="fx)")//требуется изменить перебор(per) так, что если было fx)=a то a=fx) не перебирается(так удобнее поскольку позволяет проводить проверку на приоритетность только один раз)
        {
            int idx=1, number=0, n =0, i; //n - n, num - число в строке, f - значение где функция определена, maxnumber - максимальное а в f(x+a)
            bool assigned, GoOn = true, equal=true ;
            Tree function = new Tree { index = -2 }, Ruletree;
            stroka=stroka.Replace("x", Convert.ToString(x, 2));
            for (i = 0; i < stroka.Length; i++)
            {
                for (; stroka[i] == 'o' || stroka[i] == 'l'; i++)
                {
                    idx *= 2;
                    if (stroka[i] == 'l')
                    {
                        idx++;
                    }
                }
                if (stroka[i] == 'f')
                {
                    idx -= 1;
                    function = function.Addchild(1, new Tree { index = idx });
                    idx = 1;
                }
                else if (stroka[i] == ')')
                {
                    if (function.ends.Count == 0)
                    {
                        function.ends.Add(function);
                        function.ApplyEnds();
                    }
                    function = function.parent;
                }
                else if (stroka[i] == '0' || stroka[i] == '1')
                {
                    for (; i < stroka.Length && (stroka[i] == '0' || stroka[i] == '1'); i++)
                    {
                        number *= 2;
                        if (stroka[i] == '1')
                        { number++; }
                    }
                    function.number=number;
                    number = 0;
                    i--;
                }
            }
            

            KeyValuePair<int,List<int>> posandlevel;
            List<KeyValuePair<Tree, Dictionary<Tree,int>>> trees= new List<KeyValuePair<Tree, Dictionary<Tree, int>>>(), TreesToAdd=new List<KeyValuePair<Tree, Dictionary<Tree, int>>>();
            List<List<int>> Levels=new List<List<int>>();
            Tree tree, Xtree, AddTree;
            HashSet<Tree> Ends, used=new HashSet<Tree>(), Zeroes = new HashSet<Tree>();
            Dictionary<Tree, Tree> used2 = new Dictionary<Tree, Tree>();
            List<Tuple<int,int,int>> Divides=new List<Tuple<int, int, int>>();
            int sum, sum2=0;//пофиксить отрицательные и положительные числа
            for ( i=0; i<proga.s.Count ;i++ )
            {
                trees.Add(ConvertToTree( proga.s[i].Substring(0, SortedFunctions(proga.s[i]).Key)));
                TreesToAdd.Add(ConvertToTree(proga.s[i].Substring(SortedFunctions(proga.s[i]).Key), false));
            }
            while(function.children.Count!=0)
            {
                for (i = 0; i < trees.Count; i++)
                {
                    Ruletree = trees[i].Key;
                    Xtree = trees[i].Value.Keys.First();
                    Ends = new HashSet<Tree>(function.ends);
                    foreach (Tree end in Ends)
                    {
                        GoOn = true;
                        assigned = false;
                        if (!used.Contains(end)&& Ruletree.index == end.index)
                        {
                            if (trees[i].Value.TryGetValue(Ruletree, out int xs))
                            {//назначаем х
                                if (Ruletree.number == end.number)
                                {
                                    assigned = true;
                                }
                                else{
                                    if ((end.number - Ruletree.number)% xs==0)
                                    {
                                        number = (end.number - Ruletree.number) / xs;
                                        foreach (var Xend in trees[i].Value)
                                        {
                                            Xend.Key.number+=number * Xend.Value;
                                        }
                                        assigned = true;
                                    }
                                    else { GoOn = false; }
                                }
                            }
                            tree = end;
                            while (GoOn && tree.parent!=null && tree.parent.children[tree]==Ruletree.parent.children[Ruletree])//подъем по дереву
                            {
                                Ruletree = Ruletree.parent;
                                tree = tree.parent;
                                if (tree.index != Ruletree.index && Ruletree.index!=-2)
                                {
                                    GoOn = false;
                                    break;
                                }
                                if (!assigned && Ruletree==Xtree)//ecли содержит х назначаем х
                                {
                                    xs = trees[i].Value[Xtree];
                                    if (Ruletree.number != tree.number)
                                    {
                                        if ((tree.number - Ruletree.number)%xs==0)
                                        {
                                            number = (tree.number - Ruletree.number) / xs;
                                            foreach (var Xend in trees[i].Value)
                                            {
                                                Xend.Key.number+=number * Xend.Value;
                                            }
                                        }
                                        else
                                        {
                                            GoOn = false;
                                            break;
                                        }
                                    }
                                    assigned = true;
                                }
                                if (Ruletree.index == -2)
                                {
                                    foreach (Tree child1 in Ruletree.children.Keys)
                                    {
                                        equal = false;
                                        foreach (Tree child2 in tree.children.Keys)
                                        {
                                            if (!used2.ContainsValue(child2) && child1 == child2)
                                            {//divides хранит сколько раз можно заменить конкретное дерево, количество деревьев за одну замену и остаток от деления
                                                Divides.Add(new Tuple<int, int, int>(tree.children[child2] / Ruletree.children[child1], Ruletree.children[child1], tree.children[child2] % Ruletree.children[child1]));
                                                used2[child1] = child2;
                                                equal = true;
                                                break;
                                            }
                                        }
                                        if(!equal)
                                        {
                                            Divides.Add(new Tuple<int, int, int>(0, Ruletree.children[child1], 0));
                                        }
                                    }//считаем divides
                                    Divides.Sort((a, b) => a.Item1.CompareTo(b.Item1));
                                    sum = Divides.Sum(a => Math.Abs(a.Item2));
                                    for (int j = 0; j < Divides.Count; j++) //записывает в sum2 сколько раз нужно применить правило
                                    {
                                        sum2 += Math.Abs(Divides[j].Item2);
                                        if (sum2 > sum / 2)
                                        {
                                            for (int k = j; k > -1 && Divides[j].Item1 == Divides[k].Item1; k--)
                                            {
                                                sum2 -= Divides[k].Item3;
                                            }
                                            if (sum2 <= sum / 2)
                                            {
                                                j++;
                                                equal = true;
                                                for (int k = j; j < Divides.Count && Divides[j].Item1 == Divides[k].Item1; j++)
                                                {
                                                    sum2 += Divides[j].Item2 - Divides[j].Item3;
                                                    if (sum2 > sum / 2)
                                                    {
                                                        sum2 = Divides[j].Item1;//по остаткам не проходит
                                                        equal = false;
                                                        break;
                                                    }
                                                }
                                                if (equal)//по остаткам проходит
                                                {
                                                    if (j >= Divides.Count) { j--; }
                                                    sum2 = Divides[j].Item1+1;
                                                }
                                            }
                                            break;
                                        }
                                    }
                                    foreach (Tree child in Ruletree.children.Keys)//применяем ruletree
                                    {
                                        if (used2.ContainsKey(child) )
                                        {
                                            if (Ruletree.children[child] * sum2 == tree.children[used2[child]]) { tree.Removechild(used2[child]); }
                                            else { tree.children[used2[child]] -= Ruletree.children[child] * sum2; }
                                            used.UnionWith(used2[child].ends);
                                        }
                                        else
                                        {
                                            tree.Addchild(-Ruletree.children[child] * sum2, new Tree(child));
                                        }
                                    }
                                    AddTree = TreesToAdd[i].Key;
                                    foreach (var Xend in TreesToAdd[i].Value)//устанавливаем иксы у низкоуровневых деревьев
                                    {
                                        Xend.Key.number+=number * Xend.Value;
                                    }
                                    tree.number += sum2 * AddTree.number;
                                    foreach (Tree child in AddTree.children.Keys)//добавляем "низкоуровневые" деревья
                                    {
                                        used.UnionWith(tree.Decrease(AddTree.children[child] * sum2, child));
                                    }
                                    sum2 = 0;
                                    Divides.Clear();
                                    used2.Clear();
                                    if (tree.children.Count == 0)
                                    {
                                        tree.ends.Add(tree);
                                        tree.ApplyEnds();
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    Ends = new HashSet<Tree>(function.ends);
                    foreach (Tree child in Ends)//заменяем все *f(0)на ноль
                    {
                        if (child.number == 0)
                        { 
                            child.parent.Removechild(child);
                        }
                    }
                    used.Clear();
                }

            }
            
            return function.number;
        }


        static void Main(string[] args)
        {
            prog abc=new prog();
            abc *= "fx+1)=fx)+x";
            _123.function(8);
            string a, al;
            int n = 0;
            Console.Write(aaa);
            n = Convert.ToInt32(Console.ReadLine());
            HashSet<List<int>> AA = new HashSet<List<int>> { new List<int> { 1 } }, B = new HashSet<List<int>> { new List<int> { 1 } };

            // per("f", 1, new List<int> { 1 }, n, new List<dbool> { new dbool() }, 1, 1, 0);
            var tree1 = ConvertToTree("olfx+x)");
            Tree tree2 = new Tree { index = 1 }, tree3 = new Tree { index = 2 };

            Console.Write(Exe(abc, n));
            Console.Read();
            al = "f)xn01ol+";

        }
    }
}