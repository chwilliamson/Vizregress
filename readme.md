#Visual Driven Development (VDD)
Spot visual regression in your web application by comparing screenshots against a approved set of approved screenshots.

This is my project where I use TDD (Test Driver Development) to develop units of code and behaviors when writing tests first.  
You'll see that *Williamson.VDD.Tests* performs tests against *Williamson.VDD*.

I use [SpecFlow](http://www.specflow.org/) as my BDD (Behavior Driven Development) tool to perform browser based tests. 
For me BDD sits strongly within the QA domain and maps nicely when documenting features and scenarios.

##About
###Background experience
From experience I have used the Selenium WebDriver to run Chrome and FireFox browser automation tests which notifies us of regression 
and highlights some user interface design issues.  

The integration tests are executed on Jenkins which we use as our Continuous Delivery platform.  
After successful integration tests a staging release is made which is reviewed by the QA team.

###The Problem
One thing that we kept missing in terms of regression was spotting visual elements that change.  
A simple CSS change can be catastrophic for another view/page that has some css hacks.
The Selenium WebDriver will continue to perform and run tests despite the page looking visually incorrect.

This is a research project using [AForge.Net Framework](http://code.google.com/p/aforge/). 
The idea to a create a set of expected images for different parts of an application that we can compare in a test.

#Project Breakdown
##Williamson.VDD
Contains the core image utilies based on [AForge.Net Framework](http://code.google.com/p/aforge/) for determining whether or not images are equal.

##Williamson.VDD.Tests
Contains tests for *Williamson.VDD* using NUnit.

##Williamson.BDD.Examples
Contains example selenium tests using the WebDriver to assert DOM properties.  
It also takes screenshots and compares with some base embedded resources.

I have used SpecFlow to create features so you'll need to download the SpecFlow with NUnit extension to run them.

Take this project and modify, make your own and grow :)  It's mean't to be your starting point.

##Stuff to know

###Image naming conventions
Images are currently stored as embedded resources at *Williamson.BDD.Images* and structured using the following naming convention:

* Site name (e.g GitHub)
  * Browser (e.g. FireFox)
   * Page.png
   * Page.en-GB.png

If a page variant Page.en-US.png isn't found then it'll fall fack to Page.png.


###Exclude regions/zones 
You can add regions/zones to indicate parts of the application you want to ignore during image comparison.  
The RGB region is configurable but defaults to FFFFD800.

e.g. The colour FFD800 (like yellow) is the ignore zone. The zone below ignores the github stats bar. 
The stats can be testing using the selenium web driver.
![](https://raw.github.com/cwilliamson1980/Williamson.VDD/master/Williamson.VDD.Tests/Images/Github.Home.IgnoreSections.png?raw=true)

###Images currently have to be saved as png files
This is the way it is for the moment.

###Fragile Tests
It's likely some tests will fail since I'm basing them on websites that are dynamically changing every day.  
As I add loads of samples I will try to find something a bit more stable and maybe include a self hosted web application.

###Differing images
Your default browser size may differ from mine so you might have failing tests due to mismatching image sizes.
I'm using FireFox 3.6.28 so it's possible later versions might render things differently.

###Images are exported on failure
If a failure occurs the actual image is written to the executing domain base directory with naming convention of *foo.actual.png*, *foo.expected.png* and *foo.difference.png*.  
These paths should be detailed in the asserting like:

Expected Image: foo.expected.png
But was: foo.actual.png
Difference:foo.difference.png

*foo.difference.png* will give you an idea of which zones to check. You could try inverting the image to see if it's clearer.

You can then update the relavent resources in */Williamson.BDD/Images/GitHub/* to get your tests running.

#My Notes
VDD is probably used by me wrong at the moment since the development is NOT driven by an image.  
Images are captured after the development process to help spot regression. I'll need to change this at some point.

QA will create/update set of accepted images which will be compared against in future tests.  
This should allow us to spot subtle changes and act accordinally.

#TODO

##Resource Fallback rules
Decide how to fallback when a resource isn't found.

e.g. FireFox.en-GB.png may not eists so lets try FireFox.png.