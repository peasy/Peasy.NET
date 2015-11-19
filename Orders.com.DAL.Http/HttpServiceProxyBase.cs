using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Peasy.Core;
using Peasy.Exception;
using Peasy;

namespace Orders.com.DAL.Http
{
    public abstract class HttpServiceProxyBase<T, TKey> : IServiceDataProxy<T, TKey> where T : IDomainObject<TKey>
    {
        protected abstract string RequestUri { get; }

        protected virtual string AcceptHeader
        {
            get { return "application/json"; }
        }

        /// <exception cref="ServiceException">Thrown when server returns 400 - Bad Request</exception>
        public virtual IEnumerable<T> GetAll()
        {
            return GET<IEnumerable<T>>(RequestUri);
        }

        /// <exception cref="DomainObjectNotFoundException">Thrown when server returns 404 - Not Found</exception>
        /// <exception cref="ServiceException">Thrown when server returns 400 - Bad Request</exception>
        public virtual T GetByID(TKey id)
        {
            string requestUri = $"{RequestUri}/{id}";
            return GET<T>(requestUri);
        }

        /// <exception cref="ServiceException">Thrown when server returns 400 - Bad Request</exception>
        public virtual T Insert(T entity)
        {
            return POST<T, T>(entity, RequestUri);
        }

        /// <exception cref="DomainObjectNotFoundException">Thrown when server returns 404 - Not Found</exception>
        /// <exception cref="ConcurrencyException">Thrown when server returns 409 - Conflict</exception> 
        /// <exception cref="ServiceException">Thrown when server returns 400 - Bad Request</exception>
        public virtual T Update(T entity)
        {
            string requestUri = $"{RequestUri}/{entity.ID}";
            return PUT<T, T>(entity, requestUri);
        }

        /// <exception cref="DomainObjectNotFoundException">Thrown when server returns 404 - Not Found</exception>
        /// <exception cref="ServiceException">Thrown when server returns 400 - Bad Request</exception>
        public virtual void Delete(TKey id)
        {
            string requestUri = $"{RequestUri}/{id}";
            DELETE(requestUri);
        }

        /// <exception cref="ServiceException">Thrown when server returns 400 - Bad Request</exception>
        public async virtual Task<IEnumerable<T>> GetAllAsync()
        {
            return await GETAsync<IEnumerable<T>>(RequestUri);
        }

        /// <exception cref="DomainObjectNotFoundException">Thrown when server returns 404 - Not Found</exception>
        /// <exception cref="ServiceException">Thrown when server returns 400 - Bad Request</exception>
        public async virtual Task<T> GetByIDAsync(TKey id)
        {
            string requestUri = $"{RequestUri}/{id}";
            return await GETAsync<T>(requestUri);
        }

        /// <exception cref="ServiceException">Thrown when server returns 400 - Bad Request</exception>
        public async virtual Task<T> InsertAsync(T entity)
        {
            return await POSTAsync<T, T>(entity, RequestUri);
        }

        /// <exception cref="DomainObjectNotFoundException">Thrown when server returns 404 - Not Found</exception>
        /// <exception cref="ConcurrencyException">Thrown when server returns 409 - Conflict</exception> 
        /// <exception cref="ServiceException">Thrown when server returns 400 - Bad Request</exception>
        public async virtual Task<T> UpdateAsync(T entity)
        {
            string requestUri = $"{RequestUri}/{entity.ID}";
            return await PUTAsync<T, T>(entity, requestUri);
        }

        /// <exception cref="DomainObjectNotFoundException">Thrown when server returns 404 - Not Found</exception>
        /// <exception cref="ServiceException">Thrown when server returns 400 - Bad Request</exception>
        public async virtual Task DeleteAsync(TKey id)
        {
            string requestUri = $"{RequestUri}/{id}";
            await DELETEAsync(requestUri);
        }

        /// <summary>
        /// Invokes a GET against the supplied Uri
        /// </summary>
        /// <typeparam name="TOut">The type that will be deserialized from the service response and returned</typeparam>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        protected virtual TOut GET<TOut>(string requestUri)
        {
            using (var client = BuildConfiguredClient())
            {
                TOut returnValue = default(TOut);
                var task = client.GetAsync(requestUri)
                                 .ContinueWith(response =>
                                 {
                                     returnValue = ParseResponse<TOut>(response);
                                 });

                task.Wait();
                return returnValue;
            }
        }

        /// <summary>
        /// Invokes a GET against the supplied Uri
        /// </summary>
        /// <typeparam name="TIn">The type serialized and sent to the service</typeparam>
        /// <typeparam name="TOut">The type that will be deserialized from the service response and returned</typeparam>
        /// <param name="args"></param>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        protected virtual async Task<TOut> GETAsync<TOut>(string requestUri)
        {
            using (var client = BuildConfiguredClient())
            {
                var response = await client.GetAsync(requestUri);
                var entity = await ParseResponseAsync<TOut>(response);
                return entity;
            }
        }

        /// <summary>
        /// Invokes a POST against the supplied Uri
        /// </summary>
        /// <typeparam name="TIn">The type serialized and sent to the service</typeparam>
        /// <typeparam name="TOut">The type that will be deserialized from the service response and returned</typeparam>
        /// <param name="args"></param>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        protected virtual TOut POST<TIn, TOut>(TIn args, string requestUri)
        {
            using (var client = BuildConfiguredClient())
            {
                var returnValue = default(TOut);
                var task = client.PostAsync<TIn>(requestUri, args, GetMediaTypeFormatter())
                                 .ContinueWith(response =>
                                 {
                                     returnValue = ParseResponse<TOut>(response);
                                 });
                task.Wait();
                return returnValue;
            }
        }

