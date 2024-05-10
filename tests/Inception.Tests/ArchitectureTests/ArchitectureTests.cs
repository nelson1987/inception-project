using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Inception.Api.Features.Account.Login;
using Inception.Api.Features.ContasBancarias;
using Inception.Database;
using Inception.Domain;
using NetArchTest.Rules;
using System.Reflection;

namespace Inception.Tests.Integrations;

public class ArchitectureTests
{
    private readonly Assembly DomainAssembly = typeof(User).Assembly;
    private readonly Assembly ApiAssembly = typeof(ContasBancariasController).Assembly;
    private readonly Assembly InfrastructureAssembly = typeof(InceptionDbContext).Assembly;
    [Fact]
    public async Task DomainLayer_Should_NotHaveDependencyOn_ApplicationLayer()
    {
        var result = Types.InAssembly(DomainAssembly)
            .Should()
            .NotHaveDependencyOn(ApiAssembly.GetName().Name)
            .GetResult();
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public async Task DomainLayer_Should_Sealed_ApplicationLayer()
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
    public async Task DomainLayer_Should_NotHaveDependencyOn_InfrastructureLayer()
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