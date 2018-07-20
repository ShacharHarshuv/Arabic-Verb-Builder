using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabicVerbBuilder
{
    public class Verb_Container : List<Verb>
    {
        public Verb_Container(int form, Root root, Mood mood, Pronoun pronoun, Pronouns_Partciple pronoun_participle, bool Passive, char vowel, bool pastVowelKasra, int ncase, bool known)
        {
            int option_index = 0;
            do
            {
                Add(new Verb(form, root, mood, pronoun, pronoun_participle, Passive, vowel, pastVowelKasra, ncase, known, option_index));
                option_index++;
            } while (this[Count - 1].IsThereAnotherOption); //if there is another option after last one.
        }

        public override string ToString() //prints a list of the verb devided by "/"
        {
            StringBuilder output = new StringBuilder();
            for(int i = 0; i<Count-1; i++)
            {
                output.Append(this[i]);
                output.Append(" / ");
            }
            output.Append(this[Count - 1]);
            
            return output.ToString(); 
        }
    }


}
