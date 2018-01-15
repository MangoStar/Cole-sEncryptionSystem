using System;
using System.IO;
using System.Collections.Generic;

namespace ColesEncryption
{
    class Encrypter
    {
        /// <summary>
        /// Checks for user input before doing the encryption
        /// </summary>
        /// <param name="_string"></param>
        /// <param name="twice"></param>
        /// <param name="quick"></param>
        public void Encrypt(string _string, bool twice, bool quick)
        {
            string input;
            if (!quick)
            {
                Console.WriteLine(_string + Environment.NewLine);
            }
            Console.WriteLine("Would you like to encrypt this? [y] [n]");
            input = Convert.ToString(Console.ReadLine());
            if(input == "y")
            {
                if(!twice)
                {
                    DoEncryption(Reverse.GetReversed(_string), twice, quick);
                }
                if(twice)
                {
                    DoEncryption(Reverse.GetReversed(_string), twice, quick);
                    IDS.intervalDoneE = true;
                }
            }
            if(input == "n")
            {
                Console.WriteLine(Environment.NewLine + "Encryption Declined" + Environment.NewLine);
            }
            if(input != "y" && input != "n")
            {
                Console.WriteLine("error: unexpected command, declining");
                Console.WriteLine(Environment.NewLine + "Encryption Declined" + Environment.NewLine);
            }
        }

        /// <summary>
        /// Converts the string to encrypted data
        /// </summary>
        /// <param name="_string"></param>
        /// <param name="twice"></param>
        /// <param name="quick"></param>
        public void DoEncryption(string _string, bool twice, bool quick)
        {
            if(!quick)
            {
                File.WriteAllText(IDS.outputDir, "");
            }
            for (int i = 0; i < _string.Length; i++)
            {
                // Checks for matching IDS for upper case characters
                for (int x = 0; x < IDS.charsUC.GetLength(0); x++)
                {
                    if(_string[i] == IDS.charsUC[x])
                    {
                        Console.Write("%");
                        Console.Write(IDS.IDS_UC[x]);
                        if (!quick)
                        {
                            File.AppendAllText(IDS.outputDir, "%");
                            File.AppendAllText(IDS.outputDir, IDS.IDS_UC[x]);
                        }
                    }
                }
                // Checks for matching IDS for lower case characters
                for (int x = 0; x < IDS.charsLC.GetLength(0); x++)
                {
                    if (_string[i] == IDS.charsLC[x])
                    {
                        Console.Write("%");
                        Console.Write(IDS.IDS_LC[x]);
                        if (!quick)
                        {
                            File.AppendAllText(IDS.outputDir, "%");
                            File.AppendAllText(IDS.outputDir, IDS.IDS_LC[x]);
                        }
                    }
                }
                // Checks for matching IDS for number characters
                for (int x = 0; x < IDS.charsNum.GetLength(0); x++)
                {
                    if (_string[i] == IDS.charsNum[x])
                    {
                        Console.Write("%");
                        Console.Write(IDS.IDS_Num[x]);
                        if (!quick)
                        {
                            File.AppendAllText(IDS.outputDir, "%");
                            File.AppendAllText(IDS.outputDir, IDS.IDS_Num[x]);
                        }
                    }
                }
                // Checks for matching IDS for unique characters
                for (int x = 0; x < IDS.charsUnique.GetLength(0); x++)
                {
                    if (_string[i] == IDS.charsUnique[x])
                    {
                        Console.Write("%");
                        Console.Write(IDS.IDS_Unique[x]);
                        if (!quick)
                        {
                            File.AppendAllText(IDS.outputDir, "%");
                            File.AppendAllText(IDS.outputDir, IDS.IDS_Unique[x]);
                        }
                    }
                }
            }
            /*
            if(twice)
            {
                Console.WriteLine(Environment.NewLine + "Was doubled");
                File.AppendAllText(IDS.outputDir, Environment.NewLine + "Was doubled");
            }
            if(!twice)
            {
                Console.WriteLine(Environment.NewLine + "Wasn't doubled");
                File.AppendAllText(IDS.outputDir, Environment.NewLine + "Wasn't doubled");
            }
            */
            Console.WriteLine();
            Console.WriteLine(Environment.NewLine + "Encryption Complete!");
        }
    }
}
