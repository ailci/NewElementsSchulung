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

    public RepositoryManager(QotdContext context, IQuoteRepository quoteRepository)
    {
        _context = context;
        _quoteRepository = quoteRepository;
    }

    public IQuoteRepository QuoteRepo => _quoteRepository;

    public async Task SaveAsync() => await _context.SaveChangesAsync();
}