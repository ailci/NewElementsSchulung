using AutoMapper;
using Domain.Entities;
using Shared.DataTransferObjects;

namespace Api;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Quote, QuoteOfTheDayDto>();
    }
}