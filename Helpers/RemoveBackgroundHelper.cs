using System.Net.Http;
using System.Net.Http.Headers;

namespace Wardrobe.Helpers
{
    public class RemoveBackgroundHelper
    {
        private readonly string apiKey = "Q3zJQaTWwpba4vBgRcEsdVEA";

        public async Task<string> RemoveBackgroundAsync(string imagePath)
        {
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
        }
    }
}
