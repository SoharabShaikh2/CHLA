using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ChlaDataRepository
{
    public interface ILog
    {
        [Key]
        long id { get; set; }
        string log { get; set; }
        string mlog { get; set; }
        string Session_ID { get; set; }
        string clientname { get; set; }
    }
}
