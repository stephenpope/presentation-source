using System.Web.Mvc;
using MvcSite.Models;
using Microsoft.Framework.OptionsModel;
using Microsoft.Framework.Configuration;

namespace MvcSite.Controllers
{
    public class SampleController : Controller
    {
        #region Private vars
        private readonly IConfigurationRoot _config;
        private readonly IExampleService _exampleService;
        private readonly IOptions<ExampleOptions> _options;
        #endregion

        public SampleController(IExampleService exampleService, IOptions<ExampleOptions> options)
        {
            _exampleService = exampleService;
            _options = options;
        }

        public ActionResult Index()
        {
            return View(new SampleModel() { Data = "Hello From Controller - " + _exampleService.SomeData + " - " + _options.Value.Name + " - " + _options.Value.Age });
        }
    }
}