using Application.Features.Authors.Commands;
using FluentValidation.TestHelper;

public class CreateAuthorCommandValidatorTests
{
    private readonly CreateAuthorCommandValidator _validator;

    public CreateAuthorCommandValidatorTests()
    {
        _validator = new CreateAuthorCommandValidator();
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_Empty()
    {
        var command = new CreateAuthorCommand("");

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.Name);
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_Less_Than_3_Characters()
    {
        var command = new CreateAuthorCommand("Jo");

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.Name);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Name_Is_Valid()
    {
        var command = new CreateAuthorCommand("John");

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveValidationErrorFor(c => c.Name);
    }
}
