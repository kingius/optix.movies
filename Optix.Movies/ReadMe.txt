1. Please change the connection strings in appsettings within the Optix.Movies.Api project and the Optix.DataImporter project to the database of your choice.
2. Open the Package Manager Console, set Optix.Movies.Model as the default project and then type in:
	Update-Database
3. You now have the database tables in place. Set the Optix.Movies.DataImporter as your start up project and run it.
4. You now have the CSV file imported into the database and ready for the API to query.
5. Set the Optix.Movies.Api project as startup and run the project.
6. You can now use the 'try it out' feature of each API in swagger to test the API and get results!

There are also some unit tests to show basic functionality is working, although I ran out of time to add more.
I started this by originally reading the CSV and serving by the API, then moved to the EF side and importing it into the database.
Finally I connected the API to the database.

Deployment:
Example given using a Visual Studio publish profile (time was against me!)

Limitations:
The API only supports exact matches at this point, though they are case insensitive.
Error handling is not present in the API (no friendly error messages).
No authentication or authorisation, though Identity is referenced in the API project which would make this possible.
Finally the code is a little bit scrappy and not very polished - this is the problem with only having so much time to do these tests and they are really big that take multiple days to put together.
