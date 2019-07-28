using PersonalInfoSampleApp.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalInfoSampleApp.Repositories
{
    public interface IPersonalInfoRepository
    {
        Task SubmitPersonalInfo(PersonalInfo personalInfo);
    }
}