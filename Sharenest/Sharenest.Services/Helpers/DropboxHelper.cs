using System.IO;
using Dropbox.Api;
using Dropbox.Api.Files;

namespace Sharenest.Services.Helpers
{
    public static class DropboxHelper
    {
        public static string AccessToken => "rHiQ9PgqsTAAAAAAAAADxenF4wi3_SDpUF_yL4T0dITt7-xaPy4PaxWgQZEyM_KK";

        public static byte[] Download(DropboxClient dbx, string folder, string file)
        {
            using (var response = dbx.Files.DownloadAsync(folder + file))
            {
                return response.Result.GetContentAsByteArrayAsync().Result;
            }
        }

        public static string Upload(DropboxClient dbx, string folder, string file, byte[] content)
        {
            using (var mem = new MemoryStream(content))
            {
                var updated = dbx.Files.BeginUpload(
                    folder + "/" + file,
                    WriteMode.Overwrite.Instance,
                    body: mem);
                dbx.Files.EndUpload(updated);
                return $"{folder}/{file}";
            }
        }
    }
}