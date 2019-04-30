using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QAndA.Model
{
    public class Question
    {
        public int id { get; set; }
        public string question { get; set;}
        public string author { get; set;}
        public List<Option> options { get; set;}
    }
}
