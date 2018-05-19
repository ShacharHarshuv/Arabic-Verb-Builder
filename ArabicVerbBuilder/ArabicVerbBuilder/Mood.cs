using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabicVerbBuilder
{
    public enum Mood { Past, nonPast, nonPast_Maj, nonPast_Man, imperative, agentActive, agentPassive, infinitive }; //m/f = male/female, s/p = singular, plural

    class Mood_wrapper
    {
        Mood pronoun_index;
        readonly static string[] names = { "الماضي", "المضارع", "المضارع المجزوم", "المضارع المنصوب", "الأمر", "إسم الفاعل", "إسم المفعول", "المصدر"};

        public Mood_wrapper(Mood pi)
        {
            pronoun_index = pi;
        }

        public static implicit operator Mood(Mood_wrapper p)
        {
            return p.pronoun_index;
        }

        public static implicit operator Mood_wrapper(Mood ep)
        {
            return new Mood_wrapper(ep);
        }

        public static Mood_wrapper[] Options
        {
            get
            {
                Mood[] options = (Mood[])Enum.GetValues(typeof(Mood));
                Mood_wrapper[] rv = new Mood_wrapper[options.Length];
                foreach (Mood option in options)
                    rv[(int)option] = option;
                return rv;
            }
        }

        public override string ToString()
        {
            return names[(int)pronoun_index];
        }
    }

   
}