        /// <summary>
        /// Invokes a POST against the supplied Uri
        /// </summary>
        /// <typeparam name="TIn">The type serialized and sent to the service</typeparam>
        /// <typeparam name="TOut">The type that will be deserialized from the service response and returned</typeparam>
        /// <param name="args"></param>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        protected virtual async Task<TOut> POSTAsync<TIn, TOut>(TIn args, string requestUri)
        {
            using (var client = BuildConfiguredClient())
            {
                var response = await client.PostAsync<TIn>(requestUri, args, GetMediaTypeFormatter());
                var returnValue = await ParseResponseAsync<TOut>(response);
                return returnValue;
            }
        }

        /// <summary>
        /// Invokes a PUT against the supplied Uri
        /// </summary>
        /// <typeparam name="TIn">The type serialized and sent to the service</typeparam>
        /// <typeparam name="TOut">The type that will be deserialized from the service response and returned</typeparam>
        /// <param name="args"></param>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        protected virtual TOut PUT<TIn, TOut>(TIn args, string requestUri)
        {
            using (var client = BuildConfiguredClient())
            {
                var returnValue = default(TOut);
                var task = client.PutAsync<TIn>(requestUri, args, GetMediaTypeFormatter())
                                 .ContinueWith(response =>
                                 {
                                     returnValue = ParseResponse<TOut>(response);
                                 });
                task.Wait();
                return returnValue;
            }
        }

        /// <summary>
        /// Invokes a PUT against the supplied Uri
        /// </summary>
        /// <typeparam name="TIn">The type serialized and sent to the service</typeparam>
        /// <typeparam name="TOut">The type that will be deserialized from the service response and returned</typeparam>
        /// <param name="args"></param>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        protected virtual async Task<TOut> PUTAsync<TIn, TOut>(TIn args, string requestUri)
        {
            using (var client = BuildConfiguredClient())
            {
                var response = await client.PutAsync<TIn>(requestUri, args, GetMediaTypeFormatter());
                var returnValue = await ParseResponseAsync<TOut>(response);
                return returnValue;
            }
        }

        /// <summary>
        /// Invokes a DELETE synchronously against the supplied Uri
        /// </summary>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        protected virtual void DELETE(string requestUri)
        {
            using (var client = BuildConfiguredClient())
            {
                var task = client.DeleteAsync(requestUri)
                                 .ContinueWith(response =>
                                 {
                                     EnsureSuccessStatusCode(response.Result);
                                 });
                task.Wait();
            }
        }

        /// <summary>
        /// Invokes a DELETE asynchronously against the supplied Uri
        /// </summary>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        protected virtual async Task DELETEAsync(string requestUri)
        {
            using (var client = BuildConfiguredClient())
            {
                var response = await client.DeleteAsync(requestUri);
                EnsureSuccessStatusCode(response);
            }
        }

        protected virtual HttpClient BuildConfiguredClient()
        {
            HttpClientHandler handler = new HttpClientHandler() { UseDefaultCredentials = true };
            return new HttpClient(handler); 
        }

        protected virtual MediaTypeFormatter GetMediaTypeFormatter()
        {
            return new JsonMediaTypeFormatter();
        }

        protected virtual TOut ParseResponse<TOut>(Task<HttpResponseMessage> response)
        {
            var result = response.Result;
            EnsureSuccessStatusCode(result);
            var task = result.Content.ReadAsAsync<TOut>(new[] { GetMediaTypeFormatter() });
            task.Wait();
            var entity = task.Result;
            OnParseResponse(result, entity);
            return entity;
        }

        protected virtual async Task<TOut> ParseResponseAsync<TOut>(HttpResponseMessage result)
        {
            EnsureSuccessStatusCode(result);
            var entity = await result.Content.ReadAsAsync<TOut>(new[] { GetMediaTypeFormatter() });
            OnParseResponse(result, entity);
            return entity;
        }

        protected virtual void OnParseResponse<TOut>(HttpResponseMessage response, TOut entity) 
        {
        }

        /// <summary>
        /// Throws an exception if the System.Net.Http.HttpResponseMessage.IsSuccessStatusCode property for the HTTP response is false.
        /// </summary>
        /// <returns>
        /// Returns System.Net.Http.HttpResponseMessage.The HTTP response message if the call is successful.
        /// </returns>
        protected virtual HttpResponseMessage EnsureSuccessStatusCode(HttpResponseMessage response)
        {
            string message = string.Empty;
            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.NotFound:
                    message = StripMessage(response.Content.ReadAsAsync<object>().Result.ToString());
                    throw new DomainObjectNotFoundException(message);

                case System.Net.HttpStatusCode.Conflict:
                    message = StripMessage(response.Content.ReadAsAsync<object>().Result.ToString());
                    throw new ConcurrencyException(message);

                case System.Net.HttpStatusCode.BadRequest:
                    message = StripMessage(response.Content.ReadAsAsync<object>().Result.ToString());
                    throw new ServiceException(message);
            }
            return response.EnsureSuccessStatusCode();
        }

        protected virtual string StripMessage(string message)
        {
            var msg = message.Split(new[] { ':' })[1];
            Regex rgx = new Regex("[\\{\\}\"]"); // get rid of the quotes and braces
            msg = rgx.Replace(msg, "").Trim();
            return msg;
        }

        public virtual bool IsLatencyProne
        {
            get { return true; }
        }

        public virtual bool SupportsTransactions
        {
            get { return false; }
        }
    }
}