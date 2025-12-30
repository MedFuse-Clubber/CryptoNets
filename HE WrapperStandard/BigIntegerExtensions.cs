// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Numerics;

namespace HEWrapper
{
    /// <summary>
    /// Extension methods for BigInteger
    /// </summary>
    public static class BigIntegerExtensions
    {
        /// <summary>
        /// Computes the modular multiplicative inverse of a BigInteger value modulo m
        /// using the Extended Euclidean Algorithm.
        /// </summary>
        /// <param name="a">The value to find the inverse of</param>
        /// <param name="m">The modulus</param>
        /// <returns>The modular multiplicative inverse of a modulo m</returns>
        /// <exception cref="ArgumentException">Thrown when the modular inverse does not exist</exception>
        public static BigInteger ModuloInvert(this BigInteger a, BigInteger m)
        {
            if (m <= 0)
                throw new ArgumentException("Modulus must be positive", nameof(m));

            a = a % m;
            if (a < 0)
                a += m;

            BigInteger t = 0;
            BigInteger newT = 1;
            BigInteger r = m;
            BigInteger newR = a;

            while (newR != 0)
            {
                BigInteger quotient = r / newR;
                
                BigInteger temp = t;
                t = newT;
                newT = temp - quotient * newT;

                temp = r;
                r = newR;
                newR = temp - quotient * newR;
            }

            if (r > 1)
                throw new ArgumentException($"The value {a} does not have a modular inverse modulo {m}");

            if (t < 0)
                t += m;

            return t;
        }
    }
}
