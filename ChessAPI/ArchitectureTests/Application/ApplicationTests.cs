namespace ArchitectureTests.Application;

public class ApplicationTests
{
    private const string DomainNamespace = "Chess.Domain";
    
    [Fact]
    public void Handlers_Should_HaveDependencyOnDomain()
    {
        // Arrange
        var assembly = typeof(Chess.Application.AssemblyReference).Assembly;

        // Act
        var result = Types
            .InAssembly(assembly)
            .That()
            .HaveNameEndingWith("Handler")
            .Should()
            .HaveDependencyOn(DomainNamespace)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

}