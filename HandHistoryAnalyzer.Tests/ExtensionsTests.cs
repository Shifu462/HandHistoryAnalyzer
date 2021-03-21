using System.Collections.Generic;
using System.Linq;
using HandHistoryAnalyzer.Core.Extensions;
using HandHistoryAnalyzer.Core.Querying.Abstractions;
using Xunit;

namespace HandHistoryAnalyzer.Tests
{
    public class ExtensionsTests
    {
        [Fact]
        public void CollectionExtensions_AddRange()
        {
            ICollection<int> collection = new List<int>();
            var appendedCollection = new[] { 1, 2, 3 };

            collection.AddRange(appendedCollection);

            Assert.True(collection.SequenceEqual(appendedCollection), "Unexpected sequence contents.");
        }


        class SomeTypeWithGenericInterface :
            IRequest<int>,
            IRequest<string>
        {
        }

        [Fact]
        public void TypeExtensions_GetGenericInterfaces()
        {
            var genericInterfaces = typeof(SomeTypeWithGenericInterface).GetGenericInterfaces(typeof(IRequest<>)).ToArray();

            Assert.True(
                genericInterfaces.SequenceEqual(new[]
                {
                    typeof(IRequest<int>),
                    typeof(IRequest<string>),
                }),
                "Unexpected interface sequence."
            );
        }
    }
}
