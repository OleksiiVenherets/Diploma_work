﻿using System;
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
            var random = new Random();
            var p = primeNumberManager.GetPrimeNumber(random);
            var q = primeNumberManager.GetPrimeNumber(random);

            var n = this.GetN(p, q);

            var eulerfunction = this.GetEulerFunction(p, q);
            var e = this.GetE(eulerfunction);
            return new RsaElements
            {
                E = e,
                D = this.GetD(e, eulerfunction)
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

        private long GetE(long eulerFunction)
        {
            var e = 2;

            while (e < eulerFunction)
            {
                if (this.GetLaggestCommonDivider(eulerFunction, e) == 1)
                {
                    return e;
                }
                else
                {
                    e++;
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

        private long GetD(long e, long eulerFunction)
        {
            var d = eulerFunction - 1;
            long result = 0;
            while (d > 0)
            {
                if ((e * d) % eulerFunction == 1)
                {
                    result = d;
                    break;
                }
                d--;
            }

            return result;
        }
    }
}
