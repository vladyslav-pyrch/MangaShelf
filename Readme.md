# MangaShelf Project

## How to run

## File Strcture
### The Project
```
root
├───src
│   ├───ApiGateway
│   │
│   ├───Services
│   │   ├───Catalogue
│   │   ├───Collection
│   │   ├───Community
│   │   ├───Customer
│   │   ├───Identity
│   │   ├───Marketplace
│   │   ├───Order
│   │   ├───Payment
│   │   └───Recommendation
│   │
│   ├───Shared
│   │   ├───Application.Abstractions
│   │   ├───Domain.Abstractions
│   │   └───Infrastructure.Abstractions
│   │
│   └───SpaClient
└───tests
    ├───ApiGateway
    │   └───Integration
    │
    ├───Catalogue
    ├───Collection
    ├───Community
    ├───Customer
    ├───Identity
    ├───Marketplace
    ├───Order
    ├───Payment
    ├───Recommendation
    │
    └───Shared
        ├───Integration
        │   └───Infrastructure
        └───Unit
            ├───Application
            └───Domain
```
### A service
```
Service 
├───Api
├───Application
├───Domain
└───Infrastructure
```
### Tests for a service
```
Service
├───Integration
│   ├───Api
│   └───Infrastructure
└───Unit
    ├───Application
    └───Domain
```

## Notes
- Recommendation Context will be created at the end when everything else is done.<br>

- Marketplace and its supporting contexts (Order, Payment) will be added only after Collection, Community, Identity, Profile, Catalogue contexts.<br>

- Use acceptance tests instead of unit tests for the domain model. (Even though they will be in the unit tests project.)

- Use BDD (Behaviour Driven Design) designing tests (Given - When - Then tests)

- Add dotnet Aspire.

## Todo log:
- [x] Add project references 
  - references will be set up for individual projects when I start working on them.
- [x] Add domain abstractions
- [x] Add documentation for the domain abstractions
- Catalogue Context
  - [ ] Brainstorm on the domain model, What there is, What it does.
  - [ ] Implement basic functionality of the domain model.
  - [ ] Write XML documentation to the <b>public</b> members/classes and so on.
- Collection Context
- Community Context
- Identity Context
- [ ] Rename Customer Context to Profile Context
- Profile Context
- [ ] Write a how to run guideline.
- [ ] Make a documentation
- [ ] Make some kind of blogpost about the project. Use your notes for that.