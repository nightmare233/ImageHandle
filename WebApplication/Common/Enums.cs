using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Common
{
    public enum EnumImageType
    {
        全部 = 1,
        圆形章 = 2,
        扁章 = 3,
        闲章 = 4,
        方形章 = 5,
        儿童印章 = 6,
        个性签名章 = 7
    }

    public enum EnumStatus
    {
        待审批 = 0,
        待生产 = 1,
        已完成 = 2,
        已删除 = 3
    }

    public enum EnumImageStyle
    {
        阴文 = 0,
        阳文 = 1
    }

    public enum EnumFont
    {
        宋体 = 0,
        微软雅黑 = 1
    }


}