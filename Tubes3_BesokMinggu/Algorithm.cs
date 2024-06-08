using System;

namespace Tubes3_BesokMinggu;

public static class Algorithm
{
    /**
     * <summary>Knuth-Morris-Pratt (KMP) algorithm</summary>
     * <param name="text">the text to search</param>
     * <param name="pattern">the pattern to search</param>
     * <returns>the index of the first occurrence of the pattern in the text</returns>
     */
    public static int KMPSearch(string text, string pattern)
    {
        // Get the length of the text and the pattern
        int n = text.Length;
        int m = pattern.Length;

        // Compute the Border array for the pattern
        int[] border = ComputeBorder(pattern);

        // Initialize indices for the text and the pattern
        int i = 0;
        int j = 0;

        // Iterate over the text
        while (i < n)
        {
            // If the current characters of the pattern and the text match
            if (pattern[j] == text[i])
            {
                // If we've reached the end of the pattern, return the start index of the match in the text
                if (j == m - 1)
                {
                    return i - m + 1;
                }
                // Move to the next characters in the pattern and the text
                i++;
                j++;
            }
            // don't match, have not reached the start of the pattern
            else if (j > 0)
            {
                // Move the pattern index back to the last occurrence of the current character in the pattern
                j = border[j - 1];
            }
            // don't match, have reached the start of the pattern
            else
            {
                // Move to the next character in the text
                i++;
            }
        }

        // If no match is found, return -1
        return -1;
    }
    
    /**
     * <summary>Compute the Longest Prefix Suffix (LPS) array</summary>
     * <param name="pattern">the pattern to search</param>
     * <returns>the LPS array</returns>
     */
    private static int[] ComputeBorder(string pattern)
    {
        // Get the length of the pattern
        int m = pattern.Length;

        // Initialize the border array with the same length as the pattern
        int[] border = new int[m];
    
        // Initialize indices for the pattern
        int j = 0;
        int i = 1;

        // The first element of the border array is always 0
        border[0] = 0;

        // Iterate over the pattern
        while (i < m)
        {
            // If the current characters of the pattern match
            if (pattern[i] == pattern[j])
            {
                // Increment the index of the pattern
                j++;

                // Set the border at the current index to the pattern index
                border[i] = j;

                // Move to the next character in the pattern
                i++;
            }
            // don't match, and we've not reached the start of the pattern
            else if (j > 0)
            {
                // Move the pattern index back to the last occurrence of the current character in the pattern
                j = border[j - 1];
            }
            // don't match and we've reached the start of the pattern
            else
            {
                // Set the border at the current index to 0
                border[i] = 0;

                // Move to the next character in the pattern
                i++;
            }
        }

        // Return the computed border array
        return border;
    }
    
    /**
     * <summary>Boyer-Moore algorithm</summary>
     * <param name="text">the text to search</param>
     * <param name="pattern">the pattern to search</param>
     * <returns>the index of the first occurrence of the pattern in the text</returns>
     */
    public static int BoyerMoore(string text, string pattern)
    {
        // Get the length of the text and the pattern
        int n = text.Length;
        int m = pattern.Length;

        // Build the 'last' array from the pattern
        int[] last = BuildLast(pattern);

        // Initialize indices for the text and the pattern
        int i = m - 1;
        int j = m - 1;

        // If the pattern is longer than the text, no match is possible
        if (i > n - 1)
        {
            return -1;
        }

        // Iterate over the text and the pattern
        do
        {
            // If the current characters of the pattern and the text match
            if (pattern[j] == text[i])
            {
                // If we've reached the start of the pattern, return the start index of the match in the text
                if (j == 0)
                {
                    return i;
                }
                // Move to the previous characters in the pattern and the text
                i--;
                j--;
            }
            // If the current characters of the pattern and the text don't match
            else
            {
                // Skip over the mismatched character in the text and reset the pattern index to the end
                i = i + m - Math.Min(j, 1 + last[text[i]]);
                j = m - 1;
            }
        } while (i <= n - 1);

        // If no match is found, return -1
        return -1;
    }
    
    /**
     * <summary>Build the 'last' array from the pattern</summary>
     * <param name="pattern">the pattern to search</param>
     * <returns>the 'last' array</returns>
     */
    private static int[] BuildLast(string pattern)
    {
        int[] last = new int[256];
        for (int i = 0; i < 256; i++)
        {
            last[i] = -1;
        }
        
        for (int i = 0; i < pattern.Length; i++)
        {
            last[pattern[i]] = i;
        }
        
        return last;
    }
    
    /**
     * <summary>Levenshtein distance algorithm</summary>
     * <param name="s">the first string</param>
     * <param name="t">the second string</param>
     * <returns>the Levenshtein distance between the two strings</returns>
     */
    public static int LevenshteinDistance(string s, string t, int length = 100)
    {
        // Get n char of s and t
        s = s.Substring((s.Length/2)-length/2, length);
        t = t.Substring((t.Length/2)-length/2, length);
        
        // Get the length of the two strings
        int m = t.Length;
        int n = s.Length;

        // Initialize a 2D array for dynamic programming
        int[,] dp = new int[n + 1, m + 1];
    
        // Fill the first row of the array with the index values
        for (int i = 0; i <= n; i++)
        {
            dp[i, 0] = i;
        }
    
        // Fill the first column of the array with the index values
        for (int j = 0; j <= m; j++)
        {
            dp[0, j] = j;
        }
    
        // Iterate over the strings, comparing each character
        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j <= m; j++)
            {
                // If the characters are the same, the cost is 0, otherwise it's 1
                int cost = s[i - 1] == t[j - 1] ? 0 : 1;

                // Calculate the minimum cost of inserting, deleting or substituting a character
                dp[i, j] = Math.Min(dp[i - 1, j] + 1, Math.Min(dp[i, j - 1] + 1, dp[i - 1, j - 1] + cost));
            }
        }
    
        // The Levenshtein distance is the value in the bottom right corner of the array
        return dp[n, m];
    }
}