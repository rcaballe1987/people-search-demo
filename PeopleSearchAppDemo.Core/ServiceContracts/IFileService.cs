namespace PeopleSearchAppDemo.Core.ServiceContracts
{
    public interface IFileService
    {
        /// <summary>
        /// Uploads an image to file.io for testing purposes
        /// </summary>
        /// <returns>The Url of the image</returns>
        string UploadImage(string fileName);
    }
}
