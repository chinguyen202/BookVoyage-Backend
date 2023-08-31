using Microsoft.EntityFrameworkCore;
using Moq;

namespace BookVoyage.Tests.Unit.Mocks
{
    public static class DbContextMockHelper
    {
        public static Mock<DbSet<TEntity>> CreateDbSetMock<TEntity>(IEnumerable<TEntity> data) where TEntity : class
        {
            var queryableData = data.AsQueryable();

            var dbSetMock = new Mock<DbSet<TEntity>>();
            dbSetMock.As<IQueryable<TEntity>>().Setup(m => m.Provider).Returns(queryableData.Provider);
            dbSetMock.As<IQueryable<TEntity>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            dbSetMock.As<IQueryable<TEntity>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            dbSetMock.As<IQueryable<TEntity>>().Setup(m => m.GetEnumerator()).Returns(queryableData.GetEnumerator());

            return dbSetMock;
        }
    }
}