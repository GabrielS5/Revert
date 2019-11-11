using AutoMapper;
using Core.Entities;
using Core.Entities.Models;

namespace Application.Profiles
{
    public class RecordProfile : Profile
    {
        public RecordProfile()
        {
            CreateMap<Record, RecordModel>();
            CreateMap<RecordModel, Record>();
        }
    }
}