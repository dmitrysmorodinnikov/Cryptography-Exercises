using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Crypto1
{
    class Program
    {
        static void Main(string[] args)
        {
            var aArr = new List<BigInteger>();
            var mArr = new List<BigInteger>();
            Init(aArr, mArr);

            var mBig = CalcmBig(mArr);
            var mBigList = CalcmBigList(mBig, mArr);
            var nBigList = CalcnBigList(mBigList, mArr);
            var res = Solve(aArr, mBigList, nBigList, mBig);
            BigInteger rm;
            var r = nthRoot(res, 3, out rm).ToString();
            var bytes = new byte[r.Length/2];
            for (int i = 0; i < r.Length; i += 2)
            {
                string hex = r.Substring(i, 2);
                bytes[i/2] = Convert.ToByte(hex);

            }
            var str = ASCIIEncoding.ASCII.GetString(bytes);
            Console.Write(str);
            Console.ReadLine();
        }

        static BigInteger nthRoot(BigInteger value, int root, out BigInteger remainder)
        {
            if (root < 1) throw new Exception("root must be greater than or equal to 1");
            if (value < 0) throw new Exception("value must be a positive integer");

            //special conditions
            if (value == 1)
            {
                remainder = 0;
                return 1;
            }
            if (value == 0)
            {
                remainder = 0;
                return 0;
            }
            if (root == 1)
            {
                remainder = 0;
                return value;
            }

            //set the upper and lower limits
            var upperbound = value;
            var lowerbound = BigInteger.Parse("0");

            while (true)
            {
                var nval = (upperbound + lowerbound) / 2;
                var tstsq = BigInteger.Pow(nval, root);
                if (tstsq > value) upperbound = nval;
                if (tstsq < value)
                {
                    lowerbound = nval;
                }
                if (tstsq == value)
                {
                    lowerbound = nval;
                    break;
                }
                if (lowerbound == upperbound - 1) break;
            }
            remainder = value - BigInteger.Pow(lowerbound, root);
            return lowerbound;
        }

        static void Init(List<BigInteger> aArr, List<BigInteger> mArr)
        {
            var aValues = new List<BigInteger>
            {
                BigInteger.Parse("1338853906351615603845328037909"),
                BigInteger.Parse("742171294423584777417515756208"),
                BigInteger.Parse("1364412949550017047575133658763")
            };
            var mValues = new List<BigInteger>
            {
                BigInteger.Parse("4963703553974661181865803149931"),
                BigInteger.Parse("2112487534646562115950045691561"),
                BigInteger.Parse("3655661175938877172141578380053")
            };
            aArr.AddRange(aValues);
            mArr.AddRange(mValues);
        }

        static BigInteger Solve(List<BigInteger>aList, List<BigInteger>mBigList,List<BigInteger>nBigList,BigInteger mBig)
        {
            BigInteger res = 0;
            for (int i = 0; i < aList.Count; ++i)
            {
                res += (aList[i]*mBigList[i]*nBigList[i]);
            }
            res = res%mBig;
            return res;
        }

        static List<BigInteger> CalcmBigList(BigInteger mBig,IEnumerable<BigInteger> mList)
        {
            return mList.Select(m => mBig/m).ToList();
        }

        static List<BigInteger> CalcnBigList(List<BigInteger> mBigList, IEnumerable<BigInteger> mList )
        {
            return mList.Select((t, i) => Inverse(mBigList[i], t)).ToList();
        }

        static BigInteger CalcmBig(List<BigInteger>pArr)
        {
            BigInteger res = 1;
            pArr.ForEach(delegate(BigInteger p) { res *= p; });
            return res;
        }

        static BigInteger Inverse(BigInteger a, BigInteger p)
        {
            BigInteger x,y;
            Gcd(a, p, out x, out y);
            if (x < 0)
                x += p;
            return x;
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
            BigInteger d = Gcd(b%a, a, out x1, out y1);
            x = y1 - (b/a)*x1;
            y = x1;
            return d;
        }
    }
}
