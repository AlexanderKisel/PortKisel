using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PortKisel.Api.Models;
using PortKisel.Api.ModelsRequest.CompanyPer;
using PortKisel.Api.Tests.Infrastructure;
using PortKisel.Context.Contracts.Models;
using PortKisel.Services.Contracts.ModelsRequest;
using PortKisel.Services.Tests;
using System.Text;
using Xunit;

namespace PortKisel.Api.Tests.IntegrationTests
{
    public class CompanyPerIntegrationTest : BaseIntegrationTest
    {
        public CompanyPerIntegrationTest(PortApiFixture fixture) : base(fixture){ }

        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var client = factory.CreateClient();
            var companyPer = mapper.Map<CreateCompanyPerRequest>(TestDataGenerator.CompanyPerRequestModel());

            //Act
            string data = JsonConvert.SerializeObject(companyPer);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/CompanyPer", contextdata);
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CompanyPerResponse>(resultString);

            var companyPerFirst = await context.CompanyPers.FirstAsync(x => x.Id == result!.Id);

            //Assert
            companyPer.Should()
                .BeEquivalentTo(companyPer);
        }

        [Fact]
        public async Task GetShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var companyPer1 = TestDataGenerator.CompanyPer();
            var companyPer2 = TestDataGenerator.CompanyPer();

            await context.CompanyPers.AddRangeAsync(companyPer1, companyPer2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync($"/CompanyPer/{companyPer1.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CompanyPerResponse>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    companyPer1.Id,
                    companyPer1.Name
                });
        }

        [Fact]
        public async Task GetAllShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var companyPer1 = TestDataGenerator.CompanyPer();
            var companyPer2 = TestDataGenerator.CompanyPer(x => x.DeletedAt = DateTimeOffset.Now);

            await context.CompanyPers.AddRangeAsync(companyPer1, companyPer2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync("/CompanyPer");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<CompanyPerResponse>>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .Contain(x => x.Id == companyPer1.Id)
                .And
                .NotContain(x => x.Id == companyPer2.Id);
        }

        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var companyPer = TestDataGenerator.CompanyPer();
            await context.CompanyPers.AddAsync(companyPer);
            await unitOfWork.SaveChangesAsync();

            var companyPerRequest = mapper.Map<EditCompanyPerRequest>(TestDataGenerator.CompanyPerRequestModel(x => x.Id = companyPer.Id));

            // Act
            string data = JsonConvert.SerializeObject(companyPerRequest);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            await client.PutAsync("/CompanyPer", contextdata);

            var companyPerFirst = await context.CompanyPers.FirstAsync(x => x.Id == companyPerRequest.Id);

            // Assert           
            companyPerFirst.Should()
                .BeEquivalentTo(companyPerRequest);
        }

        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var companyPer = TestDataGenerator.CompanyPer();
            await context.CompanyPers.AddAsync(companyPer);
            await unitOfWork.SaveChangesAsync();

            // Act
            await client.DeleteAsync($"/CompanyPer/{companyPer.Id}");

            var companyPerFirst = await context.CompanyPers.FirstAsync(x => x.Id == companyPer.Id);

            // Assert
            companyPerFirst.DeletedAt.Should()
                .NotBeNull();

            companyPerFirst.Should()
                .BeEquivalentTo(new
                {
                    companyPer.Name
                });
        }
    }
}
