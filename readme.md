# Emperor Code Test

## Description
This project consists of an Umbraco 16.3.4 Starter kit site, with a new page type: "External News Page" this consists of property for the feed source; the URL to an XML list of articles.

Admin login: *admin@admin.com / MW>Y5XSxG?*

## Objective
Integrate this XML feed 
https://raw.githubusercontent.com/EmperorWorks/EmperorCodeTest/refs/heads/main/xmlfeed/news_feed.xml

into this umbraco page https://localhost:44330/news/  (the port may differ on your version!)

The feed URL should be manageable in the CMS, usie the existing property or move it if it makes sense 

Each article should show:

* Image 
* Title
* description truncated to 100 chars followd by an ellipses if the provided summary is longer
* Article date as GMT formatted like these examples: 12th November 2025, 2nd December 2025
* Read more link going to the erxternal site

Articles should be listed by descending date order

HTML and CSS is already created for these components.

## Hints
We will test with other XML feeds that may not be 100% reliable or performant, your code should be able to handle these cases gracefully.

## Instructions
Clone down or fork this repositiory (preferably) and give us the url to your updated version!
This should take around an hour