# Transaction Ingest Console Application

## Overview

This console application processes transaction snapshots from the last
24 hours and maintains them in a SQLite database. The program simulates
a scheduled ingestion job that receives unordered transaction data and
reconciles it with previously stored records.

The application supports inserting new transactions, detecting updates,
marking revoked transactions, and recording an audit trail for changes.

---

## Features

- Insert new transactions using TransactionId as the unique identifier
- Update existing transactions when tracked fields change
- Maintain an audit trail of field-level changes
- Detect revoked transactions that disappear from the snapshot
- Idempotent execution (running the job multiple times does not create
  duplicates)

---

## Architecture

The application is structured into simple layers:

- **Models** Defines the data entities such as `Transaction` and
  `TransactionAudit`.

- **Data** Contains the `AppDbContext` used by Entity Framework Core
  to interact with the SQLite database.

- **Services** Contains business logic including:
  - Mock API ingestion
  - Transaction reconciliation
  - Change detection
  - Revocation handling
  - Audit logging

- **Program.cs** Entry point of the console application. Orchestrates
  the ingestion process.

---

## Project Structure

```
TransactionIngest
│
├── Data
│ └── AppDbContext.cs
│
├── Models
│ ├── Transaction.cs
│ └── TransactionAudit.cs
│
├── Services
│ ├── MockApiService.cs
│ └── TransactionService.cs
│
├── mockdata.json
├── Program.cs
├── TransactionIngest.csproj
```

---

## Tech Stack

- .NET Console Application
- Entity Framework Core
- SQLite

---

## How to Run

Restore dependencies:

dotnet restore

Build the application:

dotnet build

Run the program:

dotnet run

When executed, the application will read transaction data from
`mockdata.json`, process it, and store results in a SQLite database.

---

## Assumptions

- TransactionId uniquely identifies each transaction.
- The JSON snapshot represents the latest view of transactions within
  the last 24 hours.
- Transactions missing from the latest snapshot but still within the
  24-hour window are marked as **revoked**.
- Transactions older than 24 hours are not modified.
