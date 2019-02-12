using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge_Mln_1
{
    class ProgramUI : ConsoleScreen
    {
        public StreamingContentRepository streamingContents = new StreamingContentRepository();
        public override int MaxIndex => streamingContents.NumberOfContents;
        bool hasChangedSinceLastSave = false;

        Dictionary<string,ConsoleKey> keyBinds = new Dictionary<string,ConsoleKey>();
        Dictionary<string,string> keyBindNames = new Dictionary<string,string>();

        public ProgramUI() {
            string[] tokens = {
                "indexmove.up","indexmove.down","selection.remove","selection.edit",
                "program.exit","program.keybinds","program.save","program.load",
                "program.clear","program.new"
            };
            string[] names = {
                "MoveCursorUp","MoveCursorDown","RemoveSelection","EditSelection",
                "ExitProgram","EditKeybinds","Save","Load","Clear","New"
            };
            ConsoleKey[] keys = {
                ConsoleKey.UpArrow,ConsoleKey.DownArrow,ConsoleKey.Delete,ConsoleKey.E,
                ConsoleKey.Escape,ConsoleKey.K,ConsoleKey.S,ConsoleKey.L,ConsoleKey.C,
                ConsoleKey.N
            };
            for (int i = 0; i < 10; i++) {
                keyBinds.Add(tokens[i],keys[i]);
                keyBindNames.Add(tokens[i],names[i]);
            }
        }

        public void CreateAndAddNewContent() {
            Console.Clear();
            Console.Write("What is this content called? ");
            string name = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Rating: ");
            ContentRating rating = StreamingContentRepository.GetContentRating(Console.ReadLine());
            Console.Clear();
            Console.WriteLine("What genre is it? ");
            ContentGenre genre = StreamingContentRepository.GetContentGenre(Console.ReadLine());
            Console.Clear();
            Console.WriteLine("Is this a show (y/N)? ");
            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                int eps;
                double[] times;

                Console.Write("How many episodes are there? ");
                eps = int.Parse(Console.ReadLine());
                times = new double[eps];

                Console.WriteLine("Enter the duration of each episode in seconds (separated by spaces)");
                int n = 0;
                foreach (string s in Console.ReadLine().Split(' '))
                    times[n++] = double.Parse(s);

                SeriesStreamingContent content = new SeriesStreamingContent(name,ContentRating.G,times,ContentType.TVShow,genre);
                streamingContents.AddStreamingContent(content);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("How long is it (in seconds)? ");
                double time = double.Parse(Console.ReadLine());
                BasicStreamingContent content = new BasicStreamingContent(name,ContentType.Movie,rating,time,genre);
                streamingContents.AddStreamingContent(content);
            }
            hasChangedSinceLastSave = true;
        }

        public bool Confirm(string s,bool isYesByDefault=false) {
            Console.Clear();
            Console.Write(s.Replace("%P", isYesByDefault ? "(Y/n)" : "(y/N)" ));
            ConsoleKey key = Console.ReadKey().Key;
            return isYesByDefault ? key != ConsoleKey.N : key == ConsoleKey.Y ;
        }

        public void Load() {
            Console.Clear();
            Console.Write("Load from: ");
            index = 0;
            streamingContents.Load(Console.ReadLine());
            hasChangedSinceLastSave = false;
        }

        public void Save() {
            Console.Clear();
            Console.Write("Save As: ");
            streamingContents.Save(Console.ReadLine());
            hasChangedSinceLastSave = false;
        }

        public void Quit() {
            if (hasChangedSinceLastSave && Confirm("Do you want to save changes before exiting %P? "))
                Save();
            IsRunning = false;
        }

        public override bool OnKeyPress(ConsoleKeyInfo keyInfo)
        {
            if (base.OnKeyPress(keyInfo)) return true;
            ConsoleKey key = keyInfo.Key;

            if (keyBinds.Values.Contains(key))
            {
                if (key == keyBinds["program.new"])
                {
                    CreateAndAddNewContent();
                }
                else if (key == keyBinds["selection.remove"])
                {
                    StreamingContent content = streamingContents[index];
                    if (Confirm($"Are you sure you want to delete \"{content.Name}\" %P? "))
                    {
                        streamingContents.RemoveStreamingContent(content);
                        if (index == MaxIndex && index > 0) index--;
                        hasChangedSinceLastSave = true;
                    }
                }
                else if (key == keyBinds["program.exit"])
                {
                    Quit();
                }
                else if (key == keyBinds["program.save"])
                {
                    Save();
                }
                else if (key == keyBinds["program.load"])
                {
                    Load();
                }
                else if (key == keyBinds["program.clear"])
                {
                    streamingContents.RemoveAllStreamingContent();
                }
                else if (key == keyBinds["selection.edit"])
                {
                    StreamingContentEditor editor = new StreamingContentEditor(streamingContents[index]);
                    editor.Run();
                }
                else if (key == keyBinds["program.keybinds"])
                {
                    KeyBindsMenu menu = new KeyBindsMenu(keyBindNames,keyBinds);
                    menu.Run();
                }
                return true;
            }

            return false;
        }

        public override void Draw()
        {
            base.Draw();
            Console.WriteLine("Name                     Duration  Genre             IsMovie");
            Console.WriteLine("------------------------------------------------------------");
            for (int i = 0; i < MaxIndex; i++)
            {
                bool b = i == index;
                StreamingContent content = streamingContents[i];
                string str = $"{content.Name}";
                while (str.Length < 25) str += " ";
                string str0 = "" + content.TotalTime.ToString("0.00s");
                string str1 = "" + content.Genre;
                string str2 = "" + (content.Type == ContentType.Movie);
                while (str0.Length < 16) str0 += " ";
                while (str1.Length < 12) str1 += " ";
                str += (str0 + str1 + str2);
                if (b)
                {
                    ConsoleColor fg = Console.ForegroundColor;
                    ConsoleColor bg = Console.BackgroundColor;
                    Console.ForegroundColor = bg;
                    Console.BackgroundColor = fg;
                    Console.WriteLine(str);
                    Console.ForegroundColor = fg;
                    Console.BackgroundColor = bg;
                }
                else
                {
                    Console.WriteLine(str);
                }
            }
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine($"[{keyBinds["program.exit"]}] to exit, [{keyBinds["program.keybinds"]}] for keybinds");
        }
    }
}
