using System;
using GodelTech.Data.Tests.Fakes;
using Moq;
using Xunit;

namespace GodelTech.Data.Tests.Specifications
{
    public abstract class SpecificationBaseTests
    {
        protected abstract Func<bool, bool, bool> Func { get; }

        protected abstract Specification<TEntity, TKey> CreateSpecification<TEntity, TKey>(
            Specification<TEntity, TKey> left,
            Specification<TEntity, TKey> right)
            where TEntity : class, IEntity<TKey>;

        public static TheoryData<Guid, SpecificationTestDataModel<Guid>> SpecificationGuidTestData =>
            new TheoryData<Guid, SpecificationTestDataModel<Guid>>
            {
                {
                    default,
                    new SpecificationTestDataModel<Guid>
                    {
                        Entity = new FakeEntity<Guid>
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000001"),
                            Name = "TestName"
                        },
                        LeftExpression = entity => entity.Id == new Guid("00000000-0000-0000-0000-000000000001"),
                        RightExpression = entity => entity.Name == "TestName"
                    }
                },
                {
                    default,
                    new SpecificationTestDataModel<Guid>
                    {
                        Entity = new FakeEntity<Guid>
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000001"),
                            Name = "TestName"
                        },
                        LeftExpression = entity => entity.Id == new Guid("00000000-0000-0000-0000-000000000001"),
                        RightExpression = entity => entity.Name == "Other TestName"
                    }
                },
                {
                    default,
                    new SpecificationTestDataModel<Guid>
                    {
                        Entity = new FakeEntity<Guid>
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000001"),
                            Name = "TestName"
                        },
                        LeftExpression = entity => entity.Id == new Guid("00000000-0000-0000-0000-000000000002"),
                        RightExpression = entity => entity.Name == "TestName"
                    }
                },
                {
                    default,
                    new SpecificationTestDataModel<Guid>
                    {
                        Entity = new FakeEntity<Guid>
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000001"),
                            Name = "TestName"
                        },
                        LeftExpression = entity => entity.Id == new Guid("00000000-0000-0000-0000-000000000002"),
                        RightExpression = entity => entity.Name == "Other TestName"
                    }
                }
            };

        public static TheoryData<int, SpecificationTestDataModel<int>> SpecificationIntTestData =>
            new TheoryData<int, SpecificationTestDataModel<int>>
            {
                {
                    default,
                    new SpecificationTestDataModel<int>
                    {
                        Entity = new FakeEntity<int>
                        {
                            Id = 1,
                            Name = "TestName"
                        },
                        LeftExpression = entity => entity.Id == 1,
                        RightExpression = entity => entity.Name == "TestName"
                    }
                },
                {
                    default,
                    new SpecificationTestDataModel<int>
                    {
                        Entity = new FakeEntity<int>
                        {
                            Id = 1,
                            Name = "TestName"
                        },
                        LeftExpression = entity => entity.Id == 1,
                        RightExpression = entity => entity.Name == "Other TestName"
                    }
                },
                {
                    default,
                    new SpecificationTestDataModel<int>
                    {
                        Entity = new FakeEntity<int>
                        {
                            Id = 1,
                            Name = "TestName"
                        },
                        LeftExpression = entity => entity.Id == 2,
                        RightExpression = entity => entity.Name == "TestName"
                    }
                },
                {
                    default,
                    new SpecificationTestDataModel<int>
                    {
                        Entity = new FakeEntity<int>
                        {
                            Id = 1,
                            Name = "TestName"
                        },
                        LeftExpression = entity => entity.Id == 2,
                        RightExpression = entity => entity.Name == "Other TestName"
                    }
                }
            };

        public static TheoryData<string, SpecificationTestDataModel<string>> SpecificationStringTestData =>
            new TheoryData<string, SpecificationTestDataModel<string>>
            {
                {
                    string.Empty,
                    new SpecificationTestDataModel<string>
                    {
                        Entity = new FakeEntity<string>
                        {
                            Id = "TestId",
                            Name = "TestName"
                        },
                        LeftExpression = entity => entity.Id == "TestId",
                        RightExpression = entity => entity.Name == "TestName"
                    }
                },
                {
                    string.Empty,
                    new SpecificationTestDataModel<string>
                    {
                        Entity = new FakeEntity<string>
                        {
                            Id = "TestId",
                            Name = "TestName"
                        },
                        LeftExpression = entity => entity.Id == "TestId",
                        RightExpression = entity => entity.Name == "Other TestName"
                    }
                },
                {
                    string.Empty,
                    new SpecificationTestDataModel<string>
                    {
                        Entity = new FakeEntity<string>
                        {
                            Id = "TestId",
                            Name = "TestName"
                        },
                        LeftExpression = entity => entity.Id == "OtherTestId",
                        RightExpression = entity => entity.Name == "TestName"
                    }
                },
                {
                    string.Empty,
                    new SpecificationTestDataModel<string>
                    {
                        Entity = new FakeEntity<string>
                        {
                            Id = "TestId",
                            Name = "TestName"
                        },
                        LeftExpression = entity => entity.Id == "OtherTestId",
                        RightExpression = entity => entity.Name == "Other TestName"
                    }
                }
            };

        [Theory]
        [MemberData(nameof(SpecificationGuidTestData))]
        [MemberData(nameof(SpecificationIntTestData))]
        [MemberData(nameof(SpecificationStringTestData))]
        public void AsExpression_Success<TKey, TSpecificationTestDataModel>(
            TKey id,
            TSpecificationTestDataModel model)
            where TSpecificationTestDataModel : SpecificationTestDataModel<TKey>
        {
            // Arrange
            var leftSpecification = new Mock<Specification<FakeEntity<TKey>, TKey>>(MockBehavior.Strict);
            leftSpecification
                .Setup(x => x.AsExpression())
                .Returns(model.LeftExpression);

            var rightSpecification = new Mock<Specification<FakeEntity<TKey>, TKey>>(MockBehavior.Strict);
            rightSpecification
                .Setup(x => x.AsExpression())
                .Returns(model.RightExpression);

            // Act
            var result = CreateSpecification(leftSpecification.Object, rightSpecification.Object).AsExpression();

            // Assert
            Assert.NotNull(id);
            Assert.Equal(
                Func
                    .Invoke(
                        model.LeftExpression.Compile().Invoke(model.Entity),
                        model.RightExpression.Compile().Invoke(model.Entity)
                    ),
                result.Compile().Invoke(model.Entity)
            );
        }
    }
}
