Feature: Testcase11
	I want to buy a Dell Laptop model from 2017

Scenario: Buy a Dell laptop model from 2017
	Given I am on the homepage
	When I search for the product that I need
	And I add it to my cart
	Then the correct item is displayed in the cart
