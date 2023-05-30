using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.BL
{
    internal class APIRouteClient : IRouteClient
    {
        public IRequestBuilder GetBuilder(string apiKey)
        {
            throw new NotImplementedException();
        }

        public Task RequestImageAsync(IRequestBuilder builder, string fileName)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> RequestImageDataAsync(IRequestBuilder builder)
        {
            throw new NotImplementedException();
        }

        public Task<string> RequestJsonStringAsync(IRequestBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}
