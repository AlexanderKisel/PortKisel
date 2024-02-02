using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PortKisel.Api.Models;
using PortKisel.Api.ModelsRequest.Cargo;
using PortKisel.Api.ModelsRequest.Vessel;
using PortKisel.Api.Tests.Infrastructure;
using PortKisel.Common.Entity.InterfaceDB;
using PortKisel.Context.Contracts.Models;
using PortKisel.Services.Tests;
using System.Text;
using Xunit;

namespace PortKisel.Api.Tests.IntegrationTests
{
    public class VesselIntegrationTest : BaseIntegrationTest
    {
        private readonly CompanyPer companyPer;
        public VesselIntegrationTest(PortApiFixture fixture) : base(fixture)
        {
            companyPer = TestDataGenerator.CompanyPer();

            context.CompanyPers.Add(companyPer);
            unitOfWork.SaveChangesAsync().Wait();
        }

        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var client = factory.CreateClient();
            var vessel = mapper.Map<CreateVesselRequest>(TestDataGenerator.VesselRequestModel());
            vessel.CompanyPerId = companyPer.Id;

            //Act
            string data = JsonConvert.SerializeObject(vessel);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/Vessel", contextdata);
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<VesselResponse>(resultString);

            var vesselFirst = await context.Vessels.FirstAsync(x => x.Id == result!.Id);

            //Assert
            vesselFirst.Should()
                .BeEquivalentTo(vessel);
        }

        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var client = factory.CreateClient();
            var vessel = TestDataGenerator.Vessel();

            SetDependeciesOrVessel(vessel);
            await context.Vessels.AddAsync(vessel);
            await unitOfWork.SaveChangesAsync();

            var vesselRequest = mapper.Map<EditVesselRequest>(TestDataGenerator.VesselRequestModel(x => x.Id = vessel.Id));
            SetDependenciesOrVesselRequestModelWithVessel(vessel, vesselRequest);

            //Act
            string data = JsonConvert.SerializeObject(vesselRequest);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            await client.PutAsync("/Vessel", contextdata);

            var vesselFirst = await context.Vessels.FirstAsync(x => x.Id == vesselRequest.Id);
            //Assert
            vesselFirst.Should()
                .BeEquivalentTo(vesselRequest);
        }

        [Fact]
        public async Task GetAllShouldWork()
        {
            // Arrange 
            var client = factory.CreateClient();
            var vessel1 = TestDataGenerator.Vessel();
            var vessel2 = TestDataGenerator.Vessel(x => x.DeletedAt = DateTimeOffset.Now);


            SetDependeciesOrVessel(vessel1);
            SetDependeciesOrVessel(vessel2);

            await context.Vessels.AddRangeAsync(vessel1, vessel2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var respose = await client.GetAsync("/Vessel");

            // Assert
            respose.EnsureSuccessStatusCode();
            var resultString = await respose.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<VesselResponse>>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .Contain(x => x.Id == vessel1.Id)
                .And
                .NotContain(x => x.Id == vessel2.Id);
        }

        [Fact]
        public async void GetShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var vessel1 = TestDataGenerator.Vessel();
            var vessel2 = TestDataGenerator.Vessel();

            SetDependeciesOrVessel(vessel1);
            SetDependeciesOrVessel(vessel2);

            await context.Vessels.AddRangeAsync(vessel1, vessel2);
            await unitOfWork.SaveChangesAsync();
            // Act 
            var response = await client.GetAsync($"/Vessel/{vessel1.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<VesselResponse>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    vessel1.Id,
                    vessel1.Name,
                    vessel1.LoadCapacity
                });
        }

        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var vessel = TestDataGenerator.Vessel();

            SetDependeciesOrVessel(vessel);
            await context.Vessels.AddAsync(vessel);
            await unitOfWork.SaveChangesAsync();

            // Act
            await client.DeleteAsync($"/Vessel/{vessel.Id}");

            var vesselFirst = await context.Vessels.FirstAsync(x => x.Id == vessel.Id);

            // Assert
            vesselFirst.DeletedAt.Should()
                .NotBeNull();

            vesselFirst.Should()
                .BeEquivalentTo(new
                {
                    vessel.Name,
                    vessel.LoadCapacity,
                    vessel.Description,
                    vessel.CompanyPerId
                });
        }

        private void SetDependeciesOrVessel(Vessel vessel)
        {
            vessel.CompanyPerId = companyPer.Id;
        }

        private void SetDependenciesOrVesselRequestModelWithVessel(Vessel vessel, EditVesselRequest editVesselRequest)
        {
            editVesselRequest.CompanyPerId = vessel.CompanyPerId;
        }
    }
}
