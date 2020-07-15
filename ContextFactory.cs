using Microsoft.SharePoint.Client;

namespace SharePoint.Integration.ACS
{
    public interface IContextFactory
    {
        ClientContext GetContext(string sharePointSite);
    }

    public class ContextFactory : IContextFactory
    {
        private readonly IAzureAuthProvider _authProvider;

        public ContextFactory(IAzureAuthProvider authProvider)
        {
            _authProvider = authProvider;
        }

        public ClientContext GetContext(string sharePointSite)
        {
            var context = new ClientContext(sharePointSite);
            context.ExecutingWebRequest += (sender, e) =>
            {
                var accessToken = _authProvider.AcquireTokenAsync().GetAwaiter().GetResult();
                e.WebRequestExecutor.RequestHeaders["Authorization"] = "Bearer " + accessToken;
            };

            return context;
        }
    }
}
