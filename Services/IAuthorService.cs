using Domain.Entities;
using Shared.DataTransferObjects;

namespace Services;

public interface IAuthorService
{
    Task<IEnumerable<AuthorDto>> GetAuthorsAsync(bool trackChanges);
    Task<AuthorDto?> GetAuthorAsync(Guid authorId);
    Task<AuthorDto> CreateAuthorAsync(AuthorForCreateDto authorForCreateDto);
    Task<bool> DeleteAuthorAsync(Guid authorId, bool trackChanges);
}