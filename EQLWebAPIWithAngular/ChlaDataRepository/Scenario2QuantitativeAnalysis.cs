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
            RegisterAnalyser(new MedicationsS1QualitativeAnalyser());
            RegisterAnalyser(new MedicationsS2QualitativeAnalyser());
            RegisterAnalyser(new MedicationsS3QualitativeAnalyser());
            RegisterAnalyser(new MedicationsS4QualitativeAnalyser());

            RegisterAnalyser(new PhysicalExamS1QualitativeAnalyser());
            RegisterAnalyser(new PhysicalExamS2QualitativeAnalyser());
            RegisterAnalyser(new PhysicalExamS3QualitativeAnalyser());
            RegisterAnalyser(new PhysicalExamS4QualitativeAnalyser());

            RegisterAnalyser(new VasopressorS1QualitativeAnalyser());

            RegisterAnalyser(new WorseningS1QualitativeAnalyser());
            RegisterAnalyser(new WorseningS2QualitativeAnalyser());

        }
    }
}
