using System;
using CubicLinearRsaEncryption.Abstract.Managers;

namespace CubicLinearRsaEncryption.BusinesLogic.Managers
{
    public class PrimeNumberManager : IPrimeNumberManager
    {
        public long GetPrimeNumber()
        {
            var number = new Random().Next(10, 100);
            if (IsPrime(number))
            {
                return number;
            }
            else
            {
                while (!IsPrime(number))
                {
                    number++;
                }
                return number;
            }
        }

        private bool IsPrime(long number)
        {
            long divider = 2;

            while (divider < Math.Sqrt(number))
            {
                if (number % divider == 0)
                    return false;
                divider++;
            }

            return true;
        }
    }
}
