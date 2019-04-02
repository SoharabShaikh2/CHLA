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

    class Scenario3QualitativeAnalysis : Analysis
    {
        protected override void Initialise()
        {
            RegisterAnalyser(new Medications1QualitativeAnalyser());
            RegisterAnalyser(new Medications2QualitativeAnalyser());
            RegisterAnalyser(new Medications3QualitativeAnalyser());
            RegisterAnalyser(new Medications4QualitativeAnalyser());
            RegisterAnalyser(new Medications5QualitativeAnalyser());
            RegisterAnalyser(new Medications6QualitativeAnalyser());
            RegisterAnalyser(new Medications7QualitativeAnalyser());
            RegisterAnalyser(new Medications8_1QualitativeAnalyser());
            RegisterAnalyser(new Medications8_2QualitativeAnalyser());
            RegisterAnalyser(new Medications8_3QualitativeAnalyser());
            RegisterAnalyser(new Medications8_4QualitativeAnalyser());

            RegisterAnalyser(new Oxygen1QualitativeAnalyser());
            RegisterAnalyser(new Oxygen2QualitativeAnalyser());

            RegisterAnalyser(new PhysicalExam1QualitativeAnalyser());
            RegisterAnalyser(new PhysicalExam2QualitativeAnalyser());
            RegisterAnalyser(new PhysicalExam3QualitativeAnalyser());

            RegisterAnalyser(new Suction1QualitativeAnalyser());
            RegisterAnalyser(new Suction2QualitativeAnalyser());

        }
    }
}
