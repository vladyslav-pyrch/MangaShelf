## File Strcture
```
root
├───src
│   ├───ApiGateway
│   ├───Services
│   │   ├───Catalogue
│   │   │   ├───Api
│   │   │   ├───Application
│   │   │   ├───Domain
│   │   │   └───Infrastructure
│   │   ├───Collection
│   │   │   ├───Api
│   │   │   ├───Application
│   │   │   ├───Domain
│   │   │   └───Infrastructure
│   │   ├───Community
│   │   │   ├───Api
│   │   │   ├───Application
│   │   │   ├───Domain
│   │   │   └───Infrastructure
│   │   ├───Customer
│   │   │   ├───Api
│   │   │   ├───Application
│   │   │   ├───Domain
│   │   │   └───Infrastructure
│   │   ├───Identity
│   │   │   ├───Api
│   │   │   ├───Application
│   │   │   ├───Domain
│   │   │   └───Infrastructure
│   │   ├───Marketplace
│   │   │   ├───Api
│   │   │   ├───Application
│   │   │   ├───Domain
│   │   │   └───Infrastructure
│   │   ├───Order
│   │   │   ├───Api
│   │   │   ├───Application
│   │   │   ├───Domain
│   │   │   └───Infrastructure
│   │   ├───Payment
│   │   │   ├───Api
│   │   │   ├───Application
│   │   │   ├───Domain
│   │   │   └───Infrastructure
│   │   └───Recommendation
│   │       ├───Api
│   │       ├───Application
│   │       ├───Domain
│   │       └───Infrastructure
│   ├───Shared
│   │   ├───Application.Abstractions
│   │   ├───Domain.Abstractions
│   │   └───Infrastructure.Abstractions
│   └───SpaClient
└───tests
    ├───ApiGateway
    │   └───Integration
    ├───Catalogue
    │   ├───Integration
    │   │   ├───Api
    │   │   └───Infrastructure
    │   └───Unit
    │       ├───Application
    │       └───Domain
    ├───Collection
    │   ├───Integration
    │   │   ├───Api
    │   │   └───Infrastructure
    │   └───Unit
    │       ├───Application
    │       └───Domain
    ├───Community
    │   ├───Integration
    │   │   ├───Api
    │   │   └───Infrastructure
    │   └───Unit
    │       ├───Application
    │       └───Domain
    ├───Customer
    │   ├───Integration
    │   │   ├───Api
    │   │   └───Infrastructure
    │   └───Unit
    │       ├───Application
    │       └───Domain
    ├───Identity
    │   ├───Integration
    │   │   ├───Api
    │   │   └───Infrastructure
    │   └───Unit
    │       ├───Application
    │       └───Domain
    ├───Marketplace
    │   ├───Integration
    │   │   ├───Api
    │   │   └───Infrastructure
    │   └───Unit
    │       ├───Application
    │       └───Domain
    ├───Order
    │   ├───Integration
    │   │   ├───Api
    │   │   └───Infrastructure
    │   └───Unit
    │       ├───Application
    │       └───Domain
    ├───Payment
    │   ├───Integration
    │   │   ├───Api
    │   │   └───Infrastructure
    │   └───Unit
    │       ├───Application
    │       └───Domain
    ├───Recommendation
    │   ├───Integration
    │   │   ├───Api
    │   │   └───Infrastructure
    │   └───Unit
    │       ├───Application
    │       └───Domain
    └───Shared
        ├───Integration
        │   └───Infrastructure
        └───Unit
            ├───Application
            └───Domain
```
## Note
Recommendation Context will be created at the end when everything else is done.<br>
Marketplace and its supporting contexts (Order, Customer, Payment) will be added only after Collection, Community, Identity, Catalogue contexts.

## Todo log:
- [ ] Add project references