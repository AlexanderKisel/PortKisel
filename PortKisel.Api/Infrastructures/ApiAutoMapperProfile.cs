using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using Microsoft.OpenApi.Extensions;
using PortKisel.Api.Models;
using PortKisel.Api.Models.Enums;
using PortKisel.Api.ModelsRequest.Cargo;
using PortKisel.Api.ModelsRequest.CompanyPer;
using PortKisel.Api.ModelsRequest.CompanyZakazchik;
using PortKisel.Api.ModelsRequest.Staff;
using PortKisel.Api.ModelsRequest.Vessel;
using PortKisel.Services.Contracts.Models;
using PortKisel.Services.Contracts.Models.Enums;
using PortKisel.Services.Contracts.ModelsRequest;


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
            CreateMap<CreateCompanyPerRequest, CompanyPerRequestModel>(MemberList.Destination);
            CreateMap<EditCompanyPerRequest, CompanyPerRequestModel>(MemberList.Destination);

            CreateMap<CompanyZakazchikModel, CompanyZakazchikResponse>(MemberList.Destination);
            CreateMap<CreateCompanyZakazhikRequest, CompanyZakazchikRequestModel>(MemberList.Destination);
            CreateMap<EditCompanyZakazhikRequest, CompanyZakazchikRequestModel>(MemberList.Destination);

            CreateMap<CargoModel, CargoResponse>(MemberList.Destination)
                .ForMember(x => x.CompanyZakazchikName, y => y.MapFrom(z => $"{z.CompanyZakazchik.Name}"));
            CreateMap<CreateCargoRequest, CargoRequestModel>(MemberList.Destination);
            CreateMap<EditCargoRequest, CargoRequestModel>(MemberList.Destination);

            CreateMap<VesselModel, VesselResponse>(MemberList.Destination)
                .ForMember(x => x.CompanyPerName, y => y.MapFrom(z => $"{z.CompanyPer.Name}"));
            CreateMap<CreateVesselRequest, VesselRequestModel>(MemberList.Destination);
            CreateMap<EditVesselRequest, VesselRequestModel>(MemberList.Destination);

            CreateMap<StaffModel, StaffResponse>(MemberList.Destination)
                .ForMember(x => x.Posts, y => y.MapFrom(z => z.Post.GetDisplayName()));
            CreateMap<CreateStaffRequest, StaffRequestModel>(MemberList.Destination);
            CreateMap<EditStaffRequest, StaffRequestModel>(MemberList.Destination);

            CreateMap<DocumentiModel, DocumentiResponse>(MemberList.Destination)
                .ForMember(x => x.CargoName, y => y.MapFrom(z => $"{z.Cargo.Name} {z.Cargo.Weight}"))
                .ForMember(x => x.VesselName, y => y.MapFrom(z => $"{z.Vessel.Name} {z.Vessel.LoadCapacity}"))
                .ForMember(x => x.StaffName, y => y.MapFrom(z => $"{z.Staff.FIO} {z.Staff.Post}"));
        }
    }
}
