using System;
using System.Collections.Generic;
using System.IO;
using ArchieB.LightingModes;
using ArchieB.Util;
using LedCSharp;
using Newtonsoft.Json;

namespace ArchieB
{
    class Program
    {
        static void Main(string[] args)
        {
            string configFilePath = Environment.CurrentDirectory + "\\archieB.conf";
            {
                //Setup console 
                Console.Title = "ArchieB";
                Console.WindowHeight = 40;
                Console.CursorVisible = false;

                //Read config file
                string json = File.ReadAllText(configFilePath);
                Configuration config = new Configuration();
                try
                {
                    ConfigurationReader cr = JsonConvert.DeserializeObject<ConfigurationReader>(json);
                    config = cr.CreateConfiguration();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                //Initialize Logitech SDK and load lighting modes
                LogitechGSDK.LogiLedInit();
                CpuTime cpuTime = new CpuTime(300, config);
                MemUsage memUsage = new MemUsage(1000, config);
                Console.WriteLine("Press any key to exit.");
                memUsage.Start();
                cpuTime.Start();

                //Wait for key input to exit
                bool keepRunning = true;
                while (keepRunning)
                {
                    Console.ReadKey();                    
                    return;
                }
            }
        }
    }
}
