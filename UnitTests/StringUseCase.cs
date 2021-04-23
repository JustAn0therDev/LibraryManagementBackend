using UseCases;
using Xunit;

namespace UnitTests
{
    public class StringUseCaseTest
    {
        [Fact]
        public void StringUseCaseMethodDoesNotReturnNull()
        {
            IUseCase useCase = new StringUseCase();

            Assert.NotNull(useCase.UseCaseMethod());
        }

        [Fact]
        public void TestUseCaseMethodForStringShouldEqual()
        {
            IUseCase useCase = new StringUseCase();

            Assert.Equal("string...?", useCase.UseCaseMethod().ToString());
        }

    }
}
