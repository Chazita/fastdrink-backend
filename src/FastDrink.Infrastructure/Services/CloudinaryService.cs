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

    public Task DeletePhotos(string photoId)
    {
        throw new NotImplementedException();
    }

    public async Task<ImageUploadResult[]> UploadPhotos(IFormFileCollection photos)
    {
        List<Task<ImageUploadResult>> tasks = new();
        List<Stream> streamList = new();

        var cloudinary = new Cloudinary(_account);

        foreach (var photo in photos)
        {
            var stream = photo.OpenReadStream();
            streamList.Add(stream);
            var uploadPhoto = new ImageUploadParams
            {
                File = new FileDescription(photo.FileName, stream),
            };

            tasks.Add(cloudinary.UploadAsync(uploadPhoto));
        }

        var imageUploads = await Task.WhenAll(tasks);

        foreach (var stream in streamList)
        {
            stream.Close();
        }

        return imageUploads;
    }
}
