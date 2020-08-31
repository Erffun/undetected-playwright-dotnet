using System.Linq;
using System.Threading.Tasks;
using PlaywrightSharp.Tests.BaseTests;
using PlaywrightSharp.Tests.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace PlaywrightSharp.Tests.QuerySelector
{
    ///<playwright-file>queryselector.spec.js</playwright-file>
    ///<playwright-describe>css selector</playwright-describe>
    [Collection(TestConstants.TestFixtureBrowserCollectionName)]
    public class CssSelectorTests : PlaywrightSharpPageBaseTest
    {
        /// <inheritdoc/>
        public CssSelectorTests(ITestOutputHelper output) : base(output)
        {
        }

        ///<playwright-file>queryselector.spec.js</playwright-file>
        ///<playwright-describe>css selector</playwright-describe>
        ///<playwright-it>should work for open shadow roots</playwright-it>
        [Fact(Timeout = PlaywrightSharp.Playwright.DefaultTimeout)]
        public async Task ShouldWorkForOpenShadowRoots()
        {
            await Page.GoToAsync(TestConstants.ServerUrl + "/deep-shadow.html");
            Assert.Equal("Hello from root1", await Page.QuerySelectorEvaluateAsync<string>("css=span", "e => e.textContent"));
            Assert.Equal("Hello from root3 #2", await Page.QuerySelectorEvaluateAsync<string>("css =[attr=\"value\\ space\"]", "e => e.textContent"));
            Assert.Equal("Hello from root3 #2", await Page.QuerySelectorEvaluateAsync<string>("css =[attr='value\\ \\space']", "e => e.textContent"));
            Assert.Equal("Hello from root2", await Page.QuerySelectorEvaluateAsync<string>("css=div div span", "e => e.textContent"));
            Assert.Equal("Hello from root3 #2", await Page.QuerySelectorEvaluateAsync<string>("css=div span + span", "e => e.textContent"));
            Assert.Equal("Hello from root3 #2", await Page.QuerySelectorEvaluateAsync<string>("css=span + [attr *=\"value\"]", "e => e.textContent"));
            Assert.Equal("Hello from root3 #2", await Page.QuerySelectorEvaluateAsync<string>("css=[data-testid=\"foo\"] + [attr*=\"value\"]", "e => e.textContent"));
            Assert.Equal("Hello from root2", await Page.QuerySelectorEvaluateAsync<string>("css=#target", "e => e.textContent"));
            Assert.Equal("Hello from root2", await Page.QuerySelectorEvaluateAsync<string>("css=div #target", "e => e.textContent"));
            Assert.Equal("Hello from root2", await Page.QuerySelectorEvaluateAsync<string>("css=div div #target", "e => e.textContent"));
            Assert.Null(await Page.QuerySelectorAsync("css=div div div #target"));
            Assert.Equal("Hello from root2", await Page.QuerySelectorEvaluateAsync<string>("css=section > div div span", "e => e.textContent"));
            Assert.Equal("Hello from root3 #2", await Page.QuerySelectorEvaluateAsync<string>("css=section > div div span:nth-child(2)", "e => e.textContent"));
            Assert.Null(await Page.QuerySelectorAsync("css=section div div div div"));

            var root2 = await Page.QuerySelectorAsync("css=div div");
            Assert.Equal("Hello from root2", await root2.QuerySelectorEvaluateAsync<string>("css=#target", "e => e.textContent"));
            Assert.Null(await root2.QuerySelectorAsync("css:light=#target"));
            var root2Shadow = await root2.EvaluateHandleAsync("r => r.shadowRoot") as IElementHandle;
            Assert.Equal("Hello from root2", await root2Shadow.QuerySelectorEvaluateAsync<string>("css:light=#target", "e => e.textContent"));
            var root3 = (await Page.QuerySelectorAllAsync("css=div div")).ElementAt(1);
            Assert.Equal("Hello from root3", await root3.QuerySelectorEvaluateAsync<string>("text=root3", "e => e.textContent"));
            Assert.Equal("Hello from root3 #2", await root3.QuerySelectorEvaluateAsync<string>("css=[attr *=\"value\"]", "e => e.textContent"));
            Assert.Null(await root3.QuerySelectorAsync("css:light=[attr*=\"value\"]"));
        }

        ///<playwright-file>queryselector.spec.js</playwright-file>
        ///<playwright-describe>css selector</playwright-describe>
        ///<playwright-it>should work with > combinator and spaces</playwright-it>
        [Fact(Timeout = PlaywrightSharp.Playwright.DefaultTimeout)]
        public async Task ShouldWorkWithCombinatorAndSpaces()
        {
            await Page.SetContentAsync("<div foo=\"bar\" bar=\"baz\"><span></span></div>");
            Assert.Equal("<span></span>", await Page.QuerySelectorEvaluateAsync<string>("div[foo=\"bar\"] > span", "e => e.outerHTML"));
            Assert.Equal("<span></span>", await Page.QuerySelectorEvaluateAsync<string>("div[foo=\"bar\"] > span", "e => e.outerHTML"));
            Assert.Equal("<span></span>", await Page.QuerySelectorEvaluateAsync<string>("div[foo=\"bar\"] > span", "e => e.outerHTML"));
            Assert.Equal("<span></span>", await Page.QuerySelectorEvaluateAsync<string>("div[foo=\"bar\"] > span", "e => e.outerHTML"));
            Assert.Equal("<span></span>", await Page.QuerySelectorEvaluateAsync<string>("div[foo=\"bar\"] > span", "e => e.outerHTML"));
            Assert.Equal("<span></span>", await Page.QuerySelectorEvaluateAsync<string>("div[foo=\"bar\"] > span", "e => e.outerHTML"));
            Assert.Equal("<span></span>", await Page.QuerySelectorEvaluateAsync<string>("div[foo=\"bar\"] > span", "e => e.outerHTML"));
            Assert.Equal("<span></span>", await Page.QuerySelectorEvaluateAsync<string>("div[foo=\"bar\"][bar=\"baz\"] > span", "e => e.outerHTML"));
            Assert.Equal("<span></span>", await Page.QuerySelectorEvaluateAsync<string>("div[foo=\"bar\"][bar=\"baz\"] > span", "e => e.outerHTML"));
            Assert.Equal("<span></span>", await Page.QuerySelectorEvaluateAsync<string>("div[foo=\"bar\"][bar=\"baz\"] > span", "e => e.outerHTML"));
            Assert.Equal("<span></span>", await Page.QuerySelectorEvaluateAsync<string>("div[foo=\"bar\"][bar=\"baz\"] > span", "e => e.outerHTML"));
            Assert.Equal("<span></span>", await Page.QuerySelectorEvaluateAsync<string>("div[foo=\"bar\"][bar=\"baz\"] > span", "e => e.outerHTML"));
            Assert.Equal("<span></span>", await Page.QuerySelectorEvaluateAsync<string>("div[foo=\"bar\"][bar=\"baz\"] > span", "e => e.outerHTML"));
            Assert.Equal("<span></span>", await Page.QuerySelectorEvaluateAsync<string>("div[foo=\"bar\"][bar=\"baz\"] > span", "e => e.outerHTML"));
        }

        ///<playwright-file>queryselector.spec.js</playwright-file>
        ///<playwright-describe>css selector</playwright-describe>
        ///<playwright-it>should work with comma separated list</playwright-it>
        [Fact(Timeout = PlaywrightSharp.Playwright.DefaultTimeout)]
        public async Task ShouldWorkWithCommaSeparatedList()
        {
            await Page.GoToAsync(TestConstants.ServerUrl + "/deep-shadow.html");
            Assert.Equal(5, await Page.QuerySelectorAllEvaluateAsync<int>("css=span, section #root1", "els => els.length"));
            Assert.Equal(5, await Page.QuerySelectorAllEvaluateAsync<int>("css=section #root1, div span", "els => els.length"));
            Assert.Equal("root1", await Page.QuerySelectorEvaluateAsync<string>("css=doesnotexist, section #root1", "e => e.id"));
            Assert.Equal(1, await Page.QuerySelectorAllEvaluateAsync<int>("css=doesnotexist, section #root1", "els => els.length"));
            Assert.Equal(4, await Page.QuerySelectorAllEvaluateAsync<int>("css=span,div span", "els => els.length"));
            Assert.Equal(4, await Page.QuerySelectorAllEvaluateAsync<int>("css=span,div span, div div span", "els => els.length"));
            Assert.Equal(2, await Page.QuerySelectorAllEvaluateAsync<int>("css=#target,[attr=\"value\\ space\"]", "els => els.length"));
            Assert.Equal(4, await Page.QuerySelectorAllEvaluateAsync<int>("css=#target,[data-testid=\"foo\"],[attr=\"value\\ space\"]", "els => els.length"));
            Assert.Equal(4, await Page.QuerySelectorAllEvaluateAsync<int>("css=#target,[data-testid=\"foo\"],[attr=\"value\\ space\"],span", "els => els.length"));
        }

        ///<playwright-file>queryselector.spec.js</playwright-file>
        ///<playwright-describe>css selector</playwright-describe>
        ///<playwright-it>should keep dom order with comma separated list</playwright-it>
        [Fact(Timeout = PlaywrightSharp.Playwright.DefaultTimeout)]
        public async Task ShouldKeepDomOrderWithCommaSeparatedList()
        {
            await Page.SetContentAsync("<section><span><div><x></x><y></y></div></span></section>");
            Assert.Equal("SPAN,DIV", await Page.QuerySelectorAllEvaluateAsync<string>("css=span, div", "els => els.map(e => e.nodeName).join(',')"));
            Assert.Equal("SPAN,DIV", await Page.QuerySelectorAllEvaluateAsync<string>("css=div, span", "els => els.map(e => e.nodeName).join(',')"));
            Assert.Equal("DIV", await Page.QuerySelectorAllEvaluateAsync<string>("css=span div, div", "els => els.map(e => e.nodeName).join(',')"));
            Assert.Equal("SECTION", await Page.QuerySelectorAllEvaluateAsync<string>("*css = section >> css = div, span", "els => els.map(e => e.nodeName).join(',')"));
            Assert.Equal("DIV", await Page.QuerySelectorAllEvaluateAsync<string>("css=section >> *css = div >> css = x, y", "els => els.map(e => e.nodeName).join(',')"));
            Assert.Equal("SPAN,DIV", await Page.QuerySelectorAllEvaluateAsync<string>("css=section >> *css = div, span >> css = x, y", "els => els.map(e => e.nodeName).join(',')"));
            Assert.Equal("SPAN,DIV", await Page.QuerySelectorAllEvaluateAsync<string>("css=section >> *css = div, span >> css = y", "els => els.map(e => e.nodeName).join(',')"));
        }

        ///<playwright-file>queryselector.spec.js</playwright-file>
        ///<playwright-describe>css selector</playwright-describe>
        ///<playwright-it>should work with comma inside text</playwright-it>
        [Fact(Timeout = PlaywrightSharp.Playwright.DefaultTimeout)]
        public async Task ShouldWorkWithCommaInsideText()
        {
            await Page.SetContentAsync("<span></span><div attr=\"hello,world!\"></div>");
            Assert.Equal("<div attr=\"hello,world!\"></div>", await Page.QuerySelectorEvaluateAsync<string>("css=div[attr=\"hello,world!\"]", "e => e.outerHTML"));
            Assert.Equal("<div attr=\"hello,world!\"></div>", await Page.QuerySelectorEvaluateAsync<string>("css =[attr=\"hello,world!\"]", "e => e.outerHTML"));
            Assert.Equal("<div attr=\"hello,world!\"></div>", await Page.QuerySelectorEvaluateAsync<string>("css=div[attr='hello,world!']", "e => e.outerHTML"));
            Assert.Equal("<div attr=\"hello,world!\"></div>", await Page.QuerySelectorEvaluateAsync<string>("css=[attr='hello,world!']", "e => e.outerHTML"));
            Assert.Equal("<span></span>", await Page.QuerySelectorEvaluateAsync<string>("css=div[attr=\"hello,world!\"], span", "e => e.outerHTML"));
        }

        ///<playwright-file>queryselector.spec.js</playwright-file>
        ///<playwright-describe>css selector</playwright-describe>
        ///<playwright-it>should work with attribute selectors</playwright-it>
        [Fact(Timeout = PlaywrightSharp.Playwright.DefaultTimeout)]
        public async Task ShouldWorkWithAttributeSelectors()
        {
            await Page.SetContentAsync("<div attr=\"hello world\" attr2=\"hello-''>>foo=bar[]\" attr3=\"] span\"><span></span></div>");
            await Page.EvaluateAsync("() => window.div = document.querySelector('div')");
            string[] selectors = new[] {
                "[attr=\"hello world\"]",
                "[attr = \"hello world\"]",
                "[attr ~= world]",
                "[attr ^=hello ]",
                "[attr $= world ]",
                "[attr *= \"llo wor\" ]",
                "[attr2 |= hello]",
                "[attr = \"Hello World\" i ]",
                "[attr *= \"llo WOR\" i]",
                "[attr $= woRLD i]",
                "[attr2 = \"hello-''>>foo=bar[]\"]",
                "[attr2 $=\"foo=bar[]\"]",
            };

            foreach (string selector in selectors)
            {
                Assert.True(await Page.QuerySelectorEvaluateAsync<bool>(selector, "e => e === div"));
            }

            Assert.True(await Page.QuerySelectorEvaluateAsync<bool>("[attr*=hello] span", "e => e.parentNode === div"));
            Assert.True(await Page.QuerySelectorEvaluateAsync<bool>("[attr*=hello] >> span", "e => e.parentNode === div"));
            Assert.True(await Page.QuerySelectorEvaluateAsync<bool>("[attr3=\"] span\"] >> span", "e => e.parentNode === div"));
        }
    }
}