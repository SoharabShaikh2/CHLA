using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EQLWebAPIWithAngular.Models
{
    public class SearchUserDto
    {
        public int id { get; set; }
        public string text { get; set; }
        public string dateTime { get; set; }
        public string input { get; set; }
    }

}
