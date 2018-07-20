using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabicVerbBuilder 
{
    public class ArWord : List<Letter> 
    {

        public ArWord() { } //default C'tor
        //copy c'tor
        public ArWord(ArWord referece) : base(referece) { } //using the base copy constructor
        public ArWord(string input)
        {
            Init(input);
        }
        public void Init(string input) 
        {
            Clear();
            Add(new Letter(input, 0));
            for (int i = 1; i < input.Length; i++)
            {
                //for every consonance we encounter - we had a new "letter" to the structure. 
                if (Letter.isCharCons(input[i])) Add(new Letter(input, i));
            }
        }
        public void addPostfix(string input)
        {
            Exception e = new Exception("ERROR: adding postfix.");

            //special rule! Adding plural to female
            //if (input == "َاتٌ")
            //{
            //    if (this[Count - 1].Cons == 'ة')
            //    {
            //        RemoveAt(Count - 1);
            //        this[Count - 1].Vowel = '0';
            //    }
            //}

            if (input == "") return; //nothing we need to do.

            ArWord postfix = new ArWord(input);
            if(this[Count - 1].Vowel != '0') //there IS a vowel in the last "letter"
            {
                if (postfix[0].Cons == '0') //if there is no consonance in the postfix, adding is not legitimate. 
                    throw e;

                AddRange(postfix); //normal adding of the postfix.
            }
            else //there is no vowel
            {
                if (postfix[0].Cons != '0') // postfix does not begin with a vowel - easy adding
                    AddRange(postfix);

                else //tricky part - I don't have a vowel but postfix begin with.
                {
                    this[Count - 1].Vowel = postfix[0].Vowel;
                    postfix.RemoveAt(0); //remove the one that contained only a vowel.

                    if (postfix.Count == 0) return;

                    //merging identical consonants
                    
                    if (this[Count - 1].Vowel == Letter.sukun && this[Count-1].Cons == postfix[0].Cons)
                    {
                        RemoveAt(Count - 1);
                        postfix[0].IsShada = true;
                    }
                    AddRange(postfix);

                }
            }
        }
              

        public void changeVowel(int i, char vowel)
        {
            if (!(vowel == Letter.fatha || vowel == Letter.dama || vowel == Letter.kasra)) 
                throw new Exception("Invalid Input"); //make sure input is legal

            if (this[i].Vowel == Letter.sukun || this[i].Vowel == '0') return; //if we don't have a vowel we can't change it (according to rules)

            this[i].Vowel = vowel;
            
            if (this[i+1].Vowel == '0') //if next letter is "Mater lectionis", we need to change it
            {
                this[i + 1].Cons = Letter.getMaterLectionis(this[i].Vowel);
            }
        }
        public void changeCons(int i, char cons)
        {
            if (!Letter.isCharCons(cons)) throw new Exception("Invalid Input");

            if(Letter.isAlif(cons) && this[i].Cons != 'ا' && this[i].Cons != 'آ') //find the right Kursi for Hamza
            {
                if (i == 0)//beginning
                {
                    this[i].Cons = Letter.getKursiAlif(this[i].Vowel);
                    return;
                }
                if (i == Count - 1) //end
                {
                    switch(this[i-1].Vowel)
                    {
                        case Letter.fatha:
                        case Letter.dama:
                        case Letter.kasra:
                            this[i].Cons = Letter.getHamzaKursi(this[i-1].Vowel); //determined by the vowel before.
                            break;
                        default:
                            this[i].Cons = 'ء';
                            break;
                    }
                    return;
                }
                //else, if we are in the MIDDLE
                if(this[i-1].Cons == 'ا' && this[i].Vowel == Letter.fatha)
                {
                    this[i].Cons = 'ء';
                    return;
                }
                if (this[i].Vowel == Letter.kasra || this[i - 1].Vowel == Letter.kasra)
                {
                    this[i].Cons = Letter.getHamzaKursi(Letter.kasra);
                    return;
                }
                if (this[i].Vowel == Letter.dama || this[i - 1].Vowel == Letter.dama)
                {
                    this[i].Cons = Letter.getHamzaKursi(Letter.dama);
                    return;
                }
                if (this[i].Vowel == Letter.fatha || this[i - 1].Vowel == Letter.fatha)
                {
                    this[i].Cons = Letter.getHamzaKursi(Letter.fatha);
                    return;
                }
                //if we got here - something went wrong and this is illegal!
                throw Letter.invalid_input;
            }
            this[i].Cons = cons; //consonance is not hamza
        }
        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            for (int i = 0; i < Count; i++ )
            {
                if (this[i].Cons == 'أ' && (i + 1 < Count) && this[i+1].Cons == 'ا') //long fatha vowel in alif - alif mada
                {
                    output.Append("آ");
                    i++; //we want to skip the next one.
                }
                else
                    output.Append(this[i]);
            }
            return output.ToString();
        }

        public string ToLetString() //for testing only!!
        {
            StringBuilder output = new StringBuilder();
            foreach (Letter l in this)
            {
                output.AppendFormat("{0}, ", l);
            }
            return output.ToString();
        }
    }
}
