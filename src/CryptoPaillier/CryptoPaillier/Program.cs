using System;
using System.IO;
using System.Numerics;

namespace CryptoPaillier
{
    class Program
    {
        static void Main(string[] args)
        {
            //put your own data path
            var path = GetDataPath("/PaillierInput.txt");

            //put a given Paillier public key
            var n = BigInteger.Parse("1110875290280920009961998978166106038302156763");

            //put a given Paillier private key
            var phi = BigInteger.Parse("1110875290280920009961932304108708457006287400");
            Paillier(path,n,phi);
        }

        static void Paillier(string dataPath, BigInteger n, BigInteger phi)
        {
            var cMultiply = new BigInteger(1);
            using (var sr = new StreamReader(dataPath))
            {
                for (int i = 0; i < 1000; ++i)
                {
                    var s = sr.ReadLine();
                    var index = s.IndexOf("s") + 1;
                    var subs = s.Substring(index);
                    cMultiply *= BigInteger.Parse(subs);
                }    
            }
            
            var multiplyRes = (cMultiply)%(n*n);
            var c = multiplyRes;
            int yes, no;
            Decrypt(n,phi,c,out yes,out no);

            Console.WriteLine("Election results:");
            Console.WriteLine("Yes:{0}, No:{1}",yes,no);
            Console.ReadLine();
        }

        static void Decrypt(BigInteger n, BigInteger phi,BigInteger c, out int yes, out int no)
        {
            BigInteger phiInverse, y;
            Gcd(phi, n, out phiInverse, out y);
            if (phiInverse < 0)
            {
                phiInverse += n;
            }
            var cExpMod = BigInteger.ModPow(c, phi, n*n);
            var leftPart = ((cExpMod - 1)/n)%n;
            BigInteger m = (phiInverse*leftPart)%n;
            yes = (int)m;
            no = 1000 - yes;
        }

        //Extended Euclidean Theorem
        static void Gcd(BigInteger a, BigInteger b, out BigInteger x, out BigInteger y)
        {
            if (a == 0)
            {
                x = 0;
                y = 1;
                return; 
            }
            BigInteger x1, y1;
            
                Gcd(b % a, a, out x1, out y1);
            x = y1 - (b / a) * x1;
            y = x1;
        }

        static string GetDataPath(string filename)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            for (int i = 0; i < 4; i++)
                path = Directory.GetParent(path).FullName;
            path += filename;
            return path;
        }
    }
}
