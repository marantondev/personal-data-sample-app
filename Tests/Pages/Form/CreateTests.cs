using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using NUnit.Framework;
using PersonalInfoSampleApp.Pages.Form;
using PersonalInfoSampleApp.Pages.Form.Handlers;
using PersonalInfoSampleApp.Pages.Form.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalInfoSampleApp.Tests.Pages.Form
{
    [TestFixture]
    public sealed class CreateTests
    {
        [Test]
        public void OnGet_ExecutesGetCitiesQueryAndReturnsPage()
        {
            var sutSetup = new SutSetup();

            var result = sutSetup.CreateSut().OnGet();

            Assert.That(sutSetup.GetExecuted);
            Assert.That(result is PageResult);
        }

        [Test]
        public async Task OnPostAsync_ValidModel_SubmitsModelRedirectsToThankYou()
        {
            var sutSetup = new SutSetup();
            var sut = sutSetup.CreateSut();
            sut.PersonalInfo = sutSetup.ValidModel;

            var result = await sut.OnPostAsync() as RedirectToPageResult;

            Assert.That(sutSetup.SubmitExecuted);
            Assert.That(result != null);
            Assert.That(result.PageName == "ThankYou");
        }

        [Test]
        public async Task OnPostAsync_InValidModel_ReturnsPage()
        {
            var sutSetup = new SutSetup();
            var sut = sutSetup.CreateSut();
            sut.PersonalInfo = null;
            sut.ModelState.AddModelError("PersonalInfo.FirstName", "FieldRequired");

            var result = await sut.OnPostAsync();

            Assert.That(!sutSetup.SubmitExecuted);
            Assert.That(result is PageResult);
        }

        private sealed class SutSetup
        {
            private ISubmitPersonalInfoCommandHandler _submitHandler;
            private IGetCityListQueryHandler _getHandler;

            public PersonalInfoViewModel ValidModel { get; private set; }
            public bool SubmitExecuted { get; private set; }
            public bool GetExecuted { get; private set; }

            public SutSetup()
            {
                _submitHandler = CreateSubmitHandler();
                _getHandler = CreateGetHandler();
            }

            private IGetCityListQueryHandler CreateGetHandler()
            {
                var mock = new Mock<IGetCityListQueryHandler>();
                mock.Setup(p => p.Execute())
                    .Returns(new List<SelectListItem>() { new SelectListItem("CityName", "1")})
                    .Callback(() => GetExecuted = true);
                return mock.Object;
            }

            private ISubmitPersonalInfoCommandHandler CreateSubmitHandler()
            {
                var mock = new Mock<ISubmitPersonalInfoCommandHandler>();
                mock.Setup(p => p.Execute(ValidModel)).Callback(() => SubmitExecuted = true);
                return mock.Object;
            }

            public CreateModel CreateSut()
            {
                return new CreateModel(_getHandler, _submitHandler);
            }
        }
    }
}
