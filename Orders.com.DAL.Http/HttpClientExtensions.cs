using System.Net.Http;
using System.Net.Http.Headers;

namespace Orders.com.DAL.Http
{
    public static class HttpClientExtensions
    {
        public static HttpClient WithAcceptHeader(this HttpClient client, string mimeType)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mimeType));
            return client;
        }
    }
}
