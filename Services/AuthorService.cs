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
using Shared.Utilities;

namespace Services;

public class AuthorService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper) 
    : IAuthorService
{
    public async Task<AuthorDto> CreateAuthorAsync(AuthorForCreateDto authorForCreateDto)
    {
        logger.LogInformation($"{nameof(CreateAuthorAsync)} wurde mit Objekt {authorForCreateDto.LogAsJson()} aufgerufen...");

        var authorEntity = mapper.Map<Author>(authorForCreateDto);

        //logger.LogInformation($"AuthorEntity: {authorEntity.LogAsJson()}");

        repositoryManager.AuthorRepo.CreateAuthor(authorEntity);
        await repositoryManager.SaveAsync();

        return mapper.Map<AuthorDto>(authorEntity);
    }

    public async Task<bool> DeleteAuthorAsync(Guid authorId, bool trackChanges)
    {
        logger.LogInformation($"Der Author mit der Id {authorId} zum Löschen ausgewählt...");

        var authorEntity = await GetAuthorAndCheckIfItExistsAsync(authorId);

        repositoryManager.AuthorRepo.DeleteAuthor(authorEntity);

        //2 Optionen => 1. Exception werfen und mit GlobalExceptionHandler auswerten oder 2. Controller entscheiden lassen
        return await repositoryManager.SaveAsync();
    }

    public async Task<AuthorDto?> GetAuthorAsync(Guid authorId)
    {
        logger.LogInformation($"{nameof(GetAuthorAsync)} mit AuthorId: {authorId} aufgerufen...");

        var author = await GetAuthorAndCheckIfItExistsAsync(authorId);

        return mapper.Map<AuthorDto>(author);
    }

    public async Task<IEnumerable<AuthorDto>> GetAuthorsAsync(bool trackChanges)
    {
        var authors = await repositoryManager.AuthorRepo.GetAuthorsAsync(trackChanges);

        //Ohne Automapper
        //var authorsDto = authors.Select(author => new AuthorDto
        //{
        //    Name = author.Name,
        //    Description = author.Description
        //});

        return mapper.Map<IEnumerable<AuthorDto>>(authors);
    }

    private async Task<Author> GetAuthorAndCheckIfItExistsAsync(Guid authorId)
    {
        var author = await repositoryManager.AuthorRepo.GetAuthorAsync(authorId);
        if (author is null) throw new AuthorNotFoundException(authorId);

        return author;
    }
}