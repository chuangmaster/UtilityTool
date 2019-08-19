using Microsoft.VisualStudio.TestTools.UnitTesting;
using UtilityTool.Helper;
using System;
using System.Collections.Generic;
namespace UtilityTool.Helper.Tests
{
    [TestClass()]
    public class EnumHelperTests
    {
        [TestMethod()]
        [TestCategory("UtilityTool.Helper")]
        [TestProperty("EnumHelper", "GetAllMemberDescription")]
        public void GetAllMemberDescriptionTest_取得所有ProductTypeEnum成員的屬性描述()
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

        [TestMethod()]
        [TestCategory("UtilityTool.Helper")]
        [TestProperty("EnumHelper", "GetAllMemberDescription")]
        public void ParseToTest_解析preorder為EnumType_忽略大小寫_應回傳值PreOrder的值()
        {
            //arrange
            //actual
            var act = EnumHelper.ParseTo("preorder", true, ProductTypeEnum.None);
            var expected = ProductTypeEnum.PreOrder;
            //assert
            act.Equals(expected);
        }

        [TestMethod()]
        [TestCategory("UtilityTool.Helper")]
        [TestProperty("EnumHelper", "GetAllMemberDescription")]
        public void ParseToTest_解析preorder為EnumType_不忽略大小寫_應回傳預設值()
        {
            //arrange

            //actual
            var act = EnumHelper.ParseTo("preorder", true, ProductTypeEnum.None);
            var expected = ProductTypeEnum.None;
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