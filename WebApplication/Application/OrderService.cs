﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Repositories;
using WebApplication.Models;
using WebApplication.Common;

namespace WebApplication.Application
{
    public class OrderService
    {
        private readonly Contexto contexto;

        public OrderService()
        {
            contexto = new Contexto();
        }

        public List<Order> ListAll()
        {
            var Orders = new List<Order>();
            const string strQuery = @"SELECT o1.*, u1.`Name` as 'AuditorName',u2.`Name` as 'ProductorName' FROM `orders` o1
                                    left join users as u1 on o1. auditor = u1.id
                                    left join users as u2 on o1.Productor = u2.id
                                    where o1.`Status` <> 4
                                    order by o1.id desc ";
          
            var rows = contexto.ExecuteCommandSQL(strQuery, null);
            foreach (var row in rows)
            {
                Order tempOrder = new Order();
                tempOrder.Id = int.Parse(row["Id"].ToString());
                tempOrder.TaobaoId = int.Parse(row["TaobaoId"].ToString());
                tempOrder.ImageType = int.Parse(row["ImageType"].ToString());
                tempOrder.ImageTypeName = ((EnumImageType)int.Parse(row["ImageType"].ToString())).ToString();
                tempOrder.ImageSize = int.Parse(row["ImageSize"].ToString());
                tempOrder.Font = int.Parse(row["Font"].ToString());
                tempOrder.FontName = ((EnumFont)int.Parse(row["Font"].ToString())).ToString();
                tempOrder.Style = int.Parse(row["Style"].ToString());
                tempOrder.StyleName = ((EnumImageStyle)int.Parse(row["Style"].ToString())).ToString();
                tempOrder.Text = row["Text"].ToString();
                tempOrder.ImageUrl = row["ImageUrl"].ToString();
                tempOrder.BgImage = int.Parse(row["BgImage"].ToString());
                tempOrder.BgImageUrl = ""; //to do
                tempOrder.SubmitTime = DateTime.Parse(row["SubmitTime"].ToString());
                tempOrder.Status = int.Parse(row["Status"].ToString());
                tempOrder.StatusName = ((EnumStatus)int.Parse(row["Status"].ToString())).ToString();
                tempOrder.Auditor = int.Parse(row["Auditor"].ToString());
                tempOrder.AuditorName = row["AuditorName"] == null ? "" : row["AuditorName"].ToString();
                tempOrder.Productor = int.Parse(row["Productor"].ToString());
                tempOrder.ProductorName = row["ProductorName"] == null ? "" : row["ProductorName"].ToString();
                tempOrder.ProductTime = DateTime.Parse(row["ProductTime"].ToString());
                tempOrder.DeleteTime = DateTime.Parse(row["DeleteTime"].ToString()); 
                Orders.Add(tempOrder);
            }
            return Orders;
        }

        public List<Order> ListAll(string status)
        {
            var Orders = new List<Order>();
            const string strQuery = @"SELECT o1.*, u1.`Name` as 'AuditorName',u2.`Name` as 'ProductorName' FROM `orders` o1
                                    left join users as u1 on o1. auditor = u1.id
                                    left join users as u2 on o1.Productor = u2.id 
                                    where o1.`Status` = @status and o1.`Status` <> 4
                                    order by o1.id desc";
            var parameters = new Dictionary<string, object>
            {
                { "Status", status }
            };
            var rows = contexto.ExecuteCommandSQL(strQuery, parameters);
            foreach (var row in rows)
            {
                var tempOrder = new Order
                {
                    Id = int.Parse(row["Id"].ToString()),
                    TaobaoId = int.Parse(row["TaobaoId"].ToString()),
                    ImageType = int.Parse(row["ImageType"].ToString()),
                    ImageTypeName = ((EnumImageType)int.Parse(row["ImageType"].ToString())).ToString(),
                    ImageSize = int.Parse(row["ImageSize"].ToString()),
                    Font = int.Parse(row["Font"].ToString()),
                    FontName = ((EnumFont)int.Parse(row["Font"].ToString())).ToString(),
                    Style = int.Parse(row["Style"].ToString()),
                    StyleName = ((EnumImageStyle)int.Parse(row["Style"].ToString())).ToString(),
                    Text = row["Text"].ToString(),
                    ImageUrl = row["ImageUrl"].ToString(),
                    BgImage = int.Parse(row["BgImage"].ToString()),
                    BgImageUrl = "", //to do
                    SubmitTime = DateTime.Parse(row["SubmitTime"].ToString()),
                    Status = int.Parse(row["Status"].ToString()),
                    StatusName = ((EnumStatus)int.Parse(row["Status"].ToString())).ToString(),
                    Auditor = int.Parse(row["Auditor"].ToString()),
                    AuditorName = row["AuditorName"] == null ? "" : row["AuditorName"].ToString(),
                    Productor = int.Parse(row["Productor"].ToString()),
                    ProductorName = row["ProductorName"] == null ? "" : row["ProductorName"].ToString(),
                    ProductTime = DateTime.Parse(row["ProductTime"].ToString()),
                    DeleteTime = DateTime.Parse(row["DeleteTime"].ToString())
                };
                Orders.Add(tempOrder);
            }
            return Orders;
        }

