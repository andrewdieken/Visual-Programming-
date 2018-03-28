using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Timers;
using System.Net.NetworkInformation;

namespace aDieken_proj04
{
    class Options
    {
        /*
         * Function takes in user specified csv file, parses file and puts file
         * contents into a list 
         */ 
        public List<string[]> parseCSV(string file)
        {
            List<string[]> parsedData = new List<string[]>();
            string[] fields;

            TextFieldParser parser = new TextFieldParser(file);
            
               
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                while (!parser.EndOfData)
                {
                    fields = parser.ReadFields();
                    StripFront(fields);
                    StripBack(fields);

                    parsedData.Add(fields);
                }

                parser.Close();

            return parsedData;
        }

        /*
         * Function uses list made from .parseCSV function and pings the contents 
         * of the list and out puts it to the list resultData
         */
        public static List<string> RunPing(List<string[]> list)
        {
            List<string> resultData = new List<string>();
            bool pingable = false;
            Ping pinger = new Ping();

            try
            {
                foreach (var item in list)
                {
                    PingReply reply = pinger.Send(item.GetValue(0).ToString(), 5000); 
                    
                    resultData.Add("Web address: " + item.GetValue(0).ToString() + "|" + " Status: " + reply.Status + "|" + " Time: " + reply.RoundtripTime.ToString());

                    pingable = reply.Status == IPStatus.Success;
                }
            }
            catch (PingException)
            {
                pingable = false; 
            }

            Console.WriteLine("End ping...");
            return resultData;
            
        }

        /*
         * Function takes the list containing all ping information and writes it to a file named 'Test_Results.csv"
         */
        public void WriteData(List<string> list)
        {
            string file_name = "Results.csv";
            File.Create(file_name).Close();
            string delimiter = ",";
            int length = list.Count;

            using (TextWriter writer = File.CreateText(file_name))
            {
                for (int index = 0; index < length; index++)
                {
                    writer.WriteLine(string.Join(delimiter, list[index]));
                }
            }


        }

        /*
         * Goes through each value in the input csv file and if it contains 'https://' or 'http://' it removes it
         */
        public string[] StripFront(string[] fields)
        {
            string temp_fields = fields[0].ToString();
            if (temp_fields.Contains("https://"))
            {
                temp_fields = temp_fields.Replace("https://", "");
                //Console.WriteLine(temp_fields);
                fields[0] = temp_fields;
            }
            else if (temp_fields.Contains("http://"))
            {
                temp_fields = temp_fields.Replace("http://", "");
                //Console.WriteLine(temp_fields);
                fields[0] = temp_fields;
            }


            return fields; 
        }

        /*
         * Goes through each value in the input csv file and removes '/' at the end if it contains exisits  
         */
        public string[] StripBack(string[] fields)
        {
            string temp_fields = fields[0].ToString();
            if (temp_fields.EndsWith("/"))
            {
                temp_fields = temp_fields.Replace("/", "");
                //Console.WriteLine(temp_fields);
                fields[0] = temp_fields;
            }

            return fields; 
        }


    }//close Options class 




}
