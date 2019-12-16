using Core.Entities;
using System.Collections.Generic;

namespace Core.Services
{
    public interface IClusteringService
    {
        string Cluster(List<Record> records, Record newRecord);
    }
}
