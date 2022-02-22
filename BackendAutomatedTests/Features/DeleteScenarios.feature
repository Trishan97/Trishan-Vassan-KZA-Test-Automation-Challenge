Feature: Delete Backend Automated Tests
Covered Scenarios :
- Delete newly Created Board Through API
- Attempt to Delete Board with an invalid ID through API

Background:
	Given the Trello API is healthy and working

@Positive
Scenario: Delete Newly Created Board Through API
	Given the user wants to create a new Board through the API
	When the Post request is made to create the Board
	And the Board has been successfully created with the correct Values
	Then the newly created Board is deleted
	And validation is done to confirm the Board has been successfully deleted

@Negative
Scenario: Attempt to delete a Board that does not exist Through API
	When a <Board_Id> that does not exist attempts to be deleted
	Then an "invalid id" and "BadRequest" error occurs and the board is not deleted