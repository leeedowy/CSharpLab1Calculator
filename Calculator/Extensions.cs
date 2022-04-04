using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public static class Extensions
    {
        public static string GetDescription(this Operators operator_)
        {
            Type type = operator_.GetType();
            MemberInfo[] memberInfo = type.GetMember(operator_.ToString());

            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return operator_.ToString();
        }

        public static decimal Normalize(this decimal value)
        {
            return value / 1.000000000000000000000000000000000m;
        }
    }
}
