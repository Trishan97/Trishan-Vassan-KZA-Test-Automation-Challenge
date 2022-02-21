using RestSharp;
using System.Collections.Generic;

namespace BackendAutomatedTests.Consts
{
    public class UrlParamValues
    {
        public const string TrelloUrl = "https://api.trello.com";

        public const string ExistingBoardId = "620fe4163bb44a55800219cf";
        public const string UnauthorizedBoardId = "620ea15b87b6323daf7d4478";
        public const string BoardIdToUpdate = "6212e1d7de21a83206f1db42";

        public const string UserName = "trishankzaassessment";

        public const string ValidKey = "9d79c86f2280453b6e168bbde432f07c";
        public const string ValidToken = "386af2bb7e34df0b0f59b5c160bd306181e62edee9e5009a22e57d6b21d0a6bc";


        public static readonly IEnumerable<Parameter> AuthQueryParams = new[]
        {

            new Parameter("key",ValidKey, ParameterType.QueryString),
            new Parameter("token", ValidToken, ParameterType.QueryString)

        };
    }
}
