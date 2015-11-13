using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Diagnostics.Contracts;
using System.Collections.Generic;
using Peasy;
using Peasy.Core;
using Peasy.Exception;

namespace Orders.com.Web.Api
{
    public abstract class ApiControllerBase<T, TKey> : ApiController where T : IDomainObject<TKey>, new()
    {
        protected IService<T, TKey> _businessService;

        // GET api/contracts
        public virtual HttpResponseMessage Get()
        {
            var result = _businessService.GetAllCommand().Execute();

            if (result.Success)
            {
                var results = result.Value;
                // http://www.w3.org/Protocols/rfc2616/rfc2616-sec9.html#sec9.3
                return Request.CreateResponse(HttpStatusCode.OK, results);
            }

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Join("\n", result.Errors));
        }

        // GET api/contracts/5
        public virtual HttpResponseMessage Get(TKey id)
        {
            // http://www.w3.org/Protocols/rfc2616/rfc2616-sec9.html#sec9.3
            var result = _businessService.GetByIDCommand(id).Execute();
            if (result.Success)
            {
                if (result.Value == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, BuildNotFoundHttpError(id));
                    //var message = BuildNotFoundResponseMessage(className, id);
                    //throw new HttpResponseException(message);
                }
                var response = Request.CreateResponse(HttpStatusCode.OK, result.Value);
                //response.Headers.ETag = new EntityTagHeaderValue(string.Format("\"{0}\"", result.ETagValue(), true));
                //response.Content.Headers.LastModified = DateTime.Now.ToUniversalTime(); // result.LastModified.Value.ToUniversalTime();

                return response;
                //.AppendCacheControl(36000)
                //.AppendExpires(60)
                //.AppendLastModified();
            }

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Join("\n", result.Errors));
        }

        // POST api/contracts
        public virtual HttpResponseMessage Post(T value)
        {
            if (value == null) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The request payload could not be parsed");

            var result = _businessService.InsertCommand(value).Execute();

            if (result.Success)
            {
                T newEntity = result.Value;
                // http://www.w3.org/Protocols/rfc2616/rfc2616-sec9.html#sec9.5
                var responseMessage = Request.CreateResponse(HttpStatusCode.Created, newEntity);
                responseMessage.Headers.Location = new Uri(BuildNewResourceUriString(newEntity.ID));
                return responseMessage;
            }

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState.ClearFirst().ThenAddRange(result.Errors));
        }

        //// POST api/contracts
        ///// <remarks>Supports batch inserts</remarks>
        //public HttpResponseMessage Post(IEnumerable<T> items)
        //{
        //    throw new NotImplementedException();
        //}

        //// POST api/contracts
        //public async virtual Task<HttpResponseMessage> Post(T value)
        //{
        //    if (value == null) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The request payload could not be parsed");

        //    var InsertCommand = _businessService.GetInsertAsyncCommand(value);

        //    if (InsertCommand.CanInvoke)
        //    {
        //        T newEntity = await InsertCommand.InvokeAsync();
        //        // http://www.w3.org/Protocols/rfc2616/rfc2616-sec9.html#sec9.5
        //        var responseMessage = Request.CreateResponse(HttpStatusCode.Created, newEntity);
        //        responseMessage.Headers.Location = new Uri(BuildNewResourceUriString(newEntity.ID));
        //        return responseMessage;
        //    }

        //    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState.ClearFirst().ThenAddRange(InsertCommand.GetValidationErrors(value)));
        //}


        // PUT api/contracts/5
        public virtual HttpResponseMessage Put(TKey id, T value)
        {
            if (value == null) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The request payload could not be parsed");

            //if (Request.Headers.IfMatch.Count == 0) 
            //    return Request.CreateErrorResponse(HttpStatusCode.PreconditionFailed, "A PUT request must contain a If-Match header value whose content is the ETag value from a previous GET request");

            try
            {
                var result = _businessService.UpdateCommand(value).Execute();

                if (result.Success)
                {
                    //if (currentSnapshot.ETagValue() != Request.Headers.IfMatch.First().Tag)
                    //    return Request.CreateErrorResponse(HttpStatusCode.PreconditionFailed, "A concurrency issue occurred.  Try a GET on the resource and attempt the PUT again.");

                    value.ID = id;
                    T updatedEntity = result.Value;

                    // http://www.w3.org/Protocols/rfc2616/rfc2616-sec9.html#sec9.6
                    var response = Request.CreateResponse(HttpStatusCode.OK, updatedEntity);

                    //response.Headers.ETag = new EntityTagHeaderValue(string.Format("\"{0}\"", result.ETagValue(), true));
                    //response.Content.Headers.LastModified = DateTime.Now.ToUniversalTime(); // result.LastModified.Value.ToUniversalTime();

                    return response;
                }

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState.ClearFirst().ThenAddRange(result.Errors));
            }
            catch (DomainObjectNotFoundException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message);
            }
            catch (ConcurrencyException)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, "A concurrency issue occurred.  Try a GET on the resource and attempt the PUT again.");
            }
        }

        // PUT api/contracts
        /// <remarks>Supports batch updates</remarks>
        //public virtual HttpResponseMessage Put(IEnumerable<T> items)
        //{
        //    throw new NotImplementedException();
        //}

        // DELETE api/contracts/5
        public virtual HttpResponseMessage Delete(TKey id)
        {
            try
            {
                if (_businessService.GetByIDCommand(id).Execute().Value == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, BuildNotFoundHttpError(id));

                var result = _businessService.DeleteCommand(id).Execute();

                if (result.Success)
                {
                    // http://www.w3.org/Protocols/rfc2616/rfc2616-sec9.html#sec9.7
                    var responseMessage = Request.CreateResponse(HttpStatusCode.NoContent);
                    return responseMessage;
                }

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Join("\n", result.Errors));
            }
            catch (DomainObjectNotFoundException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message);
            }
            catch (ConcurrencyException)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, "A concurrency issue occurred.  Try a GET on the resource and attempt the PUT again.");
            }
        }

        protected virtual string BuildNewResourceUriString(TKey id)
        {
            return BuildNewResourceUriString(id, Constants.ROUTE_NAME_DEFAULT);
        }

        protected virtual string BuildNewResourceUriString(TKey id, string routeName)
        {
            var link = Url.Link(routeName, new { id = id });
            return link;
        }

        private static string BuildNotFoundString(string className, TKey id)
        {
            return string.Format("{0} ID {1} could not be found.", className, id.ToString());
        }

        private static HttpResponseMessage BuildNotFoundResponseMessage(string className, TKey id)
        {
            var message = new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent(BuildNotFoundString(className, id)),
                ReasonPhrase = string.Format("{0} not found.", className),
            };
            return message;
        }

        protected static HttpError BuildNotFoundHttpError(TKey id)
        {
            var className = BuildDomainClassName();
            string errorMessage = BuildNotFoundString(className, id);
            return new HttpError(errorMessage);
        }

        private static string BuildDomainClassName()
        {
            var domainObject = new T();
            return domainObject.ClassName();
        }
    }
}