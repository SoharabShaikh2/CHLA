using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChlaDataRepository
{
    public interface IAnalyser
    {
        JObject Analyse(JObject jsonObject);
    }
}
