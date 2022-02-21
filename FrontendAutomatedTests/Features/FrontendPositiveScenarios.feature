Feature: TrelloFrontend Scenarios


Background: 
	Given the user has navigated to Trello's Website 
	When the user logs in with valid credentials 
	Then the user should be logged in and taken to the Trello Homepage
	

@TrelloPositive
Scenario: Navigate to Trello UI 
	Given the user can see their board
	    
