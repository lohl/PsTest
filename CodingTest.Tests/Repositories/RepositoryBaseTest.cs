using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodingTest;
using CodingTest.Controllers;
using CodingTest.Models;
using CodingTest.Repositories;
using Moq;

namespace CodingTest.Tests.Repositories
{
    [TestClass]
    public class MagRepositoryTest
    {
        private Mock<DataContext> _mockContext;
        private MagRepository _sut;

        [TestInitialize]
        public void Initialize()
        {
            _mockContext = new Mock<DataContext>();
            _sut = new MagRepository(_mockContext.Object);


        }
        [TestMethod]
        public async Task Should_get_all_items()
        {
            var data = new List<Reading>()
            {
                new Reading { Id = 1, MagX = 1, MagY = 2, MaxZ = 3},
                new Reading { Id = 2, MagX = 3.4m, MagY = 2.9m, MaxZ = 5.6m },
                new Reading { Id = 3, MagX = 9.9m, MagY = 10m, MaxZ = 0 }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Reading>>();
            mockSet.As<IQueryable<Reading>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Reading>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Reading>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Reading>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mockSet.As<IDbAsyncEnumerable<Reading>>().Setup(m => m.GetAsyncEnumerator()).Returns(new TestDbAsyncEnumerator<Reading>(data.GetEnumerator()));
            _mockContext.Setup(m => m.Set<Reading>()).Returns(mockSet.Object);

            List<Reading> readings = await _sut.GetAll();

            Assert.AreEqual(3, readings.Count);
        }

        [TestMethod]
        public async Task Should_get_by_id()
        {
            var data = new List<Reading>()
            {
                new Reading { Id = 1, MagX = 1, MagY = 2, MaxZ = 3},
                new Reading { Id = 2, MagX = 3.4m, MagY = 2.9m, MaxZ = 5.6m },
                new Reading { Id = 3, MagX = 9.9m, MagY = 10m, MaxZ = 0 }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Reading>>();
            mockSet.As<IQueryable<Reading>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Reading>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Reading>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Reading>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mockSet.As<IDbAsyncEnumerable<Reading>>().Setup(m => m.GetAsyncEnumerator()).Returns(new TestDbAsyncEnumerator<Reading>(data.GetEnumerator()));
            mockSet.Setup(t => t.FindAsync(It.IsAny<int>())).Returns(Task.FromResult(new Reading() { Id = 2, MagY = 2.7m}));
            _mockContext.Setup(m => m.Set<Reading>()).Returns(mockSet.Object);

            var reading = await _sut.Get(2);

            Assert.AreEqual(2, reading.Id);
            Assert.AreEqual(2.7M, reading.MagY);
        }

        internal class TestDbAsyncEnumerator<T> : IDbAsyncEnumerator<T>
        {
            private readonly IEnumerator<T> _inner;

            public TestDbAsyncEnumerator(IEnumerator<T> inner)
            {
                _inner = inner;
            }

            public void Dispose()
            {
                _inner.Dispose();
            }

            public Task<bool> MoveNextAsync(CancellationToken cancellationToken)
            {
                return Task.FromResult(_inner.MoveNext());
            }

            public T Current
            {
                get { return _inner.Current; }
            }

            object IDbAsyncEnumerator.Current
            {
                get { return Current; }
            }
        }

        internal class TestDbAsyncQueryProvider<TEntity> : IDbAsyncQueryProvider
        {
            private readonly IQueryProvider _inner;

            internal TestDbAsyncQueryProvider(IQueryProvider inner)
            {
                _inner = inner;
            }

            public IQueryable CreateQuery(Expression expression)
            {
                return new TestDbAsyncEnumerable<TEntity>(expression);
            }

            public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
            {
                return new TestDbAsyncEnumerable<TElement>(expression);
            }

            public object Execute(Expression expression)
            {
                return _inner.Execute(expression);
            }

            public TResult Execute<TResult>(Expression expression)
            {
                return _inner.Execute<TResult>(expression);
            }

            public Task<object> ExecuteAsync(Expression expression, CancellationToken cancellationToken)
            {
                return Task.FromResult(Execute(expression));
            }

            public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
            {
                return Task.FromResult(Execute<TResult>(expression));
            }
        }

        public class TestDbAsyncEnumerable<T> : EnumerableQuery<T>, IDbAsyncEnumerable<T>, IQueryable<T>
        {
            public TestDbAsyncEnumerable(IEnumerable<T> enumerable) : base(enumerable)
            { }

            public TestDbAsyncEnumerable(Expression expression) : base(expression)
            { }

            public IDbAsyncEnumerator<T> GetAsyncEnumerator()
            {
                return new TestDbAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
            }

            IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
            {
                return GetAsyncEnumerator();
            }

            IQueryProvider IQueryable.Provider
            {
                get { return new TestDbAsyncQueryProvider<T>(this); }
            }
        }

    }
}
