# Architectural Styles and Architectural Patterns

---

## 1️⃣ Architectural Style vs Architectural Pattern

### Architectural Style
- A **high-level structural approach** to organizing a system  
- Defines the **overall system shape**
- Less prescriptive and more **conceptual**

### Architectural Pattern
- A **repeatable solution** to a known architectural problem  
- More **concrete and prescriptive**
- Often **implemented directly in code**

---

## 2️⃣ Architectural Styles (System-Level)

### 1. Monolithic Architecture

#### Description
All components (UI, business logic, data access) are deployed as **one single unit**.

#### Characteristics
- Single codebase
- Single deployment
- Shared database

#### Pros
- Simple to develop
- Easy to debug
- Low operational complexity

#### Cons
- Hard to scale
- Tight coupling
- Slow deployments

#### Use Cases
- Small applications
- MVPs
- Early-stage startups

---

### 2. Microservices Architecture

#### Description
Application is broken into **independent, loosely coupled services**, each responsible for a single business capability.

#### Characteristics
- Independent deployment
- Decentralized data
- API-based communication

#### Pros
- High scalability
- Independent teams
- Fault isolation

#### Cons
- Operational complexity
- Distributed system challenges
- Network latency

#### Use Cases
- Large enterprise systems
- Cloud-native platforms

---

### 3. Service-Oriented Architecture (SOA)

#### Description
Similar to microservices but with **shared infrastructure** and **enterprise service buses (ESB)**.

#### Characteristics
- Centralized governance
- Heavy XML/SOAP
- Shared contracts

#### Pros
- Reusability
- Standardization

#### Cons
- Tight coupling via ESB
- Performance bottlenecks

#### Use Cases
- Legacy enterprise systems
- Banking and government systems

---

### 4. Event-Driven Architecture (EDA)

#### Description
Components communicate via **events**, not direct calls.

#### Characteristics
- Asynchronous communication
- Event producers and consumers
- Message brokers

#### Pros
- Loose coupling
- High scalability
- Resilient systems

#### Cons
- Hard to debug
- Event ordering complexity

#### Use Cases
- Real-time systems
- Streaming platforms
- IoT systems

---

### 5. Client–Server Architecture

#### Description
Clients request services; servers provide them.

#### Characteristics
- Centralized server
- Multiple clients

#### Pros
- Easy to manage
- Centralized control

#### Cons
- Single point of failure
- Scalability limitations

#### Use Cases
- Web applications
- Database systems

---

### 6. Peer-to-Peer (P2P) Architecture

#### Description
All nodes act as both client and server.

#### Characteristics
- Decentralized architecture
- Nodes communicate directly

#### Pros
- No central failure point
- Highly scalable

#### Cons
- Security challenges
- Data consistency issues

#### Use Cases
- Blockchain
- File-sharing systems

---

## 3️⃣ Architectural Patterns (Application-Level)

### 1. Layered Architecture (N-Tier)

#### Description
Application is organized into **layers**, each with a distinct responsibility.

#### Typical Layers
- Presentation
- Application
- Domain
- Data Access

#### Pros
- Clear separation of concerns
- Easy to understand

#### Cons
- Can become rigid
- Performance overhead

#### Use Cases
- Enterprise applications
- Traditional web applications

---

### 2. MVC (Model–View–Controller)

#### Description
Separates UI concerns into three components.

#### Components
- **Model** – Business logic
- **View** – UI
- **Controller** – Request handling

#### Pros
- Separation of concerns
- Testable UI logic

#### Cons
- Can become controller-heavy

#### Use Cases
- Web frameworks (ASP.NET MVC, Spring MVC)

---

### 3. MVVM (Model–View–ViewModel)

#### Description
Improves MVC for **data binding**.

#### Components
- ViewModel handles UI state
- View binds directly to ViewModel

#### Pros
- Excellent for rich UIs
- High testability

#### Cons
- Steeper learning curve

#### Use Cases
- WPF
- Xamarin
- Blazor

---

### 4. Clean Architecture

#### Description
Organizes code around **business rules**, not frameworks.

#### Core Rule
> Dependencies always point inward.

#### Layers
- Entities
- Use Cases
- Interface Adapters
- Frameworks

#### Pros
- Highly testable
- Framework-independent

#### Cons
- Initial complexity
- Overhead for small applications

#### Use Cases
- Long-lived enterprise systems

---

### 5. Hexagonal Architecture (Ports & Adapters)

#### Description
Core logic is isolated; external systems plug in via adapters.

#### Pros
- Technology-agnostic
- Easy testing

#### Cons
- More abstraction layers

#### Use Cases
- Domain-driven design
- Integration-heavy systems

---

### 6. Event Sourcing

#### Description
System state is derived from a **sequence of events**, not stored directly.

#### Pros
- Complete audit trail
- Temporal queries possible

#### Cons
- Complex modeling
- Hard migrations

#### Use Cases
- Financial systems
- Audit-heavy domains

---

### 7. CQRS (Command Query Responsibility Segregation)

#### Description
Separates **read models** and **write models**.

#### Pros
- Scales reads and writes independently
- Optimized performance

#### Cons
- Eventual consistency
- Increased complexity

#### Use Cases
- High-traffic systems
- Event-driven microservices

---

### 8. Microkernel (Plugin Architecture)

#### Description
Core system with plug-in extensions.

#### Pros
- Highly extensible
- Modular

#### Cons
- Plugin versioning issues

#### Use Cases
- IDEs
- Operating systems
- CMS platforms

---

### 9. Pipe-and-Filter Architecture

#### Description
Data flows through a sequence of processing components.

#### Pros
- Reusable filters
- Easy parallelism

#### Cons
- Data transformation overhead

#### Use Cases
- Compilers
- ETL pipelines
- Streaming data processing

---

## 4️⃣ One-Line Summaries

- **Monolith**: Single deployable unit  
- **Microservices**: Independent services per business capability  
- **Layered**: Separation by responsibility  
- **Clean Architecture**: Business rules at the center  
- **MVC**: UI separation  
- **EDA**: Systems communicate via events  
- **CQRS**: Separate reads and writes  

---
