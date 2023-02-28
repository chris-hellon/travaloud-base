using Travaloud.Infrastructure.Contexts;
using System.Data;

namespace Travaloud.Infrastructure.Repositories
{
    public class BaseRepository
    {
        private readonly IDapperContext _dapperContext;
        protected BaseRepository(IDapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        protected IDbConnection DapperConnection => _dapperContext.CreateConnection();
    }
}

