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
        个性签名章 = 7,
        光敏章=8,
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

    public enum EnumTextType
    {
        MainText = 1,
        SmallText = 2
    }

    public enum EnumAction
    {
        新建订单=1,
        创建订单图片=2,
        完成订单=3,
        删除订单=4,
        
        新建预设样式=11,
        删除预设样式=12,

        新建字体=21,
        删除字体=22,

        创建用户=31,
        删除用户=32,

        登录=41,
        登出=42,
    }

}
