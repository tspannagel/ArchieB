﻿using Microsoft.VisualBasic.Devices;
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
        int sleepTimer = 0;
        List<keyboardNames> memKeys;
        List<int[]> colors;

        public MemUsage(int sleep, Configuration config)
        {
            ci = new ComputerInfo();
            sleepTimer = sleep;

            memKeys = config.GetMemKeys();

            colors = config.GetMemColors();
            run = true;
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

                    for (int i = 0; i <= usageTensNumber; i++)
                    {
                        setKey(memKeys[i], colors[i][0], colors[i][1], colors[i][2], 10);
                        if (i == usageTensNumber || i < 10)
                        {
                            setKey(memKeys[i + 1], colors[i + 1][0], colors[i + 1][1], colors[i + 1][2], usageSinglesNumber);
                        }
                    }

                    ConsolePrinter.Instance.PrintMemoryUsage(ramTotalMb, ramUsedMb, ramUsagePercent);
                    System.Threading.Thread.Sleep(sleepTimer);
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
