using System;
using Xunit;

namespace GodelTech.Data.Tests.TestData
{
    public static class TypesTestData
    {
        public static TheoryData<Guid> TypesGuidTestData =>
            new TheoryData<Guid>
            {
                default
            };

        public static TheoryData<int> TypesIntTestData =>
            new TheoryData<int>
            {
                default
            };

        public static TheoryData<string> TypesStringTestData =>
            new TheoryData<string>
            {
                string.Empty
            };
    }
}
