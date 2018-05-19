using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabicVerbBuilder
{
    public enum Time { Past, nonPast, imperative, agentActive, agentPassive, infinitive };

    static class Time_c
    {
        public static string[] names = { "الماضي", "أمضارع", "الأمر", "إسم الفاعل", "إسم المفعول", "المصدر" };
    }
}
