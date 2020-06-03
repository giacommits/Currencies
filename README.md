# Currencies

The Solution contains two posibles UI to retrieve data from a Restful API about currencies exchange rates and make proper calculations.

**UI:** MVC Web app in .Net Framework, it implements Dependency Injection with Autofac, jQuery and AJAX.

**UI:**  WPF app in .Net Framework, it implements MVVM design pattern with Caliburn.Micro and Dependency Injection with Simple IoC Container.

 By default both UI projects will utilize a remote API from internet (https://exchangeratesapi.io/) but with some simple configurations they can utilize a local API built in the same solution.

**BACKEND:** A Web API app in .Net Framework that manage data from a SQL Server database. It implements Dependency Injection with Autofac, Swagger and Entity Framework.

**TEST:** The solution also contains unit tests projects with xUnit and Moq.


If you want to run with the local API, first you should create the database with the CurrenciesDbscript.sql file. Next set the solution for multiple start up projects and select the UI project you want to use (or both) and CurrenciesDataManagerAPI. In the config file for the UI Project, under appSettings set the value for "LocalApi" key as your localhost (make sure it is the port number of your API project).
Finally in Web.Config file from CurrenciesDataManagerAPI project, make sure that the connection string points to your SQL server. When you run, the UI project will automatically use the Internal API.
The database cointains only sample data, with a few currencies avilables and only 2 days of exchange rates between them.


![Screenshot](https://github.com/giacommits/Currencies/blob/master/Images/screenshot.png)

![Screenshot](https://github.com/giacommits/Currencies/blob/master/Images/MVCscreenshot.png)
