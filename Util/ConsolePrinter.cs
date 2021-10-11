using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchieB.Util
{
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

        public void PrintMemoryUsage(int total, int used, int usagePercent)
        {
            Console.SetCursorPosition(0, 2);

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("MEMORY\n" +
              ("Total (GB): " + Math.Round(total * 10 / 1024.0) / 10 + " Used(GB): " + Math.Round(used * 10 / 1024.0) / 10 + " Used(%): " + usagePercent + "     |"));

            var usageColor = Console.ForegroundColor;

            usageColor = DetermineConsoleColor(usagePercent);

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
            }
            if (singles > 3 && singles <= 6)
            {
                Console.Write("▒");
            }
            if (singles > 6 && singles <= 9)
            {
                Console.Write("▓");
            }

            for (int i = tens + 1; i < 10; i++)
            {
                Console.Write(" ");
            }
            Console.ForegroundColor = oldColor;
            Console.Write("|\n");
        }

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

        public void PrintCpuUsage(List<CpuPrintTemplate> templates)
        {
            Console.SetCursorPosition(0, 5);

            for (int i = 0; i < templates.Count; i += 4)
            {   //Horizont
                if (i >= templates.Count)
                {
                    break;
                }
                printTemplateBoxesLine(templates, i, 0);
                //Name
                printTemplateBoxesLine(templates, i, 1);
                //Usage
                printTemplateBoxesLine(templates, i, 2);
                //Horizont
                printTemplateBoxesLine(templates, i, 3);
            } 

        }

        private static void printTemplateBoxesLine(List<CpuPrintTemplate> templates, int i, int line)
        {
            Console.ForegroundColor = templates[i].Color;
            Console.Write(templates[i].GetInfoBox()[line] + " ");
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

