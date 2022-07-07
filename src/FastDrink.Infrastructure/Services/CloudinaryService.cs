using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FastDrink.Application.Common.Interfaces;
using FastDrink.Application.Common.Settings;
using Microsoft.AspNetCore.Http;

namespace FastDrink.Infrastructure.Services;

public class CloudinaryService : ICloudinaryService
{
    private readonly Account _account;

    public CloudinaryService(CloudinarySettings cloudinarySettings)
    {
        _account = new Account(
            cloudinarySettings.CloudName,
            cloudinarySettings.ApiKey,
            cloudinarySettings.ApiSecret);
    }

    public async Task<DeletionResult> DeletePhoto(string photoId)
    {
        var cloudinary = new Cloudinary(_account);

        DeletionResult deletionResult = new();

        if (photoId != "noexist")
        {
            var deletionParams = new DeletionParams(photoId);

            deletionResult = await cloudinary.DestroyAsync(deletionParams);
        }

        return deletionResult;
    }

    public async Task<ImageUploadResult> UploadPhoto(IFormFile photo)
    {
        var cloudinary = new Cloudinary(_account);

        var stream = photo.OpenReadStream();

        var uploadPhoto = new ImageUploadParams
        {
            File = new FileDescription(photo.FileName, stream),
        };

        var imageUpload = await cloudinary.UploadAsync(uploadPhoto);

        stream.Close();

        return imageUpload;
    }
}
