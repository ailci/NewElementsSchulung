using Services;

namespace Api.Controllers;

public interface IServiceManager
{
    IQotdService QotdService { get; }
}