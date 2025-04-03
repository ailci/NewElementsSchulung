using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Shared.DataTransferObjects;

namespace Services.Resolver;

public class FormFileToByteArrayResolver : IValueResolver<AuthorForCreateDto, Author, byte[]?>
{
    public byte[]? Resolve(AuthorForCreateDto source, Author destination, byte[]? destMember, ResolutionContext context)
    {
        return ConvertFileToByteArray(source.Photo);
    }

    private byte[] ConvertFileToByteArray(IFormFile file)
    {
        if (file is null) return null;

        using var memoryStream = new MemoryStream();
        file.CopyTo(memoryStream);
        return memoryStream.ToArray();
    }
}