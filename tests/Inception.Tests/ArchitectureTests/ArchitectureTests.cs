using FluentAssertions;
using Inception.Api.Features.ContasBancarias;
using NetArchTest.Rules;
using System.Reflection;

namespace Inception.Tests.ArchitectureTests;

public class ArchitectureTests
{
    private readonly Assembly DomainAssembly = typeof(User).Assembly;
    private readonly Assembly ApiAssembly = typeof(ContasBancariasController).Assembly;
    private readonly Assembly InfrastructureAssembly = typeof(Inception.Infrastructure.Persistence.InceptionDbContext).Assembly;
    [Fact]
    public void DomainLayer_Should_NotHaveDependencyOn_ApplicationLayer()
    {
        var result = Types.InAssembly(DomainAssembly)
            .Should()
            .NotHaveDependencyOn(ApiAssembly.GetName().Name)
            .GetResult();
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void DomainLayer_Should_Sealed_ApplicationLayer()
    {
        var result = Types.InAssembly(DomainAssembly)
            .That()
            .AreClasses()
            .Should()
            .BeSealed()
            .GetResult();
        result.IsSuccessful.Should().BeTrue();
        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void DomainLayer_Should_NotHaveDependencyOn_InfrastructureLayer()
    {
        var result = Types.InAssembly(DomainAssembly)
            .Should()
            .NotHaveDependencyOn(InfrastructureAssembly.GetName().Name)
            .GetResult();
        result.IsSuccessful.Should().BeTrue();
    }

    //[Fact]
    //public async Task CommandHanlder_Should_HaveNameEditingWith_CommandHandler()
    //{
    //    var result = Types.InAssembly(ApiAssembly)
    //        .That()
    //        .ImplementInterface(typeof(ICommandHandler<>))
    //        .Or()
    //        .ImplementInterface(typeof(ICommandHandler<,>))
    //        .Should().HaveNameEndingWith("CommandHanlder")
    //        .GetResults();
    //    result.IsSuccessful.Should().BeTrue();
    //}
}