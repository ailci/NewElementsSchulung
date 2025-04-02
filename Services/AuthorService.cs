using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Logging;
using Persistence.Contracts;

namespace Services;

public class AuthorService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper) 
    : IAuthorService
{
    public async Task<AuthorDto?> GetAuthorAsync(Guid authorId)
    {
        logger.LogInformation($"{nameof(GetAuthorAsync)} mit AuthorId: {authorId} aufgerufen...");

        var author = await GetAuthorAndCheckIfItExistsAsync(authorId);

        return mapper.Map<AuthorDto>(author);
    }

    public async Task<IEnumerable<AuthorDto>> GetAuthorsAsync()
    {
        var authors = await repositoryManager.AuthorRepo.GetAuthorsAsync();

        return mapper.Map<IEnumerable<AuthorDto>>(authors);
    }

    private async Task<Author> GetAuthorAndCheckIfItExistsAsync(Guid authorId)
    {
        var author = await repositoryManager.AuthorRepo.GetAuthorAsync(authorId);
        if (author is null) throw new AuthorNotFoundException(authorId);

        return author;
    }
}