using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;
using WebApi.Models;
using DatabaseService; 

using System.Net.Http.Headers;

namespace Tests
{

    //////////////////////////////////////////////////////////
    /// 
    /// Testing Constrains
    /// 
    //////////////////////////////////////////////////////////
    ///

    public class WebServiceTests
    {
        private const string AnnotationsApi = "http://localhost:5001/api/annotations/";
        private const string AuthApi = "http://localhost:5001/api/auth/";
        private const string AnswersApi = "http://localhost:5001/api/answers/";
        private const string CommentsApi = "http://localhost:5001/api/comments/";
        private const string FullPostApi = "http://localhost:5001/api/fullpost/";
        private const string MarkingsApi = "http://localhost:5001/api/markings/";
        private const string QuestionsApi = "http://localhost:5001/api/questions/";
        private const string SearchApi = "http://localhost:5001/api/search";
        private const string TagsApi = "http://localhost:5001/api/tags/";

        public string UserToken { get; set; }

        private string Username
        {
            get
            {
                return "newww";
            }
        }

        private string Password
        {
            get
            {
                return "ewwww";
            }
        }


        private string UsernameExist
        {
            get
            {
                return "palleboy92000";
            }
        }

        private string PasswordExist
        {
            get
            {
                return "passwor00d";
            }
        }

        //////////////////////////////////////////////////////////
        /// 
        /// Testing Auth Constrains
        /// 
        //////////////////////////////////////////////////////////
        ///


        [Fact]
        public void User_Signup_Bad_Request_Exists()
        {
            var signupUser = new UserForCreationDto()
            {
                UserName = UsernameExist,
                Password = PasswordExist
            };

            var (_, statusCode) = PostData(AuthApi + "create/user", signupUser, string.Empty);

            Assert.Equal(HttpStatusCode.BadRequest, statusCode);
        }


        [Fact]
        public void User_Signup_Success()
        {
            var signupUser = new UserForCreationDto()
            {
                UserName = Username,
                Password = Password
            };

            var (_, statusCode) = PostData(AuthApi + "create/user", signupUser, string.Empty);

            Assert.Equal(HttpStatusCode.Created, statusCode);
        }

        [Fact]
        public void Authenticate_User()
        {
            var user = new UserForLoginDto()
            {
                UserName = UsernameExist,
                Password = PasswordExist
            };

            var (auth_user, statusCode) = PostData(AuthApi + "token", user, string.Empty);

            var token = (string)auth_user["token"];
            var username = (string)auth_user["username"];
            UserToken = token;

            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal(user.UserName, username);
            Assert.NotNull(token);
        }


        //////////////////////////////////////////////////////////
        /// 
        /// Testing Annotations Constraints
        /// 
        //////////////////////////////////////////////////////////
        ///


        [Fact]
        public void ApiAnnotations_AddAnnotations()
        {
            var db_service = new SOContext();
            var new_annotation = new Annotation()
            {
                AnnotationId = db_service.Annotations.Max(x => x.AnnotationId) + 1,
                MarkingId = 2,
                Body = "Test new add annotaion"
            };

            // Auth the user to receive token
            UserToken = GetToken();

            var (_, statusCode) = PostData(AnnotationsApi, new_annotation, UserToken);

            //Assert.Equal(HttpStatusCode.Created, statusCode);
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }


        [Fact]
        public void ApiAnnotations_UpdateAnnotation()
        {
            // Auth the user to recieve token
            UserToken = GetToken();

            var data = new Annotation()
            {
                AnnotationId = 10,
                MarkingId = 10,
                Body = "Updated TESTTTTTTTTT NEw "
            };

            var statusCode = PutData(AnnotationsApi + "1", data, UserToken);
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }


        [Fact]
        public void ApiAnnotations_GetAnnotations()
        {
            // Auth the user to receive token
            UserToken = GetToken();

            var (annotations, statusCode) = GetArray(AnnotationsApi + "marking/" + 1, UserToken);

            //Assert.True(annotations.Count() > 0);
            //if(statusCode >= 200 &&  statusCode <= 299)
            //{
            //    Assert.True(true);
            //}
            Assert.Equal(HttpStatusCode.NoContent, statusCode);
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }


