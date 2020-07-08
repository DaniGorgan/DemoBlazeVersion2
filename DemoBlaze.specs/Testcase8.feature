Feature: Testcase8
	In order to check the price of a product
	I firstly need to be on the homepage
	I need to click on the product and check for the listed price

Scenario: Access the home page, select the first available product, then display the products price in the console.
	Given I am on the homepage
	When I select the first product
	Then the price should be displayed in the new page
