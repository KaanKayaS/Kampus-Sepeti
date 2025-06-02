using KampüsSepeti.Application.Interfaces.Service;

namespace KampüsSepeti.Api.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;

        public FileService(IWebHostEnvironment env)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
        }

        public async Task<string> SaveProfilePhotoAsync(IFormFile file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file), "The file cannot be null.");
            }

            if (_env.WebRootPath == null)
            {
                throw new InvalidOperationException("WebRootPath is null. Check if IWebHostEnvironment is properly configured.");
            }

            // Önce wwwroot klasörünün varlığını kontrol et
            if (!Directory.Exists(_env.WebRootPath))
            {
                Directory.CreateDirectory(_env.WebRootPath);
            }

            // uploads klasörünün varlığını kontrol et
            var uploadsPath = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsPath))
            {
                Directory.CreateDirectory(uploadsPath);
            }

            // profile_photos klasörünün varlığını kontrol et
            var folderPath = Path.Combine(uploadsPath, "profile_photos");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/uploads/profile_photos/{fileName}";
        }
    }
}
