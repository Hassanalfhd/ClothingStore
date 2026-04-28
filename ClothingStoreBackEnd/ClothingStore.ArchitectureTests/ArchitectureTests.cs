using System.Reflection;
using FluentAssertions;
using NetArchTest.Rules;

namespace ClothingStore.ArchitectureTests;
public class ArchitectureTests
{
    private const string DomainNamespace = "ClothingStore.Domain";
    private const string ApplicationNamespace = "ClothingStore.Application";
    private const string InfrastructureNamespace = "ClothingStore.Infrastructure";
    private const string PresentationNamespace = "ClothingStore.Api";

    [Fact]
    public void Domain_Should_Not_Have_Dependency_On_Other_Layers()
    {
        //1. Arrange
        var assembly = Assembly.Load(DomainNamespace);

        //2.Act
        var otherLayers = new[]
        {
            ApplicationNamespace,
            InfrastructureNamespace,
            PresentationNamespace
        };


        var result = Types.InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherLayers)
            .GetResult();

        //3.Assert 
        result.IsSuccessful.Should().BeTrue("Domain Does not have dependency with any extarnal resource");
    }

    [Fact]
    public void Application_Should_Not_Have_Dependency_On_Infrastructure()
    {
        var assembly = Assembly.Load(ApplicationNamespace);

        var result = Types.InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOn(InfrastructureNamespace)
            .GetResult();

        result.IsSuccessful.Should().BeTrue("Application has to depend on Interfaces not Implementation");
    }


    [Fact]
    public void Controllers_Should_Not_Have_Logic_Inside_Them()
    {
        var assembly = Assembly.Load(PresentationNamespace);

        var result = Types.InAssembly(assembly)
            .That()
            .HaveNameEndingWith("Controller")
            .Should()
            .HaveDependencyOn(ApplicationNamespace)
            .GetResult();


        result.IsSuccessful.Should().BeTrue("Just Application can pass to Controllers.");

    }



    [Fact]
    public void All_Interfaces_Should_Start_With_I()
    {
        var assembly = Assembly.Load(ApplicationNamespace);

        var result = Types.InAssembly(assembly)
            .That()
            .AreInterfaces()
            .Should()
            .HaveNameStartingWith("I")
            .GetResult();


        result.IsSuccessful.Should().BeTrue("Interfaces should start with I char.");


    }
}
