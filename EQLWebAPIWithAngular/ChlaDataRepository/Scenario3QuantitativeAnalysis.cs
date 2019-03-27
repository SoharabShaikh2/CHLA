using System;
using System.Collections.Generic;
using System.Text;

namespace ChlaDataRepository
{
    class Scenario3QuantitativeAnalysis : Analysis
    {
        protected override void Initialise()
        {
            RegisterAnalyser(new AdvancedAtivanMedicationUsedAnalyser());
            RegisterAnalyser(new AdvancedFosphenytoinMedicationUsedAnalyser());
            RegisterAnalyser(new AtivanMedicationUsedAnalyser());
            RegisterAnalyser(new BloodGlucoseLevelAnalyser());
            RegisterAnalyser(new CheckBreathAnalyser());
            RegisterAnalyser(new D50WAdminAnalyser());
            RegisterAnalyser(new FirstPulseCheckAnalyser());
            RegisterAnalyser(new FirstPupilCheckAnalyser());
            RegisterAnalyser(new IntubationFromSeizureAnalyser());
            RegisterAnalyser(new NRBmaskFromStartAnalyser());
            RegisterAnalyser(new OxygenDeviceFromStartAnalyser());
            RegisterAnalyser(new SuctionUsedAnalyser());
        }
    }
}
