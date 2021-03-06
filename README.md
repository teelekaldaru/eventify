# Eventify web application

## About
Used technologies:

- Client-side: Angular 11, Bootstrap 5.1
- Backend: .NET 6, EF Core, NUnit
- Database: PostgreSQL

## Run application
1. Clone the project into your computer
2. Open console in solution root folder
3. If you are running application for the first time, install npm packages
	- cd Eventify.Web/ClientApp
	- npm install
4. Go back to root folder and run following command
	```
	dotnet run --project Eventify.Web
	```
5. Open http://localhost:44350/ in browser

## Run unit tests
1. Open console in solution root folder
2. Run following command:
	```
	dotnet test
	```
	

---
To update database structure, open console in solution root folder and run this command:
```
dotnet ef database update --project Eventify.DAL --startup-project Eventify.Web
```
---

*Teele Kaldaru, 2022*

