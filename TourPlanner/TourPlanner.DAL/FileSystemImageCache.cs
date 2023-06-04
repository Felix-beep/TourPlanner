using log4net;

namespace TourPlanner.DAL
{
    public class FileSystemImageCache : IImageCache
    {
        readonly ILog log = LogManager.GetLogger(typeof(FileSystemImageCache));

        readonly string ImageCachePath = @"imgcache/";

        public FileSystemImageCache() { }
        
        public FileSystemImageCache(string imageCachePath) 
        {  
            ImageCachePath = imageCachePath; 
        }

        string GetFilePath(Guid imageID) => $"{ImageCachePath}{imageID}.jpg";

        public async Task<byte[]> GetImageDataAsync(Guid imageID)
        {
            log.Debug($"Loading image {GetFilePath(imageID)} from disk");

            if (!File.Exists(GetFilePath(imageID)))
            {
                log.Error("Image does not exist!");
                return null;
            }

            return await File.ReadAllBytesAsync(GetFilePath(imageID));
        }

        public async Task SaveImageAsync(Guid imageID, byte[] imageData)
        {
            log.Debug($"Saving image {GetFilePath(imageID)} to disk");

            await File.WriteAllBytesAsync(GetFilePath(imageID), imageData);
        }
    }
}
