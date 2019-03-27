using System;
using System.Collections.Generic;
using System.Text;

namespace ChlaDataRepository
{
    class AnalysisFactory
    {
        public IAnalysis GetAnalysis(string scenario)
        {
            switch(scenario)
            {
                case "Scenario 1":
                    return new Scenario1QuantitativeAnalysis();
                                 

            }

            return null;
        }
    }
}
