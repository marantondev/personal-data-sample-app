using System.Threading.Tasks;
using PersonalInfoSampleApp.Pages.Form.ViewModel;

namespace PersonalInfoSampleApp.Pages.Form.Handlers
{
    public interface ISubmitPersonalInfoCommandHandler
    {
        Task Execute(PersonalInfoViewModel dataInput);
    }
}