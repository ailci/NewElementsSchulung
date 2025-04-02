using Domain.Entities;
using Shared.DataTransferObjects;

namespace Services;

public interface IAuthorService
{
    Task<IEnumerable<AuthorDto>> GetAuthorsAsync();
}