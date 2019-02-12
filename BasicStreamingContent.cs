using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge_Mln_1
{
    public class BasicStreamingContent : StreamingContent
    {
        public string Name { get; set; }
        public ContentRating Rating { get; set; }
        public double Time { get; set; }
        public ContentType Type { get; set; }
        public ContentGenre Genre { get; set; }

        public double TotalTime { get { return Time; } }

        public BasicStreamingContent(string n,ContentType t,ContentRating r,double ti,ContentGenre g)
        {
            this.Name = n;
            this.Type = t;
            this.Rating = r;
            this.Time = ti;
            this.Genre = g;
        }

        public string GenerateSaveString() {
            string str = $"Basic\n{Name}\n{Rating}\n{Genre}\n{Type}\n{Time}\n";
            return str;
        }
        public static BasicStreamingContent Load(string[] lines,ref int i) {
            string name = lines[i++];
            ContentRating rating = StreamingContentRepository.GetContentRating(lines[i++]);
            ContentGenre genre = StreamingContentRepository.GetContentGenre(lines[i++]);
            ContentType type = ContentType.Movie;i++;
            double time = double.Parse(lines[i++]);
            return new BasicStreamingContent(name,type,rating,time,genre);
        }

        public void ChangeDuration(double d) {
            this.Time = d;
        }
    }
}
