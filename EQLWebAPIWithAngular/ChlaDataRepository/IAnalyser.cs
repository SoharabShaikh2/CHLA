using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChlaDataRepository
{
    public interface IAnalyser
    {
        string DisplayName { get; set; }

        JObject Analyse(JObject jsonObject);



    }
}
