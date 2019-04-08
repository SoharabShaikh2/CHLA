using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ChlaDataRepository
{
    public interface IResult
    {
        [Key]
        long id { get; set; }
        string userid { get; set; }
        string scenarioname { get; set; }
        DateTime DateTimeSession { get; set; }
        string ResultJSon { get; set; }
    }
}
