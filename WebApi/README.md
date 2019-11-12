### API USAGE

> Questions endpoints

GET /api/questions
GET /api/questions/{questionId}

> Answers endpoints

GET /api/answers/{answerId}
GET /api/answers/question/{answerId}

> Annotations endpoints

GET /api/annotations/{annotationId}
GET /api/annotations BODY {"userid": 1,"questionid?": 16637748}
POST /api/annotations BODY {"userid": 1,"questionid": 16637748,"body": "api"}
PUT /api/annotations BODY {"userid": 1,"questionid": 16637748,"body": "api2"}

