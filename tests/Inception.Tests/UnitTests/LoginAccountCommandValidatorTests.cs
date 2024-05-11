using AutoFixture;
using AutoFixture.AutoMoq;
using FluentValidation;
using FluentValidation.TestHelper;
using Inception.Application.Features.LoginAccount;

namespace Inception.Tests.UnitTests;
public class LoginAccountCommandValidatorTests
{
    private readonly IFixture _fixture = new Fixture().Customize(new AutoMoqCustomization());
    private readonly LoginAccountCommand _command;
    private readonly IValidator<LoginAccountCommand> _validator;

    public LoginAccountCommandValidatorTests()
    {
        _command = _fixture.Create<LoginAccountCommand>();
        _validator = _fixture.Create<LoginAccountCommandValidator>();
    }

    [Fact]
    public void Given_a_valid_event_when_all_fields_are_valid_should_pass_validation()
    {
        _validator
            .TestValidate(_command)
            .ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Given_a_request_with_invalid_username_should_fail_validation()
    {
        _validator
            .TestValidate(_command with { Username = string.Empty })
            .ShouldHaveValidationErrorFor(x => x.Username)
            .Only();
    }

    [Fact]
    public void Given_a_request_with_invalid_password_should_fail_validation()
    {
        _validator
            .TestValidate(_command with { Password = string.Empty })
            .ShouldHaveValidationErrorFor(x => x.Password)
            .Only();
    }
}