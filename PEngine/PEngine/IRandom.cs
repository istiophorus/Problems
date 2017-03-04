using System;

namespace PEngine
{
    public interface IRandom
    {
        Int32 Next();

        Int32 Next(Int32 range);
    }
}
