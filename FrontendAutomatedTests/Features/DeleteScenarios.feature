Feature: DeleteScenarios
Covered Scenarios :
- Create and Delete New Board Through Frontend UI

Background:
	Given the user has navigated to Trello's Website
	When the user logs in with valid credentials
	Then the user should be logged in and taken to the Trello Homepage

@Positive
Scenario: : Create and Delete New Board Through Frontend UI
	Given the user clicks on the "Create new board" option
	When the "Create board" modal appears
	And the user fills in all required fields
	Then the user is taken to the newly created board
	And the user is able to close and Delete the newly created board
	And the user can verify that the board no longer exists