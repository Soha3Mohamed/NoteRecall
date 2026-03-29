# Progress Tracker
# 9-3-2026
- started to work on the structure of the project -> onion architecture
- Added a first version of the entities
- Added ServiceResult in Common folder
- Added the repository interfaces in the Core layer

# 10-3-2026
- Today was all about doing the most automatic simple thing in the world, opening the project in the browser and using swagger, 
   ooh that was hard but it turned to be in launcsettings.json file, i just needed to change the launchUrl to swagger and launchbroswer to true and it worked,

- Added the DbContext and the connection string, and added the service for the DbContext in the Program.cs file, and now i can use it in the application without any problem, 
   but i need to add the migration and update the database to create the tables in the database.
- Migration and updating database is done.

# 23-3-2026
- Added the repositories implementation in the Infrastructure layer
- 
# 24-3-2026
- Added the services interfaces in the Core layer

# 25-3-2026
- Added the user service implementation in the Application layer
- 
----------
what needs to be done today at night?
- Add the note service implementation in the Application layer ###################################################
- Add the question service implementation in the Application layer ###################################################
- Add the review session service implementation in the Application layer ###################################################
 
after that?
- decide and read about the best way to implement the scheduling of the questions, and then implement it in the application, 
- and then add the controllers for the user, note, question and review session, and then test the application using swagger.

after that?
- starting to implement fake ai integraiton to see request and response 
- maybe integration with chatgpt?

after that?
- and then add some unit tests for the services and repositories, and then test them using xUnit or any other testing framework.
- post on linkedin 
- and then maybe start working on the frontend part of the application using Blazor or any other frontend framework, and then connect it with the backend using API calls.
- 