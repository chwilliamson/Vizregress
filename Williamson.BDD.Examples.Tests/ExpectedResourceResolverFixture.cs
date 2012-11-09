﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Rhino.Mocks;

namespace Williamson.BDD.Examples.Tests
{
    /// <summary>
    /// Performs tests against <see cref="ExpectedResourceResolver"/>
    /// </summary>
    [TestFixture]
    public class ExpectedResourceResolverFixture
    {
        [Test]
        [TestCase("foo.bar", Browsers.FireFox_3_6_28, null, "Williamson.BDD.Examples.Images.foo.FireFox_3_6_28.bar.png", TestName="No Locale")]
        [TestCase("foo.bar", Browsers.FireFox_3_6_28, "en-GB", "Williamson.BDD.Examples.Images.foo.FireFox_3_6_28.bar.en-gb.png", TestName="en-GB")]      
        public void ResourceResolveBasic(string name, Browsers browser, string ietf, string expectedResource) 
        {            
            var resolver = MockRepository.GenerateStrictMock<ExpectedResourceResolver>();
            var info = MockRepository.GenerateStub<IInformationalWebDriver>();

            info.Expect(o => o.Locale).Return(ietf);
            info.Expect(o => o.Browser).Return(browser);

            var split = name.Split('.');

            resolver.Expect(o => o.GetStreamForResource(Arg<string>.Is.Equal(expectedResource)))
                .Return(new MemoryStream());

            resolver.Replay();
            resolver.Resolve(name,info);
            resolver.VerifyAllExpectations();
        }
    }
}
