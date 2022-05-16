###Migrations
##### Add or remove migrations
dotnet ef migrations add InitialCreate --project Eventify.DAL --startup-project Eventify.Web
ef migrations remove

##### Update database
dotnet ef database update --project Eventify.DAL --startup-project Eventify.Web

