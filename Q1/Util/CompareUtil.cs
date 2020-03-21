using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Q1.Util
{
    public class CompareUtil : ICompareUtil
    {
        public Func<string, string, bool> getComparer(Type dataType)
        {
            Func<string, string, bool> func = null;
            
            if(dataType == typeof(DateTime))
            {
                func = dateTimeCompareFunc;
            }
            else if(dataType == typeof(String))
            {
                func = stringCompareFunc;
            }
            else if(dataType == typeof(int))
            {
                func = intCompareFunc;
            }

            return func;
        }

        public bool stringCompareFunc(string a, string b)
        {
            return a.CompareTo(b)<=0;
        }

        public bool intCompareFunc(string a, string b)
        {
            if (!string.IsNullOrEmpty(a) && !string.IsNullOrEmpty(b))
            {
                //Try int
                int i = -1;
                int j = -1;
                if (Int32.TryParse(a, out i) && Int32.TryParse(b, out j))
                {
                    return i <= j;
                }
            }
            return !string.IsNullOrEmpty(a);
        }

        public bool dateTimeCompareFunc(string a, string b)
        {
            if (!string.IsNullOrEmpty(a) && !string.IsNullOrEmpty(b))
            {
                //Try int
                DateTime i = DateTime.Now;
                DateTime j = DateTime.Now;
                if (DateTime.TryParse(a, out i) && DateTime.TryParse(b, out j))
                {
                    return i.CompareTo(j) <= 0;
                }
            }
            return !string.IsNullOrEmpty(a);
        }
    }
}
