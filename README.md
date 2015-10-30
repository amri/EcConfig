# EcConfig (Easy Client Configuration)
[![Build Status](https://travis-ci.org/spywen/EcConfig.svg?branch=master)](https://travis-ci.org/spywen/EcConfig)

### What is EcConfig ?
**EcConfig is a simple Nuget plugin (for Microsoft development platform including .NET) which enable you to give the control to your client of your .NET application without risking to break your service by using different file(s) than app.config or web.config files.**

What can you find inside common app.config or web.config files ? Connection string (of course !), links to wcf services, entity frameworks configurations, config section, many other part ununderstandable for most of us... ;) AND obviously APP SETTINGS !!!
Actually from the customer point view :
* the connexion string : "we don't care !",
* wcf services linked to the app : "we don't care !",
* entity framework configs : "we don't care !",
* configSections : "what is this ? Don't care !"
* app settings : "OHHH i can configurate my app by myself thanks to app settings without asking for an evolution and be facturated more than 2000$ to change a "P" by a "D" or provide the possibility to download 4 documents in the same time rather than 3 for the same price ? Great jobs guys !"

BUT, trying to enumerate issues with app settings : 
- if customer trying to change app settings and unfortunately change another part of the configuration files -> **service failures could occured**
- app setting part could be composed of thousand of settings -> **how many time to find the correct configurations ? And service failures could occured if we change wrong setting**
- app settings could be modifiate without type considerations -> **service failures could still occured**
- etc...

**The purpose of EcConfig (Easy Client Configuration) is to provide an easy and powerful way to edit these configurations for the client and limiting risks.**

**How it works ?** : EcConfig simply externalizes app settings from configuration file(s) (app.config/web.config) in order to separate application development configurations (which should not be modifiate without development team approbation) from app settings which enable the client to change some global options of his application. For future release a simple web application will enable the customer to update these settings thanks to a simple web page.


### 1. How to use it ?
##### a. Install thanks to Nuget Package Manager...

##### b. ... inside console project

##### c. ... or inside web project

##### d. ... or any other kind of .NET applications
--

---
### 2. How to configure it  ?
(options)

--

---
### 3. How to test it ?

--

---
### 4. How it works ?
##### a. Architecture

##### b. Cache

##### c. Testing
--

---
### 5. Releases
##### 1.0.0 : Initial release
* Get properties inside other file than app or web .config files
* Configure file name, path and case sensitivity
* Properties cached for performance reasons
--

---
### 6. License
MIT
--

---


