Feature: Testcase4
	In order to save the prices
	I need to filter by product type

Scenario Outline: Get mean value product cost
Given I am on the homepage
When I filter by “<Product>”
Then I can see in the test output the mean value of each product
Examples: 
| Product   |
| Phones    |
| Laptops   |
| Monitors |