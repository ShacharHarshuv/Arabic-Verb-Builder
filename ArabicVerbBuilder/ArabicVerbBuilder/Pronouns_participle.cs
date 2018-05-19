using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabicVerbBuilder
{
    public enum Pronouns_Partciple { ms, fs, mp, fp };

    class Pronouns_Partciple_wrapper : IEnumWrapper
    {
        Pronouns_Partciple pronoun_index;
        readonly static string[] names = { "المفرد", "المفردة", "الجمع", "الجمعات" }; //not sure how to call it...

        public Pronouns_Partciple_wrapper(Pronouns_Partciple pi)
        {
            pronoun_index = pi;
        }

        public static implicit operator Pronouns_Partciple(Pronouns_Partciple_wrapper p)
        {
            if (p == null) return Pronouns_Partciple.ms;
            return p.pronoun_index;
        }

        public static implicit operator Pronouns_Partciple_wrapper(Pronouns_Partciple ep)
        {
            return new Pronouns_Partciple_wrapper(ep);
        }

        public static Pronouns_Partciple_wrapper[] Options
        {
            get
            {
                Pronouns_Partciple[] options = (Pronouns_Partciple[])Enum.GetValues(typeof(Pronouns_Partciple));
                Pronouns_Partciple_wrapper[] rv = new Pronouns_Partciple_wrapper[options.Length];
                foreach (Pronouns_Partciple option in options)
                    rv[(int)option] = option;
                return rv;
            }
        }

        public static Pronouns_Partciple_wrapper[] Options_masdar
        {
            get
            {
                Pronouns_Partciple[] options = {Pronouns_Partciple.ms, Pronouns_Partciple.mp};
                Pronouns_Partciple_wrapper[] rv = new Pronouns_Partciple_wrapper[options.Length];
                for (int i = 0; i < options.Length; i++)
                    rv[i] = (Pronouns_Partciple_wrapper)options[i];
                return rv;
            }
        }

        public override string ToString()
        {
            return names[(int)pronoun_index];
        }

        public IEnumWrapper GroupHeader
        {
            get
            {
                return null;
            }
        }

        public bool IsLike(IEnumWrapper other)
        {
            if (other is Pronouns_Partciple_wrapper)
            {
                return GroupHeader == other.GroupHeader;
            }
            // TODO
            return false;
        }

    }
    
}
