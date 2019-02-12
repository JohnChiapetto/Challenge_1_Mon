using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge_Mln_1
{
    public class StreamingContentRepository
    {
        List<StreamingContent> contents = new List<StreamingContent>();

        public int NumberOfContents { get { return contents.Count; } }
        public StreamingContent this[int n] { get { return contents[n]; } }

        public void AddStreamingContent(StreamingContent content) {
            contents.Add(content);
        }
        public void RemoveStreamingContent(StreamingContent content) {
            contents.Remove(content);
        }
        public void RemoveStreamingContent(int n) {
            contents.Remove(contents[n]);
        }

        public static ContentRating GetContentRating(string s) {
            switch (s.ToLower()) {
                case "pg-13":
                    return ContentRating.PG13;
                case "pg":
                    return ContentRating.PG;
                case "g":
                    return ContentRating.G;
                case "r":
                    return ContentRating.R;
                case "m":
                    return ContentRating.M;
            }
            return ContentRating.NR;
        }

        public static ContentGenre GetContentGenre(string s) {
            switch (s.ToLower()) {
                case "action":
                    return ContentGenre.Action;
                case "science-fiction":
                case "sciencefiction":
                case "science fiction":
                    return ContentGenre.ScienceFiction;
                case "documentary":
                    return ContentGenre.Documentary;
                case "romance":
                    return ContentGenre.Romance;
                case "comedy":
                    return ContentGenre.Comedy;
            }
            return ContentGenre.Comedy;
        }

        public void Load(string file) {
            Console.Clear();
            contents.Clear();
            string[] lines = new string[0];
            try
            {
                lines = System.IO.File.ReadAllLines(file);
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < lines.Length; i++) {
                Console.WriteLine(lines[i]);
                switch (lines[i]) {
                    case "Series":
                        i++;
                        contents.Add(SeriesStreamingContent.Load(lines,ref i));
                        i--;
                        break;
                    case "Basic":
                        i++;
                        contents.Add(BasicStreamingContent.Load(lines,ref i));
                        i--;
                        break;
                }
            }
            Console.ReadKey();
        }

        public void Save(string file)
        {
            string contents = "";
            for (int i = 0; i < NumberOfContents; i++)
            {
                StreamingContent content = this[i];
                contents += content.GenerateSaveString();
            }
            System.IO.File.WriteAllText(file,contents);
        }

        public void RemoveAllStreamingContent() {
            contents.Clear();
        }
    }
}
