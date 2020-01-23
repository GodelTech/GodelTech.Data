using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace GodelTech.Data.Tests.Fakes
{
    [ExcludeFromCodeCoverage]
    public class FakeDataMapper : IDataMapper
    {
        public IQueryable<TDestination> Map<TDestination>(IQueryable source)
        {
            throw new Exception("Map is fake method!");
        }
    }
}