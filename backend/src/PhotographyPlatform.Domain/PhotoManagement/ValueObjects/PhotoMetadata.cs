using ErrorOr;
using PhotographyPlatform.Domain.Common.BaseTypes;
using PhotographyPlatform.Domain.Common.Errors;

namespace PhotographyPlatform.Domain.PhotoManagement.ValueObjects;

public class PhotoMetadata : ValueObject
{
    public string FileName { get; private set; }
    public string FileExtension { get; private set; }
    public long FileSizeBytes { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }
    public DateTime CapturedAt { get; private set; }
    public string? CameraModel { get; private set; }
    public string? CameraSettings { get; private set; }

    private PhotoMetadata(
        string fileName,
        string fileExtension,
        long fileSizeBytes,
        int width,
        int height,
        DateTime capturedAt,
        string? cameraModel = null,
        string? cameraSettings = null)
    {
        FileName = fileName;
        FileExtension = fileExtension;
        FileSizeBytes = fileSizeBytes;
        Width = width;
        Height = height;
        CapturedAt = capturedAt;
        CameraModel = cameraModel;
        CameraSettings = cameraSettings;
    }

    public static ErrorOr<PhotoMetadata> Create(
        string fileName,
        string fileExtension,
        long fileSizeBytes,
        int width,
        int height,
        DateTime capturedAt,
        string? cameraModel = null,
        string? cameraSettings = null)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            return Errors.PhotoManagement.FileNameRequired;

        if (string.IsNullOrWhiteSpace(fileExtension))
            return Errors.PhotoManagement.FileExtensionRequired;

        if (fileSizeBytes <= 0)
            return Errors.PhotoManagement.InvalidFileSize;

        if (width <= 0 || height <= 0)
            return Errors.PhotoManagement.InvalidImageDimensions;

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".tiff", ".raw", ".dng" };
        if (!allowedExtensions.Contains(fileExtension.ToLowerInvariant()))
            return Error.Validation(
                code: "PhotoManagement.UnsupportedFileExtension",
                description: $"File extension '{fileExtension}' is not supported");

        return new PhotoMetadata(
            fileName,
            fileExtension.ToLowerInvariant(),
            fileSizeBytes,
            width,
            height,
            capturedAt,
            cameraModel,
            cameraSettings);
    }

    public string GetFullFileName()
    {
        return $"{FileName}{FileExtension}";
    }

    public decimal GetFileSizeMB()
    {
        return Math.Round(FileSizeBytes / 1024m / 1024m, 2);
    }

    public string GetResolution()
    {
        return $"{Width}x{Height}";
    }

    public decimal GetAspectRatio()
    {
        return Math.Round((decimal)Width / Height, 2);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FileName;
        yield return FileExtension;
        yield return FileSizeBytes;
        yield return Width;
        yield return Height;
        yield return CapturedAt;
        yield return CameraModel ?? string.Empty;
        yield return CameraSettings ?? string.Empty;
    }
}