﻿using Microsoft.AspNetCore.Http;

namespace SMS.Model.Common
{
    public class EmailMessage
    {
        public string? ToEmail { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public List<IFormFile>? Attachments { get; set; }
    }
}
