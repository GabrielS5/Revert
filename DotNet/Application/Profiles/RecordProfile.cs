using AutoMapper;
using Core.Entities;
using Core.Entities.Models;

namespace Application.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Record, RecordModel>();
            CreateMap<RecordModel, Record>();
            CreateMap<Keyword, KeywordModel>();
            CreateMap<KeywordModel, Keyword>();
        }
    }
}