﻿using TourPlanner.DAL;
using TourPlanner.Models;

namespace TourPlanner.BL
{
    internal interface IRouteClient
    {
        IRequestBuilder GetBuilder(string apiKey);
        Task<string> RequestJsonStringAsync(IRequestBuilder builder);
        Task<Tour> RequestTourData(string from, string to, TransportType transportType, string apiKey, IImageCache imageCache);
        Task RequestImageAsync(IRequestBuilder builder, string fileName);
        Task<byte[]> RequestImageDataAsync(IRequestBuilder builder);
    }
}
