using AutoMapper;
using BookVoyage.Domain.Entities;

namespace BookVoyage.Application.Common.Mappings;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<Category, Category>();
    }
}