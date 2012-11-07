#VDD: my version of Visual Driven Development
This is an experiment where I use TDD to develop coding units and behaviours by writing tests first.
I use [SpecFlow](http://www.specflow.org/)  as my BDD tool to perform browser based integration tests against a fully deployed running system. 
For me BDD sits strongly within the QA domain and maps nicely when documenting features and scenarios.

##The problem
We currently use Selenium WebDriver to run Chrome and FireFox browser Automation Tests 
which is notifying us of regression and highlights some user interface design issues.  

One thing that we kept missing in terms of regression was spotting visual elements that change.  
A simple CSS change can be catastrophic for another view that has some css hacks.
The Selenium WebDriver will continue to perform and run tests despite the page looking visually corrupt.

This is a research project using AForge.Net Framework ([http://code.google.com/p/aforge/](http://code.google.com/p/aforge/)). 
The idea to a create a set of expected images for different parts of an application that we can compare in a test.

##Stuff to know
###Exclude regions/zones 
You can add regions/zones to indicate parts of the application you want to ignore during image comparison.  
The RGB region is configurable but defaults to FFFFD800.

e.g. The colour FFD800 (like yellow) is the ignore zone. The zone below ignores the github stats bar. 
The stats can be testing using the selenium web driver.
![](https://raw.github.com/cwilliamson1980/Williamson.VDD/master/Williamson.VDD.Tests/Images/Github.Home.IgnoreSections.png?raw=true)

###Images currently have to be saved as png files

###Fragile Tests
It's likely some tests will fail since I'm basing them on websites that are dynamically changing every day.  
As I add loads of samples I will try to find something a bit more stable and maybe include a self hosted web application.

###Differing images
Your default browser size may differ from mine so you might have failing tests due to mismatching image sizes.
I'm using FireFox 12.0 so it's possible later versions might render things slightly differently.

###Actual Images are exported on failure
If a failure occurs the actual image is written to the executing domain base directory with naming convention of *foo.actual.png*.  
You can then update the relavent resources in */Williamson.BDD/Images/GitHub/* to get your tests running.