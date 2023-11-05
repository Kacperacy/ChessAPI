using System.Reflection;

namespace ArchitectureTests.Presentation;

public class PresentationTests
{
    private static readonly Assembly PresentationAssembly = typeof(Chess.Presentation.AssemblyReference).Assembly;

    [Fact]
    public void Controllers_Should_HaveDependencyOnMediatR()
    {
        // Arrange

        // Act
        var result = Types
            .InAssembly(PresentationAssembly)
            .That()
            .HaveNameEndingWith("Controller")
            .Should()
            .HaveDependencyOn("MediatR")
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }
}