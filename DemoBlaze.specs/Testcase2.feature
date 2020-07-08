Feature: Testcase2
	In order to create a new user
	I need to press the sign up button
	I need to complete the username and password fields

Scenario: Register new user
Given I am on the homepage
And I click on Sign Up button
When I fill in required data
Then I get registered