Feature: Github allows people to store their source code

Scenario: Does the home page look the same
     Given I visit github
     Then the screen should look like GitHub.Home

Scenario: I like to see that the number of respositories has increased
    Given I visit github
    Then the repository count should be greater than 4244904

Scenario: I want to view pricing information
    Given I visit github
    And click the Plans, Pricing and Signup button
    Then I should be at Plans & Pricing page
    And the screen should look like GitHub.Pricing