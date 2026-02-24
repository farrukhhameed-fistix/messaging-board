# Messaging Board

A high-performance, real-time messaging application built with **ASP.NET Core** and **Angular**. This project demonstrates a sophisticated implementation of **CQRS (Command Query Responsibility Segregation)**, combining **Entity Framework Core** for data integrity and **Dapper** for high-speed read performance.

## 🏗️ Technical Architecture

This project follows **Clean Architecture** principles to decouple business logic from infrastructure and UI.

### Backend Strategy
* **CQRS Pattern:** Utilizes **MediatR** to separate read and write operations.
* **Command Side (Writes):** Uses **Entity Framework Core** and the **Unit of Work** pattern to ensure consistent state transitions when creating messages.
* **Query Side (Reads):** Uses **Dapper** for lightweight, high-performance fetching of message history via optimized SQL queries.
* **Validation:** Integrated **FluentValidation** pipeline to intercept and validate commands before they reach the domain.
* **Real-time Layer:** **SignalR** broadcasts new messages to all connected clients immediately upon successful database persistence.

### Frontend Strategy
* **Reactive UI:** Built with **Angular** and **Angular Material** for a modern, responsive user experience.
* **State Management:** Reactive services handle the asynchronous flow of data from both RESTful APIs and WebSocket streams.
* **Real-time Integration:** A dedicated SignalR service manages the connection lifecycle and listens for server-side broadcasts to update the UI instantly.

---

## 🔄 Messaging Flow

The following diagram illustrates how a message travels from the Angular UI, through the CQRS pipeline, into the database, and finally out to all connected users via WebSockets.

```mermaid
sequenceDiagram
    participant UI as Angular Client
    participant API as MessagingController
    participant Med as MediatR (Command/Query)
    participant EF as EF Core (Write)
    participant Dap as Dapper (Read)
    participant DB as SQL Database
    participant Hub as SignalR Hub
    participant Users as All Active Users

    Note over UI, Users: Sending a Message
    UI->>API: POST /api/Messaging
    API->>Med: Send(MessageCreateCommand)
    Med->>EF: Create(Message)
    EF->>DB: SaveChangesAsync()
    DB-->>EF: Success
    API->>Hub: Broadcast("ReceiveMessage")
    Hub-->>Users: New Message Received (Real-time)
    API-->>UI: 200 OK

    Note over UI, Users: Loading History
    UI->>API: GET /api/Messaging
    API->>Med: Send(FetchAllMessagesQuery)
    Med->>Dap: QueryAsync("SELECT *")
    Dap->>DB: Fast Read
    DB-->>Dap: Result Set
    Dap-->>UI: Message List
