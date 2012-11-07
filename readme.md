#VDD my version of Visual Driven Development
This is an experiment where I use TDD to develop coding units and behaviours by writing tests first.
I used SpecFlow and my BDD tool to perform browser based integration testing against a fully deployed running system. 
For me BDD sits strongly within the QA domain and maps nicely when documenting features and scenarios.

##My Concept
We (my current company) currently use Selenium WebDriver to run Chrome and FireFox browser Automation Tests
 which is notifying us of regression and highlights some user interface design issues.  

One thing that we kept missing in terms of regression was spotting visual elements that change.  The Selenium WebDriver
will continue to perform and run tests despite the page looking visually corrupt.

This is a research library using AForge.Net Framework (http://code.google.com/p/aforge/). The idea to a create a set of expected images for different parts
of the application that we can compare during a running test.

#Stuff to know
- You can add rgb regions/zones to indicate parts of the application you want to ignore.  The RGB region is configurable but defaults to FFFFD800.
- Images currently have to be saved as 24 bit png files.
