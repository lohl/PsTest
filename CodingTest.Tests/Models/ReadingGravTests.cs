using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingTest.Models;
using Should.Fluent;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingTest.Tests.Models
{
    [TestClass]
    public class ReadingGravTests
    {
        [TestMethod]
        public void TotalGrav_should_be_calculated()
        {
            var sut = new ReadingGrav();
            sut.GravX = 3.4m;
            sut.GravY = 1.7m;
            sut.GravZ = 3m;
            sut.TotalGrav.Should().Equal(1.7m);
        }

        [TestMethod]
        public void TotalGrav_should_be_zero_if_GravZ_is_zero_to_prevent_divide_by_zero()
        {
            var sut = new ReadingGrav();
            sut.GravX = 5m;
            sut.GravY = 3.3m;
            sut.GravZ = 0.0m;
            sut.TotalGrav.Should().Equal(0m);
        }
    }
}
