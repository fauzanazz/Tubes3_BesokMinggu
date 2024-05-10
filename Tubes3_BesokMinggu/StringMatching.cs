﻿using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace Tubes3_BesokMinggu;

public class StringMatching
{
    public string toBahasaAlay(string text)
    {
        string temp = toBahasaBesarKecil(text);
        temp = toBahasaAngka(temp);
        temp = toBahasaSingkat(temp);
        return temp;
    }
    
    private string toBahasaBesarKecil(string text)
    {
        Random random = new Random();
        string tempResult = "";
        
        foreach (var c in text)
        {
            int n = random.Next(0, 1);
            if (n == 0)
                tempResult += c.ToString().ToUpper();
            else
                tempResult += c.ToString().ToLower();
        }
        return tempResult;
    }
    
    private string toBahasaAngka(string text)
    {
        Random random = new Random();
        string tempResult = "";
        Dictionary<string, string> huruf_angka = new Dictionary<string, string>
        {
            { "a", "4" },
            { "A", "4" },
            { "i", "1" },
            { "I", "1" },
            { "e", "3" },
            { "E", "3" },
            { "o", "0" },
            { "O", "0" },
            { "z", "2" },
            { "Z", "2" },
            { "s", "5" },
            { "S", "5" },
            { "g", "6" },
            { "G", "6" }
        };
        
        foreach (var c in text)
        {
            int n = random.Next(0, 1);
            if (n == 0 && huruf_angka.ContainsKey(c.ToString()))
                tempResult += huruf_angka[c.ToString()];
            else
                tempResult += c.ToString();
                
        }
        return tempResult;
    }
    
    private string toBahasaSingkat(string text)
    {
        Random random = new Random();
        string tempResult = "";
        List<char> huruf_vokal = new List<char> { 'a', 'i', 'u', 'e', 'o' };
        
        foreach (var c in text)
        {
            int n = random.Next(0, 1);
            // jika n == 0 dan c bukan huruf vokal tambahkan c ke tempResult
            if (n == 0 && !huruf_vokal.Contains(c))
                tempResult += c.ToString();
            else if (n == 1)
                tempResult += c.ToString();
        }

        return tempResult;
    }
    
    public string getBahasaAlayPattern(string text)
    {
        string pattern = getBahasaSingkatPattern(text);
        pattern = getBahasaBesarKecilPattern(pattern);
        pattern = getBahasaAngkaPattern(pattern);
        return pattern;
    }
    
    private string getBahasaBesarKecilPattern(string text)
    {
        string pattern = "";
        foreach (var c in text)
        {
            if (c == ' ')
                pattern += "\\s";
            else if (char.IsDigit(c))
                pattern += c.ToString();
            else
                pattern += "[" + c.ToString().ToLower() + c.ToString().ToUpper() + "]";
        }
        return pattern;
    }
    
    private string getBahasaAngkaPattern(string text)
    {
        string pattern = "";
        Dictionary<char, string> hurufAngka = new Dictionary<char, string>
        {
            { '4', "[aA]" },
            { '1', "[iI]" },
            { '3', "[eE]" },
            { '0', "[oO]" },
            { '2', "[zZ]" },
            { '5', "[sS]" },
            { '6', "[gG]" }
        };
        
        foreach (var c in text)
        {
            if (hurufAngka.ContainsKey(c))
                pattern += hurufAngka[c];
            else
                pattern += c.ToString();
        }
        
        return pattern;
    }
    
    private string getBahasaSingkatPattern(string text){
        
        string pattern = "";
        List<char> hurufVokal = new List<char> { 'a', 'i', 'u', 'e', 'o' };

        for (int i = 0; i < text.Length - 1; i++)
        {
            if (!hurufVokal.Contains(text[i]) && !hurufVokal.Contains(text[i + 1]))
                pattern += text[i] + "[aiueoAIUEO]*";
            else 
                pattern += text[i];
        }
        return pattern;
    }
    
    public bool isMatch(string text, string pattern)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(text, pattern);
    }
}