using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge_Mln_1
{
    class StreamingContentEditor : ConsoleScreen
    {
        public static readonly string[] BASIC_OPTIONS = {
            "Rename",
            "Change Rating",
            "Change Type",
            "Change Genre",
            "Change Duration"
        };
        public static readonly string[] SERIES_OPTIONS = {
            "Rename",
            "Change Rating",
            "Change Type",
            "Change Genre",
            "Change Episode Durations"
        };

        ProgramUI ui;
        StreamingContent content;

        public override int MaxIndex { get { return (content is SeriesStreamingContent ? SERIES_OPTIONS : BASIC_OPTIONS).Length; } }

        public StreamingContentEditor(StreamingContent content)
        {
            this.content = content;
        }

        public override bool OnKeyPress(ConsoleKeyInfo keyInfo)
        {
            if (base.OnKeyPress(keyInfo)) return true;
            bool series = content is SeriesStreamingContent;

            switch (keyInfo.Key)
            {
                case ConsoleKey.Enter:
                    switch (index)
                    {
                        case 0:
                            Console.Clear();
                            Console.Write($"Rename \"{content.Name}\" to: ");
                            content.Name = Console.ReadLine();
                            break;
                        case 1:
                            Console.Clear();
                            Console.Write($"Change rating of \"{content.Name}\" from {content.Rating} to: ");
                            content.Rating = StreamingContentRepository.GetContentRating(Console.ReadLine());
                            break;
                        case 2:
                            Console.Clear();
                            if (content is SeriesStreamingContent)
                            {
                                SeriesStreamingContent c = (SeriesStreamingContent)content;
                                Console.WriteLine("NotYetImplemented");
                                Console.ReadKey();
                            }
                            else
                            {
                                BasicStreamingContent c = (BasicStreamingContent)content;
                                Console.Write($"Change duration of \"{c.Name}\" from {c.TotalTime.ToString("0.00")}s to: ");
                                c.ChangeDuration(double.Parse(Console.ReadLine()));
                            }
                            break;
                        case 3:
                            Console.Clear();
                            Console.Write($"Change genre of \"{content.Name}\" from {content.Genre} to: ");
                            content.Genre = StreamingContentRepository.GetContentGenre(Console.ReadLine());
                            break;
                        case 4:
                            Console.Clear();
                            Console.WriteLine("Placeholder for NYI Feature");
                            break;
                    }
                    return true;
                case ConsoleKey.S:
                    IsRunning = false;
                    return true;
            }

            return false;
        }

        public override void Draw()
        {
            base.Draw();
            string[] opts = content is BasicStreamingContent ? BASIC_OPTIONS : SERIES_OPTIONS;
            for (int i = 0; i < MaxIndex; i++)
            {
                bool b = i == index;
                string opt = opts[i];
                if (b)
                {
                    ConsoleColor fg = Console.ForegroundColor;
                    ConsoleColor bg = Console.BackgroundColor;
                    Console.ForegroundColor = bg;
                    Console.BackgroundColor = fg;
                    Console.WriteLine(opt);
                    Console.ForegroundColor = fg;
                    Console.BackgroundColor = bg;
                }
                else
                {
                    Console.WriteLine(opt);
                }
            }
        }
    }
}
