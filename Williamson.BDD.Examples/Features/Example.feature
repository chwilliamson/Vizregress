@SelfHost
Feature: Example Web Application
	In order to have a beginning
    As a n00b
    I want to see how things work

Scenario: I visit the example page then the title should be set
Given I visit Example
Then the page title should be Colins Example

Scenario: Does the home page look the same
Given I visit Example
Then the screen should look like Example.Home

Scenario: Does the SelfHost component look the same
Given I visit Example
And I click the 'Yes please!' button
Then the screen should look like Example.SelfHost