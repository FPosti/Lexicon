using BookTestingDemo;
using Xunit;

namespace BookTestingDemo.Tests;

public class SetupTests
{
    [Fact]
    public void ProjectReference_CanCreateCalculator()
    {
        var calculator = new Calculator();

        Assert.NotNull(calculator);
    }
}
