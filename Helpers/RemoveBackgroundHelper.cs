using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Wardrobe.Helpers
{
    public class RemoveBackgroundHelper
    {
        private readonly string apiKey = "Q3zJQaTWwpba4vBgRcEsdVEA";

        public async Task<string> RemoveBackgroundAsync(string imagePath)
        {
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

        }
    }
}
