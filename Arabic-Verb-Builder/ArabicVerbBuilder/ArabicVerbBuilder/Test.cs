using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabicVerbBuilder
{
    class Test
    {
        public static string test()
        {
            return AddPostfixTest();
        }

        private static string letterTest()
        {
            Letter l = new Letter("الفعل");
            string shada;
            if (l.IsShada) shada = "yes";
            else shada = "no";
            return "consonance: " + l.Cons + ", vowel: " + l.Vowel + ", shada: " + shada + ", string: " + l.ToString();
        }

        private static string ArWordTest()
        {
            //ArWord postfix = new ArWord("ْتُنَّ");
            //ArWord word  = new ArWord("كَتَب");
            //ArWord merged = word + postfix;
            ArWord word = new ArWord("الفعل");
            return word.ToLetString();
        }
        private static string AddPostfixTest()
        {
            ArWord word = new ArWord("كَتَبَ");
            word.addPostfix("تُ");
            return word.ToLetString();
        }

        private static string stringBuilderTest()
        {
            StringBuilder output = new StringBuilder("hello");
            output.Append(" world");
            return output.ToString();
        }

        private static string rootTest(string input = "كتب")
        {
            Root wrote = new Root(input);
            return " ع = " + wrote.F.ToString() + ", ف = " + wrote.A.ToString() + ", ل = " + wrote.L.ToString();
        }
    }
}
