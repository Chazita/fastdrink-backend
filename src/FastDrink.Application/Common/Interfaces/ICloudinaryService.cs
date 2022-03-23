﻿using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace FastDrink.Application.Common.Interfaces;

public interface ICloudinaryService
{
    Task<ImageUploadResult[]> UploadPhotos(IFormFileCollection photos);
    Task<DeletionResult[]> DeletePhotos(List<string> photoId);
}
