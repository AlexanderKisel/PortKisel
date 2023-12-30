using PortKisel.Api.Models.Enums;
using PortKisel.Api.ModelsRequest.Cargo;
using PortKisel.Api.ModelsRequest.CompanyPer;
using PortKisel.Api.ModelsRequest.CompanyZakazchik;
using PortKisel.Api.ModelsRequest.Documenti;
using PortKisel.Api.ModelsRequest.Staff;
using PortKisel.Api.ModelsRequest.Vessel;

namespace PortKisel.Api.Tests
{
    static public class TestDataGenerators
    {
        static public CreateCargoRequest CreateCargoRequest(Action<CreateCargoRequest>? action = null)
        {
            var item = new CreateCargoRequest
            {
                Name = $"Name{Guid.NewGuid()}",
                Description = $"Description{Guid.NewGuid()}",
                Weight = "10000",
                CompanyZakazchikId = Guid.NewGuid()
            };

            action?.Invoke(item);
            return item;
        }

        static public CreateCompanyPerRequest CreateCompanyPerRequest(Action<CreateCompanyPerRequest>? action = null)
        {
            var item = new CreateCompanyPerRequest
            {
                Name = $"Name{Guid.NewGuid()}",
                Description = $"Description{Guid.NewGuid()}"
            };

            action?.Invoke(item);
            return item;
        }


        static public CreateCompanyZakazhikRequest CreateCompanyZakazhikRequest(Action<CreateCompanyZakazhikRequest>? action = null)
        {
            var item = new CreateCompanyZakazhikRequest
            {
                Name = $"Name{Guid.NewGuid()}",
                Description = $"Description{Guid.NewGuid()}"
            };

            action?.Invoke(item);
            return item;
        }

        static public CreateDocumentiRequest CreateDocumentiRequest(Action<CreateDocumentiRequest>? action = null)
        {
            var item = new CreateDocumentiRequest
            {
                Number = $"Number{Guid.NewGuid()}",
                IssaedAt = DateTime.Now,
                VesselId = Guid.NewGuid(),
                CargoId = Guid.NewGuid(),
                StaffId = Guid.NewGuid(),
            };

            action?.Invoke(item);
            return item;
        }

        static public CreateStaffRequest CreateStaffRequest(Action<CreateStaffRequest>? action = null)
        {
            var item = new CreateStaffRequest
            {
                FIO = $"Name{Guid.NewGuid()}",
                Post = PostApi.Responsible_cargo
            };

            action?.Invoke(item);
            return item;
        }

        static public CreateVesselRequest CreateVesselRequest(Action<CreateVesselRequest>? action = null)
        {
            var item = new CreateVesselRequest
            {
                Name = $"Name{Guid.NewGuid()}",
                Description = $"Description{Guid.NewGuid()}",
                LoadCapacity = "12000",
                CompanyPerId = Guid.NewGuid()
            };

            action?.Invoke(item);
            return item;
        }

        static public EditCargoRequest EditCargoRequest(Action<EditCargoRequest>? action = null)
        {
            var item = new EditCargoRequest
            {
                Id = Guid.NewGuid(),
                Name = $"Name{Guid.NewGuid()}",
                Description = $"Description{Guid.NewGuid()}",
                Weight = "10000",
                CompanyZakazchikId = Guid.NewGuid()
            };

            action?.Invoke(item);
            return item;
        }

        static public EditCompanyPerRequest EditCompanyPerRequest(Action<EditCompanyPerRequest>? action = null)
        {
            var item = new EditCompanyPerRequest
            {
                Id = Guid.NewGuid(),
                Name = $"Name{Guid.NewGuid()}",
                Description = $"Description{Guid.NewGuid()}"
            };

            action?.Invoke(item);
            return item;
        }


        static public EditCompanyZakazhikRequest EditCompanyZakazhikRequest(Action<EditCompanyZakazhikRequest>? action = null)
        {
            var item = new EditCompanyZakazhikRequest
            {
                Id = Guid.NewGuid(),
                Name = $"Name{Guid.NewGuid()}",
                Description = $"Description{Guid.NewGuid()}"
            };

            action?.Invoke(item);
            return item;
        }

        static public EditDocumentiRequest EditDocumentiRequest(Action<EditDocumentiRequest>? action = null)
        {
            var item = new EditDocumentiRequest
            {
                Id = Guid.NewGuid(),
                IssaedAt = DateTime.Now,
                Number = $"Number{Guid.NewGuid()}",
                VesselId = Guid.NewGuid(),
                CargoId = Guid.NewGuid(),
                StaffId = Guid.NewGuid(),
            };

            action?.Invoke(item);
            return item;
        }

        static public EditStaffRequest EditStaffRequest(Action<EditStaffRequest>? action = null)
        {
            var item = new EditStaffRequest
            {
                Id = Guid.NewGuid(),
                FIO = $"Name{Guid.NewGuid()}",
                Post = PostApi.Responsible_cargo,
            };

            action?.Invoke(item);
            return item;
        }

        static public EditVesselRequest EditVesselRequest(Action<EditVesselRequest>? action = null)
        {
            var item = new EditVesselRequest
            {
                Id = Guid.NewGuid(),
                Name = $"Name{Guid.NewGuid()}",
                Description = $"Description{Guid.NewGuid()}",
                LoadCapacity = "12000",
                CompanyPerId = Guid.NewGuid()
            };

            action?.Invoke(item);
            return item;
        }

    }
}
