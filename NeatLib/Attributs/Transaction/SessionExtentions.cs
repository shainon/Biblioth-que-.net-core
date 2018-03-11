using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NeatLib.Session;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeatLib.Attributs.Transaction
{
    public static class SessionExtentions
    {
        public static T GetTransactionSession<T>(this ISession session, RouteData routeData)
        {
            return session.GetObjectFromJson<T>((string)routeData.Values["guid"]);
        }

        public static void SaveTransactionSession(this ISession session, RouteData routeData,object sessionData)
        {
            session.SetObjectAsJson((string)routeData.Values["guid"],sessionData);
        }
    }
}
