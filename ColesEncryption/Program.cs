using System;
using System.IO;
using System.Collections.Generic;

namespace ColesEncryption
{
    class Program
    {
        // Used for stat to identify if directory has been swapped
        static bool dirSwapped = false;
        // Instances of encrypters and decrypters
        private static Encrypter _encrypter = new Encrypter();
        private static Decrypter _decrypter = new Decrypter();

        /// <summary>
        /// Entry Point
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Initialize();
            Intro();
        }

        /// <summary>
        /// Initializes all variables for encryption and decryption
        /// </summary>
        static void Initialize()
        {
            PushChars();
            PushIDS();
            Console.ForegroundColor = ConsoleColor.Green;
        }

        /// <summary>
        /// Introduces to app and grabs starting input data
        /// </summary>
        static void Intro()
        {
            // users input
            string input = null;
            Console.WriteLine("Welcome to Cole Mangios Encryption and Decryption System version 1.0!" + Environment.NewLine);
            Console.WriteLine("Choose whether to use default directories or your own");
            Console.WriteLine("[1] to use default (recommended) or [2] to use custom");
            input = Convert.ToString(Console.ReadLine());
            if (input == "2")
            {
                GetDirs(input);
            }
            else if (input == "1")
            {
                SetDirsDefault(input);
            }
            if (input != "1" && input != "2")
            {
                Console.WriteLine("Error: Unexpected command.");
                Console.WriteLine("Press enter to continue...");
                Console.ReadKey();
                Console.Clear();
                Intro();
            }
        }

        /// <summary>
        /// Sets directories to base directory data files
        /// </summary>
        /// <param name="input"></param>
        static void SetDirsDefault(string input)
        {
            IDS.inputDir = AppDomain.CurrentDomain.BaseDirectory + @"dataInput.txt";
            IDS.outputDir = AppDomain.CurrentDomain.BaseDirectory + @"dataOutput.txt";
            IDS.cedDir = AppDomain.CurrentDomain.BaseDirectory + @"dataOutput.ced";
            Console.Clear();
            Console.WriteLine("directories set to default!");
            Command(input);
        }

        /// <summary>
        /// Sets directories to users input 
        /// </summary>
        /// <param name="input"></param>
        static void GetDirs(string input)
        {
            try
            {
                Console.Write("Input Dir: ");
                input = Convert.ToString(Console.ReadLine());
                IDS.inputDir = input;
                Console.Write("Output Dir: ");
                input = Convert.ToString(Console.ReadLine());
                IDS.outputDir = input;
                Console.Write("Ced Dir (Optional/Must Be Created First): ");
                input = Convert.ToString(Console.ReadLine());
                IDS.cedDir = input;
                Console.Clear();
                Command(input);
            }
            catch
            {
                Console.WriteLine("An error has occured. Set directories to default...");
                SetDirsDefault(input);
                Command(input);
            }
        }

        /// <summary>
        /// Waits for users command
        /// </summary>
        /// <param name="input"></param>
        static void Command(string input)
        {
            try
            {
                Console.Write("<" + AppDomain.CurrentDomain.BaseDirectory + ">: ");
                input = Convert.ToString(Console.ReadLine());
                Condition(input);
            }
            catch
            {
                Console.WriteLine("An error has occured! Try checking your directories!");
                Console.WriteLine("Press any enter to continue...");
                Console.ReadKey();
                Command(input);
            }
        }

