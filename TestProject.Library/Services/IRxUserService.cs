using DataAccess.DomainModels;
using System;

namespace TestProject.Library.Services
{
    public interface IRxUserService
    {
        RxUserModel GetById(Guid RxUserId);
    }
}
