using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PortKisel.Api.Models;
using PortKisel.Api.ModelsRequest.Documenti;
using PortKisel.Api.Tests.Infrastructure;
using PortKisel.Context;
using PortKisel.Context.Contracts.Models;
using PortKisel.Services.Tests;
using System.Net.Sockets;
using System.Text;
using Xunit;

namespace PortKisel.Api.Tests.IntegrationTests
{
    public class DocumentiIntegrationTest : BaseIntegrationTest
    {
        private readonly Cargo cargo;
        private readonly Vessel vessel;
        private readonly Staff staff;

        public DocumentiIntegrationTest(PortApiFixture fixture) : base(fixture) 
        {
            cargo = TestDataGenerator.Cargo();
            vessel = TestDataGenerator.Vessel();
            staff = TestDataGenerator.Staff();

            context.Cargos.Add(cargo);
            context.Vessels.Add(vessel);
            context.Staffs.Add(staff);
            unitOfWork.SaveChangesAsync().Wait();
        }

        //[Fact]
        //public async Task AddShouldWork()
        //{
        //    // Arrange
        //    var clientFactory = factory.CreateClient();

        //    var documenti = mapper.Map<CreateDocumentiRequest>(TestDataGenerator.Documenti());
        //    documenti.CargoId = cargo.Id;
        //    documenti.VesselId = vessel.Id;
        //    documenti.StaffId = staff.Id;

        //    // Act 
        //    string data = JsonConvert.SerializeObject(documenti);
        //    var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
        //    var response = await clientFactory.PostAsync("/Documenti", contextdata);
        //    var resultString = await response.Content.ReadAsStringAsync();
        //    var result = JsonConvert.DeserializeObject<DocumentiResponse>(resultString);

        //    var documentiFirst = await context.Documentis.FirstAsync(x => x.Id == result!.Id);

        //    // Assert
        //    documentiFirst.Should()
        //        .BeEquivalentTo(documenti);
        //}

    }
}
