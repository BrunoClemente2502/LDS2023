using System;

namespace AnaliseImagens
{
    public class ResultsEventArgs : EventArgs
    {
        public ColorPercentages Results { get; set; }

        public ResultsEventArgs(ColorPercentages results)
        {
            Results = results;
        }
    }
}
