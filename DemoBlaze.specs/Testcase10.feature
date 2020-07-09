Feature: Testcase10
	In order to visit every page
	I need to be on the Homepage and click on the navbar buttons

Scenario: Create a parameterized test (using scenario outline) that can access all the pages from the header and check that the correct page/popup is displayed
	Given I am on the homepage
	When I access “<page>”
	Then the correct page should be displayed

	Examples:
		| page     |
		| Home     |
		| Contact  |
		| About us |
		| Cart     |
		| Log in   |
		| Sign up  |