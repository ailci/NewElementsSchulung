using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contracts;

public interface IRepositoryManager
{
    IQuoteRepository QuoteRepo { get; }
    IAuthorRepository AuthorRepo { get; }

    Task SaveAsync();
}