namespace TourPlanner.DAL
{
    public interface IImageCache
    {
        Task SaveImageAsync(Guid imageID, byte[] imageData);
        Task<byte[]> GetImageDataAsync(Guid imageID);
    }
}
