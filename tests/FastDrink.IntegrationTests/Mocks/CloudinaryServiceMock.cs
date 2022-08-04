using CloudinaryDotNet.Actions;
using FastDrink.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace FastDrink.IntegrationTests.Mocks;

public class CloudinaryServiceMock : ICloudinaryService
{
    public async Task<DeletionResult> DeletePhoto(string photoId)
    {
        var algo = new DeletionResult();
        return algo;
    }

    public async Task<ImageUploadResult> UploadPhoto(IFormFile photo)
    {
        var result = new ImageUploadResult()
        {
            SecureUrl = new Uri($"https://res.cloudinary.com/dtvsu8lo9/image/upload/v1648846637/sample.jpg"),
            PublicId = "noexiste"
        };

        return result;
    }
}
