using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Interfaces.Service
{
    public interface IFileService
    {
        Task<string> SaveProfilePhotoAsync(IFormFile file);
    }
}
