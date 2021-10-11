using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchieB.Util
{
    internal class ConsoleArea
    {
        int cursorPosX;
        int cursorPosY;
        int lines;
        string name;

        public ConsoleArea(string name, int x, int y)
        {
            this.name = name;
            cursorPosX = x;
            cursorPosY = y;
            lines = 0;
        }

        public int CursorPosX { get => cursorPosX; set => cursorPosX = value; }
        public int CursorPosY { get => cursorPosY; set => cursorPosY = value; }
        public int Lines { get => lines; set => lines = value; }
        public string Name { get => name; set => name = value; }
    }
}
