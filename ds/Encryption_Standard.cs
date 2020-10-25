using System;

namespace ds
{
    public interface Encryption_Standard
    {
        String Encrypt(String Text);

        String Decrypt(String Text);
    }
}