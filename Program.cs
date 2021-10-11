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
                Console.WindowHeight = 40;
                Console.CursorVisible = false;
                string json = File.ReadAllText(configFilePath);
                ConfigurationReader cr = JsonConvert.DeserializeObject<ConfigurationReader>(json);
                var config = cr.CreateConfiguration();

                bool keepRunning = true;

                LogitechGSDK.LogiLedInit();

                CpuTime cpuTime = new CpuTime(300, config);
                MemUsage memUsage = new MemUsage(1000, config);

                memUsage.Start();
                cpuTime.Start();

                while (keepRunning)
                {
                    Console.ReadKey();
                    return;
                }
            }
        }
    }
}
