using System;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using CaveCore.Services;

namespace CaveCore.Service.Impl
{
    /// <summary>
    /// Shared base of all services, common utilities of service placed here
    /// </summary>
    public abstract class ContextAwareService : IContextAwareService
    {
        public ClaimsPrincipal UserPrincipal { get; private set; }
        public string CurrentId
        {
            get
            {
                if (!TryGetContextValue<string>(UserPrincipal, ClaimTypes.Sid, out string id))
                    throw new InvalidOperationException($"Cant get {nameof(ClaimTypes.Sid)} of current acting principal");
                return id;
            }
        }
        public string CurrentUsername
        {
            get
            {
                if (!TryGetContextValue<string>(UserPrincipal, ClaimTypes.Name, out string name))
                    throw new InvalidOperationException($"Cant get {nameof(ClaimTypes.Name)} of current acting principal");
                return name;
            }
        }
        public string CurrentEmail
        {
            get
            {
                if (!TryGetContextValue<string>(UserPrincipal, ClaimTypes.Email, out string email))
                    throw new InvalidOperationException($"Cant get {nameof(ClaimTypes.Email)} of current acting principal");
                return email;
            }
        }

        protected ContextAwareService(ClaimsPrincipal principal)
        {
            UserPrincipal = principal ?? throw new ArgumentNullException();
        }

        private bool TryGetContextValue<T>(ClaimsPrincipal principal, string claimType, out T value)
        {
            if (principal == null) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(claimType)) throw new ArgumentNullException();
            value = default(T);
            var claim = principal.FindFirst(claimType);
            if (claim == null) return false;
            try
            {
                value = (T)Convert.ChangeType(claim.Value, typeof(T), CultureInfo.InvariantCulture);
            }
            catch (InvalidCastException)
            {
                return false;
            }
            catch (FormatException)
            {
                return false;
            }
            return true;
        }

        public void Dispose()
        {
            //TODO: release resources
        }
    }
}