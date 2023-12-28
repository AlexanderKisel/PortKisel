using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using PortKisel.Api.Models;
using PortKisel.Api.Models.Enums;
using PortKisel.Services.Contracts.Models.Enums;


namespace PortKisel.Api.Infrastructures
{
    public class ApiAutoMapperProfile : Profile
    {
        public ApiAutoMapperProfile()
        {
            CreateMap<PostModels, PostApi>()
                .ConvertUsingEnumMapping(opt => opt.MapByName())
                .ReverseMap();

            CreateMap<CompanyPerRequestModel, CompanyPerResponse>(MemberList.Destination);
            CreateMap<CompanyZakazchikRequestModel, CompanyZakazchikResponse>(MemberList.Destination);

            CreateMap<CargoRequestModel, CargoResponse>(MemberList.Destination)
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.CompanyZakazchik != null
                    ? $"{x.CompanyZakazchik.Name}"
                    : string.Empty));

            CreateMap<VesselRequestModel, VesselResponse>(MemberList.Destination)
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.CompanyPer != null
                    ? $"{x.CompanyPer.Name}"
                    : string.Empty));

            CreateMap<StaffRequestModel, StaffResponse>(MemberList.Destination);
            CreateMap<DocumentiRequestModel, DocumentiResponse>(MemberList.Destination)
                .ForMember(x => x.CargoName, opt => opt.MapFrom(x => x.Cargo!.Name))
                .ForMember(x => x.VesselName, opt => opt.MapFrom(x => x.Vessel!.Name))
                .ForMember(x => x.CompanyPerId, opt => opt.MapFrom(x => x.Vessel!.CompanyPer))
                .ForMember(x => x.CompanyZakazchikId, opt => opt.MapFrom(x => x.Cargo!.CompanyZakazchik))
                .ForMember(x => x.Responsible_cargoName, opt => opt.MapFrom(x => $"{x.Responsible_cargo!.FIO}"));
        }
    }
}
