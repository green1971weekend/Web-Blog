using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PhotoSauce.MagicScaler;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Blog.Data.FileManager
{
    ///<inheritdoc cref="IFileManager"/>
    public class FileManager : IFileManager
    {
        private readonly string _imagePath;

        public FileManager(IConfiguration configuration)
        {
            _imagePath = configuration["Path:Images"];
        }

        ///<inheritdoc />
        public FileStream ImageStream(string image)
        {
            return new FileStream(Path.Combine(_imagePath, image), FileMode.Open, FileAccess.Read); 
        }

        ///<inheritdoc />
        public string SaveImage(IFormFile image)
        {
            try
            {
                // Instead of hardcode path like "/wwwroot/blog/.." use Path.Combine() which prevents different sorts of errors.
                var save_path = Path.Combine(_imagePath);

                if (!Directory.Exists(_imagePath))
                {
                    Directory.CreateDirectory(save_path);
                }

                var mime = image.FileName.Substring(image.FileName.LastIndexOf("."));
                var fileName = $"img_{DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss")}{mime}";

                using (var fileStream = new FileStream(Path.Combine(save_path, fileName), FileMode.Create))
                {
                    MagicImageProcessor.ProcessImage(image.OpenReadStream(), fileStream, ImageOptions());
                }

                return fileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Error";
            }
        }

        ///<inheritdoc />
        public bool RemoveImage(string image)
        {
            try
            {
                var file = Path.Combine(_imagePath, image);

                if(File.Exists(file))
                    File.Delete(file);

                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Image optimization for uploading not too large files to the host server.
        /// </summary>
        /// <returns></returns>
        private ProcessImageSettings ImageOptions() => new ProcessImageSettings
        {
            Width = 800,
            Height = 500, 
            ResizeMode = CropScaleMode.Crop,

            SaveFormat = FileFormat.Jpeg,
            JpegQuality = 100,
            JpegSubsampleMode = ChromaSubsampleMode.Subsample420
        };
    }
}
