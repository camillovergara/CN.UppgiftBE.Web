using CN.UppgiftBE.Web.Util;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.UppgiftBE.Web.Tests.UtilsTests
{
    [TestClass]
    public class FormatUtils_TryParseInt
    {

        private readonly IFormatUtils _sut;
        public FormatUtils_TryParseInt() {

            
            _sut = new FormatUtils();
        }

        [TestMethod]
        public void FormatUtils_TryParseInt_Int()
        {
           var i= _sut.TryParseInt("2");
           Assert.IsInstanceOfType(i,typeof(Int32));
        }
    }
}
