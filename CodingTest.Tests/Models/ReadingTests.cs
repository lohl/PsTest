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
    public class ReadingTests
    {
        [TestMethod]
        public void TotalMag_should_be_the_sum_of_other_3_mags()
        {
            var sut = new Reading();
            sut.MagX = 3.3M;
            sut.MagY = 4.7M;
            sut.MaxZ = 8;
            sut.TotalMag.Should().Equal(16M);
        }
    }
}
