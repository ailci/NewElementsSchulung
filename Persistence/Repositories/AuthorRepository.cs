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
    public void CreateAuthor(Author author)
    {
        Create(author);
    }

    public void DeleteAuthor(Author author)
    { 
        //Statt zu löschen auf inaktiv zustellen
        //author.Active = false;
        Delete(author);
    }

    public async Task<Author?> GetAuthorAsync(Guid authorId)
    {
        return await FindByCondition(author => author.Id == authorId).SingleOrDefaultAsync();
        
        //var author2 = QotdContext.Authors.FindAsync(authorId); //Suche mit Primärschlüsel
    }

    public async Task<IEnumerable<Author>> GetAuthorsAsync()
    {
        return await GetAll().ToListAsync();
    }
}