# ITO I Exam – Full Stack Developer

## Overview

This is a simple task management web application built using **.NET 10**, **Blazor**, and **SQLite**.

The system allows users to:

* Create tasks
* View their tasks
* Update task status (Pending, In Progress, Done)

---

## ⚠️ Note on Authentication

Due to time constraints during the exam, the following features were **not fully implemented**:

* Login
* Registration
* User authorization / role-based access

All other required features (Task CRUD, UI, backend structure) are functional.

---

## 🛠️ Tech Stack

* .NET 10
* Blazor (Server/WebAssembly depending on your setup)
* Entity Framework Core
* SQLite

---

## 🗄️ Database

* Database provider: **SQLite**
* The database is created automatically on first run (if migrations are applied)

To apply migrations manually:

```bash
dotnet ef database update
```

---

## ▶️ How to Build

```bash
dotnet build
```

---

## ▶️ How to Run

```bash
dotnet run
```

After running, open the browser at:

```
https://localhost:xxxx
```

(or the URL shown in the terminal)

---

## 📦 Run as Executable (.exe)

To generate a standalone executable:

```bash
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

Output will be located in:

```
On user selection
```

Run the application using:

```
yourapp.exe
```

---

## 📌 Notes

* This project focuses on demonstrating core CRUD functionality and framework usage.
* Authentication and authorization can be added using JWT or ASP.NET Identity if extended further.
* SQLite is used for simplicity and portability.

---
