Feature: Stockport Kitchens
	In order to find more information
	As a customer
	I want to view portfolio, about us and make contact

Scenario: Home page looks correct
	Given I visit http://www.stockport-kitchens.co.uk
	Then the page should look like StockportKitchens/Home.png
		
Scenario: Home page has correct title
	Given I visit http://www.stockport-kitchens.co.uk
	Then the page title should be R&R - Home

Scenario: About page has correct title
	Given I visit http://www.stockport-kitchens.co.uk/about
	Then the page title should be R&R - About

Scenario: Contact page has correct title
	Given I visit http://www.stockport-kitchens.co.uk/contact
	Then the page title should be R&R - Contact

Scenario: Contact form looks correct
	Given I visit http://www.stockport-kitchens.co.uk/contact
	Then the page should look like StockportKitchens/Contact.png

Scenario: Clicking twitter opens new window and goes to twitter page
	Given I visit http://www.stockport-kitchens.co.uk
	When I click the Twitter button
	Then a new window should open with url https://twitter.com/richardskbb