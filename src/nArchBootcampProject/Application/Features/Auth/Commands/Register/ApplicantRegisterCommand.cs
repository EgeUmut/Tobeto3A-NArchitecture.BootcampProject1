﻿using Application.Features.ApplicationInformations.Constants;
using Application.Features.Auth.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Dtos;
using NArchitecture.Core.Security.Hashing;
using NArchitecture.Core.Security.JWT;

namespace Application.Features.Auth.Commands.Register;

public class ApplicantRegisterCommand : IRequest<RegisteredResponse>
{
    public ApplicantRegisterDto UserForRegisterDto { get; set; }
    public string IpAddress { get; set; }

    public ApplicantRegisterCommand()
    {
        UserForRegisterDto = null!;
        IpAddress = string.Empty;
    }

    public ApplicantRegisterCommand(ApplicantRegisterDto userForRegisterDto, string ipAddress)
    {
        UserForRegisterDto = userForRegisterDto;
        IpAddress = ipAddress;
    }

    public class RegisterCommandHandler : IRequestHandler<ApplicantRegisterCommand, RegisteredResponse>
    {
        private readonly IAuthService _authService;
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IApplicantRepository _applicantRepository;
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;

        public RegisterCommandHandler(
            IAuthService authService,
            AuthBusinessRules authBusinessRules,
            IApplicantRepository applicantRepository,
            IUserOperationClaimRepository userOperationClaimRepository
        )
        {
            _authService = authService;
            _authBusinessRules = authBusinessRules;
            _applicantRepository = applicantRepository;
            _userOperationClaimRepository = userOperationClaimRepository;
        }

        public async Task<RegisteredResponse> Handle(ApplicantRegisterCommand request, CancellationToken cancellationToken)
        {
            await _authBusinessRules.UserEmailShouldBeNotExists(request.UserForRegisterDto.Email);

            HashingHelper.CreatePasswordHash(
                request.UserForRegisterDto.Password,
                passwordHash: out byte[] passwordHash,
                passwordSalt: out byte[] passwordSalt
            );
            Applicant newUser =
                new()
                {
                    FirstName = request.UserForRegisterDto.FirstName,
                    LastName = request.UserForRegisterDto.LastName,
                    NationalIdentity = request.UserForRegisterDto.NationalIdentity,
                    DateOfBirth = request.UserForRegisterDto.DateOfBirth,
                    About = request.UserForRegisterDto.About,
                    UserName = request.UserForRegisterDto.UserName,
                    Email = request.UserForRegisterDto.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                };
            Applicant createdUser = await _applicantRepository.AddAsync(newUser);

            UserOperationClaim userOperationClaim1 = new() { UserId = createdUser.Id, OperationClaimId = 31 };
            UserOperationClaim userOperationClaim2 = new() { UserId = createdUser.Id, OperationClaimId = 33 };
            UserOperationClaim userOperationClaim3 = new() { UserId = createdUser.Id, OperationClaimId = 35 };
            UserOperationClaim userOperationClaim4 = new() { UserId = createdUser.Id, OperationClaimId = 49 };

            UserOperationClaim userOperationClaim5 = new() { UserId = createdUser.Id, OperationClaimId = 19 };
            UserOperationClaim userOperationClaim6 = new() { UserId = createdUser.Id, OperationClaimId = 20 };
            UserOperationClaim userOperationClaim7 = new() { UserId = createdUser.Id, OperationClaimId = 22 };
            UserOperationClaim userOperationClaim8 = new() { UserId = createdUser.Id, OperationClaimId = 23 };

            UserOperationClaim userOperationClaim9 = new() { UserId = createdUser.Id, OperationClaimId = 25 };

            await _userOperationClaimRepository.AddAsync(userOperationClaim1);
            await _userOperationClaimRepository.AddAsync(userOperationClaim2);
            await _userOperationClaimRepository.AddAsync(userOperationClaim3);
            await _userOperationClaimRepository.AddAsync(userOperationClaim4);
            await _userOperationClaimRepository.AddAsync(userOperationClaim5);
            await _userOperationClaimRepository.AddAsync(userOperationClaim6);
            await _userOperationClaimRepository.AddAsync(userOperationClaim7);
            await _userOperationClaimRepository.AddAsync(userOperationClaim8);
            await _userOperationClaimRepository.AddAsync(userOperationClaim9);

            AccessToken createdAccessToken = await _authService.CreateAccessToken(createdUser);

            Domain.Entities.RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(
                createdUser,
                request.IpAddress
            );
            Domain.Entities.RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

            RegisteredResponse registeredResponse = new() { AccessToken = createdAccessToken, RefreshToken = addedRefreshToken };
            return registeredResponse;
        }
    }
}
