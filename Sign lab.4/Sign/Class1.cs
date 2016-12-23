using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Numerics;

namespace Sign
{
    public class RSASign
    {
     
        static BigInteger N;
        static BigInteger d, c;
        static char[] characters = new char[] { '#', 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И',
                                                        'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 
                                                        'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ь', 'Ы', 'Ъ',
                                                        'Э', 'Ю', 'Я', ' ', '1', '2', '3', '4', '5', '6', '7',
                                                        '8', '9', '0' };

        public interface RSAInterface
        {
            void Sign(Stream input, Stream output);
            bool Verify(Stream input);
        }

        public class CypherRSA : RSAInterface
        {
            private static RSASign rsa;

            public void Sign(Stream input, Stream output)
            {
                rsa.Sign(input, output);
            }

          

            public bool Verify(Stream input)
            {
                return rsa.Verify(input);
            }
        }
        public void ProcessRSAParams()
        {
            BigInteger P = GeneratePrime();
            BigInteger Q = GeneratePrime();
            P = 3; Q = 11;
            N = P * Q;
            BigInteger f = (P - 1) * (Q - 1);

            Random rand = new Random();

            while (true)
            {
                d = rand.Next(1, Convert.ToInt32(f.ToString()));
                if (!CheckMutualPrime(Convert.ToInt32(d.ToString()), Convert.ToInt32(f.ToString())))
                {
                    continue;
                }
                break;
            }
            c = Reverse(d, f);
        }

        public void ProcessEuclidean(BigInteger a, BigInteger b, out BigInteger x, out BigInteger y, out BigInteger NOD)
        {
            if (a < b)
            {
                BigInteger temp = a;
                a = b;
                b = temp;
            }

            BigInteger[] U = { a, 1, 0 };
            BigInteger[] V = { b, 0, 1 };
            BigInteger[] T = new BigInteger[3];

            while (V[0] != 0)
            {
                BigInteger q = U[0] / V[0];
                T[0] = U[0] % V[0];
                T[1] = U[1] - q * V[1];
                T[2] = U[2] - q * V[2];
                V.CopyTo(U, 0);
                T.CopyTo(V, 0);
            }

            NOD = U[0];
            x = U[1];
            y = U[2];
        }

        public BigInteger Reverse(BigInteger c, BigInteger m)
        {
            BigInteger x, y, NOD;
            ProcessEuclidean(m, c, out x, out y, out NOD);

            if (y < 0)
            {
                y += m;
            }
            return y;

        }

        public int GeneratePrime()
        {
            Random rand = new Random();
            int a = rand.Next(10000, 11000);

            if (a % 2 == 0)
            {
                a++;
            }
            while (true)
            {
                if (CheckPrime(a))
                {
                    return a;
                }
                a += 2;
            }
        }

        public bool CheckPrime(int n)
        {
            bool isPrime = true;

            for (int i = 2; i < n; i++)
            {
                if (n % i == 0)
                {
                    isPrime = false;
                    break;
                }
            }
            return isPrime;
        }

        public static bool CheckMutualPrime(int a, int b)
        {
            if (a < b)
            {
                int temp = a;
                a = b;
                b = temp;
            }

            while (b != 0)
            {
                int t = a % b;
                a = b;
                b = t;
            }
            return a == 1;
        }

        public void Sign(Stream input, Stream output)
        {
            byte[] buf = new byte[input.Length];

            MemoryStream hash = new MemoryStream();
            Sha2.Sha2 has = new Sha2.Sha2();
            has.HashFile(input, hash);
           // hashFunction.HashStream(input, hash);

            buf = new byte[hash.Length];
            hash.Position = 0;
            hash.Read(buf, 0, buf.Length);

            string hash_msg = Convert.ToString(buf);
         
            ProcessRSAParams();

            List<string> result = SignRSA(hash_msg, (int)c, (int)N);

            output.Position = 0;
            output.Write(Encoding.Default.GetBytes(hash_msg), 0, Encoding.Default.GetBytes(hash_msg).Length);
            output.Write(Encoding.Default.GetBytes(" "), 0, Encoding.Default.GetBytes(" ").Length);
            for (int i = 0; i < result.Count; ++i)
            {
                output.Write(Encoding.Default.GetBytes(result[i]), 0, Encoding.Default.GetBytes(result[i]).Length);
                output.Write(Encoding.Default.GetBytes(" "), 0, Encoding.Default.GetBytes(" ").Length);
            }
        }

        private List<string> SignRSA(string s, long e, long n)
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

        private string CheckRSA(List<string> input, long d, long n)
        {
            string result = "";
            BigInteger bi;

            foreach (string item in input)
            {
                bi = new BigInteger(Convert.ToInt32(item));
                bi = BigInteger.Pow(bi, (int)d);

                BigInteger n_ = new BigInteger((int)n);

                bi = bi % n_;

                int index = Convert.ToInt32(bi.ToString());
                result += characters[index].ToString();
            }
            return result;
        }

        public bool Verify(Stream input)
        {
            byte[] buf = new byte[input.Length];
            input.Position = 0;
            input.Read(buf, 0, buf.Length);

            List<string> inp = new List<string>(Encoding.UTF8.GetString(buf).Split(' '));
            List<string> sign = new List<string>(inp.Count - 1);

            for (int i = 0; i < inp.Count - 2; ++i)
            {
                sign.Add(inp[i + 1]);
            }

            return CheckRSA(sign, (int)d, (int)N) == inp[0];
        }
    }
}
