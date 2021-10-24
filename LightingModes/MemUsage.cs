using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LedCSharp;
using ArchieB.Util;

namespace ArchieB.LightingModes
{
    class MemUsage
    {
        ComputerInfo ci;
        bool run = false;
        Configuration configuration;

        public MemUsage(Configuration config)
        {
            ci = new ComputerInfo();
            run = true;
            configuration = config;
        }

        public Task Start()
        {
            return Task.Factory.StartNew(() =>
            {
                int ramTotalMb = Convert.ToInt32(ci.TotalPhysicalMemory / 1024 / 1024);
                int ramAvailableMb = 0;
                int ramUsagePercent = 0;

                while (run)
                {
                    ramAvailableMb = Convert.ToInt32(ci.AvailablePhysicalMemory / 1024 / 1024);
                    int ramUsedMb = ramTotalMb - ramAvailableMb;

                    double usage = ramUsedMb / (ramTotalMb * 1.0);
                    ramUsagePercent = Convert.ToInt32(Math.Floor(usage * 100));
                    int usageSinglesNumber = ramUsagePercent % 10;
                    int usageTensNumber = ramUsagePercent / 10;

                    //Console.WriteLine("Usage: {0}", ramUsagePercent);


                    for(int i = 0; i < 10; i++)
                    {
                        if( i <= usageTensNumber) {
                            setKey(configuration.MemKeys[i], configuration.MemColors[i][0], configuration.MemColors[i][1], configuration.MemColors[i][2], 10);
                        }
                        if(i == usageTensNumber+1 && usageSinglesNumber > 0)
                        {
                                setKey(configuration.MemKeys[i], configuration.MemColors[i][0], configuration.MemColors[i][1], configuration.MemColors[i][2], usageSinglesNumber);
                        }
                        if((i > usageTensNumber && usageSinglesNumber == 0) || i > usageTensNumber + 1)
                        {
                            LogitechGSDK.LogiLedRestoreLightingForKey(configuration.MemKeys[i]);
                        }

                    }

                    ////update "used" keys
                    //for (int i = 0; i < 10; i++)
                    //{
                    //    if (i <= usageTensNumber)
                    //    {
                    //        setKey(configuration.MemKeys[i], configuration.MemColors[i][0], configuration.MemColors[i][1], configuration.MemColors[i][2], 10);
                    //    }
                    //    else
                    //    {
                    //        if (i > usageTensNumber)
                    //        {
                    //            if (usageSinglesNumber > 0)
                    //            {
                    //                setKey(configuration.MemKeys[i + 1], configuration.MemColors[i + 1][0], configuration.MemColors[i + 1][1], configuration.MemColors[i + 1][2], usageSinglesNumber);
                    //            }
                    //            else
                    //            {
                                    
                    //            }
                    //        }
                    //    }
                    //}
                    ConsolePrinter.Instance.PrintMemoryUsage(ramTotalMb, ramUsedMb, ramUsagePercent);
                    System.Threading.Thread.Sleep(configuration.MemTime);
                }
            });
        }

        private void setKey(keyboardNames key, int r, int g, int b, int a)
        {
            int redPercent = calcColorAndBrightness(r, a);
            int greenPercent = calcColorAndBrightness(g, a);
            int bluePercent = calcColorAndBrightness(b, a);

            //Console.WriteLine("key: {4}| r:{0} g: {1} b: {2} a: {3}", red, green, blue, a/10, key.ToString());

            LogitechGSDK.LogiLedSetLightingForKeyWithKeyName(key, redPercent, greenPercent, bluePercent);

        }

        private int calcColorAndBrightness(int c, int a)
        {
            double brightness = a / 10d;
            double color = ((c * brightness) / 255d) * 100;

            return Convert.ToInt32(color);
        }

        public void Stop()
        {
            run = false;
        }
    }
}
