using DataAccess.DomainModels;
using DataAccess.Repositories;
using Microsoft.Extensions.Logging;
using System;

namespace TestProject.Library.Services
{
    public class RxUserService : IRxUserService
    {
        private readonly IRxUserRepository _RxUserRepository;
        private readonly ILogger<RxUserService> _logger;

        public RxUserService(IRxUserRepository RxUserRepository, ILogger<RxUserService> logger)
        {
            _RxUserRepository = RxUserRepository;
            _logger = logger;
        }

        public RxUserModel GetById(Guid RxUserId)
        {
            var RxUser = _RxUserRepository.GetById(RxUserId);
            _logger.LogInformation("Looking for a user: {RxUserId}", RxUserId);

            if(RxUser == null)
            {
                _logger.LogWarning("No user found!");
                return null;
            }
            _logger.LogInformation("Found a user");
            return MapToDomain(RxUser, RxUserId);
        }

        private RxUserModel MapToDomain(RxUserModel RxUser, Guid id)
        {
            return new RxUserModel { Id = id.ToString(), FirstName = RxUser.FirstName, LastName = RxUser.LastName };
        }
    }
}
