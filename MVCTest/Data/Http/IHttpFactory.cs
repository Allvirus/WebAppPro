using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCTest.Data.Http
{
    public interface IHttpFactory
    {
        Task<string> HttpGet(string uri, string cookie);

        Task<string> HttpPost(string uri, string cookie, List<KeyValuePair<string, string>> values);
    }
}
