using System;
using System.Collections.Generic;
using System.Text;

namespace ChlaDataRepository
{
    class QualitativeAnalysis : Analysis
    {

        protected override void Initialise()
        {
            RegisterAnalyser(new SuctionUsedAnalyser());
        }

    }
}
