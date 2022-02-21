using System;
using TechTalk.SpecFlow;
using RestSharp;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using BackendAutomatedTests.Consts;
using BackendAutomatedTests.Hooks;

namespace BackendAutomatedTests.Steps
{
    [Binding]
    public class CreateScenariosSteps : SharedSteps
    {

        private string? _createdBoardID;

        private static IRestClient _client;
        private readonly ScenarioContext _scenarioContext;

        public CreateScenariosSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"the user wants to create a new Board through the API")]
        public void GivenTheUserWantsToCreateANewBoardThroughTheAPI()
        {
            _client = new RestClient(UrlParamValues.TrelloUrl);

            var boardName = "API Created Board " + DateTime.Now.ToLongTimeString();
            var request = RequestWithAuth(BoardsEndpoints.CreateBoardUrl)
                .AddJsonBody(new Dictionary<string, string> { { "name", boardName } });

            _scenarioContext["request"] = request;
            _scenarioContext["boardName"] = boardName;

        }

        [When(@"the Post request is made to create the Board")]
        public void WhenThePostRequestIsMadeToCreateTheBoard()
        {
            var request = (IRestRequest)_scenarioContext["request"];
            var boardName = (string)_scenarioContext["boardName"];
            var response = _client.Post(request);
            var responseContent = JToken.Parse(response.Content);

            _createdBoardID = responseContent.SelectToken("id").ToString();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(boardName, responseContent.SelectToken("name").ToString());

        }

        [When(@"the Board has been successfully created with the correct Values")]
        public void WhenTheBoardHasBeenSuccessfullyCreatedWithTheCorrectValues()
        {
            var boardName = (string)_scenarioContext["boardName"];
            var allBoardsRequest = RequestWithAuth(BoardsEndpoints.GetAllBoardsUrl)
              .AddQueryParameter("field", "id,name")
              .AddUrlSegment("member", UrlParamValues.UserName);

            var response = _client.Get(allBoardsRequest);
            var responseContent = JToken.Parse(response.Content);

            Assert.True(responseContent.Children().Select(token => token.SelectToken("name")).Contains(boardName));
        }


        [Then(@"the newly created Board is deleted")]
        public void ThenTheNewlyCreatedBoardIsDeleted()
        {
            var deleteRequest = RequestWithAuth(BoardsEndpoints.DeleteBoardUrl)
                .AddUrlSegment("id", _createdBoardID);
            var response = _client.Delete(deleteRequest);

            _scenarioContext["createdBoardId"] = _createdBoardID;

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(string.Empty, JToken.Parse(response.Content).SelectToken("_value").ToString());
        }



        [Given(@"the user wants to create a new Board with an invalid name")]
        public void GivenTheUserWantsToCreateANewBoardWithAnInvalidName()
        {
            _client = new RestClient(UrlParamValues.TrelloUrl);

            var request = RequestWithAuth(BoardsEndpoints.CreateBoardUrl)
                .AddJsonBody(new Dictionary<string, string> { { "names", "23422" } });
            _scenarioContext["request"] = request;

        }



        [Then(@"an ""(.*)"" and ""(.*)"" error occurs and the board is not created")]
        public void ThenAnAndErrorOccursAndTheBoardIsNotCreated(string errorMessage, string statuscode)
        {
            var request = (IRestRequest)_scenarioContext["request"];
            var response = _client.Post(request);

            Assert.AreEqual(statuscode, response.StatusCode.ToString());
            Assert.AreEqual(errorMessage, response.Content);
        }
    }
}
