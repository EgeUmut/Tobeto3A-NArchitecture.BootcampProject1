using Application.Features.BootcampImages.Constants;
using Application.Features.BootcampImages.Rules;
using Application.Services.ImageService;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using static Application.Features.BootcampImages.Constants.BootcampImagesOperationClaims;

namespace Application.Features.BootcampImages.Commands.Create;

public class CreateBootcampImageCommand
    : IRequest<CreatedBootcampImageResponse>//,
        //ISecuredRequest,
        //ICacheRemoverRequest,
        //ILoggableRequest,
        //ITransactionalRequest
{
    public int BootcampId { get; set; }
    //public string ImagePath { get; set; }
    public IFormFile Image { get; set; }

    public string[] Roles => [Admin, Write, BootcampImagesOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetBootcampImages"];

    public class CreateBootcampImageCommandHandler : IRequestHandler<CreateBootcampImageCommand, CreatedBootcampImageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBootcampImageRepository _bootcampImageRepository;
        private readonly BootcampImageBusinessRules _bootcampImageBusinessRules;
        private readonly ImageServiceBase _ýmageServiceBase;

        public CreateBootcampImageCommandHandler(
            IMapper mapper,
            IBootcampImageRepository bootcampImageRepository,
            BootcampImageBusinessRules bootcampImageBusinessRules
,
            ImageServiceBase ýmageServiceBase)
        {
            _mapper = mapper;
            _bootcampImageRepository = bootcampImageRepository;
            _bootcampImageBusinessRules = bootcampImageBusinessRules;
            _ýmageServiceBase = ýmageServiceBase;
        }

        public async Task<CreatedBootcampImageResponse> Handle(
            CreateBootcampImageCommand request,
            CancellationToken cancellationToken
        )
        {
            BootcampImage bootcampImage = _mapper.Map<BootcampImage>(request);

            
            var path = _ýmageServiceBase.UploadAsync(request.Image);
            bootcampImage.ImagePath = path.ToString();

            await _bootcampImageRepository.AddAsync(bootcampImage);

            CreatedBootcampImageResponse response = _mapper.Map<CreatedBootcampImageResponse>(bootcampImage);
            return response;
        }
    }
}
