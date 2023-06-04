namespace TourPlanner.DAL
{
    public class FileSystemImageCache : IImageCache
    {
        readonly string ImageCachePath = @"imgcache/";

        public FileSystemImageCache() { }
        
        public FileSystemImageCache(string imageCachePath) 
        {  
            ImageCachePath = imageCachePath; 
        }

        string GetFilePath(Guid imageID) => $"{ImageCachePath}{imageID}.jpg";

        public async Task<byte[]> GetImageDataAsync(Guid imageID)
        {
            if (!File.Exists(GetFilePath(imageID)))
            {

                return null;
            }

            return await File.ReadAllBytesAsync(GetFilePath(imageID));
        }

        public async Task SaveImageAsync(Guid imageID, byte[] imageData)
        {
            await File.WriteAllBytesAsync(GetFilePath(imageID), imageData);
        }
    }
}
