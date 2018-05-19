using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabicVerbBuilder
{
    class Root
    {
        static Exception e = new Exception("Root must have 3 consonances!");
        static Exception illegal_character = new Exception("Illegal character was entered");

        char f; //ل الفعل
        char a; //ع الفعل
        char l; //ل الفعل

        public Root(string input)
        {
            if (input.Length != 3) throw e;
            F = input[0];
            A = input[1];
            L = input[2];
        }

        public char F
        {
            set { f = setChar(value); }
            get { return f; }
        }
        public char A
        {
            set { a = setChar(value); }
            get { return a; }
        }
        public char L
        {
            set { l = setChar(value); }
            get { return l; }
        }

        private static char setChar(char c)
        {
            if (!Letter.isCharCons(c)) throw illegal_character;
            
            //we check the options of أ 
            if(Letter.isAlif(c)) return 'أ';
            return c;
        }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}", f, a, l);
        }
    }
}
