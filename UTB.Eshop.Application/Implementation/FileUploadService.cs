﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BistroWeb.Application.Abstraction;
using Microsoft.AspNetCore.Hosting;

namespace BistroWeb.Application.Implementation
{
    public class FileUploadService : IFileUploadService
    {
        public string RootPath { get; set; }
        
        public FileUploadService(string rootPath)
        {
            this.RootPath = rootPath;
        }
        public async Task<string> FileUploadAsync(IFormFile fileToUpload, string folderNameOnServer)
        {
            if (fileToUpload == null)
            {
                // Return a default or placeholder image path
                return "path/to/default/image.jpg";
            }
            string filePathOutput = String.Empty;

            var fileName = Path.GetFileNameWithoutExtension(fileToUpload.FileName);
            var fileExtension = Path.GetExtension(fileToUpload.FileName);

            var fileRelative = Path.Combine(folderNameOnServer, fileName + fileExtension);
            var filePath = Path.Combine(this.RootPath, fileRelative);

            Directory.CreateDirectory(Path.Combine(this.RootPath, folderNameOnServer));
            using (Stream stream = new FileStream(filePath, FileMode.Create))
            {
                await fileToUpload.CopyToAsync(stream);
            }

            filePathOutput = Path.DirectorySeparatorChar + fileRelative;

            return filePathOutput;
        }
    }
}