        [Fact]
        public void Api_Annotations_GetInvalidAnnotation()
        {
            var (_, statusCode) = GetObject(AnnotationsApi + "/-10", UserToken);

            Assert.Equal(HttpStatusCode.NotFound, statusCode);
        }

        [Fact]
        public void Api_Annotations_Get_By_User()
        {
            // Auth user to receive token
            UserToken = GetToken();

            var (annotation, statusCode) = GetArray(AnnotationsApi, UserToken);

           // Assert.True(annotation.Count() > 0);
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }

        //[Fact]
        //public void Api_Annotation_Delete()
        //{

        //    // Auth user to receive token
        //    UserToken = GetToken();
        //    var newAnnotation = new Annotation
        //    {
        //        AnnotationId = 9999,
        //        MarkingId = 2,
        //        Body = "adsadasdasas"
        //    };

        //    var (annotation, statusCodePost) = PostData($"{AnnotationsApi}", newAnnotation, UserToken);

        //    var statusCode = DeleteData($"{AnnotationsApi}", newAnnotation, UserToken);

        //    Assert.Equal(HttpStatusCode.OK, statusCode);
        //}


        //////////////////////////////////////////////////////////
        /// 
        /// Testing Answers Constraints
        /// 
        //////////////////////////////////////////////////////////
        ///


        [Fact]
        public void API_Get_Answer()
        {
            var (_, statusCode) = GetObject(AnswersApi + "676929", UserToken);

            Assert.Equal(HttpStatusCode.OK, statusCode);
        }


        [Fact]
        public void API_Get_Answers_For_Question()
        {
            var (data, statusCode) = GetArray(AnswersApi + "question/17029384", string.Empty);

            Assert.True(data.Count() > 0);
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }


        //////////////////////////////////////////////////////////
        /// 
        /// Testing Questions Constraints
        /// 
        //////////////////////////////////////////////////////////
        ///

        [Fact]
        public void API_Get_Questions()
        {
            var (Q_data, statusCode) = GetObject(QuestionsApi, string.Empty);

            //Assert.True(Q_data.Count() > 0);
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }


        [Fact]
        public void API_Get_Question()
        {
            var (_, statusCode) = GetObject(QuestionsApi + "23613310", string.Empty);

            Assert.Equal(HttpStatusCode.OK, statusCode);
        }


        //////////////////////////////////////////////////////////
        /// 
        /// Testing Comments Constraints
        /// 
        //////////////////////////////////////////////////////////
        ///

        [Fact]
        public void API_Get_Comments()
        {
            var (_, statusCode) = GetObject(CommentsApi + "post/61545", string.Empty);

            Assert.Equal(HttpStatusCode.OK, statusCode);
        }


        //////////////////////////////////////////////////////////
        /// 
        /// Testing FullPost Constraints
        /// 
        //////////////////////////////////////////////////////////
        ///

        [Fact]
        public void API_Get_Full_Post()
        {
            var (_, statusCode) = GetObject(FullPostApi + "104068", string.Empty);

            Assert.Equal(HttpStatusCode.OK, statusCode);
        }


        //////////////////////////////////////////////////////////
        /// 
        /// Testing Markings Constraints
        /// 
        //////////////////////////////////////////////////////////
        ///

        [Fact]
        public void API_Get_Markings()
        {
            // Auth user to receive token
            UserToken = GetToken();

            var (markings, statusCode1) = GetArray(MarkingsApi, UserToken);
            //  var (_, statusCode2) = GetArray(MarkingsApi, UserToken);

            //Assert.Equal(HttpStatusCode.OK | HttpStatusCode.NoContent, statusCode1 | statusCode2);'

            Assert.True(markings.Count() > 0);
            // Success authentication but no CONTENT for user!
            Assert.Equal(HttpStatusCode.NoContent, statusCode1);
        }

