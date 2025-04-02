using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contracts;

namespace Persistence.Repositories;

public class AuthorRepository(QotdContext qotdContext) : RepositoryBase<Author>(qotdContext),IAuthorRepository
{
    public async Task<IEnumerable<Author>> GetAuthorsAsync()
    {
        return await GetAll().ToListAsync();
    }
}