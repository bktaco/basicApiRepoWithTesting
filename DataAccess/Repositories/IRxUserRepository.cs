using DataAccess.DomainModels;
using System;

namespace DataAccess.Repositories
{
    public interface IRxUserRepository
    {
        RxUserModel GetById(Guid id);
    }
}