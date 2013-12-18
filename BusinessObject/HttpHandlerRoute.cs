using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Compilation;
namespace BusinessObject
{
    public class HttpHandlerRouteHandler<T> : IRouteHandler where T : IHttpHandler, new()
    {

        public HttpHandlerRouteHandler() { }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new T();
        }
    }

    public class HttpHandlerRouteHandler : IRouteHandler
    {

        private string _VirtualPath;

        public HttpHandlerRouteHandler(string virtualPath)
        {
            this._VirtualPath = virtualPath;
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return (IHttpHandler)BuildManager.CreateInstanceFromVirtualPath(this._VirtualPath, typeof(IHttpHandler));
        }

    }
}
