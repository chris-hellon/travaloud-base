namespace Travaloud.Core.Interfaces.Repositories
{
    public interface IUsersRepository
    {
        Task<string> GetRoleId(string roleName, string tenant = "fuse");
    }
}

