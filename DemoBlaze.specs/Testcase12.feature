Feature: Testcase12
	In order to purchase an item I need to go to my cart and click checkout

Scenario: After I purchase an item my cart is empty
	Given I am on the homepage
	And I add an item to the cart
	When I go to the cart page
	And I press Checkout
	Then I purchase the item
	And the cart is empty