using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Cache;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Peasy.Core;
using Peasy.Exception;
using Peasy;

namespace Orders.com.DAL.Http
{
    public abstract class HttpServiceProxyBase<T, TKey> : IServiceDataProxy<T, TKey> where T : DomainBase
    {
        protected abstract string RequestUri { get; }

        protected virtual string AcceptHeader
        {
            get { return "application/json"; }
        }

        public virtual IEnumerable<T> GetAll()
        {
            return GET<IEnumerable<T>>(RequestUri);
        }

        public virtual T GetByID(TKey id)
        {
            string requestUri = string.Format("{0}/{1}", RequestUri, id);
            return GET<T>(requestUri);
        }

        public virtual T Insert(T entity)
        {
            return POST<T, T>(entity, RequestUri);
        }

        public virtual T Update(T entity)
        {
            string requestUri = string.Format("{0}/{1}", RequestUri, entity.ID);
            return PUT<T, T>(entity, requestUri);
        }

        public virtual void Delete(TKey id)
        {
            string requestUri = string.Format("{0}/{1}", RequestUri, id);
            DELETE<TKey>(id, requestUri);
        }

        public async virtual Task<IEnumerable<T>> GetAllAsync()
        {
            return await GETAsync<IEnumerable<T>>(RequestUri);
        }

        public async virtual Task<T> GetByIDAsync(TKey id)
        {
            string requestUri = string.Format("{0}/{1}", RequestUri, id);
            return await GETAsync<T>(requestUri);
        }

        public async virtual Task<T> InsertAsync(T entity)
        {
            return await POSTAsync<T, T>(entity, RequestUri);
        }

        public async virtual Task<T> UpdateAsync(T entity)
        {
            string requestUri = string.Format("{0}/{1}", RequestUri, entity.ID);
            return await PUTAsync<T, T>(entity, requestUri);
        }

        public async virtual Task DeleteAsync(TKey id)
        {
            string requestUri = string.Format("{0}/{1}", RequestUri, id);
            await DELETEAsync<TKey>(id, requestUri);
        }

        /// <summary>
        /// Invokes a GET against the supplied Uri
        /// </summary>
        /// <typeparam name="TOut">The type that will be deserialized from the service response and returned</typeparam>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        public virtual TOut GET<TOut>(string requestUri)
        {
            var result = InvokeServiceCall(() =>
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
            });

            return result;
        }

        /// <summary>
        /// Invokes a GET against the supplied Uri
        /// </summary>
        /// <typeparam name="TIn">The type serialized as Json and sent as a parameter to the service</typeparam>
        /// <typeparam name="TOut">The type that will be deserialized from the service response and returned</typeparam>
        /// <param name="args"></param>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        public virtual async Task<TOut> GETAsync<TOut>(string requestUri)
        {
            var result = await InvokeServiceCallAsync(async () =>
            {
                using (var client = BuildConfiguredClient())
                {
                    var response = await client.GetAsync(requestUri);
                    var entity = await ParseResponseAsync<TOut>(response);
                    return entity;
                }
            });

            return result;
        }

        /// <summary>
        /// Invokes a POST against the supplied Uri
        /// </summary>
        /// <typeparam name="TIn">The type serialized as Json and sent as a parameter to the service</typeparam>
        /// <typeparam name="TOut">The type that will be deserialized from the service response and returned</typeparam>
        /// <param name="args"></param>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        public virtual TOut POST<TIn, TOut>(TIn args, string requestUri)
        {
            var result = InvokeServiceCall(() =>
            {
                using (var client = BuildConfiguredClient())
                {
                    var returnValue = default(TOut);
                    var task = client.PostAsync<TIn>(requestUri, args, new JsonMediaTypeFormatter())
                                     .ContinueWith(response =>
                                     {
                                         returnValue = ParseResponse<TOut>(response);
                                     });

                    task.Wait();
                    return returnValue;
                }
            });

            return result;
        }

        /// <summary>
        /// Invokes a POST against the supplied Uri
        /// </summary>
        /// <typeparam name="TIn">The type serialized as Json and sent as a parameter to the service</typeparam>
        /// <typeparam name="TOut">The type that will be deserialized from the service response and returned</typeparam>
        /// <param name="args"></param>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        public virtual async Task<TOut> POSTAsync<TIn, TOut>(TIn args, string requestUri)
        {
            var result = await InvokeServiceCallAsync(async () =>
            {
                using (var client = BuildConfiguredClient())
                {
                    var response = await client.PostAsync<TIn>(requestUri, args, new JsonMediaTypeFormatter());
                    var returnValue = await ParseResponseAsync<TOut>(response);
                    return returnValue;
                }
            });

            return result;
        }

        /// <summary>
        /// Invokes a PUT against the supplied Uri
        /// </summary>
        /// <typeparam name="TIn">The type serialized as Json and sent as a parameter to the service</typeparam>
        /// <typeparam name="TOut">The type that will be deserialized from the service response and returned</typeparam>
        /// <param name="args"></param>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        protected virtual TOut PUT<TIn, TOut>(TIn args, string requestUri)
        {
            var result = InvokeServiceCall(() =>
            {
                using (var client = BuildConfiguredClient())
                {
                    var returnValue = default(TOut);
                    var task = client.PutAsync<TIn>(requestUri, args, new JsonMediaTypeFormatter())
                                     .ContinueWith(response =>
                                     {
                                         returnValue = ParseResponse<TOut>(response);
                                     });

                    task.Wait();
                    return returnValue;
                }
            });

            return result;
        }

