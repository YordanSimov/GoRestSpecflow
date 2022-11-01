Feature: Users

Scenario: Get all users
	Given I want to prepare a request
	When I get all users from the users endpoint
	Then The response status code should be OK
	And The respons should contain a list of users

@Authenticate
Scenario: Create a new user
	Given I have the following user data
	When I send a request to the users endpoint
	Then The response status code shuld be Created
	And The user should be created successfully

@Authenticate
Scenario: Create an invalid user
	Given I have the following user data
	When I send an invalid request to the users endpoint
	Then The invalid user status code should be UnprocessableEntity
	And The property email should be the error field

	@Authenticate
Scenario: Update an existing user
	Given I have a created user already
	When I send an update request to the users endpoint
	Then The status code from response should be OK
	And The user should be updated successfully
	@Authenticate
Scenario: Delete an existing user
	Given I have created a user to delete already
	When I send a delete request to the users endpoint
	Then The delete request status code should be NoContent
