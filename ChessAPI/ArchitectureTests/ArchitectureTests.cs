using System.Reflection;

namespace ArchitectureTests;

public class ArchitectureTests
{
    private const string DomainNamespace = "Chess.Domain";
    private const string ApplicationNamespace = "Chess.Application";
    private const string InfrastructureNamespace = "Chess.Infrastructure";
    private const string PresentationNamespace = "Chess.Presentation";
    private const string WebNamespace = "Chess.API";
    
    private static readonly Assembly DomainAssembly = typeof(Chess.Domain.AssemblyReference).Assembly;
    private static readonly Assembly ApplicationAssembly = typeof(Chess.Application.AssemblyReference).Assembly;
    private static readonly Assembly InfrastructureAssembly = typeof(Chess.Infrastructure.AssemblyReference).Assembly;
    private static readonly Assembly PresentationAssembly = typeof(Chess.Presentation.AssemblyReference).Assembly;
    private static readonly Assembly WebAssembly = typeof(Chess.API.AssemblyReference).Assembly;
    
    [Fact]
    public void Domain_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange
        var otherProjects = new[]
        {
            ApplicationNamespace,
            InfrastructureNamespace,
            PresentationNamespace,
            WebNamespace
        };

        // Act
        var result = Types
            .InAssembly(DomainAssembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Application_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange
        var otherProjects = new[]
        {
            InfrastructureNamespace,
            PresentationNamespace,
            WebNamespace
        };

        // Act
        var result = Types
            .InAssembly(ApplicationAssembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Infrastructure_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange
        var otherProjects = new[]
        {
            PresentationNamespace,
            WebNamespace
        };

        // Act
        var result = Types
            .InAssembly(InfrastructureAssembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Presentation_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange
        var otherProjects = new[]
        {
            InfrastructureNamespace,
            WebNamespace
        };

        // Act
        var result = Types
            .InAssembly(PresentationAssembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }
}
