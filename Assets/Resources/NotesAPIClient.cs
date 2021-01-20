using System;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using UnityEngine;

namespace Assets.Resources
{
    class NotesAPIClient
    {
        private const string ApplicationJsonContentType = "application/json";
        private readonly HttpClient httpClient;

        public NotesAPIClient()
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://fridgenotes.azurewebsites.net/")
            };
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("text/plain"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJSUzI1NiIsImtpZCI6IjE2QzVDRDUyRDRFODlEMjREN0IyM0E3MDdFNzM4MEZGIiwidHlwIjoiYXQrand0In0.eyJuYmYiOjE2MTEwMTMzNjEsImV4cCI6MTYxMTAxNjk2MSwiaXNzIjoiaHR0cDovL2lkZW50aXR5c2VydmVyYXBpLXByb2QuZXUtY2VudHJhbC0xLmVsYXN0aWNiZWFuc3RhbGsuY29tIiwiYXVkIjoiSURTIiwiY2xpZW50X2lkIjoiZnJpZGdlbm90ZXMtYXBpIiwic3ViIjoiMGNiOTQ3NGEtYWViMS00YzgyLWEwMmItYTQyZWEwYzZhY2M2IiwiYXV0aF90aW1lIjoxNjExMDEzMzYxLCJpZHAiOiJsb2NhbCIsIm5hbWUiOiJtcCIsImVtYWlsIjoibXAiLCJqdGkiOiJCQjlDMTZERTk4RjA2QzI1MTFFOUZFMkI2RTUyM0QzMSIsImlhdCI6MTYxMTAxMzM2MSwic2NvcGUiOlsiZGV2ZWxvcG1lbnQiXSwiYW1yIjpbImN1c3RvbSJdfQ.uMemQ1Ylpc2Z409gXcUMgx9Om-UhLjtu-qBCCYpAhm1TuXJfGQD9Yego2o8k0EQS9lm6fp7v4HtKMPp6AGxdBU8sdA40SiWiKOxLNAxIdaHUXiyy8JA2CFz4z9tCTKYPkTM0nxTViQX8SkbMQZdrKmkJqoQRx5lcCizFz97FP21aqNQASGFslugcxfn9HpN8idfVohDTwoYFnotZgTcFe3OZX2JslNbzd_06cE3RkXTuGN9srnSFS7sLpBJ4MPxcFvTHK5Kcb5AfQsUoY0d1JEjoAaDaKi185aMZoSG0sjSPule4E17rRf5kik3L66UFgGktXxs4jYVwSvzU4n0uUQ");
        }

        public string Login(string username, string password)
        {
            var data = new
            {
                email = username,
                password = password
            };


            return null;
        }

        public void CreateNote(Note note)
        {
            var response = httpClient.SendAsync(Post("api/notes", note)).Result;
            Debug.Log(response.StatusCode.ToString());

            var response2 = httpClient.SendAsync(Get("api/notes/user/0cb9474a-aeb1-4c82-a02b-a42ea0c6acc6")).Result;
            Debug.Log(response2.StatusCode.ToString());

        }

        private static HttpRequestMessage Get(string uri)
        {
            return new HttpRequestMessage(HttpMethod.Get, uri);
        }

        private static HttpRequestMessage Delete(string uri)
        {
            return new HttpRequestMessage(HttpMethod.Delete, uri);
        }

        private static HttpRequestMessage Post(string uri, Note body)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
            requestMessage.Content = new StringContent(JsonUtility.ToJson(body), Encoding.UTF8,
                ApplicationJsonContentType);

            return requestMessage;
        }
    }
}
