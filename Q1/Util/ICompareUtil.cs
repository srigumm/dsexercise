using System;

namespace Q1.Util
{
    public interface ICompareUtil
    {
        Func<string, string, bool> getComparer(Type dataType);
    }
}