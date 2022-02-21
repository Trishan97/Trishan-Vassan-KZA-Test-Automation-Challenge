Feature: LoginTests

Covered Scenarios :
- Login to the Trello Website with valid credentials
- Attempt to Login to the Trello Website with invalid credentials
	

@Positive
Scenario: User logs in successfully to Trello's Website
	Given the user has navigated to Trello's Website
	When the user logs in with valid credentials
	Then the user should be logged in and taken to the Trello Homepage

@Negative
Scenario: User attempts to log in with Invalid Credentials
	Given the user has navigated to Trello's Website
	When the user attempts to log in with invalid credentials
	Then the user is not logged in and is shown an error message