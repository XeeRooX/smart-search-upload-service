﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadService.Domain.Common.Entities;

namespace UploadService.Domain.Services
{
    public interface IDocumentUploaderService
    {
        Task UploadAsync(DocumentInfo document, DocumentListInfo listInfo);
    }
}
