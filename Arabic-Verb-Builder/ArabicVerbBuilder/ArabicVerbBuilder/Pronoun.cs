using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabicVerbBuilder
{
    public enum Pronoun { I, ms_you, fs_you, he, she, d_you, md_they, fd_they, we, mp_you, fp_you, m_they, f_they }; //m/f = male/female, s/p = singular, plural

    public interface IEnumWrapper
    {
        IEnumWrapper GroupHeader { get; }
        bool IsLike(IEnumWrapper other);
    }

    class Pronoun_wrapper : IEnumWrapper
    {
        Pronoun pronoun_index;
        readonly static string[] names = { "أنا", "أنتَ", "أنتِ", "هو", "هي", "أنتما", "هما (ذكر)", "هما (أنثى)", "نحن", "أنتم", "أنتن", "هم", "هن" };

        public Pronoun_wrapper(Pronoun pi)
        {
            pronoun_index = pi;
        }

        public static implicit operator Pronoun(Pronoun_wrapper p)
        {
            if (p == null) return Pronoun.he;
            return p.pronoun_index;
        }

        public static implicit operator Pronoun_wrapper(Pronoun ep)
        {
            return new Pronoun_wrapper(ep);
        }

        public static Pronoun_wrapper[] Options
        {
            get
            {
                Pronoun[] options = (Pronoun[])Enum.GetValues(typeof(Pronoun));
                Pronoun_wrapper[] rv = new Pronoun_wrapper[options.Length];
                foreach (Pronoun option in options)
                    rv[(int)option] = option;
                return rv;
            }
        }
        public static Pronoun_wrapper[] Options_imperative 
        {
            get
            {
                Pronoun[] options = { Pronoun.ms_you, Pronoun.fs_you, Pronoun.d_you, Pronoun.mp_you, Pronoun.fp_you };
                Pronoun_wrapper[] rv = new Pronoun_wrapper[options.Length];
                for (int i = 0; i < options.Length; i++)
                    rv[i] = (Pronoun_wrapper) options[i];
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
                switch ( pronoun_index )
                {
                    case Pronoun.I:
                    case Pronoun.ms_you:
                    case Pronoun.he:
                        return (Pronoun_wrapper)Pronoun.ms_you;
                    case Pronoun.fs_you:
                    case Pronoun.she:
                        return (Pronoun_wrapper)Pronoun.fs_you;
                    case Pronoun.d_you:
                    case Pronoun.md_they:
                    case Pronoun.fd_they:
                        return (Pronoun_wrapper)Pronoun.d_you;
                    case Pronoun.we:
                    case Pronoun.mp_you:
                    case Pronoun.m_they:
                        return (Pronoun_wrapper)Pronoun.mp_you;
                    case Pronoun.fp_you:
                    case Pronoun.f_they:
                        return (Pronoun_wrapper)Pronoun.fp_you;     
                }
                return null;
            }
        }

        public bool IsLike(IEnumWrapper other)
        {
            if ( other is Pronoun_wrapper )
            {
                return GroupHeader.ToString() == other.GroupHeader.ToString();
            }
            // TODO
            return false;
        }
    }
}
