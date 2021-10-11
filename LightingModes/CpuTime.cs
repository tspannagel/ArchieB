using ArchieB.Util;
using LedCSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchieB.LightingModes
{
    class CpuTime
    {
        List<PerformanceCounter> cpuCounter;
        PerformanceCounter perfCounter;
        PerformanceCounterCategory perfCounterCategory = new PerformanceCounterCategory("Processor Information");
        List<keyboardNames> cpuKeys;

        bool run = false;
        int sleepTimer = 0;
        public CpuTime(int sleep, Configuration config)
        {
            cpuCounter = new List<PerformanceCounter>();
            perfCounter = new PerformanceCounter("Processor Information", "% Processor Time");
            perfCounterCategory = new PerformanceCounterCategory("Processor Information");
            cpuKeys = config.GetCpuKeys();
            sleepTimer = sleep;
            run = true;
        }

        public Task Start()
        {
            return Task.Factory.StartNew(() =>
            {

                List<string> cores = new List<string>();
                cores.AddRange(perfCounterCategory.GetInstanceNames());
                cores.Remove("0,_Total"); //dunno?!

                if(cores.Count != cpuKeys.Count)
                {
                    Console.WriteLine("ERROR: Too few keys defined for cpu keys.");
                }

                //order performance counters and add "_Total" add the end
                for (int c = 0; c < cores.Count; c++)
                {
                    var instanceName = c.ToString();
                    if (c == cores.Count - 1)
                    {
                        instanceName = "_Total";
                    }
                    cpuCounter.Add(new PerformanceCounter("Processor", "% Processor Time", instanceName));
                }

                while (run)
                {
                    int keyIterator = 0; //iterator for cpuKeys
                    List<CpuPrintTemplate> cpuTemplates = new List<CpuPrintTemplate>();

                    foreach(PerformanceCounter counter in cpuCounter)
                    {
                        var usage = counter.NextValue();
                        cpuTemplates.Add(new CpuPrintTemplate(usage, counter.InstanceName));
                        var key = cpuKeys[keyIterator];
                        CalculateAndSetColor(key, usage);
                        
                        keyIterator++;
                    }
                    ConsolePrinter.Instance.PrintCpuUsage(cpuTemplates);
                    System.Threading.Thread.Sleep(sleepTimer);
                }
            });

        }

        public void Stop()
        {
            run = false;
        }

        private void CalculateAndSetColor(keyboardNames key, float cpuTime)
        {
            int redPercent = Convert.ToInt32(((2 * (cpuTime / 100) * 255) / 255) * 100) % 100;
            int greenPercent = Convert.ToInt32(((255 - (cpuTime / 100) * 255) / 255) * 100);

            LogitechGSDK.LogiLedSetLightingForKeyWithKeyName(key, redPercent, greenPercent, 0);
        }
    }
}
