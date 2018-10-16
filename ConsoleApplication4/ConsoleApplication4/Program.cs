using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

abstract class Factory
{
    public abstract Encrypt createEncrypt();
    public abstract Decipher createDecipher();

    public abstract Encrypt createEncrypt(int i);
    public abstract Decipher createDecipher(int i);
}

class CaesarFactory : Factory
{
    public override Encrypt createEncrypt()
    {
        return new CaesarEncrypt();
    }
    public override Decipher createDecipher()
    {
        return new CaesarDecipher();
    }

    public override Encrypt createEncrypt(int i)
    {
        return new CaesarEncrypt(i);
    }
    public override Decipher createDecipher(int i)
    {
        return new CaesarDecipher(i);
    }
}

class TranspositionFactory : Factory
{
    public override Encrypt createEncrypt()
    {
        return new TranspositionEncrypt();
    }
    public override Decipher createDecipher()
    {
        return new TranspositionDecipher();
    }

    public override Encrypt createEncrypt(int i)
    {
        return new TranspositionEncrypt(i);
    }
    public override Decipher createDecipher(int i)
    {
        return new TranspositionDecipher(i);
    }
}


abstract class Encrypt
{
    protected int key { get; set; }

    public abstract string encrypt(string str);
}

abstract class Decipher
{
    protected int key { get; set; }

    public abstract string decipher(string str);
}

class CaesarEncrypt : Encrypt
{
    public CaesarEncrypt()
    {
        key = 1;
    }

    public CaesarEncrypt(int i)
    {
        if (i <= 0 || i>26)
            throw new System.ArgumentException();
        key = i;
    }
    public override string encrypt(string str)
    {
        string strcopy = str;
        int n = strcopy.Length;
        string[] mas = new string[n];
        for (int i = 0; i < n; i++)
        {
            mas[i] = strcopy.Substring(0, 1);
            strcopy = strcopy.Substring(1, strcopy.Length - 1);
        }
        int code;
        for (int i = 0; i < n; i++)
        {
            code = (int)mas[i][0];
            if (code >= 65 && code <= 90)
            {
                code = code + key;
                if (code > 90)
                    code = 64 + code - 90;
            }
            if (code >= 96 && code <= 122)
            {
                code = code + key;
                if (code > 122)
                    code = 96 + code - 122;
            }
            mas[i] = new string((char)code, 1);
        }
        string result = string.Join("", mas);
        return result;
    }


}

class CaesarDecipher : Decipher
{
    public CaesarDecipher()
    {
        key = 1;
    }

    public CaesarDecipher(int i)
    {
        if (i <= 0 || i > 26)
            throw new System.ArgumentException();
        key = i;
    }
    public override string decipher(string str)
    {
        string strcopy = str;
        int n = strcopy.Length;
        string[] mas = new string[n];
        for (int i = 0; i < n; i++)
        {
            mas[i] = strcopy.Substring(0, 1);
            strcopy = strcopy.Substring(1, strcopy.Length - 1);
        }
        int code;
        for (int i = 0; i < n; i++)
        {
            code = (int)mas[i][0];
            if (code >= 65 && code <= 90)
            {
                code = code - key;
                if (code < 65)
                    code = -64 + code + 90;
            }
            if (code >= 96 && code <= 122)
            {
                code = code - key;
                if (code > 122)
                    code = -96 + code + 122;
            }
            mas[i] = new string((char)code, 1);
        }
        string result = string.Join("", mas);
        return result;
    }
}

class TranspositionEncrypt : Encrypt
{
    public TranspositionEncrypt()
    {
        key = 1;
    }

    public TranspositionEncrypt(int i)
    {
        if (i <= 0)
            throw new System.ArgumentException();
        key = i;
    }
    public override string encrypt(string str)
    {
        string strcopy = str;
        if (key > strcopy.Length)
            throw new System.ArgumentException();
        string[] mas = new string[strcopy.Length];
        int n = strcopy.Length;
        for (int i = 0; i < n; i++)
        {
            mas[i] = strcopy.Substring(0, 1);
            strcopy = strcopy.Substring(1, strcopy.Length - 1);
        }
        string temp;
        for (int i = 0; i < n - key; i++)
        {
            temp = mas[i];
            mas[i] = mas[i + key];
            mas[i + key] = temp;
        }
        string result = string.Join("", mas);
        return result;
    }
}

class TranspositionDecipher : Decipher 
{
    public TranspositionDecipher()
    {
        key = 1;
    }

    public TranspositionDecipher(int i)
    {
        if (i <= 0)
            throw new System.ArgumentException();
        key = i;
    }

    public override string decipher(string str)
    {
        string strcopy = str;
        if (key>strcopy.Length)
            throw new System.ArgumentException();
        string[] mas = new string[strcopy.Length];
        int n = strcopy.Length;
        for (int i = 0; i < n; i++)
        {
            mas[i] = strcopy.Substring(0, 1);
            strcopy = strcopy.Substring(1, strcopy.Length - 1);
        }
        string temp;
        for (int i = n-1; i >=key; i--)
        {
            temp = mas[i];
            mas[i] = mas[i - key];
            mas[i - key] = temp;
        }
        string result = string.Join("", mas);
        return result;
    }
}

class Cipher
{
    private Encrypt encrypt;
    private Decipher decipher;
    public Cipher(Factory factory)
    {
        encrypt = factory.createEncrypt();
        decipher = factory.createDecipher();
    }

    public Cipher(Factory factory, int i)
    {
        encrypt = factory.createEncrypt(i);
        decipher = factory.createDecipher(i);
    }

    public string code (string str)
    {
        return encrypt.encrypt(str);
    }

    public string decode (string str)
    {
        return decipher.decipher(str);
    }
}


namespace ConsoleApplication4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter key for Caesar cipher:");
            string s = Console.ReadLine();
            int key1 = Convert.ToInt32(s);

            Console.WriteLine("Enter key for Transposition cipher:");
            s = Console.ReadLine();
            int key2 = Convert.ToInt32(s);

            try
            {
                Cipher caesar = new Cipher(new CaesarFactory(), key1);
                Cipher transposition = new Cipher(new TranspositionFactory(), key2);

                Console.WriteLine("Enter the text");
                string str = Console.ReadLine();
            
                string code_str = caesar.code(str);

                Console.WriteLine("\nCaesar cipher:\n");
                Console.Write("Input string: ");
                Console.WriteLine(str);
                Console.Write("Coded string: ");
                Console.WriteLine(code_str);
                Console.Write("Decoded string: ");
                Console.WriteLine(caesar.decode(code_str));

                code_str = transposition.code(str);
                Console.WriteLine("\nTransposition cipher:\n");
                Console.Write("Input string: ");
                Console.WriteLine(str);
                Console.Write("Coded string: ");
                Console.WriteLine(code_str);
                Console.Write("Decoded string: ");
                Console.WriteLine(transposition.decode(code_str));
            }
            catch(System.ArgumentException err)
            {
                Console.WriteLine("whops!"); 
            }
            

            Console.ReadKey();
        }
    }
}


