using System;
using System.Numerics;
using System.Text;

namespace Crypto2
{
    class Program
    {
        static void Main(string[] args)
        {
            int k, h;
            Gcd(399, 10007, out h, out k);
            var c1 =
                BigInteger.Parse(
                    "7536673143565776736851252766385753521577419436650852536394344162715356115698762081228368138204072962985082961552894864333610499136686606591910432873331291668739875767220409064627576671914948912999248186261197026899521412762068663451535780909400953320044733652966295421510642941974997744159108847884810721550623459027543645248469463621430821533276914761012565639407615635164613751132239");
            var c2 =
                BigInteger.Parse(
                    "34769528697321331665191012050496178932951805361555671066485334100903420952829004252656220997571380831700306275885784476779277824820310442607604219134233233062805173736521509115590453629007478551003956032206533499843468309249240298738580633828384585826624413025113558480363158304120635239296998267764449961976578139750273930111547703234975219887601372042727135973836220023715045262455984");
            var m =
                BigInteger.Parse(
                    "77521140191618283780613386239788963215636521877030830245331440024401877894874823258255306962045408180421212221275845885954524130707050376947871888757048175649167751632069031329578526737098714531024187640689621015617255307596010654794518332048429841176596675399659563247476996366152248319716835481860843847055537749352498685794750086157653365404597261149152079186366401321119425525552101");
            
            var res1 = BigInteger.ModPow(c1,h,m);
            var poweredC2 = BigInteger.Pow(c2, 187);
            BigInteger h1, k1;
            Gcd(poweredC2, m, out h1, out k1);
            var res = res1*h1;
            res = (res%m);

            var bytes = new byte[res.ToString().Length / 2];
            for (int i = 0; i < res.ToString().Length; i += 2)
            {
                string hex = res.ToString().Substring(i, 2);
                bytes[i / 2] = Convert.ToByte(hex);

            }
            var str = ASCIIEncoding.ASCII.GetString(bytes);
            Console.Write(str);
            Console.ReadLine();
        }

        static int Gcd(int a, int b, out int x, out int y)
        {
            if (a == 0)
            {
                x = 0;
                y = 1;
                return b;
            }
            int x1, y1;
            int d = Gcd(b%a, a, out x1, out y1);
            x = y1 - (b/a)*x1;
            y = x1;
            return d;
        }

        static BigInteger Gcd(BigInteger a, BigInteger b, out BigInteger x, out BigInteger y)
        {
            if (a == 0)
            {
                x = 0;
                y = 1;
                return b;
            }
            BigInteger x1, y1;
            BigInteger d = Gcd(b % a, a, out x1, out y1);
            x = y1 - (b / a) * x1;
            y = x1;
            return d;
        }
    }
}
