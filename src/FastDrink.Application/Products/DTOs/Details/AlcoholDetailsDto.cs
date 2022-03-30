using FastDrink.Application.Common.Mappings;
using FastDrink.Domain.Entities;

namespace FastDrink.Application.Products.DTOs.Details;

public class AlcoholDetailsDto : IMapFrom<AlcoholDetails>
{
    public string ProductId { get; set; }
    public float AlcoholContent { get; set; }

    //public void Mapping(Profile profile)
    //{
    //    profile.CreateMap<AlcoholDetails, AlcoholDetailsDto>()
    //        .ForMember(d => d.ProductId, opt => opt.MapFrom(s => s.ProductId.ToString()));
    //}
}