        /// <summary>
        /// Invokes a PUT against the supplied Uri
        /// </summary>
        /// <typeparam name="TIn">The type serialized as Json and sent as a parameter to the service</typeparam>
        /// <typeparam name="TOut">The type that will be deserialized from the service response and returned</typeparam>
        /// <param name="args"></param>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        protected virtual async Task<TOut> PUTAsync<TIn, TOut>(TIn args, string requestUri)
        {
            var result = await InvokeServiceCallAsync(async () =>
            {
                using (var client = BuildConfiguredClient())
                {
                    //var etag = (entity as DomainBase).ETagValue();
                    //client.DefaultRequestHeaders.IfMatch.Add(new EntityTagHeaderValue(string.Format("\"{0}\"", etag), true));
                    var response = await client.PutAsync<TIn>(requestUri, args, new JsonMediaTypeFormatter());
                    var returnValue = await ParseResponseAsync<TOut>(response);
                    return returnValue;
                }
            });

            return result;
        }

        /// <summary>
        /// Invokes a DELETE against the supplied Uri
        /// </summary>
        /// <typeparam name="TIn">The type serialized as Json and sent as a parameter to the service</typeparam>
        /// <param name="args"></param>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        protected virtual void DELETE<TIn>(TIn args, string requestUri)
        {
            InvokeServiceCall(() =>
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
            });
        }

        /// <summary>
        /// Invokes a DELETE against the supplied Uri
        /// </summary>
        /// <typeparam name="TIn">The type serialized as Json and sent as a parameter to the service</typeparam>
        /// <typeparam name="TOut">The type that will be deserialized from the service response and returned</typeparam>
        /// <param name="args"></param>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        protected virtual async Task DELETEAsync<TIn>(TIn args, string requestUri)
        {
            await InvokeServiceCallAsync(async () =>
            {
                using (var client = BuildConfiguredClient())
                {
                    var response = await client.DeleteAsync(requestUri);
                    EnsureSuccessStatusCode(response);
                }
            });

            return;
        }

        protected void InvokeServiceCall(Action serviceInvocationMethod)
        {
            try
            {
                serviceInvocationMethod();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        protected TOut InvokeServiceCall<TOut>(Func<TOut> serviceInvocationMethod)
        {
            try
            {
                return serviceInvocationMethod();
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return default(TOut);
            }
        }

        protected async Task<TOut> InvokeServiceCallAsync<TOut>(Func<Task<TOut>> serviceInvocationMethod)
        {
                return await serviceInvocationMethod();
            try
            {
                return await serviceInvocationMethod();
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return default(TOut);
            }
        }

        protected async Task InvokeServiceCallAsync(Func<Task> serviceInvocationMethod)
        {
            try
            {
                await serviceInvocationMethod();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        protected TOut ParseResponse<TOut>(Task<HttpResponseMessage> response)
        {
            var result = response.Result;
            EnsureSuccessStatusCode(result);
            var jsonTask = result.Content.ReadAsAsync<TOut>();
            jsonTask.Wait();
            var entity = jsonTask.Result;
            return entity;
        }

        protected async Task<TOut> ParseResponseAsync<TOut>(HttpResponseMessage result)
        {
            EnsureSuccessStatusCode(result);
            var entity = await result.Content.ReadAsAsync<TOut>();
            return entity;
        }

        /// <summary>
        /// Throws an exception if the System.Net.Http.HttpResponseMessage.IsSuccessStatusCode property for the HTTP response is false.
        /// </summary>
        /// <returns>
        /// Returns System.Net.Http.HttpResponseMessage.The HTTP response message if the call is successful.
        /// </returns>
        public HttpResponseMessage EnsureSuccessStatusCode(HttpResponseMessage response)
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

        private string StripMessage(string message)
        {
            var msg = message.Split(new[] { ':' })[1];
            Regex rgx = new Regex("[\\{\\}\"]"); // get rid of the quotes and braces
            msg = rgx.Replace(msg, "").Trim();
            return msg;
        }

        protected void HandleException(Exception ex)
        {
            // TODO: introduce CRUDException
            //if (ex.GetBaseException() is CRUDException)
            //    throw new CRUDException(ex.GetBaseException().Message);
            var x = ex.GetBaseException();
            throw new Exception("Error occurred in HttpServiceProxyBase", ex);
        }

        /// <summary>
        /// Returns something similar to http://someserver:1234
        /// </summary>
        public string BaseAddress
        {
            get
            {
                //string hostSettingName = "restfulHostNameAddress";
                //var baseAddress = System.Configuration.ConfigurationManager.AppSettings[hostSettingName];
                //if (baseAddress == null) throw new Exception(string.Format("The setting '{0}' in the AppSettings portion of the config file was not found.", hostSettingName));
                //return baseAddress;
                return string.Empty;
            }
        }

        public virtual bool IsLatencyProne
        {
            get { return true; }
        }

        public virtual bool SupportsTransactions
        {
            get { return false; }
        }

        /// <summary>
        /// Returns something similar to http://someserver:1234/api/projects
        /// </summary>
        public string BuildAddress(string path)
        {
            return string.Format("{0}/{1}", BaseAddress, path);
        }

        protected virtual HttpClient BuildConfiguredClient()
        {
            HttpClientHandler handler = new HttpClientHandler() { UseDefaultCredentials = true };
            return new HttpClient(handler).WithAcceptHeader(AcceptHeader);
        }
    }
}

