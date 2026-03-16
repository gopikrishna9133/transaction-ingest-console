# Transaction Ingest Console Application

## Overview

This console application ingests transaction snapshots from the last 24 hours and maintains them in a SQLite database.

## Features

- Insert new transactions
- Update existing transactions
- Audit trail for changes
- Revocation detection
- Idempotent execution

## Tech Stack

.NET Console Application
Entity Framework Core
SQLite

## How to Run

dotnet restore
dotnet build
dotnet run