        /// <summary>
        /// Checks if the command exists. If so, do its functionality
        /// </summary>
        /// <param name="input"></param>
        static void Condition(string input)
        {
            #region Conditions
            switch(input)
            {
                case "help":
                    Help();
                    break;
                case "dispIn":
                    DispIn();
                    break;
                case "dispOut":
                    DispOut();
                    break;
                case "dispCED":
                    DispCED();
                    break;
                case "disp -a":
                    Disp_A();
                    break;
                case "encrypt":
                    _encrypter.Encrypt(File.ReadAllText(IDS.inputDir), false, false);
                    break;
                case "decrypt":
                    _decrypter.Decrypt(File.ReadAllText(IDS.inputDir), false, false);
                    break;
                case "encrypt -d":
                    _encrypter.Encrypt(File.ReadAllText(IDS.inputDir), true, false);
                    break;
                case "decrypt -d":
                    _decrypter.Decrypt(File.ReadAllText(IDS.inputDir), true, false);
                    break;
                case "encrypt -q":
                    Encrypt_Q(input);
                    break;
                case "decrypt -q":
                    Decrypt_Q(input);
                    break;
                case "clear":
                    Console.Clear();
                    break;
                case "chgInputDir":
                    ChangeInputDir(input);
                    break;
                case "chgOutputDir":
                    ChangeOutputDir(input);
                    break;
                case "chgCEDDir":
                    ChangeCEDDir(input);
                    break;
                case "swapDir":
                    SwapDir();
                    break;
                case "stat":
                    Status();
                    break;
                case "setDefDir":
                    SetDirsDefault(input);
                    break;
                case "clearIn":
                    ClearIn();
                    break;
                case "clearOut":
                    ClearOut();
                    break;
                case "clearCED":
                    ClearCED();
                    break;
                case "clear -a":
                    Clear_A();
                    break;
                case "convTXTCED":
                    ConvTXTCED();
                    break;
                case "convCEDTXT":
                    ConvCEDTXT();
                    break;
                case "createCED":
                    CreateCED(input);
                    break;
                case "chgColor":
                    ChgColor(input);
                    break;
                case "exit":
                    Exit();
                    break;
            }
            #endregion
            CheckIntervalDone();
            Command(input);
        }

        /// <summary>
        /// For encrypt/decrypt -d. Checks if it has been done already. If so, do it once more.
        /// </summary>
        static void CheckIntervalDone()
        {
            if (IDS.intervalDoneE)
            {
                _encrypter.DoEncryption(Reverse.GetReversed(File.ReadAllText(IDS.outputDir)), true, false);
                IDS.intervalDoneE = false;
            }
            if (IDS.intervalDoneD)
            {
                _decrypter.DoDecryption(File.ReadAllText(IDS.outputDir), true, false);
                IDS.intervalDoneD = false;
            }
        }

        /// <summary>
        /// Exits the environment
        /// </summary>
        static void Exit()
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// Does the quick encryption
        /// </summary>
        static void Encrypt_Q(string input)
        {
            Stream inputStream = Console.OpenStandardInput(8192);
            Console.SetIn(new StreamReader(inputStream));
            Console.WriteLine(Environment.NewLine + "Simply just type/paste in your string to encrypt" + Environment.NewLine);
            input = Convert.ToString(Console.ReadLine());
            _encrypter.Encrypt(input, false, true);
        }

        /// <summary>
        /// Does the quick decryption
        /// </summary>
        static void Decrypt_Q(string input)
        {
            Stream inputStream = Console.OpenStandardInput(8192);
            Console.SetIn(new StreamReader(inputStream));
            Console.WriteLine(Environment.NewLine + "Simply just paste in your encrypt to decrypt" + Environment.NewLine);
            input = Convert.ToString(Console.ReadLine());
            _decrypter.Decrypt(input, false, true);
        }

        /// <summary>
        /// Clears the contents of the input directory
        /// </summary>
        static void ClearIn()
        {
            File.WriteAllText(IDS.inputDir, "");
            Console.WriteLine("cleared input data!");
        }

        /// <summary>
        /// Clears the contents of the output directory
        /// </summary>
        static void ClearOut()
        {
            File.WriteAllText(IDS.outputDir, "");
            Console.WriteLine("cleared output data!");
        }

        /// <summary>
        /// Clears the contents of the CED directory
        /// </summary>
        static void ClearCED()
        {
            File.WriteAllText(IDS.cedDir, "");
            Console.WriteLine("cleared ced data!");
        }

        /// <summary>
        /// Clears the contents all directories
        /// </summary>
        static void Clear_A()
        {
            ClearIn();
            ClearOut();
            ClearCED();
        }

        /// <summary>
        /// Converts text from output to CED file
        /// </summary>
        static void ConvTXTCED()
        {
            DispOut();
            File.WriteAllText(IDS.cedDir, File.ReadAllText(IDS.outputDir));
            Console.WriteLine("Placed output data into CED file");
        }

        /// <summary>
        /// Converts data from ced file to output text
        /// </summary>
        static void ConvCEDTXT()
        {
            DispCED();
            File.WriteAllText(IDS.outputDir, File.ReadAllText(IDS.cedDir));
            Console.WriteLine("Placed ced data into output text");
        }

