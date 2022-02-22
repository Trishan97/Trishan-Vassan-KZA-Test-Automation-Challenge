Feature: Create Frontend Automated tests
Covered Scenarios :
- Create New Board Through Frontend UI
- Attempt to create a board without providing a name through Frontend UI

Background:
	Given the user has navigated to Trello's Website
	When the user logs in with valid credentials
	Then the user should be logged in and taken to the Trello Homepage

@Positive
Scenario: Create New Board Through Frontend UI
	Given the user clicks on the "Create new board" option
	When the "Create board" modal appears
	And the user fills in all required fields
	Then the user is taken to the newly created board
	And the user is able to close and Delete the newly created board

@Negative
Scenario: User Unable to create Board without supplying name
	Given the user clicks on the "Create new board" option
	When the "Create board" modal appears
	And the user does not fill in all required fields
	Then the user is unable to create a new Board