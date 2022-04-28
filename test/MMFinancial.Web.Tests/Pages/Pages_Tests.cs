using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace MMFinancial.Pages
{
    public class Pages_Tests : MMFinancialWebTestBase
    {
        [Fact]
        public async Task ShoulGetUploadsPage()
        {
            var response = await GetResponseAsStringAsync("/Transactions/Upload");
            response.ShouldNotBeNull();
        }

        [Fact]
        public async Task ShoulGetSuspectPage()
        {
            var response = await GetResponseAsStringAsync("/Transactions/Suspect");
            response.ShouldNotBeNull();
        }
    }
}
