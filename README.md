
# Hotel Search API

## _Domain-Driven Design Architecture Demo_

### Lmx.HotelSearch.API
- user interface and entry point from user point of view
- depends on Application and Infrastructure layer
- hosts Controllers, logging setup and Middleware depending on needs
### Lmx.HotelSearch.Application
- application layer hosts Queries and Handlers for CQRS pattern, various View Models, Validators, Mappers and Services consumed in presentation layer (in this case it's API)
### Lmx.HotelSearch.Domain
- hosts Domain entity models and Repository Abstractions
### Lmx.HotelSearch.Infrastructure
- used for ORM and data access
- in this test solution we have a simple repository implementation that mimics DB access
- we can also implement any other external service like Payment, Email, File Storage etc.
- can be expanded to additional Persistence layers for specific DB access SQL, Postgres or simply implement Infrastructure.SQL projects for multiple data providers
### Lmx.HotelSearch.Tests
- tests for HotelValidator

# _Notes about implementation_

- API endpoints added
- initial Class library refactored to follow _S.O.L.I.D_ principles
- test names refactored to give better idea on what's tested with _AAA_ pattern
- tests run after each successful build
- Serilog setup for console and file logging
- .Net 6
- striving to have 'Magic numbers' removed from code for better overall clarity

## How to make this code base production ready?

- basic ApiKeyAuth middleware added, but it will need a proper auth
- additional logging from Controller to services and validators for easier debugging (this implementation only has some simple logging setup for demo purposes)
- add more tests
- connect to database and implement Persistence for it with (DbContext, data seeding, migrations, caching etc.)
- hook into pipeline

## Test data

    User location
    46.50, 0.32
    
    Hotels
    {
      "name": "Hotel 50",
      "price": 50,
      "geoLocation": {
        "latitude": 46.50,
        "longitude": 0.20
      }
    }
    
    {
      "name": "Hotel 51",
      "price": 51,
      "geoLocation": {
        "latitude": 46.50,
        "longitude": 0.10
      }
    }
    
    {
      "name": "Hotel 52",
      "price": 52,
      "geoLocation": {
        "latitude": 46.50,
        "longitude": -0.10
      }
    }
    
    {
      "name": "Hotel 53",
      "price": 10,
      "geoLocation": {
        "latitude": 46.50,
        "longitude": -0.10
      }
    }
    
    
    
    Update
    {
      "id": "ec4b2f92-7e44-4f86-b647-dee6523d370f",
      "name": "Hotel 52 - updated",
      "price": 5,
      "geoLocation": {
        "latitude": 46.50,
        "longitude": -0.10
      }
    }
