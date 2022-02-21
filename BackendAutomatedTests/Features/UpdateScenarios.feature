Feature: PUT, Update Backend Automated tests

Background:
	Given the Trello API is healthy and working

@Positive
Scenario: Update Existing Board through API
	Given an existing board to be updated exists
	When an update is made to the board through the API
	Then the Updated values are validated

@Negative
Scenario: Update an Existing Board with an Unauthorized Id through API
	Given an existing board to be updated exists
	When an update is made to the board through the API with an unauthorized id
	Then an "Unauthorized" and "unauthorized permission requested" error occurs and the board is not updated