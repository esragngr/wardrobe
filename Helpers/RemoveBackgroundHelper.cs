<<<<<<< HEAD
﻿using System.Net.Http;
using System.Net.Http.Headers;
=======
﻿using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
>>>>>>> 1b9bee640ac8e0c8aff756158b1d76e51c38e248

namespace Wardrobe.Helpers
{
    public class RemoveBackgroundHelper
    {
        private readonly string apiKey = "Q3zJQaTWwpba4vBgRcEsdVEA";

        public async Task<string> RemoveBackgroundAsync(string imagePath)
        {
<<<<<<< HEAD
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://api.remove.bg/v1.0/removebg"),
                Headers = { { "X-Api-Key", apiKey } },
                Content = new MultipartFormDataContent
                {
                    { new ByteArrayContent(await File.ReadAllBytesAsync(imagePath)), "image_file", Path.GetFileName(imagePath) },
                    { new StringContent("auto"), "size" }
                }
            };

            var response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {

                return imagePath;
            }

            var directory = Path.GetDirectoryName(imagePath) ?? "";
            var resultPath = Path.Combine(directory, Path.GetFileNameWithoutExtension(imagePath) + "_nobg.png");
            var resultBytes = await response.Content.ReadAsByteArrayAsync();
            await File.WriteAllBytesAsync(resultPath, resultBytes);

            return resultPath;
=======
            using var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.remove.bg/v1.0/removebg");
            request.Headers.Add("X-Api-Key", apiKey);

            var content = new MultipartFormDataContent();
            var imageContent = new ByteArrayContent(await File.ReadAllBytesAsync(imagePath));
            imageContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

            content.Add(imageContent, "image_file", Path.GetFileName(imagePath));
            content.Add(new StringContent("auto"), "size");

            request.Content = content;

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                // Hata varsa orijinal dosya yolu döner
                return imagePath;
            }

            // Görseli mutlaka wwwroot/uploads içine kaydet
            var directory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
            Directory.CreateDirectory(directory);

            var resultPath = Path.Combine(directory, Path.GetFileNameWithoutExtension(imagePath) + "_nobg.png");
            var resultBytes = await response.Content.ReadAsByteArrayAsync();

            await File.WriteAllBytesAsync(resultPath, resultBytes);

            var virtualPath = "/uploads/" + Path.GetFileName(resultPath);
            return virtualPath;

>>>>>>> 1b9bee640ac8e0c8aff756158b1d76e51c38e248
        }
    }
}
