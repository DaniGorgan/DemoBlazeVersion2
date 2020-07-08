Feature: Testcase13
	I want to by a phone, laptop and a monitor
	I have a budget of 1500$

Scenario: Given I have 1500$ and I want to buy a phone, laptop and a monitor, create 1 Scenario to buy all in this budget.
	Given I have 1500$
	When I add the items needed in the cart
	Then I should be able to buy them
