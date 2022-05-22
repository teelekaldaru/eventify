# Eventify webapp

1. Clone project into your computer
2. Apply migrations

	- To update database, open console in solution root folder and run this command:
	```
	dotnet ef database update --project Eventify.DAL --startup-project Eventify.Web
	```

3. Start application
	- Open console in solution folder and run this command or run Eventify.Web project in Visual Studio
	```
	dotnet run --project Eventify.Web
	```

4. Open http://localhost:44350/ in browser


*Teele Kaldaru, 2022*

