using System;
using System.Collections.Generic;
using System.Text;

namespace TachoHelper.Services.Serializing
{
    public interface ISerializer
    {
        byte[] Serialize(object obj);
        T Deserialize<T>(byte[] data);
        object Deserialize(Type type, byte[] data);
    }
}
