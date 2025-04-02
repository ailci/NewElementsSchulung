using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contracts;

namespace Persistence.Repositories;

public class QuoteRepository(QotdContext qotdContext) : RepositoryBase<Quote>(qotdContext), IQuoteRepository
{
    public async Task<Quote> GetRandomQuoteAsync()
    {
        var quotes = await GetAll().Include(c => c.Author).ToListAsync(); //alle Sprüche
        var random = new Random();

        return quotes[random.Next(quotes.Count)];
    }
}