using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Specialized;
using TechTalk.SpecFlow;

namespace BaseWebUIAutomation.Support
{
    [Binding]
    internal static class TestConfiguration
    {
        private static readonly IConfiguration Configuration;
        public static NameValueCollection AppSettings { get; }

         static TestConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

        }

        public static string GetSectionAndValue(string sectionName, string value)
        {
            return Configuration.GetSection(sectionName)[value];

        }
        public static object GetSection(string sectionName)
        {
         return Configuration.GetSection(sectionName);
        }

        internal static string GetSecretValue(string v)
        {
            throw new NotImplementedException();
        }
    }
}
