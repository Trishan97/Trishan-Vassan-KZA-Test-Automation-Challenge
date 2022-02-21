using BackendAutomatedTests.Consts;
using RestSharp;

namespace BackendAutomatedTests.Hooks
{
    public class SharedSteps
    {
        protected IRestRequest RequestWithAuth(string url)
        {
            return new RestRequest(url)
                .AddQueryParameter("key", UrlParamValues.ValidKey)
                .AddQueryParameter("token", UrlParamValues.ValidToken);
        }

        protected IRestRequest RequestWithoutAuth(string url)
        {
            return new RestRequest(url);
        }
    }
}
