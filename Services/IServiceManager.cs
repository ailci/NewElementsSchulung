namespace Services;

public interface IServiceManager
{
    IQotdService QotdService { get; }
    IAuthorService AuthorService { get; }
}