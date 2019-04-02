using System;
using System.Collections.Generic;
using System.Text;

namespace ChlaDataRepository
{
    public class AnalysisFactory
    {
        public IAnalysis GetAnalysis(string scenario)
        {
            switch (scenario)
            {
                case "Seizure_Status_Epilepticus":
                    return new Scenario1QuantitativeAnalysis();
                case "Anaphylaxis":
                    return new Scenario2QuantitativeAnalysis();
                case "Adult_Seizure_Status_Epilepticus":
                    return new Scenario3QuantitativeAnalysis();

                case "Seizure_Status_Epilepticu_Qualitative":
                    return new Scenario1QualitativeAnalysis();

                case "Adult_Seizure_Status_Epilepticus_Qualitative":
                    return new Scenario3QualitativeAnalysis();

                case "Anaphylaxis_Qualitative":
                    return new Scenario2QualitativeAnalysis();
            }

            return null;
        }
    }
}
