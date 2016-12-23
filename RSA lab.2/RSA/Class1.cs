using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace RSA_L
{
    public class RSA
    {
        char[] characters = new char[] { '#', 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И',
                                                        'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 
                                                        'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ь', 'Ы', 'Ъ',
                                                        'Э', 'Ю', 'Я', ' ', '1', '2', '3', '4', '5', '6', '7',
                                                        '8', '9', '0' };
        long p = 3;
        long q = 5;
        
        //зашифровать
        public void Encrypt(Stream input, Stream output)
        {

            byte[] buf = new byte[input.Length];
            input.Position = 0;
            input.Read(buf, 0, buf.Length);
           // List<string> inp = new List<string>(Encoding.UTF8.GetString(buf).Split(' '));//из байтов в лист
                if (IsTheNumberSimple(p) && IsTheNumberSimple(q))
                {
                  // string s ="";
                   string str = System.Text.Encoding.Default.GetString(buf);
                    str = str.ToUpper();

                    long n = p * q;
                    long m = (p - 1) * (q - 1);
                    long d = Calculate_d(m);
                    long e_ = Calculate_e(d, m);

                    List<string> result = RSA_Endoce(str, e_, n);
                    byte[] dataAsBytes = result.SelectMany(s => Encoding.ASCII.GetBytes(s)).ToArray();
                    output.Write(dataAsBytes, 0, dataAsBytes.Length);
                    
                }
                else
                    throw new Exception("p или q - не простые числа!");
          
        }

        //расшифровать
        public void Decipher(Stream inp, Stream outp)
        {
                
                long n = p * q;
                long m = (p - 1) * (q - 1);
                long d = Calculate_d(m);
                byte[] buf = new byte[inp.Length];
                inp.Position = 0;
                inp.Read(buf, 0, buf.Length);

                //List<string> input = new List<string>();
               // string arr[]=
                List<byte> input = new List<Byte>(buf);
                //List<string> input   = new List<string>(Encoding.UTF8.GetString(buf).Split(' '));
                string result = RSA_Dedoce(input, d, n);
                byte[] buffer = System.Text.Encoding.Default.GetBytes(result);
                outp.Write(buffer, 0, buffer.Length);
      
        }

        //проверка: простое ли число?
        private bool IsTheNumberSimple(long n)
        {
            if (n < 2)
                return false;

            if (n == 2)
                return true;

            for (long i = 2; i < n; i++)
                if (n % i == 0)
                    return false;

            return true;
        }

        //зашифровать
        private List<string> RSA_Endoce(string s, long e, long n)
        {
            List<string> result = new List<string>();

            BigInteger bi;

            for (int i = 0; i < s.Length; i++)
            {
                int index = Array.IndexOf(characters, s[i]);

                bi = new BigInteger(index);
                bi = BigInteger.Pow(bi, (int)e);

                BigInteger n_ = new BigInteger((int)n);

                bi = bi % n_;

                result.Add(bi.ToString());
            }

            return result;
        }

        //расшифровать
        private string RSA_Dedoce(List<byte> input, long d, long n)
        {
            string result = "";

            BigInteger bi;

            foreach (byte item in input)
            {
                bi = new BigInteger(Convert.ToDouble(item));
                bi = BigInteger.Pow(bi, (int)d);

                BigInteger n_ = new BigInteger((int)n);

                bi = bi % n_;

                int index = Convert.ToInt32(bi.ToString());

                result += characters[index].ToString();
            }

            return result;
        }

        //вычисление параметра d. d должно быть взаимно простым с m
        private long Calculate_d(long m)
        {
            long d = m - 1;

            for (long i = 2; i <= m; i++)
                if ((m % i == 0) && (d % i == 0)) //если имеют общие делители
                {
                    d--;
                    i = 1;
                }

            return d;
        }

        //вычисление параметра e
        private long Calculate_e(long d, long m)
        {
            long e = 10;

            while (true)
            {
                if ((e * d) % m == 1)
                    break;
                else
                    e++;
            }

            return e;
        }

       
    }
}
