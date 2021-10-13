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
        int cpuTime;
        int memTime;
        CpuMode cpuMode;

        public List<keyboardNames> CpuKeys { get => cpuKeys; set => cpuKeys = value; }
        public List<keyboardNames> MemKeys { get => memKeys; set => memKeys = value; }
        public List<int[]> MemColors { get => memColors; set => memColors = value; }
        public int CpuTime {  get => cpuTime; set => cpuTime = value; }
        public int MemTime {  get => memTime; set => memTime = value; }
        public CpuMode CpuMode { get => cpuMode; set => cpuMode = value; }

        public Configuration()
        {
            cpuKeys = new List<keyboardNames>();
            memKeys = new List<keyboardNames>();
            memColors = new List<int[]>();
            cpuTime = 300;
            memTime = 1000;
            cpuMode = CpuMode.Keys;
        }
    }
}
