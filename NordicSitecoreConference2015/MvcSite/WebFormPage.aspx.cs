using System;
using System.Web;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.OptionsModel;

namespace MvcSite
{
    public partial class WebFormPage : System.Web.UI.Page
    {
        #region Private vars

        private readonly IExampleService _exampleService;
        private readonly IOptions<ExampleOptions> _options;

        #endregion

        public WebFormPage()
        {
            var container = HttpContext.Current.GetContainer();
            _exampleService = container.GetService<IExampleService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("<h1> Hello From Web Form - " + _exampleService.SomeData + "</h1><br />");
        }
    }

    public static class ApplicationExtensions
    {
        public static IServiceProvider GetContainer(this HttpContext context)
        {
            return context.Application["serviceProvider"] as IServiceProvider;
        }
    }
}