# NetCoreFullStackAutomationSolution
![main image](/images/main.png)  

Test automation solution created with .NET 6 was designed to cover all types of projects setup,
such as Selenium WebDriver, Rest API, SQL tests.
The idea is to create a set of base client projects and add specific client and test projects.

## Prerequisites
- Install Visual Studio https://visualstudio.microsoft.com/downloads/ (Community is enough)
- Clone repository
- Open solution in Visual Studio and build it
- Docker should be installed on machone where you run the tests (Required only if you want to run dockerized app)

## Projects structure

### Core Clients
![core_clients image](/images/core_clients.png) 

-----------------------------------------------------------------------------------------------
#### Dneprokos.Api.Base.Client (In progress)
Base Client project for all API Clients. All basic requests and helpers can be found there

-----------------------------------------------------------------------------------------------
#### Dneprokos.Helper.Base.Client (In progress)
Set of the helper classes and method that can be used by any type of the projects

-----------------------------------------------------------------------------------------------
#### Dneprokos.SqlDb.Base.Client (In progress)
Base Client project for all SQL queries. Maybe useful if you need to perform different SQL calls 

-----------------------------------------------------------------------------------------------
#### Dneprokos.UI.Base.Client
Selenium WebDriver helpers. Very useful if you want to create UI tests


### HerokuApp projects
![heroku_app image](/images/heroku_app.png) 

Client and Test projects for "https://the-internet.herokuapp.com/" application. 
This project was created as UI project test example and has possibilityt to run tests locally or using contanerized app

-----------------------------------------------------------------------------------------------
#### Dneprokos.HerokuApp.UI.Client
UI Client was designed to store PageObjects and website specific data

-----------------------------------------------------------------------------------------------
#### Dneprokos.HerokuApp.UI.Tests
UI Tests was designed to create UI tests.

- How to run the tests from Visual Studio

1) Open "Test Explorer", top navigation menu Test --> Test Explorer (This step only required if you haven't opened it yet)
2) Select .runsettings file, top navigation menu Test --> Configure Run Settings --> Select Solution Wide runsettings File
3) Right click on test of group of the tests you want to run and select "Run" from context menu

- How to run the tests from Command Line

1) Open command line inside of the test project directory
2) Type the following command "dotnet test --settings real_app_server.runsettings"

Note: You can update runsettings file name and run with runsetting you need

![run_test_from_cmd image](/images/run_test_from_cmd.png)

### SqlMockedTests projects
![db_test_project image](/images/db_test_project.png) 
Client and project were designed to show power of CORE SQL projct. Under the hood it runs docker container and inserts data during test run

-----------------------------------------------------------------------------------------------
#### Dneprokos.Mocked.Sql.Client
Project stores some client data like models and scripts. Potentially can grow to custom client

-----------------------------------------------------------------------------------------------
#### Dneprokos.Mocked.Sql.Tests
Test project was designed to run SQL based tests.

- How to run the tests from Visual Studio

1) Open "Test Explorer", top navigation menu Test --> Test Explorer (This step only required if you haven't opened it yet)
2) Select .runsettings file, top navigation menu Test --> Configure Run Settings --> Select Solution Wide runsettings File
3) Right click on test of group of the tests you want to run and select "Run" from context menu

- How to run the tests from Command Line

1) Open command line inside of the test project directory
2) Type the following command "dotnet test --settings container_db_server.runsettings"

Note: You can update runsettings file name and run with runsetting you need


### MoviesApiTests projects
![movies_api image](/images/movies_api.png) 

These test project and client controller were designed to show a power of BaseApi project.

-----------------------------------------------------------------------------------------------
#### Dneprokos.Movies.Api.Client
This is a base Client project that covers Controller, Models, Builder and helpers for running Movies microservice REST API requests

-----------------------------------------------------------------------------------------------
#### Dneprokos.Movies.Api.Tests
This is a project with REST API tests for the Movies microservice

- Prerequisites

1) Please pull and run the following microservice before start running the tests https://github.com/dneprokos/node-rest-services

- How to run the tests from Visual Studio

1) Open "Test Explorer", top navigation menu Test --> Test Explorer (This step only required if you haven't opened it yet)
2) Select .runsettings file, top navigation menu Test --> Configure Run Settings --> Select Solution Wide runsettings File
3) Right click on test of group of the tests you want to run and select "Run" from context menu

- How to run the tests from Command Line

1) Open command line inside of the test project directory
2) Type the following command "dotnet test --settings localhost5000.runsettings"

Note: You can update runsettings file name and run with runsetting you need

- Allure reports
There are two ways how you can run Allure reports for this project. 
But it both way you need to have installed Allure command line - https://www.npmjs.com/package/allure-commandline

a) Run server locally

1) You need to have installed Java 8+ version
2) Open PowerShell for specified project and run already prepared script "GenerateAllureReportAndOpenLocally.ps1"

b) Run server in Docker container

1) You need to have Docker and Docker-compose installed
2) Open PowerShell for specified project and run already prepared script "GenerateAllureReportAndOpenUsingDocker.ps1" 

![movies_api_allure image](/images/movies_api_allure.png) 