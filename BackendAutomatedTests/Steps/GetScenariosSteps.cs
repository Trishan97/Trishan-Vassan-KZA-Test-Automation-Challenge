using System;
using TechTalk.SpecFlow;
using RestSharp;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Linq;
using System.Net;
using BackendAutomatedTests.Hooks;
using BackendAutomatedTests.Consts;

namespace BackendAutomatedTests.Steps
{
    [Binding]
    public class GetScenarioSteps : SharedSteps
    {
        private static IRestClient _client;
        private readonly ScenarioContext _scenarioContext;
        public GetScenarioSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }


        [Given(@"the Trello API is healthy and working")]
        public void GivenIHaveTheTrelloAPI()
        {
            _client = new RestClient(UrlParamValues.TrelloUrl);
            var request = new RestRequest();

            _scenarioContext["request"] = request;

            var response = _client.Get(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        }

        [Given(@"a Get request is made with an incorrect (.*) value")]
        public void GivenAGetRequestIsMadeWithAnIncorrectValue(string board_id)
        {
            var request = RequestWithAuth(BoardsEndpoints.GetBoardUrl)
               .AddUrlSegment("id", board_id);
            var response = _client.Get(request);

            _scenarioContext["response"] = response;

        }

        [Given(@"a Get request is made with an invalid Authentication")]
        public void GivenAGetRequestIsMadeWithAnInvalidAuthentication()
        {
            var request = RequestWithoutAuth(BoardsEndpoints.GetBoardUrl)
              .AddUrlSegment("id", UrlParamValues.UnauthorizedBoardId);

            var response = _client.Get(request);
            _scenarioContext["response"] = response;

        }



        [When(@"a GET all boards request is performed")]
        public void WhenAGETAllBoardsRequestIsPerformed()
        {
            var request = RequestWithAuth(BoardsEndpoints.GetAllBoardsUrl)
               .AddQueryParameter("field", "id,name")
               .AddUrlSegment("member", UrlParamValues.UserName);

            var response = _client.Get(request);
            _scenarioContext["response"] = response;
        }

        [When(@"a GET request is performed for an existing board")]
        public void WhenAGETRequestIsPerformedForAnExistingBoard()
        {
            var request = RequestWithAuth(BoardsEndpoints.GetBoardUrl)
            .AddUrlSegment("id", UrlParamValues.ExistingBoardId);
            var response = _client.Get(request);
            _scenarioContext["response"] = response;

        }


        [When(@"a GET request is performed for all lists on a specific board")]
        public void WhenAGETRequestIsPerformedForAllListsOnASpecificBoard()
        {
            var request = RequestWithAuth(BoardsEndpoints.GetAllListsOnBoardUrl)
                 .AddUrlSegment("id", UrlParamValues.ExistingBoardId);
            var response = _client.Get(request);
            _scenarioContext["response"] = response;

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);


        }

        [Then(@"the following (.*) should be displayed")]
        public void ThenTheFollowingShouldBeDisplayed(string lists)
        {
            var response = (IRestResponse)_scenarioContext["response"];
            var responseContent = JToken.Parse(response.Content);

            Assert.True(responseContent.Children().Select(token => token.SelectToken("name")).Contains(lists));

        }



        [Then(@"the response code is successful")]
        public void ThenTheResponseCodeIsSuccessful()
        {
            var response = (IRestResponse)_scenarioContext["response"];
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        }

        [Then(@"the ""(.*)"" board should be returned")]
        public void ThenTheShouldBeReturned(string existingBoard)
        {
            var response = (IRestResponse)_scenarioContext["response"];
            Assert.True(response.Content.Contains(existingBoard));
        }



        [Then(@"an (.*) and (.*) response is received")]
        public void ThenAnAndResponseIsReceived(string errorMessage, string statusCode)
        {
            var response = (IRestResponse)_scenarioContext["response"];

            Assert.AreEqual(statusCode, response.StatusCode.ToString());
            Assert.AreEqual(errorMessage, response.Content);
        }
    }
}
