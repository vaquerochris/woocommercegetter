using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleverPortal.Services
{
    public interface IWooCommerceService
    {
        Task<string> GetWooCommerceOrders(string websiteURL, string consumerKey, string consumerSecret, string afterDate, string beforeDate);
    }
}
