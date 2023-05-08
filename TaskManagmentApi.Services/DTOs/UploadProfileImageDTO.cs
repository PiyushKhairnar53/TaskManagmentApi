using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagmentApi.Services.DTOs
{
    public class UploadProfileImageDTO
    {
        public string UserId { get; set; }
        public IFormFile Image { get; set; }
    }
}
