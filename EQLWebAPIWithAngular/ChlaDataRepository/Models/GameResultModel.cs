using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChlaDataRepository
{
    public partial class GameResultModel
    {
        [JsonProperty("Details")]
        public Details Details { get; set; }

        [JsonProperty("Quantitative")]
        public JArray Quantitative { get; set; }

        [JsonProperty("Qualitative")]
        public JArray Qualitative { get; set; }
    }

    public partial class Details
    {
        [JsonProperty("Scenario")]
        public string Scenario { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }

        [JsonProperty("Difficulty")]
        public string Difficulty { get; set; }

        [JsonProperty("Distraction")]
        public string Distraction { get; set; }

        [JsonProperty("User")]
        public string User { get; set; }

        [JsonProperty("Date")]
        public long Date { get; set; }
    }

    public partial class ResultView
    {
        [JsonProperty("DisplayTitle")]
        public string DisplayTitle { get; set; }

        [JsonProperty("DisplayValue")]
        public string DisplayValue { get; set; }
    }
}
