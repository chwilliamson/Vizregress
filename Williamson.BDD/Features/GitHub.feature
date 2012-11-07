Feature: Github allows people to store their source code

Scenario: Does the home page look the same
     Given I visit github
     Then the screen should look like GitHub.Home
     And I close the browser