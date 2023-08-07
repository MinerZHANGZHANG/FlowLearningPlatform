

using FlowLearningPlatform.Data;
using FlowLearningPlatform.Models.Enum;
using FlowLearningPlatform.Services;
using Microsoft.EntityFrameworkCore;
using Moq.EntityFrameworkCore;
using NUnit.Framework;

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

	public class SchoolServiceTests
	{
		[Test]
		public async Task GetAllAsync_ReturnsListOfSchoolTypes()
		{
			// arrange
			var dbContextFactoryMock = new Mock<IDbContextFactory<DataContext>>();
			Mock<DataContext> dbContextMock =new();
			var schoolTypes = new List<SchoolType>
			{
				new(){SchoolTypeId=Guid.Parse("62386336-0F0E-42D3-8FEA-7112DEF4EA26"),Name="学院1"},
				new(){SchoolTypeId=Guid.Parse("62386336-0F0E-42D3-8FEA-7112DEF4EA27"),Name="学院2"},
				new(){SchoolTypeId=Guid.Parse("62386336-0F0E-42D3-8FEA-7112DEF4EA28"),Name="学院3"},
				new(){SchoolTypeId=Guid.Parse("62386336-0F0E-42D3-8FEA-7112DEF4EA29"),Name="学院4"},
			}.AsQueryable();

			var mockDbSet = new Mock<DbSet<SchoolType>>();
			mockDbSet.As<IQueryable<SchoolType>>().Setup(m => m.Provider).Returns(schoolTypes.Provider);
			mockDbSet.As<IQueryable<SchoolType>>().Setup(m => m.Expression).Returns(schoolTypes.Expression);
			mockDbSet.As<IQueryable<SchoolType>>().Setup(m => m.ElementType).Returns(schoolTypes.ElementType);
			mockDbSet.As<IQueryable<SchoolType>>().Setup(m => m.GetEnumerator()).Returns(schoolTypes.GetEnumerator());

			dbContextMock.Setup(m => m.SchoolTypes).Returns(mockDbSet.Object);

			dbContextFactoryMock.Setup(f => f.CreateDbContextAsync(default)).ReturnsAsync(dbContextMock.Object);

			var schoolService = new SchoolService(dbContextFactoryMock.Object);

			// Act
			var result = await schoolService.GetAllAsync();

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Count.EqualTo(4));
			Assert.Multiple(() =>
			{
				Assert.That(result[0].Name, Is.EqualTo("学院1"));
				Assert.That(result[1].Name, Is.EqualTo("学院2"));
			});
		}
	}
}