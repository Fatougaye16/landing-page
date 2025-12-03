using ErrorOr;
using PhotographyPlatform.Domain.PhotoManagement.ValueObjects;

namespace PhotographyPlatform.Domain.Common.Errors;

public static partial class Errors
{
    public static class PhotoManagement
    {
        public static Error FileNameRequired => Error.Validation(
            code: "PhotoManagement.FileNameRequired",
            description: "File name is required.");

        public static Error FileExtensionRequired => Error.Validation(
            code: "PhotoManagement.FileExtensionRequired",
            description: "File extension is required.");

        public static Error InvalidFileSize => Error.Validation(
            code: "PhotoManagement.InvalidFileSize",
            description: "File size must be greater than zero.");

        public static Error InvalidImageDimensions => Error.Validation(
            code: "PhotoManagement.InvalidImageDimensions",
            description: "Image dimensions must be greater than zero.");

        public static Error UnsupportedFileExtension => Error.Validation(
            code: "PhotoManagement.UnsupportedFileExtension",
            description: "File extension is not supported.");

        public static Error InvalidPhotoStatus => Error.Validation(
            code: "PhotoManagement.InvalidPhotoStatus",
            description: "Photo status is not valid.");

        public static Error InvalidAlbumStatus => Error.Validation(
            code: "PhotoManagement.InvalidAlbumStatus",
            description: "Album status is not valid.");
    }

    public static class Photo
    {
        public static Error NotFound(PhotoId photoId) => Error.NotFound(
            code: "Photo.NotFound",
            description: $"Photo with ID '{photoId}' was not found.");

        public static Error DuplicateFileName(AlbumId albumId, string fileName) => Error.Conflict(
            code: "Photo.DuplicateFileName",
            description: $"A photo with filename '{fileName}' already exists in album '{albumId}'.");
    }

    public static class Album
    {
        public static Error NotFound(AlbumId albumId) => Error.NotFound(
            code: "Album.NotFound",
            description: $"Album with ID '{albumId}' was not found.");

        public static Error NotFoundByUserId(AlbumId albumId) => Error.NotFound(
            code: "Album.NotFoundByUserId",
            description: $"Album with ID '{albumId}' was not found for the user.");
    }
}