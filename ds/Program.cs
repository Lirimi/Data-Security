using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using Org.BouncyCastle.Bcpg;

namespace ds
{
    public class Program
    {
        static void Main(string[] args)
        {
            Beale B = new Beale();
            Permutation P = new Permutation();
            Numerical_Encoding N = new Numerical_Encoding();
            
            UserManagement U = new UserManagement(1024);

            ExportKey E = new ExportKey();
            ImportKey I = new ImportKey();
            
            
            EncryptMessage EM = new EncryptMessage();
            DecryptMessage DM = new DecryptMessage();
            Token T = new Token();
            TokenStatus S = new TokenStatus();


            try
            {
                // Details
                if (args.Length <= 1 || args.Length > 5)
                {
                    Console.WriteLine("\nArgumentet Mungojne / Numri i argumenteve jo i lejuar!");
                    Console.WriteLine(
                        "\nPer ekzekutimin e funksionit Beale shtyp | ds Beale Encrypt <text> | ose | ds Beale Decrypt <text> |");
                    Console.WriteLine(
                        "\nPer ekzekutimin e funksionit Permutation shtyp | ds Permutation Encrypt <key><text> | ose | ds Permutation Decrypt <key><text> |");
                    Console.WriteLine(
                        "\nPer ekzekutimin e funksionit Numerical shtyp | ds Numerical Encode <text> | ose | ds Numerical Decode <text> |");
                    Console.WriteLine("\nPer ekzekutimin e funksionit CreateUser shtyp | ds create-user <name> |");
                    Console.WriteLine("\nPer ekzekutimin e funksionit DeleteUser shtyp | ds delete-user <name> |");
                    Console.WriteLine(
                        "\nPer ekzekutimin e funksionit ExportKey shtyp | ds export-key <public | private> <name> [file] |");
                    Console.WriteLine(
                        "\nPer ekzekutimin e funksionit ImportKey shtyp | ds import-key <name> <path> |");
                    Console.WriteLine(
                        "\nPer ekzekutimin e funksionit EncryptMessage shtyp | ds write-message <name> <message> [token | file] [file] |");
                    Console.WriteLine(
                        "\nPer ekzekutimin e funksionit DecryptMessage shtyp | ds read-message <encrypted-message> |");
                    Console.WriteLine("\nPer ekzekutimin e funksionit login shtyp | ds login <name> |");
                    Console.WriteLine("\nPer ekzekutimin e funksionit status shtyp | ds status <token> |");
                    Environment.Exit(1);
                }


                // args[0] which tells the main function, is implemented in switch statement for clarification
                switch (args[0])
                {
                    case "Numerical":
                        if (args[1].Equals("Encode"))
                        {
                            String text = args[2].ToLower();
                            if (Regex.IsMatch(text, "^[a-zA-Z ]+$"))
                            {
                                string Cipher = N.Encode(text);
                                Console.WriteLine("Encoded text is: " + Cipher);
                            }
                            else
                                Console.Write(
                                    "\nArgumenti i fundit lejohet te permbaje vetem shkronja sipas alfabetit anglez prej a-z!");
                        }
                        else if (args[1].Equals("Decode"))
                        {
                            String text = args[2];
                            if (Regex.IsMatch(text, "^[0-9]+"))
                            {
                                string Plain = N.Decode(text);
                                Console.WriteLine("Decoded cipher is: " + Plain);
                            }
                            else
                                Console.Write("\nArgumenti i fundit lejohet te permbaje vetem numra 0-9!");
                        }
                        else
                        {
                            Console.Write("\nArgumenti eshte jo valid! (Args must be | Encode | or | Decode |)");
                            Environment.Exit(1);
                        }

                        break;
                    case "Permutation":
                        if (args[1].Equals("Encrypt"))
                        {
                            String key = args[2];
                            String text = args[3];
                            if (Regex.IsMatch(key, "^[1-4]+$") && key.Length == 4)
                            {
                                Console.WriteLine();
                                P.setKey(key);
                                String Cipher = P.Encrypt(text);
                                Console.WriteLine("Plaintexti i enkriptuar: " + Cipher);
                                Console.WriteLine("Encryption scheme: ");
                                Console.WriteLine(P.EncryptionScheme(text, Cipher));
                            }
                            else if (Regex.IsMatch(key, "^[1-4]+$") && key.Length != 4)
                                Console.WriteLine(
                                    "\nKey is either too long or too short (Make sure its 4 charecters only!");

                            else
                                throw new Exception(
                                    "\nKeep in mind that the first argument allows only numbers from 1-4!");
                        }
                        else if (args[1].Equals("Decrypt"))
                        {
                            String key = args[2];
                            String text = args[3];
                            if (Regex.IsMatch(key, "^[1-4]+$") && key.Length == 4)
                            {
                                Console.WriteLine();
                                P.setKey(key);
                                String eMessage = P.Decrypt(text);
                                Console.Write("Ciphertexti i dekriptuar: " + eMessage + "\n");
                            }
                            else if (Regex.IsMatch(key, "^[1-4]+$") && key.Length != 4)
                                Console.WriteLine(
                                    "\nKey is either too long or too short (Make sure its 4 charecters only!");
                            else
                                throw new Exception(
                                    "\nKeep in mind that the first argument allows only numbers from 1-4!");
                        }
                        else
                        {
                            Console.Write(
                                "\nE R R O R  ! Make sure you passed the argument right | Encrypt | or | Decrypt |!");
                            Environment.Exit(1);
                        }

                        break;
                    case "Beale":
                        if (args[1].Equals("Encrypt"))
                        {
                            String plaintext = args[2];
                            if (Regex.IsMatch(plaintext, "^[a-zA-Z ]+$"))
                            {
                                String Cipher = B.Encrypt(plaintext);
                                Console.WriteLine("Encrypted plaintext is: " + Cipher);
                            }
                            else
                                Console.WriteLine(
                                    "\nArgumenti i fundit duhet te permbaje tekst a-z ose A-Z ne varesi nga teksti");
                        }
                        else if (args[1].Equals("Decrypt"))
                        {
                            String Cipher = args[2];
                            if (Regex.IsMatch(Cipher, "^[0-9 ]+$"))
                            {
                                String Text = B.Decrypt(Cipher);
                                Console.WriteLine("Decrypted Ciphertext is: " + Text);
                            }
                            else
                                Console.WriteLine("\nArgumenti i fundit duhet te permbaje vetem kod");
                        }
                        else
                        {
                            Console.Write("\nArgumenti eshte jo valid! (Args must be Encrypt or Decrypt) !");
                            Environment.Exit(1);
                        }

                        break;
                    case "create-user":
                        string user = args[1];
                        U.User = user;
                        String Password = UserManagement._generatePassword();
                        U.SetPassword(Password);
                        if (Regex.IsMatch(user, "^[A-Za-z0-9_.]+$"))
                        {
                            //Perdorimi i funksionit GenerateRsaKey per te krijuar qelesat privat dhe public me madhesi 1024(sipas deshires)
                            U.CreateUser();
                            //Trego qe u krijuan qelsat
                            Console.WriteLine("Eshte krijuar celesi privat " + "'keys//" + user + ".xml'");
                            Console.WriteLine("Eshte krijuar celesi public " + "'keys//" + user + ".pub.xml'");
                        }
                        else
                        {
                            Console.WriteLine(
                                "Lejohen vetem Emra me shkonje te madhe apo te vogel, dhe numrat 0-9 dhe _ dhe .");
                            Environment.Exit(1);
                        }

                        break;
                    case "delete-user":
                        string username = args[1];
                        U.User = username;
                        if (Regex.IsMatch(username, "^[A-Za-z0-9_.]+$"))
                        {
                            U.DeleteUser();
                            Console.WriteLine("Eshte fshire celesi publik/privat from 'keys//" + U.User);
                        }
                        else
                        {
                            Console.WriteLine(
                                "Lejohen vetem Emra me shkonje te madhe apo te vogel, dhe numrat 0-9 dhe _ dhe .");
                            Environment.Exit(1);
                        }

                        break;
                    case "export-key":
                        E.userKey = args[2];
                        if (args[1].Equals("public"))
                        {
                            try
                            {
                                if (args.Length == 3)
                                {
                                    E.Export(true);
                                    Console.WriteLine();
                                }
                                else if (args.Length == 4)
                                {
                                    string exportToPath = args[3];
                                    E.Export(true, exportToPath);
                                    Console.WriteLine("Celesi publik u ruajt ne fajllin " + exportToPath);
                                }
                            }
                            catch(FileNotFoundException)
                            {
                                throw new Exception("Celesi publik " + E.userKey + " nuk ekziston!");
                            }
                        }
                        else if (args[1].Equals("private"))
                        {
                            try
                            {
                                if (args.Length == 3)
                                {
                                    E.Export(false);
                                    Console.WriteLine();
                                }
                                else if (args.Length == 4)
                                {
                                    string exportToPath = args[3];
                                    E.Export(false, exportToPath);
                                    Console.WriteLine("Celesi privat u ruajt ne fajllin " + exportToPath);
                                }
                            }
                            catch (FileNotFoundException)
                            {
                                throw new Exception("Celesi privat " + E.userKey + " nuk ekziston!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Argument is not passed right! Make sure is Public or Private!");
                            Environment.Exit(1);
                        }

                        break;
                    case "import-key":
                        string import = args[1];
                        string frompath = args[2];
                        var publicKey = true;
                        if (Regex.IsMatch(import, "^[A-Za-z0-9_.]+$"))
                        {
                            
                            try
                            {

                                if (PathManagement.CreatePath(import))
                                {
                                    I.Import(import, frompath, out publicKey);
                                    if (publicKey)
                                        Console.WriteLine("Celsi publik u ruajt ne fajllin keys/" + import +
                                                          ".pub.xml");
                                    else
                                    {

                                        Console.WriteLine("Celsi privat u ruajt ne fajllin keys/" + import + ".xml");
                                        Console.WriteLine("Celsi publik u ruajt ne fajllin keys/" + import +
                                                          ".pub.xml");
                                    }
                                }
                                else
                                {
                                    throw new Exception("Celsi " + import + " ekziston paraprakisht!");
                                }
                            }
                            catch(Exception)
                            {
                                throw new Exception("Fajlli i dhene nuk eshte cels valid! Check the path right!");
                            }
                        }
                        else
                        {
                            Console.WriteLine(
                                "Lejohen vetem Emra me shkonje te madhe apo te vogel, dhe numrat 0-9 dhe _ dhe .");
                            Environment.Exit(1);
                        }

                        break;
                    case "login":
                        try
                        {
                            string uname = args[1];
                            Console.Write("Jep passwordin: ");
                            string password = null;
                            while (true)
                            {
                                var key = Console.ReadKey(true);
                                if (key.Key == ConsoleKey.Enter)
                                    break;
                                password += key.KeyChar;
                            }

                            Console.WriteLine();
                            T.Login(uname, password);
                        }
                        catch (Exception error)
                        {
                            throw new Exception(error.Message);
                        }

                        break;

                    case "status":
                        try
                        {
                            String token = args[1];
                            S.Status(token);
                            S.PasstheValue();
                        }
                        catch
                        {
                            throw new Exception("Gabim: Vlera e dhene nuk eshte token valid.");
                        }

                        break;

                    case "write-message":
                        string userName = args[1];
                        string message = args[2];
                        if (args.Length == 3)
                        {
                            EM.Encrypt(userName, message);
                        }
                        else if (args.Length == 4)
                        {
                            object TokenorPath = args[3];
                            EM.Encrypt(userName, message, TokenorPath.ToString());
                        }
                        else if (args.Length == 5)
                        {
                            object TokenorPath = args[3];
                            string Path = args[4];
                            EM.Encrypt(userName, message, TokenorPath, Path);
                        }

                        break;
                    case "read-message":
                        try
                        {
                            string ciphertext = args[1];
                            DM.Decrypt(ciphertext);
                        }
                        catch (Exception exception)
                        {
                            if (exception is FormatException || exception is IndexOutOfRangeException)
                            {
                                throw new Exception("Mesazhi i dhene nuk paraqet cipher ose path valid! ");
                            }

                            Console.WriteLine(exception.Message);
                        }

                        break;
                    default:
                        Console.WriteLine("First Argument is not valid! Make sure you passed it right!");
                        Console.WriteLine("Pass no arguments for DETAILS!");
                        Environment.Exit(1);
                        break;
                }
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
        }

        /*--------------------------------------------------------------------------*/
    }
}