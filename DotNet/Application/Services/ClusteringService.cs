using Aglomera;
using Aglomera.Linkage;
using Application.Aglomera;
using Core.Entities;
using Core.Services;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services
{
    public class ClusteringService : IClusteringService
    {
        private readonly AgglomerativeClusteringAlgorithm<Record> algorithm;

        public ClusteringService()
        {
            var metric = new RecordDissimilarityMetric();
            var linkage = new AverageLinkage<Record>(metric);
            algorithm = new AgglomerativeClusteringAlgorithm<Record>(linkage);
        }

        public string Cluster(List<Record> records, Record newRecord)
        {
            records.Add(newRecord);
            var set = new HashSet<Record>(records);

            var clusteringResult = algorithm.GetClustering(set);

            var involvedClusters = new List<ClusterModel>();
            var currentCluster = clusteringResult.SingleCluster;

            while (currentCluster.Parent1 != null && currentCluster.Parent2 != null)
            {
                if (currentCluster.Parent1.Contains(newRecord))
                {
                    currentCluster = currentCluster.Parent1;
                }
                else
                {
                    currentCluster = currentCluster.Parent2;
                }

                involvedClusters.Add(GetClusterModel(currentCluster));
            }

            return involvedClusters
                .Where(c => c != null)
                .OrderByDescending(c => c.Percentage)
                .ThenByDescending(t => involvedClusters.Where(ic => ic.Diagnosis == t.Diagnosis))
                .First().Diagnosis;
        }

        private ClusterModel GetClusterModel(Cluster<Record> cluster)
        {
            var enumerator = cluster.GetEnumerator();
            var records = new List<Record>();

            while (enumerator.MoveNext())
            {
                records.Add(enumerator.Current);
            }

            records = records.Where(r => !string.IsNullOrEmpty(r.Diagnosis)).ToList();
            if (!records.Any())
            {
                return null;
            }

            var bestGroup = records.GroupBy(g => g.Diagnosis).OrderByDescending(o => o.Count()).First();

            return new ClusterModel
            {
                Cluster = cluster,
                Diagnosis = bestGroup.Key,
                Percentage = ((double)bestGroup.Count() / (double)cluster.Count) * 100
            };
        }

        public class ClusterModel
        {
            public double Percentage { get; set; }
            public string Diagnosis { get; set; }
            public Cluster<Record> Cluster { get; set; }
        }
    }
}
