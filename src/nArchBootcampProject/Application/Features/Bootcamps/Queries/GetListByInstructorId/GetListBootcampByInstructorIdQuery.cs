using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using static Application.Features.Bootcamps.Constants.BootcampsOperationClaims;

namespace Application.Features.Bootcamps.Queries.GetList;

public class GetListBootcampByInstructorIdQuery : IRequest<GetListResponse<GetListBootcampListItemDto>> , ISecuredRequest
{
    public PageRequest PageRequest { get; set; }
    public Guid InstructorId { get; set; }
    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListBootcamps({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetBootcamps";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListBootcampByInstructorIdQueryHandler
        : IRequestHandler<GetListBootcampByInstructorIdQuery, GetListResponse<GetListBootcampListItemDto>>
    {
        private readonly IBootcampRepository _bootcampRepository;
        private readonly IMapper _mapper;

        public GetListBootcampByInstructorIdQueryHandler(IBootcampRepository bootcampRepository, IMapper mapper)
        {
            _bootcampRepository = bootcampRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListBootcampListItemDto>> Handle(
            GetListBootcampByInstructorIdQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<Bootcamp> bootcamps = await _bootcampRepository.GetListAsync(
                predicate: x => x.InstructorId == request.InstructorId,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: p => p.Include(x => x.Instructor).Include(p => p.BootcampState).Include(a=>a.BootcampImage)
            );

            GetListResponse<GetListBootcampListItemDto> response = _mapper.Map<GetListResponse<GetListBootcampListItemDto>>(
                bootcamps
            );
            return response;
        }
    }
}
