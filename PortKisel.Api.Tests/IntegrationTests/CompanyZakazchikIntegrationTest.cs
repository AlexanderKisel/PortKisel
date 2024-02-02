using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PortKisel.Api.Models;
using PortKisel.Api.ModelsRequest.CompanyZakazchik;
using PortKisel.Api.Tests.Infrastructure;
using PortKisel.Services.Contracts.ModelsRequest;
using PortKisel.Services.Tests;
using System.Text;
using Xunit;

namespace PortKisel.Api.Tests.IntegrationTests
{
    public class CompanyZakazchikIntegrationTest : BaseIntegrationTest
    {
        public CompanyZakazchikIntegrationTest(PortApiFixture fixture) : base(fixture) { }

        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var client = factory.CreateClient();
            var companyZakazchik = mapper.Map<CreateCompanyZakazhikRequest>(TestDataGenerator.CompanyZakazchikRequestModel());

            //Act
            string data = JsonConvert.SerializeObject(companyZakazchik);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/CompanyZakazchik", contextdata);
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CompanyZakazchikResponse>(resultString);

            var companyZakazchikFirst = await context.CompanyZakazchiks.FirstAsync(x => x.Id == result!.Id);

            //Assert
            companyZakazchik.Should()
                .BeEquivalentTo(companyZakazchik);
        }

        [Fact]
        public async Task GetShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var companyZakazchik1 = TestDataGenerator.CompanyZakazchik();
            var companyZakazchik2 = TestDataGenerator.CompanyZakazchik();

            await context.CompanyZakazchiks.AddRangeAsync(companyZakazchik1, companyZakazchik2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync($"/CompanyZakazchik/{companyZakazchik1.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CompanyZakazchikResponse>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    companyZakazchik1.Id,
                    companyZakazchik1.Name
                });
        }

        [Fact]
        public async Task GetAllShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var companyZakazchik1 = TestDataGenerator.CompanyZakazchik();
            var companyZakazchik2 = TestDataGenerator.CompanyZakazchik(x => x.DeletedAt = DateTimeOffset.Now);

            await context.CompanyZakazchiks.AddRangeAsync(companyZakazchik1, companyZakazchik2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync("/CompanyZakazchik");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<CompanyZakazchikResponse>>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .Contain(x => x.Id == companyZakazchik1.Id)
                .And
                .NotContain(x => x.Id == companyZakazchik2.Id);
        }

        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var companyZakazchik = TestDataGenerator.CompanyZakazchik();
            await context.CompanyZakazchiks.AddAsync(companyZakazchik);
            await unitOfWork.SaveChangesAsync();

            var companyZakazchikRequest = mapper.Map<EditCompanyZakazhikRequest>(TestDataGenerator.CompanyZakazchikRequestModel(x => x.Id = companyZakazchik.Id));

            // Act
            string data = JsonConvert.SerializeObject(companyZakazchikRequest);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            await client.PutAsync("/CompanyZakazchik", contextdata);

            var companyPerFirst = await context.CompanyZakazchiks.FirstAsync(x => x.Id == companyZakazchikRequest.Id);

            // Assert           
            companyPerFirst.Should()
                .BeEquivalentTo(companyZakazchikRequest);
        }

        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var companyZakazchik = TestDataGenerator.CompanyZakazchik();
            await context.CompanyZakazchiks.AddAsync(companyZakazchik);
            await unitOfWork.SaveChangesAsync();

            // Act
            await client.DeleteAsync($"/CompanyZakazchik/{companyZakazchik.Id}");

            var companyZakazchikFirst = await context.CompanyZakazchiks.FirstAsync(x => x.Id == companyZakazchik.Id);

            // Assert
            companyZakazchikFirst.DeletedAt.Should()
                .NotBeNull();

            companyZakazchikFirst.Should()
                .BeEquivalentTo(new
                {
                    companyZakazchik.Name
                });
        }
    }
}
