using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabicVerbBuilder
{
    class Letter
    {
        //the vowels
        public const char kasra = 'ِ';
        public const char fatha = 'َ';
        public const char dama = 'ُ';
        public const char sukun = 'ْ';
        public const char fathaten = 'ً';
        public const char damaten = 'ٌ';
        public const char kastraten = 'ٍ';
        public const char shadda = 'ّ';
        
        public static Exception invalid_input = new Exception("Invalid string input");

        //member fields
        char cons = '0';
        char vowel = '0';
        bool isShada = false;

        //member functions
        public Letter(string input, int startInIndex=0)
        {
            int i = startInIndex; //iterator for running on the input array
            if (isCharVowel(input[i])) //in that case we don't have a consonance. 
            {
                cons = '0'; //this will represent the empty character;
            }
            else if (isCharCons(input[i])) //the first character is a consonance
            {
                cons = input[i];
                i++;
                if (i == input.Length) return;
            }
            else throw invalid_input; //the character is illegal - not arabic.

            if (input[i] == shadda) //if letter is shada.
            {
                isShada = true;
                i++;
                if (i == input.Length) return;
            }
            else
                isShada = false;

            if(isCharVowel(input[i])) vowel = input[i];
            //i++;
        }

        public char Cons
        {
            get { return cons; }
            set { cons = value; }
        }
        public char Vowel
        {
            get { return vowel; }
            set 
            { 
                vowel = value; 

                //change HAMZA position
                if (cons == 'أ' && vowel == Letter.kasra)
                    cons = 'إ';
                if (cons == 'إ' && vowel != Letter.kasra)
                    cons = 'أ';
            }
        }
        public bool IsShada
        {
            get { return isShada; }
            set { isShada = value; }
        }

        public static bool isCharVowel(char t)
        {
            if (t == fatha || t == kasra || t == dama || t == sukun ||
                t == fathaten || t == kastraten || t == damaten || t == shadda)
                return true;
            else
                return false;
        }
        public static bool isCharCons(char t)
        {
            const string consonances = "أإاىئؤآءبتثجحخدذرزسشصضطظعغفقكلمنهوية";
            foreach(char c in consonances)
            {
                if (t == c)
                {
                    return true;
                }
            }
            return false;

        }

        public static bool isAlif(char c)
        {
            foreach (char alif in "أاإءئؤآ")
            {
                if (c == alif) return true;
            }
            return false;
        }

        public static bool isAlshamsiaLetter(char c)
        {
            foreach(char l in "تثدذرزسشصضطظلن")
            {
                if (c == l)
                    return true;
            }
            return false;
        }

        public static char getHamzaKursi(char v)
        {
            switch(v)
            {
                case Letter.kasra:
                    return 'ئ';
                case Letter.dama:
                    return 'ؤ';
                case Letter.fatha:
                    return getKursiAlif(v);
                default:
                    throw invalid_input;
            }
        }
        public static char getKursiAlif(char v)
        {
            switch(v)
            {
                case Letter.kasra:
                    return 'إ';
                case Letter.dama:
                case Letter.fatha:
                    return 'أ';
                default:
                    throw invalid_input; 
            }
        }
        public static char getMaterLectionis(char v)
        {
            switch (v)
            {
                case Letter.fatha:
                    return 'ا';
                case Letter.dama:
                    return 'و';
                case Letter.kasra:
                    return 'ي';
                default:
                    throw invalid_input;
            }
        }
        public static char getVowelFromMaterLectionis(char ml)
        {
            switch (ml)
            {
                case 'ا':
                    return Letter.fatha;
                case 'و':
                    return Letter.dama;
                case 'ي':
                    return Letter.kasra;
                default:
                    throw invalid_input;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}", 
                (cons == '0') ? "" : cons.ToString(), 
                (isShada) ? shadda.ToString() : "", 
                (vowel =='0') ? "" : vowel.ToString());

        }


    }
}
