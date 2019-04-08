using System;
using System.Collections.Generic;
using System.Text;

namespace ChlaDataRepository
{
    class Scenario2QuantitativeAnalysis : Analysis
    {
        protected override void Initialise()
        {
            RegisterAnalyser(new EpinephrineInjectionFromStartAnalyser());
            RegisterAnalyser(new AlbuterolFromStartAnalyser());
            RegisterAnalyser(new MethylprednisoloneFromStartAnalyser());
            RegisterAnalyser(new DiphenhydramineFromStartAnalyser());
            RegisterAnalyser(new RanitidineFromStartAnalyser());
            RegisterAnalyser(new IntubationFromStartOfScene2Analyser());
            RegisterAnalyser(new CricothyroidotomyFromTimeOfBeingToldTongueIsSwollenAnalyser());
            RegisterAnalyser(new TimeToEpinephrineInfusionFromStartOfScene2());
            RegisterAnalyser(new FirstListeningToBreathSoundsAnalyser());
            RegisterAnalyser(new FirstListeningToBreathSoundsFromStartOfScene2Analyser());
            RegisterAnalyser(new FirstPulsesCheckFromStartAnalyser());
            RegisterAnalyser(new FirstPulseCheckFromStartOfScene2Analyser());
                      
        }
    }
    class Scenario2QualitativeAnalysis : Analysis
    {
        protected override void Initialise()
        {
            RegisterAnalyser(new MedicationsS1C_QualitativeAnalyser());
            RegisterAnalyser(new MedicationsS1M_QualitativeAnalyser());
            RegisterAnalyser(new MedicationsS1Mild_QualitativeAnalyser());
            RegisterAnalyser(new MedicationsS2M_QualitativeAnalyser());
            RegisterAnalyser(new MedicationsS2Mild_QualitativeAnalyser());
            RegisterAnalyser(new MedicationsS3QualitativeAnalyser());
            RegisterAnalyser(new MedicationsS4C_QualitativeAnalyser());
            RegisterAnalyser(new MedicationsS4M_QualitativeAnalyser());

            RegisterAnalyser(new PhysicalExamS1C_QualitativeAnalyser());
            RegisterAnalyser(new PhysicalExamS1M_QualitativeAnalyser());
            RegisterAnalyser(new PhysicalExamS1Mild_QualitativeAnalyser());
            RegisterAnalyser(new PhysicalExamS2C_QualitativeAnalyser());
            RegisterAnalyser(new PhysicalExamS2M_QualitativeAnalyser());
            RegisterAnalyser(new PhysicalExamS3QualitativeAnalyser());
            RegisterAnalyser(new PhysicalExamS4QualitativeAnalyser());

            RegisterAnalyser(new VasopressorS1C_QualitativeAnalyser());
            RegisterAnalyser(new VasopressorS1M_QualitativeAnalyser());

            RegisterAnalyser(new WorseningS1QualitativeAnalyser());
            RegisterAnalyser(new WorseningS2QualitativeAnalyser());

        }
    }
}
