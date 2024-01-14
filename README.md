# HotelBooking
 
# Install:
* "Microsoft.AspNetCore.Identity.EntityFrameworkCore" (6.0.23)
* "Microsoft.AspNetCore.Identity.UI" (6.0.23)
* "Microsoft.EntityFrameworkCore" (6.0.25)
* "Microsoft.EntityFrameworkCore.Sqlite" (6.0.25)
* "Microsoft.EntityFrameworkCore.SqlServer" (6.0.25)
* "Microsoft.EntityFrameworkCore.Tools" (6.0.25)
* "Microsoft.VisualStudio.Web.CodeGeneration.Design" (6.0.16)

# Rest operations
* In appsettings.json file change server name and make "Hotel" database in SQL Server
* Next step, make migrations
* Add-Migration init
* Update-Database

# Already made users and logins
* Admin user data: Email: "admin@admin.com", Password: "Test123."
* Test user data: Email: "user@user.com", Passowrd: "Test123."

# Informations

* The database has the user, rooms, bookings connected to each other and one table not connected, i.e. events
* The user can view events by clicking on the relevant link. The user can book rooms and check their bookings on a separate page. In order to make a booking, you must log in.
* The administrator can also book rooms, add, edit and delete rooms and events.
 
