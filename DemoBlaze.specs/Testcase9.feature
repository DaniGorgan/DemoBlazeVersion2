Feature: Testcase9
	In order to have the correct product and price displayed
	I need to select the product
	I need to add it to my cart
	I need to go to the cart page

Scenario: Access the home page, select the first available product, add it to your cart, navigate to the shopping cart page, then the selected product with the correct price should be displayed
	Given I am on the homepage
	And I select the first available product
	When I add it to my cart
	And I navigate to the cart page
	Then the selected product with the correct price should be displayed
