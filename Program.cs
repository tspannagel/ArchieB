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

                CpuTime cpuTime = new CpuTime(config);

                string output = "Press x to exit. CPU-Cycle(ms) " + config.CpuTime;

                if (config.CpuMode == CpuMode.keys)
                {
                    MemUsage memUsage = new MemUsage(config);
                    memUsage.Start();
                    output += " | RAM-Cycle(ms): " + config.MemTime;
                }

                Console.WriteLine(output);

                cpuTime.Start();

                //Wait for key input to exit
                bool keepRunning = true;
                while (keepRunning)
                {
                    var key = Console.ReadKey();
                    if (key.KeyChar == 'x')
                    {
                        return;
                    }
                }
            }
        }
    }
}
