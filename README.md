# EcConfig (Easy Client Configuration)
[![Build Status](https://travis-ci.org/spywen/EcConfig.svg?branch=master)](https://travis-ci.org/spywen/EcConfig)

### What is EcConfig ?
**EcConfig is a simple Nuget plugin (for Microsoft development platform including .NET) to give the control to your client of your .NET application without risking to break your service by using different file(s) than app.config or web.config files, and then some !**

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
1. Import EcConfig.Core
```csharp
using EcConfig.Core; //EcConfig.Core.Config.Get(...)
```
2. Get property
```csharp
Config.Get("myKey");
```
3. Get property inside sub part
```csharp
Config.Get("mysubpart.myKeyInsideSubPart");
```
4) Get property as an integer
```csharp
Config.Get("myKey").toInt();
```

##### e. Example : Use it inside console project
*Find full example inside EcConfig Github repository with the project :* `EcConfig.Example.Console` <br/>
This example is based on a simple default.config file at the root of the project: <br/> 
EcConfig.Example.Console/default.config
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
EcConfig.Example.Console/Program.cs
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
Console output: <br/>
![EcConfig.Example.Console Console output](https://raw.githubusercontent.com/spywen/EcConfig/master/Resources/ConsoleOutput.png "EcConfig.Example.Console Console output")

##### f. Example : Use it inside web project
*Find this example inside EcConfig Github repository with the project :* `EcConfig.Example.Web` <br/>

This example is a little bit more complete because configured thanks app settings. So the configuration file which contains properties is stored a specific folder : "./configs/" and is called : "configs.config"<br/> 
EcConfig.Example.Web/Web.config
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

EcConfig.Example.Web/configs/configs.config
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
EcConfig.Example.Web/Views/Shared/_Layout.cshtml
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
Web site output: <br/>
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


