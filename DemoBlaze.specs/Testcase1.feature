Feature: Testcase1
	In order to buy some phones
	I need to be logged in
	And have a budget of 1500$

Scenario: Buy random phones using given budget

Given I am logged in
And I have a budget of 1500$
When I filter by Phones
Then I can add to cart 2 random phones that don't exceed my budget