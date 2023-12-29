using AutoMapper;
using PortKisel.Services.AutoMappers;
using Xunit;

namespace PortKisel.Services.Tests
{
    public class MapperTest
    {
        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void TestMap()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });

            config.AssertConfigurationIsValid();
        }
    }
}
