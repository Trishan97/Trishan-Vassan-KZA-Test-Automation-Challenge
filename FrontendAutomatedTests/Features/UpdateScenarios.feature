Feature: Update Frontend Automated tests
Covered Scenarios :
- Update Existing Card through Frontend UI

Background:
	Given the user has navigated to Trello's Website
	When the user logs in with valid credentials
	Then the user should be logged in and taken to the Trello Homepage

@Positive
Scenario: Update Existing Card through Frontend UI
	Given the user can see their existing board
	When the user navigates to their existing update board
	And the user can see their existing Cards on the update board 
	Then the user attempts to update an existing Card 
	And the card is successfully updated
	And the user updates the card back to the orignal name
