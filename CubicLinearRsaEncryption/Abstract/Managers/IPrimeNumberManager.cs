﻿using System;

namespace CubicLinearRsaEncryption.Abstract.Managers
{
    public interface IPrimeNumberManager
    {
        long GetPrimeNumber(Random random);
    }
}
