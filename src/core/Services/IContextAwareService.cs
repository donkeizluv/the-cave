using System;
using System.Security.Claims;

namespace CaveCore.Services
{
    public interface IContextAwareService : IDisposable
    {
        string CurrentUsername { get; }
        string CurrentEmail { get; }
        string CurrentId { get; }
        ClaimsPrincipal UserPrincipal { get; }

    }
}