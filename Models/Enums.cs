using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Enums
    {
    }
     
    public enum EnumImageType
    {
        全部 = 1,
        圆形章 = 2,
        方形章 = 3,
        扁章 = 4,
        闲章 = 5,
        儿童印章 = 6,
        个性签名章 = 7
    }

    public enum EnumStatus
    {
        待审批 = 0,
        待生产 = 1,
        生产中 = 2,
        已完成 = 3,
        已删除 = 4
    }

    public enum EnumImageStyle
    {
        阴文 = 0,
        阳文 = 1
    }


    public enum EnumRole
    {
        管理员 = 0,
        客服 = 1,
        生产员 = 2
    }


}
