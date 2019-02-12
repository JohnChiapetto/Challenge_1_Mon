using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge_Mln_1
{
    public enum ContentRating {
        G,PG,PG13,R,M,NR
    }
    public enum ContentType
    {
        TVShow,
        Movie
    }
    public enum ContentGenre {
        Action,
        ScienceFiction,
        Comedy,
        Romance,
        Documentary
    }

    public interface StreamingContent {
        string Name { get; set; }
        ContentType Type { get; set; }
        ContentRating Rating { get; set; }
        double Time { get; }
        ContentGenre Genre { get; set; }
        double TotalTime { get; }

        string GenerateSaveString();
    }
}

