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
        private readonly List<string> negativeWords = new List<string>() { "not", "don't", "no", "doesn't", "shouldn't", "isn't" };
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

            bool ComputeIsPositive(string context, string keyword)
            {
                try
                {
                    var words = context.Split(" ").Select(w => string.Concat(w.Where(c => !char.IsPunctuation(c)))).ToList();
                    var keywordIndex = words.IndexOf(keyword);
                    var surroundingWords = new List<string>();

                    if (keywordIndex > 0)
                    {
                        surroundingWords.Add(words[keywordIndex - 1].ToLower());
                    }
                    if (keywordIndex > 1)
                    {
                        surroundingWords.Add(words[keywordIndex - 2].ToLower());
                    }
                    if (keywordIndex > 2)
                    {
                        surroundingWords.Add(words[keywordIndex - 3].ToLower());
                    }

                    return surroundingWords.Intersect(negativeWords).ToList().Count == 0;
                }
                catch
                {
                    return true;
                }
            }

            foreach (var property in record.GetType().GetProperties())
            {
                if (property.PropertyType == typeof(string))
                {
                    var value = (string)property.GetValue(record);
                    if (!string.IsNullOrEmpty(value))
                    {
                        var translation = await translateService.Translate(value);
                        var collectedKeywords = await keywordsService.CollectKeywords(translation);
                        if (!collectedKeywords.Any())
                        {
                            collectedKeywords = translation.Split(" ").Select(w => string.Concat(w.Where(c => !char.IsPunctuation(c))))
                                                                      .Where(w => w.Length > 4).Select(w => new Keyword
                            {
                                Significance = 0.75,
                                Value = w
                            });
                        }

                        collectedKeywords = collectedKeywords.SelectMany(k => k.Value.Split(" ").Select(s => new Keyword
                        {
                            Significance = k.Significance,
                            Name = property.Name,
                            Value = s,
                            Positive = ComputeIsPositive(translation, s)
                        }));
                        keywords.AddRange(collectedKeywords);
                    }
                }
                if (property.PropertyType == typeof(int))
                {
                    if (((int)property.GetValue(record)) != 0)
                    {
                        keywords.Add(new Keyword
                        {
                            Significance = 1,
                            Name = property.Name,
                            Value = (property.GetValue(record)).ToString(),
                            Positive = true
                        });
                    }
                }
            }

            keywords.ForEach(k => k.Id = Guid.NewGuid());

            return keywords;
        }
    }
}
