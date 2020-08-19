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
        bool run = false;
        public CpuTime()
        {
            cpuCounter = new List<PerformanceCounter>();
            perfCounter = new PerformanceCounter("Processor Information", "% Processor Time");
            perfCounterCategory = new PerformanceCounterCategory("Processor Information");
            run = true;
        }

        public Task Start()
        {
            return Task.Factory.StartNew(() => {
                while (run) { 
                string[] cores = perfCounterCategory.GetInstanceNames();
                foreach (string s in cores)
                {
                    try
                    {
                        string instanceName = s.Split(',')[1];
                        cpuCounter.Add(new PerformanceCounter("Processor", "% Processor Time", instanceName));
                    }
                    catch (Exception ex) { }
                }

                var key = keyboardNames.ZERO;
                    foreach (PerformanceCounter pc in cpuCounter)
                    {
                        switch (pc.InstanceName)
                        {
                            case "0": key = keyboardNames.F1; break;
                            case "1": key = keyboardNames.F2; break;
                            case "2": key = keyboardNames.F3; break;
                            case "3": key = keyboardNames.F4; break;
                            case "4": key = keyboardNames.F5; break;
                            case "5": key = keyboardNames.F6; break;
                            case "6": key = keyboardNames.F7; break;
                            case "7": key = keyboardNames.F8; break;
                            case "_Total": key = keyboardNames.ESC; break;
                        }
                        float cpuVal = pc.NextValue();


                        int redPercent = Convert.ToInt32(((2 * (cpuVal / 100) * 255) / 255) * 100) % 100;
                        int greenPercent = Convert.ToInt32(((255 - (cpuVal / 100) * 255) / 255) * 100);

                        //Console.WriteLine("R:" + redPercent + " G:" + greenPercent);
                        LogitechGSDK.LogiLedSetLightingForKeyWithKeyName(key, redPercent, greenPercent, 0);

                        //Console.WriteLine("CPU"+pc.InstanceName + ": " + pc.NextValue() + "%");

                        System.Threading.Thread.Sleep(200);
                    }
                }
            });
           
        }

        public void Stop()
        {
            run = false;
        }
    }
}
