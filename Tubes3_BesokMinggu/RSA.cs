using System;

namespace Tubes3_BesokMinggu;



public class RSA
{
    // public key
    public static int P, Q;
    public static int N = getN();
    public static int E = getE();
    
    private static int getN()
    {
        Random r = new Random();
        while (true)
        {
            int temp1 = r.Next(); // p
            int temp2 = r.Next(); // q
            if (isPrime(temp1) && isPrime(temp2))
            {
                P = temp1;
                Q = temp2;
                return P * Q;
            }
        }
    }

    private static int getE()
    {
        int tetaN = RSA.phiN();
        for (int i = 2; i < tetaN; i++)
        {
            if (isFactorOf(i, tetaN)) return i;
        }

        return -1;
    }

    private static int phiN()
    {
        return (P - 1) * (Q - 1);
    }
    
    private static bool isFactorOf(int x, int n)
    {
        if (GCD(x, n) == 1) return true;
        return false;
    }

    private static int GCD(int x, int n)
    {
        int temp;
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
    private static int D = getD();


    private static int getD()
    {
        return ((K * phiN()) + 1) / E;
    }
    
    // encrypt
    public static string encoder(string text)
    {
        string res = "";
        foreach (var c in text)
        {
            int temp = encrypt((int) c);
            res += String.Format("%3d", temp);
        }
        return res;
    }

    private static int encrypt(int text)
    {
        int encrypted = 1;
        int e = E;
        while (e > 0)
        {
            encrypted *= text;
            encrypted %= N;
            e--;
        }

        return encrypted;
    }

    // decrypt
    public static string decoder(string text)
    {
        string res = "";
        for (int i = 0; i < text.Length; i += 3)
        {
            int temp = int.Parse(text.Substring(i, 3));
            res += (char) decrypt(temp);
        }
        return res;
    }
    
    private static int decrypt(int text)
    {
        int d = D;
        int decrypted = 1;
        while (d > 0)
        {
            decrypted *= text;
            decrypted %= N;
            d--;
        }

        return decrypted;
    }
}