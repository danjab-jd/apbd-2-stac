using System;
using System.Collections.Generic;

namespace APBD2.Models
{
    public class Result
    {
        public DateTime CreatedAt { get; set; }
        public string Author { get; set; }
        public HashSet<Student> Students { get; set; }
    }
}
