using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchieB.Util
{
    internal class CpuPrintTemplate
    {
        //Lines of text comprising a infobox
        List<string> infoBox;
        //width of an infobox
        int boxWidth = 18;
        ConsoleColor color = ConsoleColor.White;
        public CpuPrintTemplate(float usage, string instance)
        {
            infoBox = new List<string>();
            
            //Create horizontal line for top and bottom
            string horizon = "";
            string usageStr = Math.Round(usage).ToString();
            for(int i = 0; i < BoxWidth; i++)
            {
                horizon += "-";
            }
            infoBox.Add(horizon);

            //create line with borders and cpuName
            string cpuLine = "| CPU";
            string white = "";
            for (int i = 0; i < (BoxWidth - cpuLine.Length - instance.Length-1); i++)
            {
                white += " ";
            }
            infoBox.Add(cpuLine + instance + white + "|");

            //Create line with bordes to display usage 
            string usageLine = "| Usage (%): " + usageStr;
            white = "";
            for (int i = 0; i < (BoxWidth - usageLine.Length-1); i++)
            {
                white += " ";
            }
            usageLine += white + "|";
            infoBox.Add(usageLine);

            //add bottom border
            infoBox.Add(horizon);

            //Set box-specific color
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
