using CubicLinearRsaEncryption.Abstract.Managers;
using CubicLinearRsaEncryption.Models;

namespace CubicLinearRsaEncryption.BusinesLogic.Managers
{
    public class RsaManager : IRsaManager
    {
        private readonly IPrimeNumberManager primeNumberManager;

        public RsaManager(IPrimeNumberManager primeNumberManager)
        {
            this.primeNumberManager = primeNumberManager;
        }

        public RsaElements GetRsaElements()
        {
            var p = primeNumberManager.GetPrimeNumber();
            var q = primeNumberManager.GetPrimeNumber();

            var n = this.GetN(p, q);

            var eulerfunction = this.GetEulerFunction(p, q);
            var e = this.GetE(eulerfunction);
            return new RsaElements
            {
                E = e,
                D = this.GetD(e, n, eulerfunction)
            };
        }

        private long GetN(long p, long q)
        {
            return p * q;
        }

        private long GetEulerFunction(long p, long q)
        {
            return (p - 1) * (q - 1);
        }

        private long GetE(long number)
        {
            var e = number - 1;

            while (e > 1)
            {
                if (this.GetLaggestCommonDivider(number, e) == 1)
                {
                    return e;
                }
                else
                {
                    e--;
                }

            }
            return 1;
        }

        private long GetLaggestCommonDivider(long a, long b)
        {
            if (a == 0)
            {
                return b;
            }
            else
            {
                while (b != 0)
                {
                    if (a > b)
                    {
                        a = a - b;
                    }
                    else
                    {
                        b = b - a;
                    }
                }
                return a;
            }

        }

        private long GetD(long e, long n, long eulerFunction)
        {
            var d = eulerFunction - 1;
            while (d > 0)
            {
                if ((e * d) % eulerFunction == 1)
                {
                    return d;
                }
                d--;
            }

            return 0;
        }
    }
}
