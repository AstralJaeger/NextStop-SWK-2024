using System;

namespace Routing
{
    public class RouteNotFoundException : Exception
    {
        public RouteNotFoundException()
        {
        }

        public RouteNotFoundException(string message)
            : base(message)
        {
        }

        public RouteNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}