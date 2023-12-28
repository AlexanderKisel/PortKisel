using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using PortKisel.Context.Contracts.Enums;
using PortKisel.Context.Contracts.Models;
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

            CreateMap<Cargo, CargoRequestModel>(MemberList.Destination)
                .ForMember(x => x.CompanyZakazchik, next => next.Ignore());

            CreateMap<CompanyPer, CompanyPerRequestModel>(MemberList.Destination);

            CreateMap<CompanyZakazchik, CompanyZakazchikRequestModel>(MemberList.Destination);

            CreateMap<Documenti, DocumentiRequestModel>(MemberList.Destination)
                .ForMember(x => x.Cargo, next => next.Ignore())
                .ForMember(x => x.Vessel, next => next.Ignore());

            CreateMap<Staff, StaffRequestModel>(MemberList.Destination);

            CreateMap<Vessel, VesselRequestModel>(MemberList.Destination)
                .ForMember(x => x.CompanyPer, next => next.Ignore());
        }
    }
}