        [Fact]
        public void API_Add_Marking()
        {
            // Auth user to receive token
            UserToken = GetToken();

            var db = new SOContext();

            var newMarking = new Marking()
            {
                MarkingId = db.Markings.Max(x => x.MarkingId) + 1,
                UserId = 10,
                PostId = 16637748
            };

            var (_, statusCode) = PostData(MarkingsApi, newMarking, UserToken);

            //  Assert.Equal(HttpStatusCode.Created, statusCode);
            Assert.Equal(HttpStatusCode.OK, statusCode);
        
    }


        //[Fact]
        //public void API_Delete_Marking()
        //{
        //    // Auth user to receive token
        //    UserToken = GetToken();

        //    var marking = new MarkingForCreation
        //    {
        //        UserId=2,
        //        PostId= 16637748
        //    };

        //    // create
        //    var (_, statusCode) = PostData(MarkingsApi, marking, UserToken);
        //    // delete
        //    var (delete_data, statusCodedDeleted) = DeleteData(MarkingsApi, marking, UserToken);

        //    Assert.Equal(HttpStatusCode.OK, statusCodedDeleted);

        //}



        //////////////////////////////////////////////////////////
        /// 
        /// Search 
        /// 
        //////////////////////////////////////////////////////////
        ///


        [Fact]
        public void API_Search()
        {
            // Auth user to receive token
            UserToken = GetToken();

            var query = "?q=c";

            var (searchResult, statusCode) = GetArray(SearchApi + query, UserToken);

            Assert.True(searchResult.Count() > 0);
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }


        [Fact]
        public void API_Search_History()
        {
            // Auth user to receive token
            UserToken = GetToken();

            var (userSearchData, statusCode) = GetArray(SearchApi + "/history", UserToken);

          //  Assert.True(userSearchData.Count() > 0);
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }


        //////////////////////////////////////////////////////////
        /// 
        /// Tags 
        /// 
        //////////////////////////////////////////////////////////
        ///


        [Fact]
        public void API_Get_Question_Tags()
        {
            var (_, statusCode) = GetObject(TagsApi + "1326210", string.Empty);

            Assert.Equal(HttpStatusCode.OK, statusCode);
        }


        //////////////////////////////////////////////////////////
        /// 
        /// Helpers 
        /// 
        //////////////////////////////////////////////////////////
        ///


        private string GetToken()
        {
            var user = new UserForLoginDto
            {
                UserName = Username,
                Password = Password
            };

            var (authUser, statusCode) = PostData(AuthApi + "token", user, string.Empty);
            var token = (string)authUser["token"];
            return token;
        }

        (JArray, HttpStatusCode) GetArray(string url, string userToken)
        {
            var client = new HttpClient();
            if (!string.IsNullOrEmpty(userToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
            }
            var response = client.GetAsync(url).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            return ((JArray)JsonConvert.DeserializeObject(data), response.StatusCode);
        }

        (JObject, HttpStatusCode) GetObject(string url, string userToken)
        {
            var client = new HttpClient();
            if (!string.IsNullOrEmpty(userToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
            }
            var response = client.GetAsync(url).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            return ((JObject)JsonConvert.DeserializeObject(data), response.StatusCode);
        }

        (JObject, HttpStatusCode) PostData(string url, object content, string userToken)
        {
            var client = new HttpClient();
            if (!string.IsNullOrEmpty(userToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
            }
            var requestContent = new StringContent(
                JsonConvert.SerializeObject(content),
                Encoding.UTF8,
                "application/json");
            var response = client.PostAsync(url, requestContent).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            return ((JObject)JsonConvert.DeserializeObject(data), response.StatusCode);
        }

        HttpStatusCode PutData(string url, object content, string userToken)
        {
            var client = new HttpClient();
            if (!string.IsNullOrEmpty(userToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
            }
            var response = client.PutAsync(
                url,
                new StringContent(
                    JsonConvert.SerializeObject(content),
                    Encoding.UTF8,
                    "application/json")).Result;
            return response.StatusCode;
        }

        HttpStatusCode DeleteData(string url, string userToken)
        {
            var client = new HttpClient();
            if (!string.IsNullOrEmpty(userToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
            }
            var response = client.DeleteAsync(url).Result;
            return response.StatusCode;
        }
    }
}

