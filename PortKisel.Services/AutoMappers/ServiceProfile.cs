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

            CreateMap<Cargo, CargoModel>(MemberList.Destination);
            CreateMap<CompanyPer, CompanyPerModel>(MemberList.Destination);
            CreateMap<CompanyZakazchik, CompanyZakazchikModel>(MemberList.Destination);
            CreateMap<Documenti, DocumentiModel>(MemberList.Destination);
            CreateMap<Staff, StaffModel>(MemberList.Destination);
            CreateMap<Vessel, VesselModel>(MemberList.Destination);
        }
    }
}
