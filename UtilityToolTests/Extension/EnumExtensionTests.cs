using Microsoft.VisualStudio.TestTools.UnitTesting;
using UtilityTool.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityTool.Extension.Tests
{
    [TestClass()]
    public class EnumExtensionTests
    {
        [TestMethod()]
        [TestCategory("UtilityTool.Extension")]
        [TestProperty("EnumExtension", "GetAttribute")]
        public void GetAllMemberDescriptionTest()
        {
            //arrange

            //actual
            var act = ProductTypeEnum.PreOrder.GetAttribute<ProductTypeEnum, System.ComponentModel.DescriptionAttribute>();
            var expected = "預購品";
            //assert
            act.Equals(expected);
        }
    }

    internal enum ProductTypeEnum
    {
        [System.ComponentModel.Description("None")]
        None = 0,
        [System.ComponentModel.Description("預購品")]
        PreOrder = 1,
        [System.ComponentModel.Description("現貨")]
        Stocks = 2,

    }
}