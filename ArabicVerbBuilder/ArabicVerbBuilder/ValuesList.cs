using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabicVerbBuilder
{
    class ValuesList<E> where E : struct
    {
        readonly static string[] names;

        E list_index;

        public ValuesList(E li)
        {
            list_index = li;
        }


        public static implicit operator E(ValuesList<E> p)
        {
            return p.list_index;
        }

        public static implicit operator ValuesList<E>(E e)
        {
            return new ValuesList<E>(e);
        }

        public static ValuesList<E>[] Options
        {
            get
            {
                E[] options = (E[])Enum.GetValues(typeof(E));
                ValuesList<E>[] rv = new ValuesList<E>[options.Length];
                foreach (E option in options)
                    rv[(int)(object)option] = option;
                return rv;
            }
        }

        public override string ToString()
        {
            return names[(int)(object)list_index];
        }
    }
}
