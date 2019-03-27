using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChlaDataRepository
{

    public class QuantitativeAnalysis : Analysis
    {

        protected override void Initialise()
        {
            //RegisterAnalyser(new MedicationUsedAnalyser());
        }
    }
}
