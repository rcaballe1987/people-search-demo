using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using PeopleSearchAppDemo.Core.ServiceContracts;
using System.IO;
using System.Net;
using System.Text;

namespace PeopleSearchAppDemo.Core.Services
{
    public class FileService : IFileService
    {
        private readonly IHostEnvironment _hostEnvironment;
        private readonly string fileIoUrl = "https://file.io";

        public FileService(IHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        /// <summary>
        /// <see cref="IFileService.UploadImage(string)"/>
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string UploadImage(string fileName)
        {
            string linkToFile = string.Empty;

            var imagesFolder = Path.Combine($"{_hostEnvironment.ContentRootPath}.Core", "Images");
            var uniqueFileName = fileName;
            var filePath = Path.Combine(imagesFolder, uniqueFileName);
            var fileExists = File.Exists(filePath);

            if (fileExists)
            {
                using (WebClient client = new WebClient())
                {
                    var resStr = client.UploadFile(fileIoUrl, $@"{filePath}");
                    var jObjResult = JObject.Parse(Encoding.UTF8.GetString(resStr));
                    linkToFile = jObjResult["link"].ToString();
                }
            }

            return linkToFile;
        }
    }
}