        /// <summary>
        /// Creates CED File to specified directory
        /// </summary>
        static void CreateCED(string input)
        {
            string dir;
            string name;
            Console.Write(Environment.NewLine + "Directory: ");
            input = Convert.ToString(Console.ReadLine());
            dir = input;
            Console.Write("Name: ");
            input = Convert.ToString(Console.ReadLine());
            name = input;
            IDS.cedDir = dir + @"\" + name + ".ced";
            File.Create(IDS.cedDir);
            Console.WriteLine("Created!");
        }

        /// <summary>
        /// Displays input
        /// </summary>
        static void DispIn()
        {
            Console.WriteLine(Environment.NewLine + File.ReadAllText(IDS.inputDir) + Environment.NewLine);
        }

        /// <summary>
        /// Displays output
        /// </summary>
        static void DispOut()
        {
            Console.WriteLine(Environment.NewLine + File.ReadAllText(IDS.outputDir) + Environment.NewLine);
        }

        /// <summary>
        /// Displays CED Data
        /// </summary>
        static void DispCED()
        {
            Console.WriteLine(Environment.NewLine + File.ReadAllText(IDS.cedDir) + Environment.NewLine);
        }

        /// <summary>
        /// Displays input, output and CED data
        /// </summary>
        static void Disp_A()
        {
            DispIn();
            DispOut();
            DispCED();
        }

        /// <summary>
        /// Change dir to user input
        /// </summary>
        /// <param name="input"></param>
        static void ChangeInputDir(string input)
        {
            Console.Write("Change input directory to: ");
            input = Convert.ToString(Console.ReadLine());
            IDS.inputDir = input;
        }

        /// <summary>
        /// Change dir to user input
        /// </summary>
        /// <param name="input"></param>
        static void ChangeOutputDir(string input)
        {
            Console.Write("Change output directory to: ");
            input = Convert.ToString(Console.ReadLine());
            IDS.outputDir = input;
        }

        /// <summary>
        /// Changes dir to user input
        /// </summary>
        /// <param name="input"></param>
        static void ChangeCEDDir(string input)
        {
            Console.Write("Change CED directory to: ");
            input = Convert.ToString(Console.ReadLine());
            IDS.cedDir = input;
        }

        /// <summary>
        /// Shows status of directories etc.
        /// </summary>
        static void Status()
        {
            Console.WriteLine();
            if(dirSwapped)
            {
                Console.WriteLine("Directory Swapped?: True");
            } else if(!dirSwapped)
            {
                Console.WriteLine("Directory Swapped?: False");
            }
            Console.WriteLine("Input Directory: " + IDS.inputDir);
            Console.WriteLine("Output Directory: " + IDS.outputDir);
            Console.WriteLine("CED Directory: " + IDS.cedDir);
            Console.WriteLine();
        }

        // int that acts like a third party bool (3) arguments
        static int bInt = -1;
        /// <summary>
        /// Swaps input directory to output directory vice versa
        /// </summary>
        static void SwapDir()
        {
            IDS.thrdPrtyDir = IDS.outputDir;
            IDS.outputDir = IDS.inputDir;
            IDS.inputDir = IDS.thrdPrtyDir;
            Console.WriteLine("Swapped");
            bInt++;
            if(bInt == 0)
            {
                dirSwapped = true;
            }
            if(bInt == 1)
            {
                dirSwapped = false;
                bInt = -1;
            }
        }

        /// <summary>
        /// Color booth. Allows users to customize their environment
        /// </summary>
        static void ChgColor(string input)
        {
            int[] options = new int[7];
            Console.WriteLine(Environment.NewLine + "Welcome to Cole's Color Booth!");
            Console.WriteLine("Just type in the number next to your favoured color:");
            #region Options
            Console.WriteLine("White [0]");
            Console.WriteLine("Blue [1]");
            Console.WriteLine("Cyan [2]");
            Console.WriteLine("Green [3]");
            Console.WriteLine("Red [4]");
            Console.WriteLine("Yellow [5]");
            Console.WriteLine("Magenta [6]");
            #endregion
            for (int i = 0; i < options.GetLength(0); i++)
            {
                options[i] = i;
            }
            input = Convert.ToString(Console.ReadLine());
            #region Conditions
            switch(input)
            {
                case "0":
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case "1":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case "2":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case "3":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "4":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "5":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case "6":
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
            }
            #endregion
        }

