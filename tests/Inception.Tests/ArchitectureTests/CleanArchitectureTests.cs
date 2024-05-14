using FluentAssertions;
using InceptionClean.Api.Controllers;
using InceptionClean.Application.Abstractions;
using InceptionClean.Domain.Entities;
using InceptionClean.Infrastructure.DbContexts;
using NetArchTest.Rules;
using System.Reflection;

namespace Inception.Tests.ArchitectureTests;

public class CleanArchitectureTests
{
    private readonly Assembly DomainAssembly = typeof(Person).Assembly;
    private readonly Assembly ApplicationAssembly = typeof(IPersonRepository).Assembly;
    private readonly Assembly InfrastructureAssembly = typeof(PersonDbContext).Assembly;
    private readonly Assembly ApiAssembly = typeof(PersonController).Assembly;

    [Fact]
    public void DomainLayer_Should_Be_Independent_Layer()
    {
        var result = Types.InAssembly(DomainAssembly)
            .Should()
            .NotHaveDependencyOn(ApplicationAssembly.GetName().Name)
            .And()
            .NotHaveDependencyOn(InfrastructureAssembly.GetName().Name)
            .And()
            .NotHaveDependencyOn(ApiAssembly.GetName().Name)
            .GetResult();
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void ApplicationLayer_Should_HaveDependencyOn_DomainLayer()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .Should()
            .NotHaveDependencyOn(InfrastructureAssembly.GetName().Name)
            .And()
            .NotHaveDependencyOn(ApiAssembly.GetName().Name)
            .GetResult();
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void InfrastrucutureLayer_Should_NotHaveDependencyOn_ApiLayer()
    {
        var result = Types.InAssembly(InfrastructureAssembly)
            .Should()
            //.HaveDependencyOn(DomainAssembly.GetName().Name)
            //.And()
            //.HaveDependencyOn(ApplicationAssembly.GetName().Name)
            //.And()
            .NotHaveDependencyOn(ApiAssembly.GetName().Name)
            .GetResult();
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void ApiLayer_Should_NotHaveDependencyOn_DomainLayer()
    {
        var result = Types.InAssembly(ApiAssembly)
            .Should()
            //.HaveDependencyOn(InfrastructureAssembly.GetName().Name)
            //.And()
            //.HaveDependencyOn(ApplicationAssembly.GetName().Name)
            //.And()
            .NotHaveDependencyOn(DomainAssembly.GetName().Name)
            .GetResult();
        result.IsSuccessful.Should().BeTrue();
    }

    //
    //[Fact]
    //public void DomainLayer_Should_Sealed_ApplicationLayer()
    //{
    //    var result = Types.InAssembly(DomainAssembly)
    //        .That()
    //        .AreClasses()
    //        .Should()
    //        .BeSealed()
    //        .GetResult();
    //    result.IsSuccessful.Should().BeTrue();
    //    Assert.True(result.IsSuccessful);
    //}
    //
    //[Fact]
    //public void DomainLayer_Should_NotHaveDependencyOn_InfrastructureLayer()
    //{
    //    var result = Types.InAssembly(DomainAssembly)
    //        .Should()
    //        .NotHaveDependencyOn(InfrastructureAssembly.GetName().Name)
    //        .GetResult();
    //    result.IsSuccessful.Should().BeTrue();
    //}

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