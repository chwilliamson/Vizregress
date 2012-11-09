@SelfHost
Feature: Basic
	In order to have a beginning
    As a n00b
    I want to see how things work

Scenario: I visit the basic page then the title should be set
Given I visit basic
Then the page title should be Colins Basic Example