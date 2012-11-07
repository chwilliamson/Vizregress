Feature: Github

Scenario: Does the home page look the same
     Given I visit github
     Then the screen should look like
     And I close the browser