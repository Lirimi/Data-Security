using System;
using System.Text;

namespace ds
{
    public interface Encoding_Standard
    {
        String Encode(String text);

        String Decode(String text);
    }
}