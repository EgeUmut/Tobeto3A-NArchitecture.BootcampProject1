using NArchitecture.Core.Application.Responses;

namespace Application.Features.Bootcamps.Commands.Update;

public class UpdatedBootcampResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Guid InstructorId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public short BootcampStateId { get; set; }
}
