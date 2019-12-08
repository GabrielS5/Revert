using System.Threading.Tasks;

namespace Core.Services
{
    public interface ITranslateService
    {
        Task<string> Translate(string text);
    }
}
