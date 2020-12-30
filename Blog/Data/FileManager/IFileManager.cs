using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Blog.Data.FileManager
{
    /// <summary>
    /// Serves the same principle as repository pattern only instead of database requests handles with file system.
    /// </summary>
    public interface IFileManager
    {
        /// <summary>
        /// Saves an image to the file system.
        /// </summary>
        /// <param name="image">Input image.</param>
        /// <returns>Endpoint string.</returns>
        string SaveImage(IFormFile image);

        /// <summary>
        /// Download an image from the file system.
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        FileStream ImageStream(string image);

        /// <summary>
        /// Remove a specific image from a file system. Optimization configured, every time when admin changes the existing image under post to new one the older one removes from file system automatically.
        /// </summary>
        /// <param name="image">An image title.</param>
        /// <returns></returns>
        bool RemoveImage(string image);
    }
}
