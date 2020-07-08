Feature: Testcase5
	In order to navigate the website
	I can use the previous and next buttons

Scenario: Check that Image Slider change the content
Given I am on the homepage
When I click on the “Previous” button from Image Slider
Then I see a different product
When I click on the “Next” button from Image Slider
Then I see a different product