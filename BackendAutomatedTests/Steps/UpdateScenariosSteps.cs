using BackendAutomatedTests.Consts;
using BackendAutomatedTests.Hooks;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using TechTalk.SpecFlow;

namespace BackendAutomatedTests.Steps
{
    [Binding]
    public class UpdateScenariosSteps : SharedSteps
    {
        private static IRestClient _client;
        private readonly ScenarioContext _scenarioContext;

        public UpdateScenariosSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"an existing board to be updated exists")]
        public void GivenAnExistingBoardToBeUpdatedExists()
        {
            _client = new RestClient(UrlParamValues.TrelloUrl);
            var request = RequestWithAuth(BoardsEndpoints.GetBoardUrl)
                        .AddUrlSegment("id", UrlParamValues.BoardIdToUpdate);
            var response = _client.Get(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        }


        [When(@"an update is made to the board through the API")]
        public void WhenAnUpdateIsMadeToTheBoardThroughTheAPI()
        {
            var updatedName = "Updated Board Name : " + DateTime.Now.ToLongTimeString();
            var request = RequestWithAuth(BoardsEndpoints.UpdateBoardUrl)
                .AddUrlSegment("id", UrlParamValues.BoardIdToUpdate)
                .AddJsonBody(new Dictionary<string, string> { { "name", updatedName } });

            var response = _client.Put(request);
            var responseContent = JToken.Parse(response.Content);

            _scenarioContext["updatedName"] = updatedName;

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(updatedName, responseContent.SelectToken("name").ToString());

        }

        [Then(@"the Updated values are validated")]
        public void ThenTheUpdatedValuesAreValidated()
        {
            var updatedName = (string)_scenarioContext["updatedName"];

            var updateRequest = RequestWithAuth(BoardsEndpoints.GetBoardUrl)
                .AddUrlSegment("id", UrlParamValues.BoardIdToUpdate);

            var updateResponse = _client.Get(updateRequest);
            var updateResponseContent = JToken.Parse(updateResponse.Content);

            Assert.AreEqual(updatedName, updateResponseContent.SelectToken("name").ToString());
        }

        [When(@"an update is made to the board through the API with an unauthorized id")]
        public void WhenAnUpdateIsMadeToTheBoardThroughTheAPIWithAnInvalidName()
        {
            var updatedName = "Updated Board Name : " + DateTime.Now.ToLongTimeString();
            var request = RequestWithAuth(BoardsEndpoints.UpdateBoardUrl)
                .AddUrlSegment("id", UrlParamValues.UnauthorizedBoardId)
                .AddJsonBody(new Dictionary<string, string> { { "dghtrh", updatedName } });

            _scenarioContext["request"] = request;

        }

        [Then(@"an ""(.*)"" and ""(.*)"" error occurs and the board is not updated")]
        public void ThenAnAndErrorOccursAndTheBoardIsNotUpdated(string statuscode, string errorMessage)
        {
            var request = (IRestRequest)_scenarioContext["request"];
            var response = _client.Put(request);


            Assert.AreEqual(statuscode, response.StatusCode.ToString());
            Assert.AreEqual(errorMessage, response.Content);
        }
    }
}
