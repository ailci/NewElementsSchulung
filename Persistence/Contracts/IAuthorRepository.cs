using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Persistence.Contracts;

public interface IAuthorRepository
{
    Task<IEnumerable<Author>> GetAuthorsAsync();
    Task<Author?> GetAuthorAsync(Guid authorId);
    void CreateAuthor(Author author);
    void DeleteAuthor(Author author);
}