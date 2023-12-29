using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using PortKisel.Api.Models;
using PortKisel.Api.Models.Enums;
using PortKisel.Api.ModelsRequest.CompanyPer;
using PortKisel.Services.Contracts.Models;
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

            CreateMap<CompanyPerModel, CompanyPerResponse>(MemberList.Destination);
            CreateMap<CreateCompanyPerRequest, CompanyPerResponse>(MemberList.Destination);
            CreateMap<CompanyZakazchikModel, CompanyZakazchikResponse>(MemberList.Destination);

            CreateMap<CargoModel, CargoResponse>(MemberList.Destination)
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.CompanyZakazchik != null
                    ? $"{x.CompanyZakazchik.Name}"
                    : string.Empty));

            CreateMap<VesselModel, VesselResponse>(MemberList.Destination)
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.CompanyPer != null
                    ? $"{x.CompanyPer.Name}"
                    : string.Empty));

            CreateMap<StaffModel, StaffResponse>(MemberList.Destination);
            CreateMap<DocumentiModel, DocumentiResponse>(MemberList.Destination)
                .ForMember(x => x.CargoName, opt => opt.MapFrom(x => x.Cargo!.Name))
                .ForMember(x => x.VesselName, opt => opt.MapFrom(x => x.Vessel!.Name))
                .ForMember(x => x.CompanyPerId, opt => opt.MapFrom(x => x.Vessel!.CompanyPer))
                .ForMember(x => x.CompanyZakazchikId, opt => opt.MapFrom(x => x.Cargo!.CompanyZakazchik))
                .ForMember(x => x.Responsible_cargoName, opt => opt.MapFrom(x => $"{x.Staff!.FIO}"));
        }
    }
}
