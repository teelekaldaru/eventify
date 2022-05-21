using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;

namespace Eventify.UnitTests
{
    public class UnitTest
    {
        public static readonly TestServer Server;

        static UnitTest()
        {
            try
            {
                Server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [OneTimeSetUp]
        public void TestInitialize()
        {
        }

        [OneTimeTearDown]
        public void TestTearDown()
        {
        }
    }
}
