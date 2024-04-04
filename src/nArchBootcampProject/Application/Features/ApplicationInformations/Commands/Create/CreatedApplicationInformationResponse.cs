using NArchitecture.Core.Application.Responses;

namespace Application.Features.ApplicationInformations.Commands.Create;

public class CreatedApplicationInformationResponse : IResponse
{
    public int Id { get; set; }
    public Guid ApplicantId { get; set; }
    public short BootcampId { get; set; }
    public string BootcampName { get; set; }
    public short ApplicationStateId { get; set; }
    public string ApplicationStateName { get; set; }
}
