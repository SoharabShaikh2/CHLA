using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace ChlaDataRepository
{
    class Scenario1QuantitativeAnalysis : Analysis
    {
        protected override void Initialise()
        {
            RegisterAnalyser(new SuctionUsedAnalyser());
            RegisterAnalyser(new OxygenDeviceFromStartAnalyser());
            RegisterAnalyser(new NRBmaskFromStartAnalyser());
            RegisterAnalyser(new AtivanMedicationUsedAnalyser());
            RegisterAnalyser(new AdvancedAtivanMedicationUsedAnalyser());
            RegisterAnalyser(new AdvancedFosphenytoinMedicationUsedAnalyser());
            RegisterAnalyser(new IntubationFromSeizureAnalyser());
            RegisterAnalyser(new FirstPupilCheckAnalyser());
            RegisterAnalyser(new FirstPulseCheckAnalyser());
            RegisterAnalyser(new CheckBreathAnalyser());                      
        }
    }

    class Scenario1QualitativeAnalysis : Analysis
    {
        protected override void Initialise()
        {
            RegisterAnalyser(new Medications1C_QualitativeAnalyser());
            RegisterAnalyser(new Medications1M_QualitativeAnalyser());
            RegisterAnalyser(new Medications1Mild_QualitativeAnalyser());
            RegisterAnalyser(new Medications2C_QualitativeAnalyser());
            RegisterAnalyser(new Medications2M_QualitativeAnalyser());
            RegisterAnalyser(new Medications2Mild_QualitativeAnalyser());
            RegisterAnalyser(new Medications3C_QualitativeAnalyser());
            RegisterAnalyser(new Medications3M_QualitativeAnalyser());

            RegisterAnalyser(new Medications4QualitativeAnalyser());
            RegisterAnalyser(new Medications5QualitativeAnalyser());
            RegisterAnalyser(new Medications6QualitativeAnalyser());
            RegisterAnalyser(new Medications7QualitativeAnalyser());

            RegisterAnalyser(new Oxygen1C_QualitativeAnalyser());
            RegisterAnalyser(new Oxygen1M_QualitativeAnalyser());
            RegisterAnalyser(new Oxygen2C_QualitativeAnalyser());
            RegisterAnalyser(new Oxygen2M_QualitativeAnalyser());

            RegisterAnalyser(new PhysicalExam1C_QualitativeAnalyser());
            RegisterAnalyser(new PhysicalExam1M_QualitativeAnalyser());
            RegisterAnalyser(new PhysicalExam1Mild_QualitativeAnalyser());
            RegisterAnalyser(new PhysicalExam2C_QualitativeAnalyser());
            RegisterAnalyser(new PhysicalExam2M_QualitativeAnalyser());
            RegisterAnalyser(new PhysicalExam3C_QualitativeAnalyser());
            RegisterAnalyser(new PhysicalExam3M_QualitativeAnalyser());

            RegisterAnalyser(new Suction1C_QualitativeAnalyser());
            RegisterAnalyser(new Suction1M_QualitativeAnalyser());
            RegisterAnalyser(new Suction1Mild_QualitativeAnalyser());
            RegisterAnalyser(new Suction2C_QualitativeAnalyser());
            RegisterAnalyser(new Suction2M_QualitativeAnalyser());

        }
    }
}
