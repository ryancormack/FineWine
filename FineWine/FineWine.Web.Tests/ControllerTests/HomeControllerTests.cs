using System.Web.Mvc;
using FineWine.Domain.Services;
using FineWine.Features.Home;
using FluentAssertions;
using Machine.Specifications;
using Rhino.Mocks;

namespace FineWine.Web.Tests.ControllerTests
{
    public class HomeControllerTests
    {
        public class When_getting_the_home_page
        {
            Because of = () =>
            {
                _result = _controller.Index();
            };

            It should_return_the_home_page = () =>
            {
                (_result as ViewResult).ViewName.Should().Be("Index");
            };

            Establish context = () =>
            {
                _wineService = MockRepository.GenerateMock<IWineService>();
                _controller = new HomeController(_wineService);
            };

            private static ActionResult _result;
            private static HomeController _controller;
            private static IWineService _wineService;
        }
    }
}
