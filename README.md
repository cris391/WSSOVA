# StackOverflow Service
### Setup the client
1. install libman
2. go to /WebApi/wwwroot
3. execute command ```libman restore``` (installs required dependencies)


### Setup the Data service

1. Scaffold schema from [here](https://github.com/cris391/Portfolio_Projects/blob/master/InformationRetrievalDatabase/stackoverflow-normalized-schema.sql).
2. Scaffold functions, indexes, etc from [here](https://github.com/cris391/Portfolio_Projects/blob/master/InformationRetrievalDatabase/add-tfidf-weights.sql).(this might take up to 10 minutes)


### WebApi
1. dotnet restore
2. dotnet watch run --no-build (*development*)


### API USAGE

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