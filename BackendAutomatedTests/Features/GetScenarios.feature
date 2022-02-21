Feature: GET, Read Backend Automated tests

Background:
	Given the Trello API is healthy and working

@Positive
Scenario: Get All Boards Request
	When a GET all boards request is performed
	Then the response code is successful

@Positive
Scenario: Get Existing Boards
	When a GET request is performed for an existing board
	Then the "Trello FrontEnd Testing" board should be returned

@Positive
Scenario Outline: Get Existing Lists on Board
	When a GET request is performed for all lists on a specific board
	Then the following <lists> should be displayed

	Examples:
		| lists   |
		| To Do   |
		| Doing   |
		| Blocked |
		| IN QA   |
		| Done    |

@Negative
Scenario Outline: Validate Incorrect Authentication Error Response
	Given a Get request is made with an invalid Authentication
	Then an <error_message> and <status_code> response is received

	Examples:
		| error_message                     | status_code  |
		| unauthorized permission requested | Unauthorized |

@Negative
Scenario Outline: Validate Incorect ID Error Response
	Given a Get request is made with an incorrect <Board_ID> value
	Then an <error_message> and <status_code> response is received

	Examples:
		| Board_ID | error_message | status_code |
		| invalid  | invalid id    | BadRequest  |
		| !@#$%    | invalid id    | BadRequest  |