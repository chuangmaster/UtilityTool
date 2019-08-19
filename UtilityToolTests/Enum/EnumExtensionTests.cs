using Microsoft.VisualStudio.TestTools.UnitTesting;
using UtilityTool.Helper;
using System;
using System.Collections.Generic;
namespace UtilityTool.Helper.Tests
{
    [TestClass()]
    public class EnumExtensionTests
    {
        [TestMethod()]
        [TestCategory("UtilityTool.Enum")]
        [TestProperty("EnumExtension", "GetAllMemberDescription")]
        public void GetAllMemberDescriptionTest()
        {
            //arrange
            //actual
            var act = EnumHelper.GetAllMemberDescription(typeof(ProductTypeEnum));
            var expected = new Dictionary<int, string>
            {
                {0,"None"},{1,"預購品"},{2,"現貨"},
            };
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