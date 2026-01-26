# Citizen Record Information System (C# Console Application)

## 📌 Project Overview

Citizen Record Information System is a console-based C# (.NET) application that allows users to:

- View citizen details using Citizen ID  
- Register new citizens into the system  

The project follows a layered architecture using **Bean, DAO, Service, and Util** namespaces and connects to a **SQL Server** database.

This application validates citizen data before registration and uses ADO.NET for database operations.

---

## 🚀 Features

- View citizen details by Citizen ID
- Register new citizens
- Validates:
  - Unique Citizen ID
  - Age must be ≥ 18
- Custom exception handling (`InvalidCitizenException`)
- DAO pattern for database access
- Clean separation of concerns

---

## 🛠 Technologies Used

- C# (.NET Console Application)
- SQL Server
- ADO.NET (`System.Data.SqlClient`)
- Visual Studio / VS Code

---

## 📂 Project Structure

