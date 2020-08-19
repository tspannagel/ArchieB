 using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArchieB.LightingModes;
using LedCSharp;

namespace ArchieB
{
    class Program
    {
        static void Main(string[] args)
        {
            bool keepRunning = true;

            LogitechGSDK.LogiLedInit();

            CpuTime cpuTime = new CpuTime();

            cpuTime.Start();
            while(keepRunning)
            {
                
            }

        }
    }
}
