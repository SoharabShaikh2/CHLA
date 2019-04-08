using System;
using System.Collections.Generic;
using System.Text;

namespace DataRepository
{
    public class ResultDto
    {
        public long id { get; set; }
        public string userid { get; set; }
        public string scenarioname { get; set; }
        public string DateTimeSession { get; set; }
        public string ResultJSon { get; set; }
    }
}
