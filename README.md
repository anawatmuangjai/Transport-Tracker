# Transport-Tracker-API
Simple Vehicle Tracking API with ASP.NET Core Web API 3.0, Entity Framework Core, Swagger

## Prerequisites
* MongoDB

## Dependencies
* MongoDB.Driver
* Swashbuckle.AspNetCore
* Microsoft.AspNetCore.Authentication.JwtBearer
* Microsoft.AspNetCore.Mvc.Versioning
* Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer
* NSubstitute
* Xunit

## How to run the solution on local
* Install MongoDB (download from www.mongodb.com)
* Restore NuGet Packages
* Build solution

## API Authorizations
* Get token for authorize in swagger api document
* Request https://localhost:44331/api/v1/users/authenticate with request body 
* Copy token from response body
* Click Authorze button on top right
* Enter Bearer and than space and paste Token

![](res/TransportTrackerApi.PNG)