        public Order GetOrderById(int id)
        {
            var Orders = new List<Order>();
            const string strQuery = @"SELECT o1.*, u1.`Name` as 'AuditorName',u2.`Name` as 'ProductorName' FROM `orders` o1
                                    left join users as u1 on o1. auditor = u1.id
                                    left join users as u2 on o1.Productor = u2.id 
                                    where o1.id = @Id";
            var parameters = new Dictionary<string, object>
            {
                { "Id", id }
            };
            var rows = contexto.ExecuteCommandSQL(strQuery, parameters);
            var row = rows.FirstOrDefault();
            if (row == null)
            {
                return null;
            }
            var tempOrder = new Order
            {
                Id = int.Parse(row["Id"].ToString()),
                TaobaoId = int.Parse(row["TaobaoId"].ToString()),
                ImageType = int.Parse(row["ImageType"].ToString()),
                ImageTypeName = ((EnumImageType)int.Parse(row["ImageType"].ToString())).ToString(),
                ImageSize = int.Parse(row["ImageSize"].ToString()),
                Font = int.Parse(row["Font"].ToString()),
                FontName = ((EnumFont)int.Parse(row["Font"].ToString())).ToString(),
                Style = int.Parse(row["Style"].ToString()),
                StyleName = ((EnumImageStyle)int.Parse(row["Style"].ToString())).ToString(),
                Text = row["Text"].ToString(),
                ImageUrl = row["ImageUrl"].ToString(),
                BgImage = int.Parse(row["BgImage"].ToString()),
                BgImageUrl = "", //to do
                SubmitTime = DateTime.Parse(row["SubmitTime"].ToString()),
                Status = int.Parse(row["Status"].ToString()),
                StatusName = ((EnumStatus)int.Parse(row["Status"].ToString())).ToString(),
                Auditor = int.Parse(row["Auditor"].ToString()),
                AuditorName = row["AuditorName"] == null ? "" : row["AuditorName"].ToString(),
                Productor = int.Parse(row["Productor"].ToString()),
                ProductorName = row["ProductorName"] == null ? "" : row["ProductorName"].ToString(),
                ProductTime = DateTime.Parse(row["ProductTime"].ToString()),
                DeleteTime = DateTime.Parse(row["DeleteTime"].ToString())
            };
            return tempOrder;
        }

        public int Insert(Order order)
        {
            const string commandSQL = @"INSERT into orders(TaobaoId, ImageType, ImageSize, Font, Style, Text, ImageUrl, BgImage, SubmitTime, Status, AuditTime, ProductTime, DeleteTime)  
                                        VALUES(@TaobaoId, @ImageType, @ImageSize, @Font, @Style,  @Text, @ImageUrl, @BgImage, @SubmitTime, @Status, @AuditTime, @ProductTime, @DeleteTime)";
            var parameters = new Dictionary<string, object>
            {
                { "TaobaoId", order.TaobaoId },
                { "ImageType", order.ImageType },
                { "ImageSize", order.ImageSize},
                { "Font", order.Font },
                { "Style", order.Style },
                { "Text", order.Text},
                { "ImageUrl", order.ImageUrl},
                { "BgImage", order.BgImage },
                { "SubmitTime", order.SubmitTime },
                { "Status", order.Status },
                { "AuditTime", order.AuditTime },
                { "ProductTime", order.ProductTime },
                { "DeleteTime", order.DeleteTime},
            };
            return contexto.ExecuteCommand(commandSQL, parameters);
        }

        private int update(Order order)
        {
            var commandSQL = @"UPDATE Orders SET TaobaoId = @TaobaoId, ImageType= @ImageType, ImageSize= @ImageSize, Font= @Font, Style= @Style, Text = @Text, 
                            ImageUrl = @ImageUrl, BgImage = @BgImage, SubmitTime= @SubmitTime, Status= @Status, AuditTime= @AuditTime, Auditor= @Auditor, 
                            Productor= @Productor, ProductTime= @ProductTime, DeleteTime= @DeleteTime WHERE Id = @Id";
            var parameters = new Dictionary<string, object>
            {
                {"Id", order.Id},
                { "TaobaoId", order.TaobaoId },
                { "ImageType", order.ImageType },
                { "ImageSize", order.ImageSize},
                { "Font", order.Font },
                { "Style", order.Style },
                { "Text", order.Text},
                { "ImageUrl", order.ImageUrl},
                { "BgImage", order.BgImage},
                { "SubmitTime", order.SubmitTime },
                { "Status", order.Status },
                { "AuditTime", order.AuditTime },
                { "Auditor", order.Auditor },
                { "Productor", order.Productor },
                { "ProductTime", order.ProductTime },
                { "DeleteTime", order.DeleteTime },
            };
            return contexto.ExecuteCommand(commandSQL, parameters);
        }

        public void Save(Order Order)
        {
            if (Order.Id > 0)
                update(Order);
            else
                Insert(Order);
        }

        public int Delete(int Id)
        {
            const string commandSQL = "DELETE FROM Orders WHERE Id=@Id";
            var parameters = new Dictionary<string, object>
            {
                {"Id", Id}
            };
            return contexto.ExecuteCommand(commandSQL, parameters);
        }
    }
}