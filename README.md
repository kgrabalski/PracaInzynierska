This project is part of my Engineering Thesis “Analysis, design and implementation of integrated management and promotion system for gastronomy business”.
System is made of three major parts: web and mobile applications (Android + WP8) for end users, restaurant owner/employee panel and system administrator panel. End users can order food from partnership restaurants within their delivery range (currently implemented for Warsaw, Poland). From the perspective of partnership restaurant owner/employee system exposes simple interface to manage user’s orders, restaurant menu and more. 
Working system can be shipped either on Microsoft Azure platform (which I used) or on-premise installation. 

System is implemented with 4-layer architecture: Data Access Layer, Business Logic, Service and Presentation Layers. During implementation I used many frameworks, tools and libraries listed below:
-	Database: SQL Azure Database or SQL Server 2014 Express, I used stored procedures and spatial data types
-	DAL: NHibernate, FluentNHibernate, Automapper – most of data is accessed by IQueryOver interface or stored procedures
-	Service Layer
  -	Server side: REST service implemented with ASP.NET WebAPI 2, DI with Ninject
  -	Client side: PCL using Json.NET and Microsoft HTTP Client
-	Presentation Layer
  -	End-user web application interface – implemented with ASP.NET MVC 5
    - Bootstrap 3
    - Google Maps API V3
    - jQuery
    - knockout.js
    - Ninject for DI
    - Selectize.js
    - SignalR
    - Starr.js
  -	Restaurant owner/employee and system administrator panel
    - ASP.NET MVC5
    -	ASP.NET WebAPI 2
    - AngularJS
    - AngularUI
    - AdminLTE administrator panel template
  -	Mobile applications for Windows Phone 8 and Android
    - Xamarin.Forms under Xamarin Indie license (free for students)
    - Ninject Portable
    -	Xamarin.Forms Behaviors
    - Xamarin.Forms Mobile Services
    - Xamarin.Forms UserDialogs
    - Mobile applications were developed using MVVM pattern
-	Testing – unit and integration test are using nUnit, database integration was tested using LocalDb instance of database
-	While writing Engineering Thesis I used UML diagrams provided by Visual Studio 2013 Ultimate
-	Source control system – Git on Visual Studio Online platform
