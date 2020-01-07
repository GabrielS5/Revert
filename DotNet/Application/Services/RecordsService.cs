using Core.Entities;
using Core.Entities.Queries;
using Core.Repositories;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class RecordsService : IRecordsService
    {
        private readonly IRecordsRepository recordsRepository;
        private readonly ITranslateService translateService;
        private readonly IKeywordsService keywordsService;

        public RecordsService(IRecordsRepository recordsRepository, ITranslateService translateService, IKeywordsService keywordsService)
        {
            this.translateService = translateService;
            this.keywordsService = keywordsService;
            this.recordsRepository = recordsRepository;
        }

        public async Task Delete(Guid id)
        {
            await recordsRepository.Delete(id);
        }

        public async Task<IQueryable<Record>> GetAll(RecordsQuery query)
        {
            return await recordsRepository.GetAll(query);
        }

        public async Task<Record> GetById(Guid id)
        {
            return await recordsRepository.GetById(id);
        }

        public async Task Insert(Record record)
        {
            record.CreationDate = DateTime.Now;
            record.Id = Guid.NewGuid();
            record.Keywords = await GetKeywords(record);

            await recordsRepository.Insert(record);
        }

        public async Task<List<Keyword>> GetKeywords(Record record)
        {
            var keywords = new List<Keyword>();
            var tasks = new List<Task<IEnumerable<Keyword>>>();

            foreach (var property in record.GetType().GetProperties())
            {
                if (property.PropertyType == typeof(string) && !property.Name.Equals("Diagnosis"))
                {
                    var value = (string)property.GetValue(record);
                    if (!string.IsNullOrEmpty(value))
                    {
                        tasks.Add(ProcessProperty(property.Name, value));
                    }
                }
            }
            var results = await Task.WhenAll(tasks);
            foreach (var result in results)
            {
                keywords.AddRange(result);
            }

            keywords.ForEach(k => k.Id = Guid.NewGuid());

            return keywords;
        }

        private async Task<IEnumerable<Keyword>> ProcessProperty(string property, string value)
        {
            var translation = await translateService.Translate(value);
            var collectedKeywords = await keywordsService.CollectKeywords(translation);
            if (!collectedKeywords.Any())
            {
                collectedKeywords = GetDefaultKeywords(translation);
            }

            return keywordsService.ProcessKeywords(collectedKeywords, property, translation);
        }

        private static IEnumerable<Keyword> GetDefaultKeywords(string translation)
        {
            return translation.Split(" ").Select(w => string.Concat(w.Where(c => !char.IsPunctuation(c))))
                                                                                  .Where(w => w.Length > 4).Select(w => new Keyword
                                                                                  {
                                                                                      Significance = 0.75,
                                                                                      Value = w
                                                                                  });
        }

        public async Task<Record> GetLatest()
        {
            return await recordsRepository.GetLatest();
        }
    }
}
