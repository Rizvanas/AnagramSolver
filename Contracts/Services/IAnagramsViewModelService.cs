using Contracts.Models;

namespace Contracts.Services
{
    public interface IAnagramsViewModelService
    {
        AnagramsViewModel GetAnagramsViewModel(string phrase, string ip);
    }
}
