using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace MMFinancial.Pages;

public class Index_Tests : MMFinancialWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
