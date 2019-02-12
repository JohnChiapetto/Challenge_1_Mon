using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge_Mln_1
{
    class KeyBindsMenu : ConsoleScreen
    {
        Dictionary<string,string> names;
        public Dictionary<string,ConsoleKey> bindings;

        public override int MaxIndex => names.Count;

        public KeyBindsMenu(Dictionary<string,string> names,Dictionary<string,ConsoleKey> bindings)
        {
            this.names = names;
            this.bindings = bindings;
        }

        public void Edit()
        {
            string key = names.Keys.ToArray()[index];
            Console.Clear();
            Console.WriteLine($"Press the key to bind to this action (\"{names[key]}\"):");
            bindings[key] = Console.ReadKey().Key;
        }

        public override bool OnKeyPress(ConsoleKeyInfo keyInfo)
        {
            if (base.OnKeyPress(keyInfo)) return true;

            switch (keyInfo.Key)
            {
                case ConsoleKey.Enter:
                    Edit();
                    return true;
                case ConsoleKey.Escape:
                    IsRunning = false;
                    return true;
                default:
                    if (keyInfo.Key == bindings["indexmove.up"])
                    {
                        index--;
                        if (index < 0) index = MaxIndex - 1;
                        return true;
                    }
                    else if (keyInfo.Key == bindings["indexmove.down"])
                    {
                        index++;
                        if (index == MaxIndex) index = 0;
                        return true;
                    }
                    return false;
            }
            return false;
        }

        public override void Draw()
        {
            base.Draw();
            int i = 0;
            foreach (string nameKey in names.Keys)
            {
                ConsoleColor fg = Console.ForegroundColor;
                ConsoleColor bg = Console.BackgroundColor;
                string str = names[nameKey];
                while (str.Length < 30) str += ' ';
                str += bindings[nameKey];
                if (i++ == index)
                {
                    Console.ForegroundColor = bg;
                    Console.BackgroundColor = fg;
                }
                Console.WriteLine(str);
                Console.ResetColor();
            }
        }

        public static Dictionary<string,ConsoleKey> Show(Dictionary<string,string> names,Dictionary<string,ConsoleKey> bindings)
        {
            KeyBindsMenu menu = new KeyBindsMenu(names,bindings);
            menu.Run();
            return menu.bindings;
        }
    }
}
