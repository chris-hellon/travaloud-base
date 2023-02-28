namespace Travaloud.Infrastructure.Repositories
{
    public class UsersRepository : BaseRepository, IUsersRepository
    {
        public UsersRepository(IDapperContext dapperContext) : base(dapperContext)
        {

        }

        public async Task<string> GetRoleId(string roleName, string tenant = "fuse")
        {
            return await DapperConnection.GetSingle<string>($"SELECT Id FROM [Identity].Roles WHERE Name = '{roleName}' AND TenantId = '{tenant}'");
        }
    }
}

