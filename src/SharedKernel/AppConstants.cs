namespace SharedKernel;
public static class AppConstants
{
    public const int MaxFileSizeInMB = 5;
    public const int MaxFileSizeInBytes = MaxFileSizeInMB * 1024 * 1024;
    public static readonly string[] AllowedImageExtensions = [".jpg", ".png", ".jpeg"];
}
