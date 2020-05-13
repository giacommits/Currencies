# Currencies

The Solution contains

**UI:**  WPF app in .Net Framework that retrieves data from an API about currencies exchange rates, and make proper calculations. It implements MVVM design pattern with Caliburn.Micro and Dependency Injection with Simple IoC Container. The API can be a public APi (https://exchangeratesapi.io/) or a API built in the same Solution.

**BACKEND:** A Web API app in .Net Framework that manage data from a SQL Server database. It implements Dependency Injection with Autofac, Swagger and Entity Framework.

**TEST:** The solution also contains a unit tests Project with xUnit and Moq, although its not complete yet.

By default the solution is configured to run with public API https://exchangeratesapi.io/, just make sure that WPFCurrenciesUI is set as start up project. 

If you want to run with the the internal API, first you should create the database with the CurrenciesDbscript.sql file. Next set the solution for multiple start up projects and select WPFCurrenciesUI and CurrenciesDataManagerAPI. In the App.Config file for the UI Project, under appSettings set the value for "InternalApi" key as your localhost.
Finally in Web.Config file from CurrenciesDataManagerAPI project, make sure that the connection string points to your SQL server. When you run, the UI project will automatically use the Internal API.
The database cointains only sample data, with a few currencies avilables and only 2 days of exchange rates between them.


![Screenshot](https://github.com/giacommits/Currencies/blob/master/Images/screenshot.png)
