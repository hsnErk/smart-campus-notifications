# Smart Campus Announcement & Notification System

**BIL 3204 - Software Architecture & Design — Final Project**

A console application that publishes campus announcements (exam changes, events,
library hours) and notifies the right users over their preferred channel
(e-mail / SMS / push). It is built around the three mandatory design patterns inside
a four-layer architecture.

## Run

```bash
dotnet run            # from the project root
```

Requires the .NET 8 SDK. There are no external NuGet packages.

## The single idea behind the design

Every decision serves one principle: **isolate the parts that change behind stable
abstractions, and let dependencies point only inward toward those abstractions.**
Each pattern isolates a different axis of change; the layers exist to enforce the
direction of dependencies.

## Layers (dependencies point inward)

```
Presentation  ->  Application  ->  Domain  <-  Infrastructure
```

| Layer | Responsibility | Depends on |
|-------|----------------|------------|
| Domain | Business concepts + contracts (abstractions) | nothing |
| Application | Use cases / orchestration | Domain |
| Infrastructure | Technical detail (channels, logger, config, data) | Domain |
| Presentation | Entry point + composition root | Application, Infrastructure |

```
Domain/         Announcement (abstract) + Exam/Event/Library, User (abstract) + Student/Teacher,
                INotification, INotificationFactory, IObserver, ISubject, ILogger, enums
Application/    AnnouncementPublisher (Observer subject), AnnouncementService (use case),
                Factories/AnnouncementFactory
Infrastructure/ Email/Sms/Push notifications, NotificationFactory, Logger (Singleton),
                ConfigurationManager (Singleton), Data/InMemoryUserStore
Presentation/   Program.cs  (composition root + the required scenario)
```

## Design patterns — where and why

### Observer  —  *"who reacts to a change is not the publisher's concern"*
- **Subject:** `Application/AnnouncementPublisher`  •  **Observers:** `Domain/Users/User` -> `Student`, `Teacher`
- The publisher pushes an announcement to every subscriber without knowing who they
  are or how they want to hear about it. Adding a new audience is a new observer and
  **zero** changes to the publisher. Unsubscribe is demonstrated in the scenario.

### Factory  —  *"the choice of which concrete class to build lives in one place"*
- `Application/Factories/AnnouncementFactory` (announcements) and
  `Infrastructure/Notifications/NotificationFactory` (channels).
- Callers code only against the `Announcement` / `INotification` abstractions. Adding
  a new announcement type or channel touches only its factory, nothing else.

### Singleton  —  *"exactly one shared instance is the correct model"*
- `Infrastructure/Logging/Logger` and `Infrastructure/Configuration/ConfigurationManager`.
- A single audit log and a single settings source are genuinely system-wide. `Lazy<T>`
  makes creation thread-safe and the private constructor makes a second instance
  impossible. Used sparingly, as the brief requires.

### (Bonus) Dependency Inversion ties it together
The Domain defines `INotificationFactory` and `ILogger`; Infrastructure implements
them; the composition root injects the concretes. That is precisely why the Domain
can depend on nothing.

## Requirements checklist

| Requirement | Covered by |
|-------------|------------|
| 2+ user types | Student, Teacher |
| 2+ announcement types | Exam, Event, Library |
| 2+ notification types | Email, SMS, Push |
| Observer / Factory / Singleton | see above |
| Layered architecture | 4 layers |
| Interface or abstract class | INotification, IObserver, ISubject, INotificationFactory, ILogger; `Announcement` & `User` are abstract |
| Simple data management | `InMemoryUserStore` |
| Working example scenario | `Presentation/Program.cs` |

## Note on project structure

The four layers are folders/namespaces inside one runnable project. To enforce the
dependency rule *at compile time*, each layer would become its own class library; the
code is already organised so that refactor is purely mechanical.
