Feature: Create, POST Backend Automated tests
Covered Scenarios :
- Create New Board Through API
- Attempt to Create New Board with an invalid name through API

Background:
	Given the Trello API is healthy and working

@Positive
Scenario: Create New Board Through API
	Given the user wants to create a new Board through the API
	When the Post request is made to create the Board
	And the Board has been successfully created with the correct Values
	Then the newly created Board is deleted

@Negative
Scenario: Attempt to Create New Board with an invalid name through API
	Given the user wants to create a new Board with an invalid name
	Then an "invalid value for name" and "BadRequest" error occurs and the board is not created