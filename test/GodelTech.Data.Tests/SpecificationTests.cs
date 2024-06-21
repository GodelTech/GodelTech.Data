using System;
using GodelTech.Data.Specifications;
using GodelTech.Data.Tests.Fakes;
using GodelTech.Data.Tests.Specifications;
using Moq;
using Xunit;

namespace GodelTech.Data.Tests
{
    public class SpecificationTests
    {
        public static TheoryData<Guid, IEntity<Guid>, bool> IsSatisfiedGuidTestData =>
            new TheoryData<Guid, IEntity<Guid>, bool>
            {
                {
                    new Guid("00000000-0000-0000-0000-000000000001"),
                    new FakeEntity<Guid>(),
                    false
                },
                {
                    new Guid("00000000-0000-0000-0000-000000000001"),
                    new FakeEntity<Guid>
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000002")
                    },
                    false
                },
                {
                    new Guid("00000000-0000-0000-0000-000000000001"),
                    new FakeEntity<Guid>
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000001")
                    },
                    true
                }
            };

        public static TheoryData<int, IEntity<int>, bool> IsSatisfiedIntTestData =>
            new TheoryData<int, IEntity<int>, bool>
            {
                {
                    1,
                    new FakeEntity<int>(),
                    false
                },
                {
                    1,
                    new FakeEntity<int>
                    {
                        Id = 2
                    },
                    false
                },
                {
                    1,
                    new FakeEntity<int>
                    {
                        Id = 1
                    },
                    true
                }
            };

        public static TheoryData<string, IEntity<string>, bool> IsSatisfiedStringTestData =>
            new TheoryData<string, IEntity<string>, bool>
            {
                {
                    "TestId",
                    new FakeEntity<string>(),
                    false
                },
                {
                    "TestId",
                    new FakeEntity<string>
                    {
                        Id = "Other TestId"
                    },
                    false
                },
                {
                    "TestId",
                    new FakeEntity<string>
                    {
                        Id = "TestId"
                    },
                    true
                }
            };

        [Theory]
        [MemberData(nameof(IsSatisfiedGuidTestData))]
        [MemberData(nameof(IsSatisfiedIntTestData))]
        [MemberData(nameof(IsSatisfiedStringTestData))]
        public void IsSatisfiedBy_Success<TEntity, TKey>(
            TKey id,
            TEntity entity,
            bool expectedResult)
            where TEntity : class, IEntity<TKey>
        {
            // Arrange
            var specification = new FakeSpecification<TEntity, TKey>(x => x.Id != null && x.Id.Equals(id));

            // Act
            var result = specification.IsSatisfiedBy(entity);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [MemberData(nameof(SpecificationBaseTests.SpecificationGuidTestData), MemberType = typeof(SpecificationBaseTests))]
        public void And_Success<TKey, TSpecificationTestDataModel>(
            TKey id,
            TSpecificationTestDataModel model)
            where TSpecificationTestDataModel : SpecificationTestDataModel<TKey>
        {
            Method_Success(
                id,
                model,
                (specification, other) => specification.And(other),
                (left, right) => new AndSpecification<FakeEntity<TKey>, TKey>(left, right)
            );
        }

        [Theory]
        [MemberData(nameof(SpecificationBaseTests.SpecificationGuidTestData), MemberType = typeof(SpecificationBaseTests))]
        public void AndNot_Success<TKey, TSpecificationTestDataModel>(
            TKey id,
            TSpecificationTestDataModel model)
            where TSpecificationTestDataModel : SpecificationTestDataModel<TKey>
        {
            Method_Success(
                id,
                model,
                (specification, other) => specification.AndNot(other),
                (left, right) => new AndNotSpecification<FakeEntity<TKey>, TKey>(left, right)
            );
        }

        [Theory]
        [MemberData(nameof(SpecificationBaseTests.SpecificationGuidTestData), MemberType = typeof(SpecificationBaseTests))]
        public void Or_Success<TKey, TSpecificationTestDataModel>(
            TKey id,
            TSpecificationTestDataModel model)
            where TSpecificationTestDataModel : SpecificationTestDataModel<TKey>
        {
            Method_Success(
                id,
                model,
                (specification, other) => specification.Or(other),
                (left, right) => new OrSpecification<FakeEntity<TKey>, TKey>(left, right)
            );
        }

        [Theory]
        [MemberData(nameof(SpecificationBaseTests.SpecificationGuidTestData), MemberType = typeof(SpecificationBaseTests))]
        public void OrNot_Success<TKey, TSpecificationTestDataModel>(
            TKey id,
            TSpecificationTestDataModel model)
            where TSpecificationTestDataModel : SpecificationTestDataModel<TKey>
        {
            Method_Success(
                id,
                model,
                (specification, other) => specification.OrNot(other),
                (left, right) => new OrNotSpecification<FakeEntity<TKey>, TKey>(left, right)
            );
        }

        [Theory]
        [MemberData(nameof(SpecificationBaseTests.SpecificationGuidTestData), MemberType = typeof(SpecificationBaseTests))]
        public void Not_Success<TKey, TSpecificationTestDataModel>(
            TKey id,
            TSpecificationTestDataModel model)
            where TSpecificationTestDataModel : SpecificationTestDataModel<TKey>
        {
            Method_Success(
                id,
                model,
                (specification, _) => specification.Not(),
                (left, _) => new NotSpecification<FakeEntity<TKey>, TKey>(left)
            );
        }

        private static void Method_Success<TKey, TSpecificationTestDataModel>(
            TKey id,
            TSpecificationTestDataModel model,
            Func<Specification<FakeEntity<TKey>, TKey>, Specification<FakeEntity<TKey>, TKey>, Specification<FakeEntity<TKey>, TKey>> method,
            Func<Specification<FakeEntity<TKey>, TKey>, Specification<FakeEntity<TKey>, TKey>, Specification<FakeEntity<TKey>, TKey>> expectedResult)
            where TSpecificationTestDataModel : SpecificationTestDataModel<TKey>
        {
            // Arrange
            var specification = new FakeSpecification<FakeEntity<TKey>, TKey>(model.LeftExpression);

            var rightSpecification = new Mock<Specification<FakeEntity<TKey>, TKey>>(MockBehavior.Strict);
            rightSpecification
                .Setup(x => x.AsExpression())
                .Returns(model.RightExpression);

            var expectedResultObject = expectedResult(specification, rightSpecification.Object);

            // Act
            var result = method(specification, rightSpecification.Object);

            // Assert
            Assert.NotNull(id);

            Assert.IsType(expectedResultObject.GetType(), result);

            Assert.Equal(
                expectedResultObject.AsExpression().Compile().Invoke(model.Entity),
                result.AsExpression().Compile().Invoke(model.Entity)
            );
        }
    }
}
