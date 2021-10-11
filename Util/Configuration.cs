using LedCSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchieB.Util
{
    internal class Configuration
    {
        List<keyboardNames> cpuKeys;
        List<keyboardNames> memKeys;
        List<int[]> memColors;

        public List<keyboardNames> CpuKeys { get => cpuKeys; set => cpuKeys = value; }
        public List<keyboardNames> MemKeys { get => memKeys; set => memKeys = value; }
        public List<int[]> MemColors { get => memColors; set => memColors = value; }

        public Configuration()
        {
            cpuKeys = new List<keyboardNames>();
            memKeys = new List<keyboardNames>();
            MemColors = new List<int[]>();
        }


        public List<keyboardNames> GetCpuKeys()
        {
            return cpuKeys;
        }

        public List<keyboardNames> GetMemKeys()
        {
            return memKeys;
        }

        public List<int[]> GetMemColors()
        {
            return MemColors;
        }
    }
}
