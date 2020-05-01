using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;



namespace ds
{
    public class Program
    {
        static void Main(string[] args)
        {

            Beale B = new Beale();
            Permutation P = new Permutation();
            Numerical N = new Numerical();
            Createuser C = new Createuser();
            Deleteuser D = new Deleteuser();
            ExportKey E = new ExportKey();
            ImportKey I = new ImportKey();
            EncryptMessage EM = new EncryptMessage();
            

            try
            {
                if (args.Length <= 1 || args.Length > 4)
                {
                    Console.WriteLine("\n@Argumentet Mungojne / Numri i argumenteve jo i lejuar!");
                    Console.WriteLine("\n@Per ekzekutimin e funksionit Beale shtyp | ds.exe Beale Encrypt <text> | ose | ds.exe Beale Decrypt <text> ku text te decrypt duhet te jete me thonjeza like: 0 1 2 |");
                    Console.WriteLine("\n@Per ekzekutimin e funksionit Permutation shtyp | ds.exe Permutation Encrypt <key><text> | ose | ds.exe Permutation Decrypt <key><text> |");
                    Console.WriteLine("\n@Per ekzekutimin e funksionit Numerical shtyp | ds.exe Numerical Encode <text> | ose | ds.exe Numerical Decode <text> |");
                    Console.WriteLine("\n@Per ekzekutimin e funksionit CreateUser per GenerateRsaKey shtyp | ds.exe create-user <name> |");
                    Console.WriteLine("\n@Per ekzekutimin e funksionit DeleteUser per DeleteRsaKey shtyp | ds.exe delete-user <name> |");
                    Console.WriteLine("\n@Per ekzekutimin e funksionit ExportKey shtyp | ds.exe export-key <public | private> <name> [file] |");
                    Console.WriteLine("\n@Per ekzekutimin e funksionit ImportKey shtyp | ds.exe import-key <name> <path> |");
                    Environment.Exit(1);
                }

                /*---------------Args per Numerical-----------------*/
                if (args[0].Equals("Numerical"))
                {
                    if (args[1].Equals("Encode"))
                    {

                        String text = args[2];
                        if (Regex.IsMatch(text, "^[a-z ]+$"))
                        {
                            string Cipher = N.Encode(text);
                            Console.WriteLine("Encoded text is: " + Cipher);
                        }
                        else
                        {
                            Console.Write("\n@Argumenti i fundit lejohet te permbaje vetem shkronja te vogla sipas alfabetit anglez prej a-z!");

                        }
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
                        {
                            Console.Write("\n@Argumenti i fundit lejohet te permbaje vetem numra 0-9!");
                        }

                    }
                    else
                    {
                        Console.Write("\n@Argumenti eshte jo valid! (Args must be | Encode | or | Decode |)");
                        Environment.Exit(1);
                    }
                }



                /*---------------Args per Permutation-----------------*/
                else if (args[0].Equals("Permutation"))
                {
                    if (args[1].Equals("Encrypt"))
                    {

                        String key = args[2];
                        String text = args[3];
                        if (Regex.IsMatch(key, "^[1-4]+$") && key.Length == 4)
                        {
                            Console.WriteLine();
                            P.Encrypt(key, text);

                        }
                        else if (Regex.IsMatch(key, "^[1-4]+$") && key.Length != 4)
                        {
                            Console.WriteLine("\n@Key is either too long or too short (Make sure its 4 charecters only!");
                        }
                        else
                        {

                            throw new Exception("\n@Keep in mind that the first argument allows only numbers from 1-4!");
                        }
                    }
                    else if (args[1].Equals("Decrypt"))
                    {
                        String key = args[2];
                        String text = args[3];
                        if (Regex.IsMatch(key, "^[1-4]+$") && key.Length == 4)
                        {
                            Console.WriteLine();
                            P.Decrypt(key, text);

                        }
                        else if (Regex.IsMatch(key, "^[1-4]+$") && key.Length != 4)
                        {
                            Console.WriteLine("\n@Key is either too long or too short (Make sure its 4 charecters only!");
                        }
                        else
                        {

                            throw new Exception("\n@Keep in mind that the first argument allows only numbers from 1-4!");
                        }
                    }
                    else
                    {
                        Console.Write("\n@E R R O R  ! Make sure you passed the argument right | Encrypt | or | Decrypt |!");
                        Environment.Exit(1);
                    }

                }

                /*---------------Args per Beale-----------------*/
                else if (args[0].Equals("Beale"))
                {
                    if (args[1].Equals("Encrypt"))
                    {
                        String plainteksti = args[2];
                        if (Regex.IsMatch(plainteksti, "^[a-zA-Z ]+$"))
                        {
                            Console.Write("Encrypted plaintext is: ");
                            B.BealeEncrypt(plainteksti);
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("\n@Argumenti i fundit duhet te permbaje tekst a-z ose A-Z ne varesi nga teksti");
                        }

                    }
                    else if (args[1].Equals("Decrypt"))
                    {
                        String[] ciphertekst = args[2].Split(" ");
                        String Cipher = String.Concat(ciphertekst);
                        if (Regex.IsMatch(Cipher, "^[0-9]+$"))
                        {
                            Console.Write("Decrypted Ciphertext is: ");
                            B.BealeDecrypt(ciphertekst);
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("\n@Argumenti i fundit duhet te permbaje vetem kod");
                        }
                    }
                    else
                    {
                        Console.Write("\n@Argumenti eshte jo valid! (Args must be Encrypt or Decrypt) !");
                        Environment.Exit(1);
                    }
                }
                /*---------------Args per create-user-----------------*/
                else if (args[0].Equals("create-user"))
                {
                    string text = args[1];
                    if (Regex.IsMatch(text, "^[A-Za-z0-9_.]+$"))
                    {
                        //FilePath
                        string privateKeyfilePath = "keys//" + text + ".xml";
                        string publicKeyfilePath = "keys//" + text + ".pub.xml";

                        //Check nese egziston ndonje file me ate emer ne direktorin keys
                        bool privateKeyExist = File.Exists(privateKeyfilePath);
                        bool publicKeyExist = File.Exists(publicKeyfilePath);

                        if (!(privateKeyExist && publicKeyExist))
                        {
                            //Perdorimi i funksionit GenerateRsaKey per te krijuar qelesat privat dhe public me madhesi 1024(sipas deshires)
                            C.GenerateRsaKey(privateKeyfilePath, publicKeyfilePath, 1024);

                            //Trego qe u krijuan qelsat
                            Console.WriteLine("@Eshte krijuar celesi privat " + "'keys//" + args[1] + ".xml'");
                            Console.WriteLine("@Eshte krijuar celesi public " + "'keys//" + args[1] + ".pub.xml'");
                        }
                        else
                        {
                            Console.WriteLine("@File me ate emer egziston ne folderin keys, provo tjeter emer!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("@Lejohen vetem Emra me shkonje te madhe apo te vogel, dhe numrat 0-9 dhe _ dhe .");
                        Environment.Exit(1);
                    }
                }
                /*---------------Args per delete-user-----------------*/
                else if (args[0].Equals("delete-user"))
                {
                    string text = args[1];
                    if (Regex.IsMatch(text, "^[A-Za-z0-9_.]+$"))
                    {
                        //FilePath
                        string privateKeyfilePath = "keys//" + text + ".xml";
                        string publicKeyfilePath = "keys//" + text + ".pub.xml";

                        //Check nese egziston ndonje file me ate emer ne direktorin keys
                        bool privateKeyExist = File.Exists(privateKeyfilePath);
                        bool publicKeyExist = File.Exists(publicKeyfilePath);

                        if ((privateKeyExist && publicKeyExist))
                        {
                            //Perdorimi i funksionit DeleteRsaKey per te fshire qelesat privat dhe public me madhesi 1024(sipas deshires)
                            D.DeleteRsaKey(privateKeyfilePath, publicKeyfilePath, 1024);

                            //Trego qe u fshin qelsat
                            Console.WriteLine("@Eshte larguar celesi privat " + "'keys//" + text + ".xml'");
                            Console.WriteLine("@Eshte larguar celesi publik " + "'keys//" + text + ".pub.xml'");
                        }
                        else if (publicKeyExist)
                        {
                            D.DeleteRsaKey(publicKeyfilePath, 1024);
                            Console.WriteLine("@Eshte larguar celesi publik " + "'keys//" + text + ".pub.xml'");
                        }
                        else
                        {
                            Console.WriteLine("@Celesi '" + args[1] + "' nuk ekziston.");
                            
                        }
                    }
                    else
                    {
                        Console.WriteLine("@Lejohen vetem Emra me shkonje te madhe apo te vogel, dhe numrat 0-9 dhe _ dhe .");
                        Environment.Exit(1);
                    }
                }
                
                /*-----args per export-key--------------*/
                else if (args[0].Equals("export-key"))
                {
                    string userKey = args[2];
                    if (args[1].Equals("public"))
                    {
                        try
                        {
                            if (args.Length == 3)
                            {
                                E.PublicKeyToConsole(userKey);
                                Console.WriteLine();
                            }
                            else if (args.Length == 4)
                            {
                                string exportToPath = args[3];
                                E.PublicKeyToPath(userKey, exportToPath);
                                Console.WriteLine("Celesi publik u ruajt ne fajllin " + exportToPath);
                            }
                        }
                        catch
                        {
                            throw new Exception("@Celesi publik " + args[2].ToString() + " nuk ekziston ose filepath eshte gabim!");
                        }
                    }
                    else if (args[1].Equals("private"))
                    {
                        try
                        {
                            if (args.Length == 3)
                            {
                                E.PrivateKeyToConsole(userKey);
                                Console.WriteLine();
                            }
                            else if (args.Length == 4)
                            {
                                string exportToPath = args[3];
                                E.PrivateKeyToPath(userKey, exportToPath);
                                Console.WriteLine("Celesi privat u ruajt ne fajllin " + exportToPath);
                            }
                        }
                        catch
                        {
                            throw new Exception("@Celesi privat " + args[2].ToString() + " nuk ekziston ose filepathi eshte gabim!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("@Argument is not passed right! Make sure is Public or Private!");
                        Environment.Exit(1);
                    }
                }
                
                /*--------args per import key-----------*/
                else if (args[0].Equals("import-key"))
                {
                    string import = args[1];
                    string frompath = args[2];
                    if (Regex.IsMatch(import, "^[A-Za-z0-9_.]+$"))
                    {
                        try
                        {

                            I.Import(import, frompath);

                        }
                        catch
                        {
                            throw new Exception("@Fajlli i dhene nuk eshte cels valid! Check the path right!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("@Lejohen vetem Emra me shkonje te madhe apo te vogel, dhe numrat 0-9 dhe _ dhe .");
                        Environment.Exit(1);
                    }
                }
                /*----Args per Encryption-----*/
                else if (args[0].Equals("write-message"))
                {
                    string userName = args[1];
                    string message = args[2];
                    EM.Encrypt(userName, message);   
                }
                /*----Argument is wrong------*/
                else
                {
                    Console.WriteLine("@First Argument is not valid! Make sure you passed it right!");
                    Console.WriteLine("@Pass no arguments for DETAILS!");
                    Environment.Exit(1);
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


