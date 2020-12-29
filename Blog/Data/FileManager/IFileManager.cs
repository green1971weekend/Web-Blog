using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        /// <returns>Image title.</returns>
        Task<string> SaveImage(IFormFile image);
    }
}
