# EcConfig (Easy Client Config)

[![Join the chat at https://gitter.im/spywen/EcConfig](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/spywen/EcConfig?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)
[![Build Status](https://travis-ci.org/spywen/EcConfig.svg?branch=master)](https://travis-ci.org/spywen/EcConfig)
[![forthebadge](http://forthebadge.com/images/badges/built-with-love.svg)](http://forthebadge.com)

![EcConfig logo](https://raw.githubusercontent.com/spywen/EcConfig/master/Resources/EcConfigLogo.png "EcConfig logo")

### EcConfig, what is that ?
**EcConfig is a simple Nuget plugin (for Microsoft development platform including .NET) to give the control to your client of your .NET application without risking to damage your service by using different file(s) than app.config or web.config files, and then some !**

EcConfig provides you following functionalities :
* split up app.config (web.config) development configurations from client oriented configurations properties
* possibilities to create sub-parts of properties 

---

## Table of contents
1. [How to use it ?](#1-how-to-use-it-)
    - [a. Installation thanks to Nuget package manager](#a-installation-thanks-to-nuget-package-manager)
    - [b. Create your config file](#b-create-your-config-file)
    - [c. Add properties and sub-parts of properties](#c-add-properties-and-sub-parts-of-properties)
    - [d. Get properties inside my application](#d-get-properties-inside-my-application)
    - [e. Example : Use it inside console project](#e-example--use-it-inside-console-project)
    - [f. Example : Use it inside web project](#f-example--use-it-inside-web-project)
    - [g. ... or any other kind of .NET applications](#g--or-any-other-kind-of-net-applications)
2. [How to configure it ?](#2-how-to-configure-it-)
3. [How to test it ?](#3-how-to-test-it-)
4. [How it works ?](#4-how-it-works-)
    - [a. Architecture](#a-architecture)
    - [b. Cache](#b-cache)
    - [c. Testing](#c-testing)
5. [Can i improve EcConfig ?](#5-can-i-improve-ecconfig-)
6. [Releases](#6-releases)
7. [License](#7-license)
8. [FAQ](#8-faq)
9. [Trunk Based Development (TBD) branching model](#9-trunk-based-development-tbd-branching-model)

---
<br/>
As in regular app.config or web.config files you would find : Connection strings (of course !), links to wcf services, entity framework configurations, configSections, many other ununderstandable parts for most of us... ;) AND obviously APP SETTINGS !!!
Actually from the client point of view :
* the connexion strings : "I don't care !",
* wcf services linked to the app : "I don't care !",
* entity framework configs : "I don't care !",
* configSections : "What is this ? Don't care !"
* app settings : "This is the real attractive feature of my product because I could set up it SIMPLY and BY MYSELF without asking for any evolution!"

BUT, obviously all this can imply some potential issues from development team point of view: 
- when client tries to change app settings and unfortunately changes another part of the configuration files ==> **service failures could occur**
- app settings part could be composed of thousands of settings ==> **how long do we need to find the correct configurations ? Meanwhile service failures could occur if we change wrong setting**
- app settings could be modified without type considerations ==> **service failures could still occur**

**The purpose of EcConfig (Easy Client Config) is to provide an easy and powerful way to edit these configurations for the client by limiting risks.**

**How it works ?** : EcConfig simply externalizes app settings from configuration file(s) (app.config/web.config) in order to separate application development configurations (which should not be modified without development team approbation) from app settings which allow client to change some global options of his application.


---
### 1. How to use it ?
##### a. Installation thanks to Nuget package manager
1. First of all, be sure that Nuget Package Manager is installed with your Visual Studio. Find more information about how to install it at : [https://docs.nuget.org/consume/installing-nuget](https://docs.nuget.org/consume/installing-nuget)
2. Right click on your solution then click on `Manage Nuget Package`>`Online`
3. Search for `EcConfig (Easy Client Config)` then click on `Install`
4. Select projects where EcConfig will be used
5. Now it's done, you can use EcConfig ;)

##### b. Create your config file
Once EcConfig is installed inside your project(s) you just have to create your own configuration file and use it !

1. Create xml file and insert it inside your root project
2. Rename it as `default.config` (:information_source: default EcConfig filename which could be modifiate thanks to EcConfig configurations. See [2. How to configure it ?](#2-how-to-configure-it-)
3. :warning: Right click on it and click on properties. Select `Copy each time` for `Copy in output directory` option  
4. Edit the content of default.config with :
```xml
<?xml version="1.0" encoding="utf-8" ?>
<ec>

</ec>
```
The baseline content of EcConfig files should look like that.

##### c. Add properties and sub-parts of properties
Example of property :
```xml
<?xml version="1.0" encoding="utf-8" ?>
<ec>
  <p key="myKey" value="1"/> <!-- Simple property. Key : myKey-->
  <mysubpart>
    <p key="myKeyInsideSubPart" value="myValueInsideSubPart"/> <!-- Property inside a sub-part. Key : mysubpart.myKeyInsideSubPart -->
  </mysubpart>
</ec>
```

##### d. Get properties inside my application
```csharp
using EcConfig.Core; //EcConfig.Core.Config.Get(...)
[...]
Config.Get("myKey"); //Get simple property (as a string by default)
Config.Get("mysubpart.myKeyInsideSubPart"); //Get property inside sub-part
Config.Get("myKey").toInt(); //Get property as an integer
[...]
```

##### e. Example : Use it inside console project
*Find full example inside EcConfig Github repository with the project :* <a href="https://github.com/spywen/EcConfig/tree/master/EcConfig.Example.Console" target="_blank">EcConfig.Example.Console</a>
<br/>
<br/>

This example is based on a simple `default.config` file at the root of the project: 
<br/>
<br/>

:arrow_lower_right: *EcConfig.Example.Console/default.config*
```xml
<?xml version="1.0" encoding="utf-8" ?>
<ec>
  <p key="rootProperty" value="rootProperty"/>
  <page1>
    <p key="title" value="page 1 title"/>
    <part1>
      <p key="title" value="page 1 part 1 title"/>
    </part1>
    <part2>
      <p key="title" value="page 1 part 2 title"/>
    </part2>
  </page1>
  <p key="number1" value="1"/>
  <p key="number2" value="2"/>
</ec>
```
:arrow_lower_right: *EcConfig.Example.Console/Program.cs*
```csharp
using System.Configuration;
using EcConfig.Core;

namespace EcConfig.Example.Console
{
    class Program
    {
        private static void Main(string[] args)
        {
            //Get property from app.config
            System.Console.WriteLine(@"=== Get property from app.config file: ===");
            System.Console.WriteLine(@"appSettingsProperty value: {0}", ConfigurationManager.AppSettings["appSettingsProperty"]);

            System.Console.WriteLine();
            //Get properties from default.config thanks to EcConfig (default.config file is the default filename configuration for EcConfig)
            System.Console.WriteLine(@"=== Get property from default.config file: ===");
            System.Console.WriteLine(@"rootProperty value: {0}", Config.Get("rootProperty"));
            System.Console.WriteLine(@"page1/title value: {0}", Config.Get("page1.title"));
            System.Console.WriteLine(@"page1/part1/title value: {0}", Config.Get("page1.part1.title"));
            System.Console.WriteLine(@"page1/part2/title value: {0}", Config.Get("page1.part2.title"));
            System.Console.WriteLine(@"number1 + number2 value: {0}", Config.Get("number1").ToInt() + Config.Get("number2").ToInt());

            System.Console.ReadLine();
        }
    }
}
```

:arrow_lower_right: *Console output* <br/>
![EcConfig.Example.Console Console output](https://raw.githubusercontent.com/spywen/EcConfig/master/Resources/ConsoleOutput.png "EcConfig.Example.Console Console output")

##### f. Example : Use it inside web project
*Find this example inside EcConfig Github repository with the project :* <a href="https://github.com/spywen/EcConfig/tree/master/EcConfig.Example.Web" target="_blank">EcConfig.Example.Web</a>
<br/>
<br/>

For this example we decided to configure EcConfig to search for a `configs.config` file inside a `configs` folder thanks to EcConfig configurations. See [2. How to configure it ?](#2-how-to-configure-it-)
<br/> 
<br/> 

:arrow_lower_right: *EcConfig.Example.Web/Web.config*
```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
[...]
<appSettings>
  [...]
  <add key="ecconfig.filename" value="configs" />
  <add key="ecconfig.path" value="./configs/" />
  [...]
</appSettings>
[...]
</configuration>
```

:arrow_lower_right: *EcConfig.Example.Web/configs/configs.config*
```xml
<?xml version="1.0" encoding="utf-8" ?>
<ec>
  <p key="appTitle" value="EcConfig MVC APP"/>
  <home>
    <p key="title" value="Home"/>
  </home>
  <about>
    <p key="title" value="About"/>
  </about>
  <contact>
    <p key="title" value="Contact"/>
  </contact>
</ec>
```
:arrow_lower_right: *EcConfig.Example.Web/Views/Shared/_Layout.cshtml*
```html
<!DOCTYPE html>
<html>
  [...]
  <body>
    <div class="navbar navbar-inverse navbar-fixed-top">
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                @Html.ActionLink(EcConfig.Core.Config.Get("appTitle"), "Index", "Home", new { area = "" }, new { @class = "navbar-brand" })
            </div>
            <div class="navbar-collapse collapse">
                <ul class="nav navbar-nav">
                    <li>@Html.ActionLink(EcConfig.Core.Config.Get("home.title"), "Index", "Home")</li>
                    <li>@Html.ActionLink(EcConfig.Core.Config.Get("about.title"), "About", "Home")</li>
                    <li>@Html.ActionLink(EcConfig.Core.Config.Get("contact.title"), "Contact", "Home")</li>
                </ul>
                @Html.Partial("_LoginPartial")
            </div>
        </div>
    </div>
    [...]
  </body>
  [...]
</html>
```
:arrow_lower_right: *Web site output* <br/>
![EcConfig.Example.Web screenshot](https://github.com/spywen/EcConfig/blob/master/Resources/EcConfig.Example.Web.png "EcConfig.Example.Web screenshot")

##### g. ... or any other kind of .NET applications
Of course, EcConfig is not limited of this two kind of .NET applications. Be original and use it with all your projects !

---
### 2. How to configure it ?
In order to configure it, you can use specific app settings to add to your web or app config files.

#### Filename
**String as a filename**
(*Default: default(.config)*)
```xml
<appSettings>
  <add key="ecconfig.filename" value="dev" /> <!-- Search for dev.config file-->
  ...
</appSettings>
```
Configure filename which contains all your properties.

#### Path
**String as a path**
(*Default: . (project root path)*)
```xml
<appSettings>
  <add key="ecconfig.path" value="./configs/" /> <!-- Search for properties file inside 'configs' folder -->
  ...
</appSettings>
```
Configure path where is stored your config file.

#### IsCaseSensitive
**Boolean true or false**
(*Default: true*)
```xml
<appSettings>
  <add key="ecconfig.isCaseSensitive" value="false" /> <!-- EcConfig not case sensitive -->
  ...
</appSettings>
```
Configure if EcConfig should be case sensitive or not. If EcConfig is configured as case sensitive ; keys "myKey", "MyKEY" will be consider as the same key.

---
### 3. How to test it ?
In order to be able to proceed to Unit tests or any other kind of tests on a part of your application which could use EcConfig: just configured the app.config of your test project and configure it to be able to find a config file inside the same test project.

---
### 4. How it works ?
##### a. Architecture
*EcConfig Architecture*
![EcConfig Architecture](https://raw.githubusercontent.com/spywen/EcConfig/master/Resources/EcConfigArchitecture.png "EcConfig Architecture")

The entry point of the application is : EcConfig.Core.Config.cs class, with public method : `Property Get(string key)`. This class proceed in three steps :

1. Search for EcConfig configurations inside app.config or web.config thanks to `ConfigurationManager.AppSettings`
2. Extract properties from new config file (according to EcConfig previous configurations found) and return them
3. According to the key send as an attribute return corresponding value as a Property object (string by default).

##### b. Cache
In order to improve EcConfig performance:
- EcConfig configurations are cached thanks to `MemoryCache.Default` with key : *EcConfig_EcConfs_bc8c1337d0804ae74f3551c86dad2629*
- EcConfig properties are cached thanks to `MemoryCache.Default` with key : *EcConfig_Properties_bc8c1337d0804ae74f3551c86dad2629*

##### c. Testing
EcConfig is fully Unit tested thanks to the EcConfig.Tests project. This project uses NUnit framework.

---
### 5. Can i improve EcConfig ?
**Of course you can ! You are welcome !**
Don't hesitate to pull request for code modifications or documentation modifications !

---
### 6. Releases
##### 1.0.0 : Initial release
* Get properties inside other file than app or web .config files
* Configure file name, path and case sensitivity
* Properties cached for performance reasons

---
### 7. License
MIT

---
### 8. FAQ

*Nothing for the moment*

---
### 9. Trunk Based Development (TBD) branching model
EcConfig is based on a specific branching model : Trunk Based Development
![Trunk Based Model schema example](http://paulhammant.com/images/what_is_trunk.jpg "Trunk Based Model schema example")
<br/>
*Find more information at : <a href="http://paulhammant.com/2013/04/05/what-is-trunk-based-development/" target="_blank">http://paulhammant.com/2013/04/05/what-is-trunk-based-development</a>*

---

