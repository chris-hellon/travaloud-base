using System;
namespace Travaloud.Core.Interfaces.Repositories
{
	public interface IToursRepository
	{
        Task<IEnumerable<Tour>> GetTours(string tenantId);
    }
}

