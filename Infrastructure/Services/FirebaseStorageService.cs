using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using FirebaseAdmin;

namespace APIBrechoRFCC.Infrastructure.Services
{
    public class FirebaseStorageService
    {
        FirebaseAdmin.FirebaseApp FirebaseAdminApp { get; set; }
        private readonly StorageClient _storageClient;
        private readonly string _bucketName = "brechorfcc-3c413.appspot.com"; 

        public FirebaseStorageService()
        {
            GoogleCredential googleCredential = GoogleCredential.FromFile(Path.Combine("Infrastructure", "brechorfcc-3c413-firebase-adminsdk-wp9it-879db85475.json"));
            _storageClient = StorageClient.Create(googleCredential);
        }


        public async Task<string> UploadFileAsync(IFormFile file)
        {
            
            // Fazer upload da imagem para o Firebase Storage
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string tempFilePath = Path.Combine(Path.GetTempPath(), uniqueFileName);

            using (var stream = new FileStream(tempFilePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            using (var fileStream = new FileStream(tempFilePath, FileMode.Open))
            {
                var arquivo = await _storageClient.UploadObjectAsync(_bucketName, uniqueFileName, null, fileStream);
                
                 var batata = "batata";
            }
            System.IO.File.Delete(tempFilePath); // Limpar arquivo temporário
            return $"https://storage.googleapis.com/{_bucketName}/{uniqueFileName}";
        }

        public async Task DeleteFileAsync(string oldFile)
        {
            await _storageClient.DeleteObjectAsync(_bucketName, oldFile);
        }
        public async Task<List<string>> UploadFileListAsync(List<IFormFile> files)
        {
            var imageUrls = new List<string>();

            foreach (var file in files)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                using (var stream = file.OpenReadStream())
                {
                    await _storageClient.UploadObjectAsync(_bucketName, uniqueFileName, null, stream);
                    imageUrls.Add($"{uniqueFileName}");
                }
            }

            return imageUrls;
        }
    }
    
}
