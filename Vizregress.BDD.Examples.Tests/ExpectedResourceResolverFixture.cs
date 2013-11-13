using System.IO;
using NUnit.Framework;
using Rhino.Mocks;

namespace Vizregress.BDD.Examples.Tests
{
    /// <summary>
    /// Performs tests against <see cref="ExpectedResourceResolver"/>
    /// </summary>
    [TestFixture]
    public class ExpectedResourceResolverFixture
    {
        [Test]
        [TestCase("foo.bar", Browsers.Phantomjs, null, "Vizregress.BDD.Examples.Images.foo.Phantomjs.bar.png", TestName = "No Locale")]
        [TestCase("foo.bar", Browsers.Phantomjs, "en-GB", "Vizregress.BDD.Examples.Images.foo.Phantomjs.bar.en-gb.png", TestName = "en-GB")]      
        public void ResourceResolveBasic(string name, Browsers browser, string ietf, string expectedResource) 
        {            
            var resolver = MockRepository.GenerateStrictMock<ExpectedResourceResolver>();
            var info = MockRepository.GenerateStub<IInformationalWebDriver>();

            info.Expect(o => o.Locale).Return(ietf);
            info.Expect(o => o.Browser).Return(browser);

            resolver.Expect(o => o.GetStreamForResource(Arg<string>.Is.Equal(expectedResource)))
                .Return(new MemoryStream());

            resolver.Replay();
            resolver.Resolve(name,info);
            resolver.VerifyAllExpectations();
        }
    }
}
