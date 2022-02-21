using BackendAutomatedTests.Consts;
using BackendAutomatedTests.Hooks;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace BackendAutomatedTests.Steps
{
    [Binding]
    public class DeleteScenariosSteps : SharedSteps
    {
        private static IRestClient _client;
        private readonly ScenarioContext _scenarioContext;

        public DeleteScenariosSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [When(@"a (.*) that does not exist attempts to be deleted")]
        public void WhenAThatDoesNotExistAttemptsToBeDeleted(string invalidBoardId)
        {
            _client = new RestClient(UrlParamValues.TrelloUrl);

            var deleteRequest = RequestWithAuth(BoardsEndpoints.DeleteBoardUrl)
           .AddUrlSegment("id", invalidBoardId);
            var response = _client.Delete(deleteRequest);

            _scenarioContext["invalidBoardId"] = invalidBoardId;
            _scenarioContext["response"] = response;

        }




        [Then(@"validation is done to confirm the Board has been successfully deleted")]
        public void ThenValidationIsDoneToConfirmTheBoardHasBeenSuccessfullyDeleted()
        {
            _client = new RestClient(UrlParamValues.TrelloUrl);

            var _createdBoardID = (string)_scenarioContext["createdBoardId"];
            var confirmDeleteRequest = RequestWithAuth(BoardsEndpoints.GetAllBoardsUrl)
                .AddQueryParameter("field", "id,name")
                .AddUrlSegment("member", UrlParamValues.UserName);
            var confirmDeleteResponse = _client.Get(confirmDeleteRequest);

            var responseContent = JToken.Parse(confirmDeleteResponse.Content);
            Assert.False(responseContent.Children().Select(token => token.SelectToken("id")).Contains(_createdBoardID));
        }


        [Then(@"an ""(.*)"" and ""(.*)"" error occurs and the board is not deleted")]
        public void ThenAnAndErrorOccursAndTheBoardIsNotCreated(string errorMessage, string statuscode)
        {
            var response = (IRestResponse)_scenarioContext["response"];
            var invalidBoardId = (string)_scenarioContext["invalidBoardId"];

            Assert.AreEqual(statuscode, response.StatusCode.ToString());
            Assert.AreEqual(errorMessage, response.Content);
        }
    }
}
