# StackOverflow Service

This project is a full-stack application as a service with data built on top of - StackOverflow. This includes functionality like searching through Posts
& Marking specific posts to save for later retrieval. After sucessfull authentication, the search history will also be stored in the app.

- The app is a Single Page App, built using pure js, jquery and knockout js framework, with Bootstrap.

- All the data is served from our C# .Net backend. With custom fast
search built on pure SQL queries and with inverted indexes...


### Setup the Database
#### On Windows
1. Install PostgreSQL 
1. Open Terminal to create database and execute `& 'C:\Program Files\PostgreSQL\12\bin\psql.exe' -U postgres -c "create database stackoverflow"`
1. Load database file by running `& 'C:\Program Files\PostgreSQL\12\bin\psql.exe' -U postgres -d stackoverflow -f <your directory path>\stackoverflow_universal.backup`
1. Scaffold normalized schema. Go to project location DatabaseService > DatabaseModels and run in the Terminal `& 'C:\Program Files\PostgreSQL\12\bin\psql.exe' -U postgres -d stackoverflow -a -f stackoverflow-normalized.sql`
1. Scaffold functions, indexes, weights etc.(this will take around 1 minute) by running `& 'C:\Program Files\PostgreSQL\12\bin\psql.exe' -U postgres -d stackoverflow -a -f functions-indexes-weights.sql`

### Setup the Client
1. go to /WebApi/wwwroot
1. install libman `dotnet tool install -g Microsoft.Web.LibraryManager.Cli`
3. execute command `libman restore` (installs required dependencies)


### Web Service
1. go to the WebApi folder in your Terminal
1. run `dotnet run`
1. or `dotnet watch run --no-build` (*development*)


### API Usage

> Questions endpoints

GET /api/questions
GET /api/questions/{questionId}

> Answers endpoints

GET /api/answers/{answerId}
GET /api/answers/question/{answerId}

> Markings endpoints

GET /api/markings/{userId}
POST /api/markings _HEADERS_ userId _BODY_ {"postid": 16637748}
DELETE /api/markings/{markingId}

> Annotations endpoints

GET /api/annotations/{markingid}
GET /api/annotations _HEADERS_ userId
GET /api/annotations _BODY_ {"markingid": 1}
POST /api/annotations _BODY_ {"markingid": 1, "body": "api"}
PUT /api/annotations/{annotationId} _BODY_ {"markingid": 1, "body": "api2"}
DELETE /api/annotations/{annotationId}

> Comments endpoints

GET /api/comments/{postId}

> Tags endpoints

GET /api/tags/{questionId}

> Posts endpoints

GET /api/posts/{questionId}

> Search endpoints

GET /api/search?q=term+term
GET /api/search/history _HEADERS_ userId

> Auth endpoints

POST /api/auth/users _BODY_ {"username", "password"}
POST /api/auth/tokens _BODY_ {"username", "password"}

> Users endpoints

POST /api/users _BODY_ {"username", "password"}
