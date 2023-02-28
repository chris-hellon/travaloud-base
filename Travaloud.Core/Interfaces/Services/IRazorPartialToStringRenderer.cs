using System;
using Microsoft.AspNetCore.Mvc;

namespace Travaloud.Core.Interfaces.Services
{
    public interface IRazorPartialToStringRenderer
    {
        Task<string> RenderPartialToStringAsync<TModel>(string partialName, TModel model);
    }
}

