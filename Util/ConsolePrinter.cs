using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchieB.Util
{
    /// <summary>
    /// Singleton to synchronize Console Output and implement mode specific outputs
    /// </summary>
    internal class ConsolePrinter
    {
        //Singleton as described here https://csharpindepth.com/Articles/Singleton
        private static readonly ConsolePrinter instance = new ConsolePrinter();

        //List<ConsoleArea> consoleAreas;

        static ConsolePrinter() { }
        private ConsolePrinter()
        {
            //consoleAreas = new List<ConsoleArea>();
        }

        public static ConsolePrinter Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Print a info about memory usage including a colored "progressbar" 
        /// </summary>
        /// <param name="total">Total RAM (MB) available</param>
        /// <param name="used">Used RAM (MB)</param>
        /// <param name="usagePercent">Percentage of RAM used</param>
        public void PrintMemoryUsage(int total, int used, int usagePercent)
        {
            //Print memory usage always on the second line
            Console.SetCursorPosition(0, 2);

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("MEMORY\n" +
              ("Total (GB): " + Math.Round(total * 10 / 1024.0) / 10 + " Used(GB): " + Math.Round(used * 10 / 1024.0) / 10 + " Used(%): " + usagePercent + "     |"));

            //set color for progressbar
            var usageColor = Console.ForegroundColor;
            usageColor = DetermineConsoleColor(usagePercent);

            //determine usage in tens and single numbers for progress
            int tens = usagePercent / 10;
            int singles = usagePercent % 10;


            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = usageColor;

            for (int i = 0; i <= tens; i++)
            {
                Console.Write("█");
            }
            if (singles > 0 && singles <= 3)
            {
                Console.Write("░");
                tens++; //Adjust tens for additional character to output one whitespace less
            }
            if (singles > 3 && singles <= 6)
            {
                Console.Write("▒");
                tens++;//Adjust tens for additional character to output one whitespace less
            }
            if (singles > 6 && singles <= 9)
            {
                Console.Write("▓");
                tens++;//Adjust tens for additional character to output one whitespace less
            }

            //Fill progressbar with whitespace
            for (int i = tens; i < 10; i++)
            {
                Console.Write(" ");
            }
            //reset console color
            Console.ForegroundColor = oldColor;
            Console.Write("|\n");
        }

        /// <summary>
        /// Maps Percentage (0-100) to a console color
        /// </summary>
        /// <param name="usagePercent">Percentage Value 0-100</param>
        /// <returns>ConsoleColor according to usage</returns>
        public static ConsoleColor DetermineConsoleColor(int usagePercent)
        {
            var usageColor = Console.ForegroundColor;
            if (usagePercent < 100)
            {
                usageColor = ConsoleColor.Red;
            }
            if (usagePercent < 80)
            {
                usageColor = ConsoleColor.DarkYellow;
            }
            if (usagePercent < 60)
            {
                usageColor = ConsoleColor.Yellow;
            }
            if (usagePercent < 40)
            {
                usageColor = ConsoleColor.DarkGreen;
            }
            if (usagePercent < 20)
            {
                usageColor = ConsoleColor.Green;
            }

            return usageColor;
        }

        /// <summary>
        /// Print multiple boxes line-by-line
        /// </summary>
        /// <param name="templates">list of templates to be printed</param>
        public void PrintCpuUsage(List<CpuPrintTemplate> templates)
        {
            Console.SetCursorPosition(0, 5);

            //print 4 templates next to each other
            for (int i = 0; i < templates.Count; i += 4)
            {   
                if (i >= templates.Count)
                {
                    break;
                }
                //print first line (horizon)
                printTemplateBoxesLine(templates, i, 0);
                //print second line (cpuName)
                printTemplateBoxesLine(templates, i, 1);
                //print third line (usage)
                printTemplateBoxesLine(templates, i, 2);
                //print fourth line (horizon)
                printTemplateBoxesLine(templates, i, 3);
            } 

        }

        /// <summary>
        /// Print template boxes formatted to console
        /// </summary>
        /// <param name="templates">list of cpu templates to be printed</param>
        /// <param name="i">Index of first  template to be printed in the next "row"</param>
        /// <param name="line">"line of item boxes which has to be printed (0-3)</param>
        private static void printTemplateBoxesLine(List<CpuPrintTemplate> templates, int i, int line)
        {
            //Set box-specific color
            Console.ForegroundColor = templates[i].Color;
            Console.Write(templates[i].GetInfoBox()[line] + " ");
            //Check if more boxes are available
            if (i + 1 < templates.Count)
            {
                Console.ForegroundColor = templates[i+1].Color;
                Console.Write(templates[i + 1].GetInfoBox()[line] + " ");
            }
            if (i + 2 < templates.Count)
            {
                Console.ForegroundColor = templates[i+2].Color;
                Console.Write(templates[i + 2].GetInfoBox()[line] + " ");
            }
            if (i + 3 < templates.Count)
            {
                Console.ForegroundColor = templates[i+3].Color;
                Console.Write(templates[i + 3].GetInfoBox()[line] + " ");
            }
            Console.Write("\n");
        }
    }
}

