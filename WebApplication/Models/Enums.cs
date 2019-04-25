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

    public enum EnumTextType
    {
        MainText = 1,
        SmallText = 2
    }

    public enum EnumAction
    {
        AddOrder=1,
        CreateOrderImage=2,
        FinishOrder=3,
        DeleteOrder=4,
        
        AddSample=11,
        DeleteSample=12,

        AddFont=21,
        DeleteFont=22,

        AddUser=31,
        DeleteUser=32,

        Login=41,
        Logout=42,
    }

}
