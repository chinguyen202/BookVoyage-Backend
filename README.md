
# BookVoyage Backend project

## List of contents

1. [Description](#description)
2. [Backend API endpoints](#backend-api-endpoints)
3. [Backend packages](#backend-packages)
4. [Project structure](#project-structure)
5. [Database design](#database-design)


## Description

BookVoyage is an E-Commerce Backend RESTFull API with an ASP.NET Core WebAPI

## Backend API endpoints

The most up-to-date API Endpoint documentation can be found here [Swagger](https://bookvoyage-server.azurewebsites.net/index.html) 

## Backend packages
- AutoMapper.Extensions.Microsoft.DependencyInjection Version 12.0.1
- EFCore.NamingConventions Version 7.0.2
- Microsoft.AspNetCore.Authentication.JwtBearer Version 7.0.10
- Microsoft.AspNetCore.OpenApi Version 7.0.10
- Microsoft.EntityFrameworkCore Version 7.0.10
- Microsoft.EntityFrameworkCore.Design Version 7.0.10
- Npgsql.EntityFrameworkCore.PostgreSQL Version 7.0.4
- Swashbuckle.AspNetCore Version 6.5.0
- Swashbuckle.AspNetCore.Filters Version 7.0.8
- System.IdentityModel.Tokens.Jwt" Version="6.32.1"
- Microsoft.AspNetCore.Identity.EntityFrameworkCore Version="7.0.10"
- FluentValidation Version="11.7.1"
- MediatR Version="11.1.0"

## Project tree

This project on the backend side is implementing CLEAN architecture. The layers are the following:
1. Core: Domain and Application
2. Infrastructure: Infrastructure and Persistence
3. Presentation: WebAPI

![Project structure](https://github.com/chinguyen202/BookVoyage-Backend/assets/58989517/e2625679-2f75-40db-894e-c3ebed568d5f)

### Database design
![BookVoyage Database ERD](https://github.com/chinguyen202/BookVoyage-Backend/assets/58989517/84eda64c-ea48-430d-a19c-65ab31c98a30)
