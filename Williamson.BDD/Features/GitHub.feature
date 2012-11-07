Feature: Github allows people to store their source code

Scenario: Does the home page look the same
     Given I visit github
     Then the screen should look like GitHub.Home

Scenario: : I like to see that the number of respositories has increases
    Given I visit github
    Then the repository count should be greater than 4244904