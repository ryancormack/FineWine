using System.Web.Mvc;
using FineWine.Features.Home;
using FluentAssertions;
using Machine.Specifications;

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
                _controller = new HomeController();
            };

            private static ActionResult _result;
            private static HomeController _controller;
        }
    }
}
