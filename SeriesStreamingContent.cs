using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge_Mln_1
{
    public class SeriesStreamingContent : StreamingContent
    {
        public double[] Iterations { get; private set; }

        public double TotalTime
        {
            get
            {
                double t = 0D;
                for (int i = 0; i < Iterations.Length; i++)
                    t += Iterations[i];
                return t;
            }
        }

        public ContentRating Rating { get; set; }
        public string Name { get; set; }
        public double Time { get { return TotalTime; } }
        public ContentType Type { get; set; }
        public ContentGenre Genre { get; set; }

        public SeriesStreamingContent(string name,ContentRating rating,double[] times,ContentType type,ContentGenre genre)
        {
            this.Name = name;
            this.Iterations = times;
            this.Rating = rating;
            this.Type = type;
            this.Genre = genre;
        }

        public string GenerateSaveString() {
            string str = $"Series\n{Name}\n{Rating}\n{Genre}\n{Type}\n";
            for (int i = 0; i < Iterations.Length; i++) {
                str += Iterations[i];
                if (i < Iterations.Length - 1) str += " ";
            }
            return str+"\n";
        }

        public static SeriesStreamingContent Load(string[] lines,ref int i)
        {
            string name = lines[i++];
            ContentRating rating = StreamingContentRepository.GetContentRating(lines[i++]);
            ContentGenre genre = StreamingContentRepository.GetContentGenre(lines[i++]);
            ContentType type = ContentType.TVShow; i++;
            string[] timesStr = lines[i++].Split('\n');
            double[] times = new double[timesStr.Length];
            for (int j = 0; j < times.Length; j++)
                times[j] = double.Parse(timesStr[j]);
            
            return new SeriesStreamingContent(name,rating,times,type,genre);
        }
    }
}
