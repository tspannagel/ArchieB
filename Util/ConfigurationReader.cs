using LedCSharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchieB.Util
{
    internal class ConfigurationReader
    {
        public List<string> cpuKeys;
        public List<string> memKeys;
        public List<int[]> memColors;
        public int cpuTime;
        public int memTime;
        public CpuMode cpuMode;

        public ConfigurationReader()
        {
            cpuKeys = new List<string>();
            memKeys = new List<string>();
            memColors = new List<int[]>();
            cpuTime = 300;
            memTime = 1000;
            cpuMode = CpuMode.keys;
        }

        //instantiate based on json
        public Configuration CreateConfiguration()
        {
            Configuration config = new Configuration();
            config.CpuKeys = ParseTextToKey(cpuKeys);
            config.MemKeys = ParseTextToKey(memKeys);
            config.MemColors = memColors;
            config.CpuTime = cpuTime;
            config.MemTime = memTime;
            config.CpuMode = cpuMode;
            return config;
        }

        private List<keyboardNames> ParseTextToKey(List<string> keyStrings)
        {
            List<keyboardNames> keyNames = new List<keyboardNames>();

            foreach (string keyString in keyStrings)
            {
                keyboardNames key = new keyboardNames();
                Enum.TryParse<keyboardNames>(keyString, out key);
                keyNames.Add(key);
            }
            return keyNames;
        }
    }

}
