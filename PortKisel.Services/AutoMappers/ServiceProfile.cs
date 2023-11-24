using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using PortKisel.Context.Contracts.Enums;
using PortKisel.Context.Contracts.Models;
using PortKisel.Services.Contracts.Models;
using PortKisel.Services.Contracts.Models.Enums;

namespace PortKisel.Services.AutoMappers
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile() 
        {
            CreateMap<Posts, PostModels>()
                .ConvertUsingEnumMapping(opt => opt.MapByName())
                .ReverseMap();

            CreateMap<Cargo, CargoModel>(MemberList.Destination)
                .ForMember(x => x.CompanyZakazchik, next => next.Ignore());

            CreateMap<CompanyPer, CompanyPerModel>(MemberList.Destination);

            CreateMap<CompanyZakazchik, CompanyZakazchikModel>(MemberList.Destination);

            CreateMap<Documenti, DocumentiModel>(MemberList.Destination)
                .ForMember(x => x.Cargo, next => next.Ignore())
                .ForMember(x => x.Vessel, next => next.Ignore());

            CreateMap<Staff, StaffModel>(MemberList.Destination);

            CreateMap<Vessel, VesselModel>(MemberList.Destination)
                .ForMember(x => x.CompanyPer, next => next.Ignore());
        }
    }
}
