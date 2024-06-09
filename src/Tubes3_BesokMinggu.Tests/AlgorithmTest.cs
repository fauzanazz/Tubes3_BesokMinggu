using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tubes3_BesokMinggu.Tests;

[TestClass]
[TestSubject(typeof(Algorithm))]
public class AlgorithmTest
{

    [TestMethod]
    public void METHOD()
    {
        /*
         * Test 1, the answer is 70
         * with levenshtein distance the answer is 192
         */
        // Arrange
        string text = "iÿÿÿÿÿÿÿÿÿÿÌÿÿû:Ãÿÿ<Ìÿÿuwûÿkÿý,Pþÿ¿/ÿÿ\u0092»ÿÿÐ\u00be?ÿ&\u00bc¡\u00a4Úª\u0086È/Zíÿÿq\u00bcÿúTý~Rûût`íÿÿÿÿÿÿiÿÿÿÿÿÿÿÿÿöÿÿÿ\"býÿýÁüþÿÊ\u0092þ /ÿ\u009c2ÿúYÿÿ\\ ^ÿÿp fÿÿà!\u009bÿÿÿþÿÿÿÿÿý\u009a <ðÿÕ\u00b2ÿÿQûÿNºÿá\u009aÿÿÿÿÿÿiÿÿÿÿÿÿÿÿÿÿõrÛõÿÿÇ\u00bdÿÿ\u0087åÿ\u008b>ÿÙþþ6Gÿÿ;Úÿÿÿfäÿý";
        string pattern = "`íÿÿÿÿÿÿiÿÿÿ";
        
        // Act
        int kmpSearch = Algorithm.KMPSearch(text, pattern);
        int boyerMoore = Algorithm.BoyerMoore(text, pattern);
        int levenshteinDistance = Algorithm.LevenshteinDistance(text, pattern);
        
        // Assert
        Assert.AreEqual(70, kmpSearch);
        Assert.AreEqual(70, boyerMoore);
        Assert.AreEqual(192, levenshteinDistance);
        
        
        /*
         * Test 2, the answer is not found
         * with levenstein distance the answer is 93
         */
        
        text = "1230sdnioakvhjkmaplgycfxnbkjlknhgiftdrys3qawsretfyuhijnokmnjuh8yg7tf5d645extdfyguhibuob9y8vtc7rx6";
        pattern = "`3456789p[";
        
        kmpSearch = Algorithm.KMPSearch(text, pattern);
        boyerMoore = Algorithm.BoyerMoore(text, pattern);
        levenshteinDistance = Algorithm.LevenshteinDistance(text, pattern);
        
        Assert.AreEqual(-1, kmpSearch);
        Assert.AreEqual(-1, boyerMoore);
        Assert.AreEqual(93, levenshteinDistance);
        
        /*
         * Test 3, the answer is 0
         * with levenstein distance the answer is 0
         */
        
        text = "12qw3esxdr5tgvghyujmkloi8u7y6trfgbhjkiujytredcvbnjkiu87y6t5rf";
        pattern = "12qw3esxdr5tgvghyujmkloi8u7y6trfgbhjkiujytredcvbnjkiu87y6t5rf";
        
        kmpSearch = Algorithm.KMPSearch(text, pattern);
        boyerMoore = Algorithm.BoyerMoore(text, pattern);
        levenshteinDistance = Algorithm.LevenshteinDistance(text, pattern);
        
        Assert.AreEqual(0, kmpSearch);
        Assert.AreEqual(0, boyerMoore);
        Assert.AreEqual(0, levenshteinDistance);
        
        /*
         * Test 4, the answer is -1
         * with levenstein distance the answer is 2
         * This is test if pattern inputted is larger than text
         */
        
        text = "12qw3esxdr5tgvghyujmkloi8u7y6trfgbhjkiujytredcvbnjkiu87y6t5rf";
        pattern = "12qw3esxdr5tgvghyujmkloi8u7y6trfgbhjkiujytredcvbnjkiu87y6t5rf12";
        
        kmpSearch = Algorithm.KMPSearch(text, pattern);
        boyerMoore = Algorithm.BoyerMoore(text, pattern);
        levenshteinDistance = Algorithm.LevenshteinDistance(text, pattern);
        
        Assert.AreEqual(-1, kmpSearch);
        Assert.AreEqual(-1, boyerMoore);
        Assert.AreEqual(2, levenshteinDistance);
        
        /*
         * Test 5, the answer is
         * with levenstein distance the answer is 0
         * This is test if text inputted is very very large
         */
        
        text = "12qw3esxdr5tgvghyujmkloi8u7y6trfgbhjkiujytredcvbnjkiu87y6t5rfqaw3wsxde4dcfr5rfvgt6yhq12qasw2se3d4rdf5fvgt5gy6hbu7hnuh8ni0kkm0-k0mo0j9nin9h8bu8g7vyycuyrddtfyguhiuytrertyuiuytresdfghjkhgfrde56789765r4d55d45g5h8t45fd2fg5h4gf5d12fg5hj8i7u4ygfd5f8tyuj4k512,56l;98l956k2mhgfdsopd[fg]hj[hpgfodsidofgphj[;'/m',l[];'l]]ki'];'k.;].'];\'][p[;p-[[p-lpo-plo-plooit6r56789707968746375869707968576e67869708-079685746353e6475869708uokhiu7uykhmnjiu76ytr5e657869708796857463564578697uijhnbvhgdftrytuyiuolk,jmnhbghfgtr6576879iuokjhgftdre5r465t78yiuokjh3sw3sxe4xec4crv5tbyunbytvcrrtvygbuhv6r5cx4e5crvygbugyv6rc54xwe5crv6tr5c6ecfv7c6dx5sd6cf7c6d45sx4d65cfv765cd6e54sxwd5cfv6tg8v67r5de4sed5rf6vt87rf5ced65c7vgb8v67r5c7rfvgibgv86frc75fvgb8v6f75ce67fv8gb8v67rc5e6d7r6vtgb78tv6r7c6vt8vb77rcv8b8gvt67rc5vgb8v7cr567vgbv867rc57rvgb87tv6rc7vtbygt7r6t7y80uy7t7youijuifytudfygkugkfydtdfykgujkvf4589658496513468651rdtyvt7cr6tvvt6rcvtrfvbr5vtybtvftgbyhgtfrgthyuygt6frrv76rt7vb6b7r56vece54xcecdr5ecrc65esxedcrfvr6d5esx45edcrftvgby8tfv6cd5xs4za4sxdcrftvgbyhnubgvf6cd5sxdc6fv7gb8hn98gb7fv6cd54xcfv";
        pattern = "5ecrc65esxedcrfvr6d5esx45edcrftvgby8tfv6cd5xs4za4sxdcrftvgbyhnubgvf6cd5sxdc6fv7gb8hn98gb7fv6";
        
        kmpSearch = Algorithm.KMPSearch(text, pattern);
        boyerMoore = Algorithm.BoyerMoore(text, pattern);
        levenshteinDistance = Algorithm.LevenshteinDistance(text, pattern);
        
        Assert.AreEqual(960, kmpSearch);
        Assert.AreEqual(960, boyerMoore);
        Assert.AreEqual(968, levenshteinDistance);
    }
}