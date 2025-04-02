using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Contracts;

namespace Persistence.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly QotdContext _context;
    private readonly IQuoteRepository _quoteRepository;
    private readonly IAuthorRepository _authorRepository;

    public RepositoryManager(QotdContext context, IQuoteRepository quoteRepository, IAuthorRepository authorRepository)
    {
        _context = context;
        _quoteRepository = quoteRepository;
        _authorRepository = authorRepository;
    }

    public IQuoteRepository QuoteRepo => _quoteRepository;

    public IAuthorRepository AuthorRepo => _authorRepository;

    public async Task SaveAsync() => await _context.SaveChangesAsync();
}