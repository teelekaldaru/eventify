###Migrations
##### Add or remove migrations
dotnet ef migrations add InitialCreate --project Foodly.DAL --startup-project Foodly.Web
ef migrations remove

##### Update database
dotnet ef database update --project Foodly.DAL --startup-project Foodly.Web

