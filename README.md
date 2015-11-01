# EcConfig (Easy Client Config)
[![Build Status](https://travis-ci.org/spywen/EcConfig.svg?branch=master)](https://travis-ci.org/spywen/EcConfig)

### EcConfig, what is that ?
**EcConfig is a simple Nuget plugin (for Microsoft development platform including .NET) to give the control to your client of your .NET application without risking to damage your service by using different file(s) than app.config or web.config files, and then some !**

EcConfig provides you following functionalities :
* split up app.config (web.config) development configurations from client oriented configurations properties
* possibilities to create sub-parts of properties 

---
<br/>
As in regular app.config or web.config files you would find : Connection strings (of course !), links to wcf services, entity framework configurations, configSections, many other ununderstandable parts for most of us... ;) AND obviously APP SETTINGS !!!
Actually from the client point of view :
* the connexion strings : "we don't care !",
* wcf services linked to the app : "we don't care !",
* entity framework configs : "we don't care !",
* configSections : "what is this ? Don't care !"
* app settings : "This is the real attractive feature of this package because you could set up the app SIMPLY and BY YOURSELF - thanks to app settings - without asking for any evolution !"

BUT, obviously all this can imply some potential issues : 
- when client tries to change app settings and unfortunately changes another part of the configuration files ==> **service failures could occur**
- app settings part could be composed of thousands of settings ==> **how long do we need to find the correct configurations ? Meanwhile service failures could occur if we change wrong setting**
- app settings could be modified without type considerations ==> **service failures could still occur**
- etc...

**The purpose of EcConfig (Easy Client Configuration) is to provide an easy and powerful way to edit these configurations for the client in limiting risks.**

**How it works ?** : EcConfig simply externalizes app settings from configuration file(s) (app.config/web.config) in order to separate application development configurations (which should not be modified without development team approbation) from app settings which allow client to change some global options of his application. For future release a simple web application will allow client to update these settings thanks to a simple web page.



### 1. How to use it ?
##### a. Installation thanks to Nuget package manager
1. First of all, be sure that Nuget Package Manager is installed with your Visual Studio. Find more information about how to install it at : [https://docs.nuget.org/consume/installing-nuget](https://docs.nuget.org/consume/installing-nuget)
2. Right click on your solution then click on `Manage Nuget Package`>`Online`
3. Search for `EcConfig (Easy Client Config)` then click on `Install`
4. Select projects which will use EcConfig
5. Now it's done, you can use EcConfig ;)

##### b. Create your config file
Once EcConfig is installed inside your project(s) you just have to create your own configuration file and use it !

1. Create xml file and insert it inside your root project
2. Rename it as `default.config` (default EcConfig filename which could be modifiate thanks to EcConfig configurations. See [2. How to configure it ?](#2.-How-to-configure-it-?))
3. Right click on it and click on properties. Select `Copy each time` for `Copy in output directory` option  
4. Base default.config file should look like :
```xml
<?xml version="1.0" encoding="utf-8" ?>
<ec>

</ec>
```

##### c. Add properties and sub parts of properties
Example of property :
```xml
<?xml version="1.0" encoding="utf-8" ?>
<ec>
  <p key="myKey" value="1"/>
</ec>
```

Example of property inside a sub part:
```xml
<?xml version="1.0" encoding="utf-8" ?>
<ec>
  <p key="myKey" value="1"/>
  <mysubpart>
    <p key="myKeyInsideSubPart" value="myValueInsideSubPart"/>
  </mysubpart>
</ec>
```

##### d. Get properties inside my application
- Import EcConfig.Core
```csharp
using EcConfig.Core; //EcConfig.Core.Config.Get(...)
```

- Get property
```csharp
Config.Get("myKey");
```

- Get property inside sub part
```csharp
Config.Get("mysubpart.myKeyInsideSubPart");
```

- Get property as an integer
```csharp
Config.Get("myKey").toInt();
```

##### e. Example : Use it inside console project
*Find full example inside EcConfig Github repository with the project :* `EcConfig.Example.Console` <br/>
This example is based on a simple default.config file at the root of the project: <br/> 
*EcConfig.Example.Console/default.config*
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
*EcConfig.Example.Console/Program.cs*
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
*Console output* <br/>
![EcConfig.Example.Console Console output](https://raw.githubusercontent.com/spywen/EcConfig/master/Resources/ConsoleOutput.png "EcConfig.Example.Console Console output")

##### f. Example : Use it inside web project
*Find this example inside EcConfig Github repository with the project :* `EcConfig.Example.Web` <br/>

For this example we decided to configure EcConfig to search for a configs.config file inside a configs folder thanks to EcConfig configurations. See [2. How to configure it ?](#2.-How-to-configure-it-?))<br/> 
*EcConfig.Example.Web/Web.config*
```xml
<?xml version="1.0" encoding="utf-8"?>
<!--
  Pour plus d’informations sur la configuration de votre application ASP.NET, rendez-vous sur 
  http://go.microsoft.com/fwlink/?LinkId=301880
  -->
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

*EcConfig.Example.Web/configs/configs.config*
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
*EcConfig.Example.Web/Views/Shared/_Layout.cshtml*
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
*Web site output* <br/>
![EcConfig.Example.Web screenshot](https://github.com/spywen/EcConfig/blob/master/Resources/EcConfig.Example.Web.png "EcConfig.Example.Web screenshot")

##### g. ... or any other kind of .NET applications
Of course, EcConfig is not limited of this two kind of .NET applications. Be original and use it with all your products !

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
In order to be able to procceed to Unit tests or any other kind of tests on a part of your application which could use EcConfig: just configured the app.config of your test project and configure it to be able to find a config file inside the same project.

---
### 4. How it works ?
##### a. Architecture

##### b. Cache

##### c. Testing


---
### 5. Releases
##### 1.0.0 : Initial release
* Get properties inside other file than app or web .config files
* Configure file name, path and case sensitivity
* Properties cached for performance reasons

---
### 6. License
MIT


---


