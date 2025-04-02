using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Logging;
using Persistence.Contracts;

namespace Services;

public class AuthorService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper) 
    : IAuthorService
{
    public async Task<IEnumerable<AuthorDto>> GetAuthorsAsync()
    {
        var authors = await repositoryManager.AuthorRepo.GetAuthorsAsync();

        return mapper.Map<IEnumerable<AuthorDto>>(authors);
    }
}