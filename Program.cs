using System.Runtime.InteropServices;

namespace matrixflow
{
    internal class Program
    {
        static List<Line> AllLines = new();
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.CursorVisible = false;
            Random rnd = new Random();
            Console.SetWindowSize(200, 60);

            for (int i  = 0; i < 100; i++)
            {
                Line newLine = new Line(i,0);
                AllLines.Add(newLine);
            }

            while (true)
            {
                for (int i = 0; i <AllLines.Count;i++)
                {
                    Line l = AllLines[i];
                    if (!l.hasStarted)
                    {
                        if (!l.CanStart()) continue;
                        l.Start();
                    };
                    if (l.canPrint())
                    {
                        l.PrintNumber();
                    }
                    if (l.canDelete())
                    {
                        l.ClearCell();
                    }

                    if (l.CanDestruct())
                    {
                        AllLines.Remove(l);
                    }
                    if (l.CanRepeat())
                    {
                        Line newLine = new Line(l.headx, 0);
                        AllLines.Add(newLine);
                    }
                }
            }

        }

        

    }
    class Line
    {
        static Random rnd = new Random();
        static long cooldown = 1000000;
        public int headx;
        public int heady;
        public int taily;
        long printTimer;
        long deleteTimer;
        long startTimer;
        bool flag;
        public bool hasStarted;
        public Line(int x , int y)
        {
            this.headx = x;
            this.heady = y;
            this.taily = y;
            this.hasStarted = false;
            this.startTimer = DateTime.Now.Ticks + rnd.Next(100) * Line.cooldown;
            this.flag = true;
        }

        public bool CanStart()
        {
            if(DateTime.Now.Ticks >= this.startTimer)
            {
                this.startTimer = long.MaxValue;
                this.hasStarted = true;
                return true;
            }
            return false;
        }
        public void Start()
        {
            this.printTimer = DateTime.Now.Ticks;
            this.deleteTimer = DateTime.Now.Ticks + Line.cooldown * 4;
        }
        public bool canPrint()
        {
            return this.printTimer <= DateTime.Now.Ticks ;
        }
        public bool canDelete()
        {
            return this.deleteTimer<= DateTime.Now.Ticks;
        }

        public void PrintNumber()
        {
            Console.SetCursorPosition(this.headx, this.heady);
            Console.Write(rnd.Next(2));
            this.printTimer = DateTime.Now.Ticks+Line.cooldown;
            this.heady++;
            if(this.heady == 30)
            {
                this.printTimer = long.MaxValue;
            }
        }
        public void ClearCell()
        {
            Console.SetCursorPosition(this.headx, this.taily);
            Console.Write(" ");
            this.taily++;
            this.deleteTimer = DateTime.Now.Ticks + Line.cooldown * 3;
            if (this.taily == 30)
            {
                this.deleteTimer = long.MaxValue;
            }
        }

        public bool CanDestruct()
        {
            return this.taily == this.heady;
        }
        public bool CanRepeat()
        {
            if(this.flag && this.taily >= 25)
            {
                this.flag = false;
                return true;
            }
            return false;
        }
    }
}
