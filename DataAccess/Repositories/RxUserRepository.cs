using Dapper;
using DataAccess.DomainModels;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Linq;

namespace DataAccess.Repositories
{
    public class RxUserRepository : IRxUserRepository
    {
        private readonly IBaseRepository _baseRepo;

        public RxUserRepository(IBaseRepository baseRepo)
        {
            _baseRepo = baseRepo;
        }

        public RxUserModel GetById(Guid id)
        {
            using(IDbConnection connection = new MySqlConnection(_baseRepo.GetConnectionString()))
            {
                var sql = $"SELECT * FROM RxUser WHERE UserId = '{id}';";
                var result = connection.Query<RxUserModel>(sql, new { id }).FirstOrDefault();

                return result;
            }
        }
    }
}
