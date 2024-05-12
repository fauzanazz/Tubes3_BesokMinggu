using System;

namespace Tubes3_BesokMinggu
{
    public class RSA
    {
        // Method to generate a prime number with n bits
        private static int GeneratePrimeNumber(int n)
        {
            Random rand = new Random();
            while (true)
            {
                int num = rand.Next((int)Math.Pow(2, n - 1), (int)Math.Pow(2, n));
                if (IsPrime(num))
                {
                    return num;
                }
            }
        }

        // Method to check if a number is prime
        private static bool IsPrime(int num)
        {
            if (num <= 1)
                return false;
            if (num <= 3)
                return true;
            if (num % 2 == 0 || num % 3 == 0)
                return false;
            for (int i = 5; i * i <= num; i += 6)
            {
                if (num % i == 0 || num % (i + 2) == 0)
                    return false;
            }
            return true;
        }

        // Method to calculate the greatest common divisor of two numbers
        private static int GCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        // Method to find the modular inverse of a modulo m
        private static int ModInverse(int a, int m)
        {
            int m0 = m;
            int y = 0, x = 1;

            if (m == 1)
                return 0;

            while (a > 1)
            {
                // q is quotient
                int q = a / m;
                int t = m;

                // m is remainder now, process same as Euclid's algo
                m = a % m;
                a = t;
                t = y;

                // Update x and y
                y = x - q * y;
                x = t;
            }

            // Make x positive
            if (x < 0)
                x += m0;

            return x;
        }

        // Method to generate public and private keys for RSA encryption
        public static ((int, int), (int, int)) GenerateKeyPair(int bitLength = 1024)
        {
            Random rand = new Random();
            int p = GeneratePrimeNumber(bitLength / 2);
            int q = GeneratePrimeNumber(bitLength / 2);
            int n = p * q;
            int phi = (p - 1) * (q - 1);
            int e = 65537; // Common value for e
            int d = ModInverse(e, phi);
            return ((e, n), (d, n));
        }

        // Method to encrypt a message using RSA
        public static int[] Encrypt(string message, (int, int) publicKey)
        {
            int e = publicKey.Item1;
            int n = publicKey.Item2;
            int[] cipherText = new int[message.Length];
            for (int i = 0; i < message.Length; i++)
            {
                cipherText[i] = (int)Math.Pow(message[i], e) % n;
            }
            return cipherText;
        }

        // Method to decrypt a message using RSA
        public static string Decrypt(int[] cipherText, (int, int) privateKey)
        {
            int d = privateKey.Item1;
            int n = privateKey.Item2;
            char[] plainText = new char[cipherText.Length];
            for (int i = 0; i < cipherText.Length; i++)
            {
                plainText[i] = (char)(Math.Pow(cipherText[i], d) % n);
            }
            return new string(plainText);
        }
    }
}
