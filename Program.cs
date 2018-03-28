using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace aDieken_proj04
{
    class Program
    {
        public static List<string[]> csv_list;
        public static List<string> resultData;

        static void Main(string[] args)
        {
            resultData = new List<string>();
            string csv_file;
            Options new_option = new Options();
            bool correct_file = false;

            //check if file user entered exists
            while (!correct_file)
            {
                Console.WriteLine("Enter the name of the CSV file you wish to use: ");
                csv_file = Console.ReadLine();
                if (!File.Exists(csv_file))
                {
                    Console.WriteLine(csv_file + " does not exist. Please enter a file that exist");
                }
                else
                {
                    csv_list = new_option.parseCSV(csv_file);
                    correct_file = true;
                }
            }

            

            Console.WriteLine("Enter 'start' to start program: ");
            string user_choice = Console.ReadLine();
            if (user_choice.ToLower() == "start")
            {
                Timer timer = new Timer(5000);
                timer.Elapsed += HandleTimer;
                timer.Start();
                Console.WriteLine("Type 'exit' to close program");
                Console.WriteLine("Type 'stop' to pause program");
                Console.WriteLine("Enter a command...");

                while (true)
                {
                    Console.Write("Command: ");
                    string command = Console.ReadLine();

                    Console.WriteLine("Command: ");

                    if (command.ToLower() == "exit")
                    {
                        new_option.WriteData(resultData);
                        System.Environment.Exit(1);
                    }
                    else if (command.ToLower() == "stop")
                    {
                        bool correct_input = false;
                        timer.Stop();
                        while (!correct_input)
                        {
                            Console.WriteLine("Type 'start' to continue program");
                            Console.WriteLine("Command: ");
                            command = Console.ReadLine();
                            if (command.ToLower() == "start")
                            {
                                timer.Start();
                                correct_input = true;
                                continue;
                            }
                            else
                            {
                                Console.WriteLine(command + " is not a vaild input");
                            }
                        }

                    }
                }
                
            }
            else
            {
                Console.WriteLine(user_choice + " is not a vaild input");
            }
            
        }//close Main

        private static void HandleTimer(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("\nRun Ping...");
            string combinedString = string.Join(",", Options.RunPing(csv_list));
            resultData.Add(combinedString);
        }
    }


}
