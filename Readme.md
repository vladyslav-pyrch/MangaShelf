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
Recommendation Context will be created at the end when everything else is done.<br>

Marketplace and its supporting contexts (Order, Customer, Payment) will be added only after Collection, Community, Identity, Catalogue contexts.<br>

Use acceptance tests instead of unit tests for the domain model. (Even though they will be in the unit tests project.)

Use BDD (Behaviour Driven Design) designing tests (Given - When - Then tests)

## Todo log:
- [x] Add project references 
  - references will be set up for individual projects when I start working on them.
- [ ] Add domain abstractions