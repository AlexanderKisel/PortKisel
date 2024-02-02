using PortKisel.Api.ModelsRequest.Cargo;
using PortKisel.Api.Tests.Infrastructure;
using Xunit;
using PortKisel.Services.Tests;
using Newtonsoft.Json;
using System.Text;
using PortKisel.Api.Models;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using PortKisel.Context.Contracts.Models;
using Azure;

namespace PortKisel.Api.Tests.IntegrationTests
{
    public class CargoIntegrationTest : BaseIntegrationTest
    {
        private readonly CompanyZakazchik companyZakazchik;
        public CargoIntegrationTest(PortApiFixture fixture) : base(fixture) 
        {
            companyZakazchik = TestDataGenerator.CompanyZakazchik();

            context.CompanyZakazchiks.Add(companyZakazchik);
            unitOfWork.SaveChangesAsync();
        }

        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var client = factory.CreateClient();
            var cargo = mapper.Map<CreateCargoRequest>(TestDataGenerator.CargoRequestModel());
            cargo.CompanyZakazchikId = companyZakazchik.Id;

            //Act
            string data = JsonConvert.SerializeObject(cargo);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/Cargo", contextdata);
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CargoResponse>(resultString);

            var cargoFirst = await context.Cargos.FirstAsync(x => x.Id == result!.Id);

            //Assert
            cargoFirst.Should()
                .BeEquivalentTo(cargo);
        }

        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var client = factory.CreateClient();
            var cargo = TestDataGenerator.Cargo();

            SetDependeciesOrCargo(cargo);
            await context.Cargos.AddAsync(cargo);
            await unitOfWork.SaveChangesAsync();

            var cargoRequest = mapper.Map<EditCargoRequest>(TestDataGenerator.CargoRequestModel(x => x.Id = cargo.Id));
            SetDependenciesOrCargoRequestModelWithCargo(cargo, cargoRequest);

            //Act
            string data = JsonConvert.SerializeObject(cargoRequest);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            await client.PutAsync("/Cargo", contextdata);

            var cargoFirst = await context.Cargos.FirstAsync(x => x.Id == cargoRequest.Id);
            //Assert
            cargoFirst.Should()
                .BeEquivalentTo(cargoRequest);
        }

        [Fact]
        public async Task GetAllShouldWork()
        {
            // Arrange 
            var client = factory.CreateClient();
            var cargo1 = TestDataGenerator.Cargo();
            var cargo2 = TestDataGenerator.Cargo(x => x.DeletedAt = DateTimeOffset.Now);


            SetDependeciesOrCargo(cargo1);
            SetDependeciesOrCargo(cargo2);

            await context.Cargos.AddRangeAsync(cargo1, cargo2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var respose = await client.GetAsync("/Cargo");

            // Assert
            respose.EnsureSuccessStatusCode();
            var resultString = await respose.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<CargoResponse>>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .Contain(x => x.Id == cargo1.Id)
                .And
                .NotContain(x => x.Id == cargo2.Id);
        }

        [Fact]
        public async void GetShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var cargo1 = TestDataGenerator.Cargo();
            var cargo2 = TestDataGenerator.Cargo();

            SetDependeciesOrCargo(cargo1);
            SetDependeciesOrCargo(cargo2);

            await context.Cargos.AddRangeAsync(cargo1, cargo2);
            await unitOfWork.SaveChangesAsync();
            // Act 
            var response = await client.GetAsync($"/Cargo/{cargo1.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CargoResponse>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    cargo1.Id,
                    cargo1.Name,
                    cargo1.Weight
                });
        }

        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var cargo = TestDataGenerator.Cargo();

            SetDependeciesOrCargo(cargo);
            await context.Cargos.AddAsync(cargo);
            await unitOfWork.SaveChangesAsync();

            // Act
            await client.DeleteAsync($"/Cargo/{cargo.Id}");

            var cargoFirst = await context.Cargos.FirstAsync(x => x.Id == cargo.Id);

            // Assert
            cargoFirst.DeletedAt.Should()
                .NotBeNull();

            cargoFirst.Should()
                .BeEquivalentTo(new
                {
                    cargo.Name,
                    cargo.Weight,
                    cargo.Description,
                    cargo.CompanyZakazchikId
                });
        }

        private void SetDependeciesOrCargo(Cargo cargo)
        {
            cargo.CompanyZakazchikId = companyZakazchik.Id;
        }

        private void SetDependenciesOrCargoRequestModelWithCargo(Cargo cargo, EditCargoRequest editCargoRequest)
        {
            editCargoRequest.CompanyZakazchikId = cargo.CompanyZakazchikId;
        }
    }
}
