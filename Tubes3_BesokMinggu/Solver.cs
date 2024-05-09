namespace Tubes3_BesokMinggu;

public static class Solver
{
    public static int SolveKMP(string text, string pattern)
    {
        int number =  Algorithm.KMPSearch(text, pattern);
        if (number == -1)
        {
            return 0;
        }
        return Algorithm.LevenshteinDistance(text, pattern);
    }
    
    public static int SolveBoyerMoore(string text, string pattern)
    {
        int number = Algorithm.BoyerMoore(text, pattern);
        if (number == -1)
        {
            return 0;
        }
        return Algorithm.LevenshteinDistance(text, pattern);
    }
}