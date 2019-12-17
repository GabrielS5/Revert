using Aglomera;
using Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Application.Aglomera
{
    public class RecordDissimilarityMetric : IDissimilarityMetric<Record>
    {
        private readonly List<string> categories = new List<string> { "StareaGenerala", "Nutritie", "Constienta", "Facies", "Tegumente", "Mucoase", "Fanere", "TesutConjunctiv",
            "SistemGanglionar", "SistemMuscular", "SistemOsteoArticular", "AparatRespirator", "AparatCardiovascular", "AparatDigestiv", "FicatSplina", "AparatUroGeneral",
            "SistemEndocrin", "MotiveleInternarii", "Anamneza", "IstoriculBolii" };

        public double Calculate(Record instance1, Record instance2)
        {
            double score = 20;
            foreach (var category in categories)
            {
                var instance1Keywords = instance1.Keywords.Where(k => k.Name.Equals(category));
                var instance2Keywords = instance2.Keywords.Where(k => k.Name.Equals(category));
                var instance1Length = instance1Keywords.Count();
                var instance2Length = instance2Keywords.Count();

                if (instance1Length == 0 && instance2Length == 0)
                {
                    score -= 1;
                    continue;
                }

                if ((instance1Length > 0 && instance2Length == 0) || (instance1Length == 0 && instance2Length > 0))
                {
                    continue;
                }

                double totalKeywords = instance1Length + instance2Length;
                var intersectionLength = instance1Keywords.Where(k => instance2Keywords.
                        FirstOrDefault(c => c.Value.Equals(k.Value) && c.Positive == k.Positive) != null)
                        .Count();

                if (intersectionLength > 0)
                {
                    var leastKeywords = ((instance1Length > instance2Length ? instance2Length : instance1Length) - intersectionLength) * 2;

                    score -= 1 - ((1 / totalKeywords) * leastKeywords);
                }
                else
                {
                    continue;
                }
            }

            return score;
        }
    }
}
