using FluentValidation;

namespace Application.Features.BlackLists.Commands.Create;

public class CreateBlackListCommandValidator : AbstractValidator<CreateBlackListCommand>
{
    public CreateBlackListCommandValidator()
    {
        RuleFor(c => c.Reason).NotEmpty();
        RuleFor(c => c.Date).NotEmpty();
        RuleFor(c => c.ApplicantId).NotEmpty();
    }
}
