Feature: GET Frontend Automated tests
Covered Scenarios :
- Get Existing Boards Through Frontend UI
- Get Existing Cards on existing Board

Background:
	Given the user has navigated to Trello's Website
	When the user logs in with valid credentials
	Then the user should be logged in and taken to the Trello Homepage

@Positive
Scenario: Retrieve Existing Boards
	Given the user can see their existing board

@Positive
Scenario: Get Existing Cards on Board
	Given the user can see their existing board
	When the user navigates to their existing board
	Then the user can see their existing Cards