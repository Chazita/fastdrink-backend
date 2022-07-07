using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace FastDrink.Application.Common.Interfaces;

public interface ICloudinaryService
{
    Task<ImageUploadResult> UploadPhoto(IFormFile photo);
    Task<DeletionResult> DeletePhoto(string photoId);
}
