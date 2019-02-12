using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge_Mln_1
{
    abstract class ConsoleScreen
    {
        protected int index = 0;

        public abstract int MaxIndex { get; }
        public bool IsRunning { get; protected set; } = false;

        public virtual void Draw()
        {
            Console.Clear();
        }

        public virtual bool OnKeyPress(ConsoleKeyInfo keyInfo)
        {
            ConsoleKey key = keyInfo.Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    index--;
                    if (index < 0) index = MaxIndex - 1;
                    return true;
                case ConsoleKey.DownArrow:
                    index++;
                    if (index >= MaxIndex) index = 0;
                    return true;
            }

            return false;
        }

        public void Run()
        {
            IsRunning = true;

            while (IsRunning)
            {
                Draw();
                OnKeyPress(Console.ReadKey());
            }
        }
    }
}
