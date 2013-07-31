#Visual Driven Development (VDD)
Spot visual regression in your web application by comparing screenshots against a approved set of screenshots.

This is my project where I use TDD (Test Driver Development) to develop units of code and behaviours when writing tests.  
You'll see that *Williamson.VDD.Tests* performs tests against *Williamson.VDD*.

I use [SpecFlow](http://www.specflow.org/) as my BDD (Behaviour Driven Development) tool to perform browser based tests. 
For me BDD sits strongly within the QA domain and maps nicely when documenting features and scenarios.

##About
###Background experience
From experience I have used the Selenium WebDriver to run Chrome and FireFox browser automation tests identifies regression.  

The integration tests are executed on Jenkins which we use as our Continuous Delivery platform.  
After successful integration tests a staging release is made which is reviewed by the QA team.

###The Problem
One thing that we kept missing in terms of regression was spotting visual elements that change.  
A simple CSS change can be catastrophic for another view/page that has some Css hacks.
The Selenium WebDriver will continue to perform and run tests despite the page looking visually incorrect.

This is a research project using [AForge.Net Framework](http://code.google.com/p/aforge/). 
The idea to a create a set of expected images for different parts of an application that can be compared in a test.

#Project Breakdown
##Williamson.VDD
Contains the core image utilities based on [AForge.Net Framework](http://code.google.com/p/aforge/) for determining whether or not images are equal.

This is the assembly that you'll consume in your test application.

###Exclude regions/zones 
You can add regions/zones to indicate parts of the application you want to ignore during image comparison. 
This could be to excluded zones that are locale specific or contain dynamic text such as date times.

The RGB region is configurable defaulting to FFFFD800.

e.g. The zone below (yellow rectangle at top) ignores the github statistics bar. 
![](https://raw.github.com/cwilliamson1980/Williamson.VDD/master/Williamson.VDD.Tests/Images/Github.Home.IgnoreSections.png?raw=true)

###Match Regions and Zones

You can identify and compare regions within an image.

####Example:

Below is the original article

![](https://raw.github.com/cwilliamson1980/Williamson.VDD/master/Williamson.VDD.Tests/Images/Zoning/OverallStatus_NoZones.png?raw=true)

I have chosen that I want to extract the donkey image as the zone of interest

![](https://raw.github.com/cwilliamson1980/Williamson.VDD/master/Williamson.VDD.Tests/Images/Zoning/OverallStatus_Zoned.png?raw=true)

so you'll get

![](https://raw.github.com/cwilliamson1980/Williamson.VDD/master/Williamson.VDD.Tests/Images/Zoning/OverallStatus_ZoneCut.png?raw=true)

###Images currently have to be saved as png files
This is the way it is for the moment.

###Differing images
Your default browser size may differ from mine so you might have failing tests due to mismatching image sizes.
I'm using FireFox 12 so it's possible later versions might render things differently.

You'll have to take this into consideration when designing your automation platform.

* Browser size must be constant
* Browser version must be consistent
 * The images will need to be stored independently across versions

You could solve this by allowing your developers to provision VM's that are used by your build environment.

##Williamson.VDD.Tests
Contains tests for *Williamson.VDD* using NUnit.

##Williamson.BDD.Examples
Contains example Selenium tests using the WebDriver to assert DOM properties.  
It takes screenshots and compares them to some expected embedded resources.

I have used SpecFlow to create features so you'll need to download the SpecFlow with NUnit extension to run them.

Take this project and modify, make your own and grow :)  It's meant to be your starting point; or an example of how to use Williamson.VDD.

###Image naming conventions
Images are currently stored as embedded resources at *Williamson.BDD.Images* and structured using the following naming convention:

* Site name
  * Browser
     * Page.png
     * Page.en-GB.png

Example

* Example
 * FireFox12
     * Home.png
     * SelfHost.png

If a page variant Page.en-US.png isn't found then it'll fall back to Page.png.

###Images are exported on failure
If a failure occurs the actual image is written to the executing domain base directory with naming convention of *foo.actual.png*, *foo.expected.png* and *foo.difference.png*.  

These paths should be detailed in the assertion message like:

Expected Image: foo.expected.png
But was: foo.actual.png
Difference:foo.difference.png

*foo.difference.png* will give you an idea of which zones to check.
You could try inverting the image to see if it's clearer to find the difference.

##Williamson.BDD.Examples.Tests
Contains tests for *Williamson.BDD.Examples* using NUnit

##Williamson.Example.Web
A SelfHosted web example required by the selenium tests in Williamson.VDD.Tests.  
It's an ASP.NET Web API SelfHost and I added a custom MessageHandler to serve embedded content for testing purposes.

The following frameworks are used:

* ASP.NET Web API
* Knockoutjs
* jQuery
* jQueryUI