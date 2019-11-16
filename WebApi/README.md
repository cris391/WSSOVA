[I'm an inline-style link](https://www.google.com)
### API USAGE

> Questions endpoints

GET /api/questions
GET /api/questions/{questionId}

> Answers endpoints

GET /api/answers/{answerId}
GET /api/answers/question/{answerId}

> Markings endpoints

GET /api/markings/{userId}
POST /api/markings BODY {"userid": 1,"postid": 16637748}
Delete /api/markings BODY {"markingid": 1}

> Annotations endpoints

GET /api/annotations/{markingid}
GET /api/annotations BODY {"markingid": 1}
POST /api/annotations BODY {"markingid": 1,"body": "api"}
PUT /api/annotations BODY {"markingid": 1,"body": "api2"}

