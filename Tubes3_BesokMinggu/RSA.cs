using System;
using System.Numerics;
using System.Text;

namespace Tubes3_BesokMinggu;



public class RSA
{

    public RSA()
    {
        N = getN();
        E = getE();
        D = getD();
    }
    
    // public key
    public static int P, Q;
    public static long N;
    public static int E;
    
    private static long getN()
    {
        Random r = new Random();
        while (true)
        {
            int temp1 = r.Next(999999); // p
            int temp2 = r.Next(999999); // q
            
            if (isPrime(temp1) && isPrime(temp2))
            {
                P = temp1;
                Q = temp2;
                long temp = (long)P * (long)Q;
                if (temp > 0) return temp; // prevent overflow
            }
        }
    }

    private static int getE()
    {
        long tetaN = RSA.phiN();
        for (int i = 2; i < tetaN; i++)
        {
            if (isFactorOf(i, tetaN)) return i;
        }

        return -1;
    }

    private static long phiN()
    {
        return (long)(P - 1) * (long)(Q - 1);
    }
    
    private static bool isFactorOf(int x, long n)
    {
        if (GCD(x, n) == 1) return true;
        return false;
    }

    private static long GCD(long x, long n)
    {
        long temp;
        while (true)
        {
            temp = n % x;
            if (temp == 0) return x;
            n = x;
            x = temp;
        }
    }

    private static bool isPrime(int n)
    {
        int max = (int) Math.Sqrt(n);
        for (int i = 2; i <= max; i++)
        {
            if (n % i == 0) return false;
        }

        return true;
    }
    
    
    // private key
    private static int K = 2;
    private static long D;


    private static long getD()
    {
        return ((K * phiN()) + 1) / E;
        // long phi = phiN();
        // for (int i = 2; i < phi; i++)
        // {
        //     if ((i * E) % phi == 1) return i;
        // }

        return -1;
    }
    
    // encrypt
    public static string encoder(string text)
    {
        string res = "";
        byte[] bytes = Encoding.UTF8.GetBytes(text);
        
        foreach (var b in bytes)
        {
            res += encrypt((int)b) + ":";
        }
        
        return res;
    }

    private static long encrypt(int text)
    {
        BigInteger encrypted = BigInteger.ModPow(text, E, N);
        return (long) encrypted;
    }

    // decrypt
    public static string decoder(string text)
    {
        string res = "";
        string[] temp = text.Split(":");
        foreach (var t in temp)
        {
            res += (char)decrypt(long.Parse(t));
        }
        return res;
    }
    
    private static int decrypt(long text)
    {
        BigInteger decrypted = BigInteger.ModPow(text, D, N);
        return (int) decrypted;
    }
}