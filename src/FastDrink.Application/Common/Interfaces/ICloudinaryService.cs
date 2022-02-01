using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace FastDrink.Application.Common.Interfaces;

public interface ICloudinaryService
{
    Task<ImageUploadResult[]> UploadPhotos(IList<IFormFile> photos);
    Task DeletePhotos(string photoId);
}
