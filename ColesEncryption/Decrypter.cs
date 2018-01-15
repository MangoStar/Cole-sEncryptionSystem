using System;
using System.IO;
using System.Collections.Generic;

namespace ColesEncryption
{
    class Decrypter
    {
        /// <summary>
        /// Checks user input before doing the decryption
        /// </summary>
        /// <param name="ecryStr"></param>
        /// <param name="twice"></param>
        /// <param name="quick"></param>
        public void Decrypt(string ecryStr, bool twice, bool quick)
        {
            string input;
            if (!quick)
            {
                Console.WriteLine(ecryStr + Environment.NewLine);
            }
            Console.WriteLine("Would you like to decrypt this? [y] [n]");
            input = Convert.ToString(Console.ReadLine());
            if (input == "y")
            {
                if(!twice)
                {
                    DoDecryption(ecryStr, twice, quick);
                }
                if(twice)
                {
                    DoDecryption(ecryStr, twice, quick);
                    IDS.intervalDoneD = true;
                }

            }
            if (input == "n")
            {
                Console.WriteLine(Environment.NewLine + "Decryption Declined" + Environment.NewLine);
            }
            if (input != "y" && input != "n")
            {
                Console.WriteLine("error: unexpected command, declining");
                Console.WriteLine(Environment.NewLine + "Decryption Declined" + Environment.NewLine);
            }
        }

        /// <summary>
        /// Decrypts the encrypted string
        /// </summary>
        /// <param name="ecryStr"></param>
        /// <param name="twice"></param>
        /// <param name="quick"></param>
        public void DoDecryption(string ecryStr, bool twice, bool quick)
        {
            if(!quick)
            {
                File.WriteAllText(IDS.outputDir, "");
            }
            string[] ecryIDS = ecryStr.Split('%');
            string fnlDcrpt = "";
            for(int i = 0; i < ecryIDS.GetLength(0); i++)
            {
                Console.WriteLine("Item ID[{0}] found", i);
            }
            Console.WriteLine();
            for(int i = 0; i < ecryIDS.GetLength(0); i++)
            {
                // Finds matching IDS with upper case characters
                for (int x = 0; x < IDS.IDS_UC.Count; x++)
                {
                    if (ecryIDS[i] == IDS.IDS_UC[x])
                    {
                        fnlDcrpt += IDS.charsUC[x];
                    }
                }
                // Finds matching IDS with lower case characters
                for (int x = 0; x < IDS.IDS_LC.Count; x++)
                {
                    if (ecryIDS[i] == IDS.IDS_LC[x])
                    {
                        fnlDcrpt += IDS.charsLC[x];
                    }
                }
                // Finds matching IDS with number characters
                for (int x = 0; x < IDS.IDS_Num.Count; x++)
                {
                    if (ecryIDS[i] == IDS.IDS_Num[x])
                    {
                        fnlDcrpt += IDS.charsNum[x];
                    }
                }
                // Finds matching IDS with unique characters
                for (int x = 0; x < IDS.IDS_Unique.Count; x++)
                {
                    if (ecryIDS[i] == IDS.IDS_Unique[x])
                    {
                        fnlDcrpt += IDS.charsUnique[x];
                    }
                }
            }
            Console.WriteLine(Environment.NewLine + "Pre-state:" + Environment.NewLine);
            Console.WriteLine(fnlDcrpt);
            Console.WriteLine(Environment.NewLine + "Final reversed:" + Environment.NewLine);
            Console.WriteLine(Reverse.GetReversed(fnlDcrpt));
            if (!quick)
            {
                File.AppendAllText(IDS.outputDir, Reverse.GetReversed(fnlDcrpt));
            }
            Console.WriteLine();
            Console.WriteLine(Environment.NewLine + "Decryption Complete!");
            Console.WriteLine("Found {0} IDS", ecryIDS.GetLength(0));
        }
    }
}
