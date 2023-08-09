

using FlowLearningPlatform.Data;
using FlowLearningPlatform.Models;
using FlowLearningPlatform.Models.Enum;
using FlowLearningPlatform.Services;
using Microsoft.EntityFrameworkCore;

namespace FlowLearningPlatform.Test
{
	public class UnitServiceTests
	{
		[Test]
		public void Test1()
		{
			Assert.Pass();
		}
	}

    [TestFixture]
    public class SchoolServiceTests
	{

		private Mock<IDbContextFactory<DataContext>> _mockDbFactory;
		private readonly List<SchoolType> _seedData = new List<SchoolType>
		{
			new () { SchoolTypeId=Guid.Parse("62386336-0F0E-42D3-8FEA-7112DEF4EA26"),Name="学院1"},
			new () { SchoolTypeId = Guid.Parse("62386336-0F0E-42D3-8FEA-7112DEF4EA27"), Name = "学院2" },
		};
        [SetUp]
		public void SetUp() 
		{
            // ARRANGE
            _mockDbFactory = new Mock<IDbContextFactory<DataContext>>();
            var options = new DbContextOptionsBuilder<DataContext>()
                    .UseInMemoryDatabase(databaseName: "SomeDatabaseInMemory")
                    .Options;
            using (var context = new DataContext(options))
            {
                context.SchoolTypes.AddRange(_seedData);
                context.SaveChanges();
            }
            _mockDbFactory.Setup(f => f.CreateDbContextAsync(It.IsAny<CancellationToken>())).ReturnsAsync(() => new DataContext(options));
        }

        [Test]
		public async Task GetAllAsync_ReturnsListOfSchoolTypes()
        {
            // ACT
            var serviceThatNeedsDbContextFactory = new SchoolService(_mockDbFactory.Object);
			var result = await serviceThatNeedsDbContextFactory.GetAllAsync();
            Assert.That(result,Has.Count.EqualTo(2));
            Assert.Multiple(() =>
            {
                // ASSERT
                Assert.That(result[0].Name, Is.AnyOf( "学院1", "学院2" ));
                Assert.That(result[1].Name, Is.AnyOf("学院1", "学院2"));
            });
        }

       
        }
}