        /// <summary>
        /// Displays all existing commands
        /// </summary>
        static void Help()
        {
            Console.Write(Environment.NewLine);
            #region Commands
            Console.WriteLine("encrypt: starts encryption to input directory and outputs to output directory");
            Console.WriteLine("decrypt: starts decryption from input directory and outputs to output directory");
            Console.WriteLine("encrypt -d: starts the double encryption (slower but more secure) not recommended as it is very buggy and not neccessary for now");
            Console.WriteLine("decrypt -d: used to decrypt double encryptions. not recommended as it is very buggy and not neccessary for now");
            Console.WriteLine("encrypt -q: quick encrypt. encrypts pasted strings. Only use small string values");
            Console.WriteLine("decrypt -q: quick decrypt. decrypts pasted encrypts. Must be a small encryption");
            Console.WriteLine("chgInputDir: changes the input directory");
            Console.WriteLine("chgOutputDir: changes the output directory");
            Console.WriteLine("chgCEDDir: changes the CED directory");
            Console.WriteLine("swapDir: swaps the input directory to the output directory vice versa");
            Console.WriteLine("stat: displays status of encryption system");
            Console.WriteLine("dispIn: displays input text");
            Console.WriteLine("dispOut: displays output text");
            Console.WriteLine("dispCED: displays CED data");
            Console.WriteLine("disp -a: displays input, output and CED data");
            Console.WriteLine("setDefDir: sets directories to default");
            Console.WriteLine("clearIn: clears all data from input directory file");
            Console.WriteLine("clearOut: clears all data from output directory file");
            Console.WriteLine("clearCED: clears all data from ced directory file");
            Console.WriteLine("clear -a: clears all data from input, output and ced directories");
            Console.WriteLine("convTXTCED: converts output to a CED file (Cole's Encrypted Data File)");
            Console.WriteLine("convCEDTXT: converts CED data to output");
            Console.WriteLine("createCED: creates CED file to directory specified (redundent when using default directories)");
            Console.WriteLine("chgColor: brings you to the color booth");
            Console.WriteLine("clear: clears the console");
            Console.WriteLine("exit: exits the console");
            #endregion
            Console.Write(Environment.NewLine);
        }

        /// <summary>
        /// Pushes characters to the IDS lists
        /// </summary>
        static void PushChars()
        {
            IDS.charsUC = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            IDS.charsLC = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
            IDS.charsNum = "123456789".ToCharArray();
            IDS.charsUnique = "!@#$%^?&*()-=+_`~[]{}/:;., '\u0022".ToCharArray();
        }

        /// <summary>
        /// Push ids for Upper case characters
        /// </summary>
        static void PushIDS_UC()
        {
            for(int i = 0; i < IDS.charsUC.GetLength(0); i++)
            {
                IDS.IDS_UC.Add("$#CM" + (i * i).ToString() + (i*(i+(i*i))*i).ToString() + "~B#" + "$FDG4!@TFGDF");
            }
        }

        /// <summary>
        /// Push ids for Lower case characters
        /// </summary>
        static void PushIDS_LC()
        {
            for (int i = 0; i < IDS.charsLC.GetLength(0); i++)
            {
                IDS.IDS_LC.Add("$#CM" + (i * i).ToString() + (i*(i+(i*i))*i).ToString() + "~G$12B" + "$FGDG4H~^F6!DF#");
            }
        }

        /// <summary>
        /// Push ids for number characters
        /// </summary>
        static void PushIDS_Num()
        {
            for (int i = 0; i < IDS.charsNum.GetLength(0); i++)
            {
                IDS.IDS_Num.Add("$#CM" + (i * i).ToString() + (i*(i+(i*i))*i).ToString() + "~G42$12B" + "$FD!G4H31~^FG7");
            }
        }

        /// <summary>
        /// Push ids for unique characters
        /// </summary>
        static void PushIDS_Unique()
        {
            for (int i = 0; i < IDS.charsUnique.GetLength(0); i++)
            {
                IDS.IDS_Unique.Add("$#CM" + (i * i).ToString() + (i*(i+(i*i))*i).ToString() + "~GF^&~2$12B" + "$*F~$G&J!H1~^FG7^");
            }
        }

        /// <summary>
        /// Pushes all ids 
        /// </summary>
        static void PushIDS()
        {
            PushIDS_UC();
            PushIDS_LC();
            PushIDS_Num();
            PushIDS_Unique();
        }
    }
}
