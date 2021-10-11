using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchieB.Util
{
    internal class CpuPrintTemplate
    {

        List<string> infoBox;
        int boxWidth = 16;
        ConsoleColor color = ConsoleColor.White;
        public CpuPrintTemplate(float usage, string instance)
        {
            infoBox = new List<string>();
            
            string horizon = "";
            string usageStr = Math.Round(usage).ToString();
            for(int i = 0; i < BoxWidth; i++)
            {
                horizon += "-";
            }
            infoBox.Add(horizon);

            string cpuLine = "| CPU";
            string white = "";
            for (int i = 0; i < (BoxWidth - cpuLine.Length - instance.Length-1); i++)
            {
                white += " ";
            }
            infoBox.Add(cpuLine + instance + white + "|");

            string usageLine = "| Usage (%): " + usageStr;
            white = "";
            for (int i = 0; i < (BoxWidth - usageLine.Length - usageStr.Length); i++)
            {
                white += " ";
            }
            usageLine += white + "|";
            infoBox.Add(usageLine);
            infoBox.Add(horizon);
            Color = ConsolePrinter.DetermineConsoleColor(Convert.ToInt32(Math.Round(usage)));
        }

        public int BoxWidth { get => boxWidth; set => boxWidth = value; }
        public ConsoleColor Color { get => color; set => color = value; }

        public List<string> GetInfoBox()
        {
            return infoBox;
        }
    }
}
