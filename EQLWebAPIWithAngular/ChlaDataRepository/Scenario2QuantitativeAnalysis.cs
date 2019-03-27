using System;
using System.Collections.Generic;
using System.Text;

namespace ChlaDataRepository
{
    class Scenario2QuantitativeAnalysis : Analysis
    {
        protected override void Initialise()
        {
            RegisterAnalyser(new AlbuterolFromStartAnalyser());
            RegisterAnalyser(new CricothyroidotomyFromTimeOfBeingToldTongueIsSwollenAnalyser());
            RegisterAnalyser(new DiphenhydramineFromStartAnalyser());
            RegisterAnalyser(new EpinephrineInjectionFromStartAnalyser());
            RegisterAnalyser(new FirstListeningToBreathSoundsAnalyser());
            RegisterAnalyser(new FirstListeningToBreathSoundsFromStartOfScene2Analyser());
            RegisterAnalyser(new FirstPulsesCheckFromStartAnalyser());
            RegisterAnalyser(new FirstPulseCheckFromStartOfScene2Analyser());
            RegisterAnalyser(new IntubationFromStartOfScene2Analyser());
            RegisterAnalyser(new MethylprednisoloneFromStartAnalyser());
            RegisterAnalyser(new RanitidineFromStartAnalyser());
            RegisterAnalyser(new TimeToEpinephrineInfusionFromStartOfScene2());
        }
    }
}
