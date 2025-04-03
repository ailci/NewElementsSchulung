using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObjects;

namespace Services;

public interface IQotdService
{
    Task<QuoteOfTheDayDto> GetQuoteOfTheDayAsync(bool trackChanges);
}