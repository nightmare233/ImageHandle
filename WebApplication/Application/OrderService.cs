using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Repositories;
using WebApplication.Models;

namespace WebApplication.Application
{
    public class OrderService
    {
        private readonly Contexto contexto;

        public OrderService()
        {
            contexto = new Contexto();
        }

        public List<Order> ListAll(string status)
        {
            var Orders = new List<Order>();
            const string strQuery = @"SELECT o1.*, u1.`Name` as 'AuditorName',u2.`Name` as 'ProductorName' FROM `orders` o1
                                    inner join users as u1 on o1. auditor = u1.id
                                    inner join users as u2 on o1.Productor = u2.id 
                                    where o1.status = @status";
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
                    ImageType = row["ImageType"].ToString(),
                    ImageSize = int.Parse(row["ImageSize"].ToString()),
                    Font = row["Font"].ToString(),
                    Style = row["Style"].ToString(),
                    SubmitTime = DateTime.Parse(row["SubmitTime"].ToString()),
                    Status = int.Parse(row["Status"].ToString()),
                    Auditor = int.Parse(row["Auditor"].ToString()),
                    AuditorName = row["AuditorName"].ToString(),
                    Productor = int.Parse(row["Productor"].ToString()),
                    ProductorName = row["ProductorName"].ToString(),
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
                                    inner join users as u1 on o1. auditor = u1.id
                                    inner join users as u2 on o1.Productor = u2.id 
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
                ImageType = row["ImageType"].ToString(),
                ImageSize = int.Parse(row["ImageSize"].ToString()),
                Font = row["Font"].ToString(),
                Style = row["Style"].ToString(),
                SubmitTime = DateTime.Parse(row["SubmitTime"].ToString()),
                Status = int.Parse(row["Status"].ToString()),
                Auditor = int.Parse(row["Auditor"].ToString()),
                AuditorName = row["AuditorName"].ToString(),
                Productor = int.Parse(row["Productor"].ToString()),
                ProductorName = row["ProductorName"].ToString(),
                ProductTime = DateTime.Parse(row["ProductTime"].ToString()),
                DeleteTime = DateTime.Parse(row["DeleteTime"].ToString())
            };
            return tempOrder;
        }

        public int Insert(Order order)
        {
            const string commandSQL = @"INSERT into (TaobaoId, ImageType, ImageSize, Font, Style, SubmitTime, [Status], AuditTime, Auditor, Productor, ProductTime, DeleteTime) orders 
                                        VALUES(@TaobaoId, @ImageType, @ImageSize, @Font, @Style, @SubmitTime, @Status, @AuditTime, @Auditor, @Productor, @ProductTime, @DeleteTime)";
            var parameters = new Dictionary<string, object>
            {
                { "TaobaoId", order.TaobaoId },
                { "ImageType", order.ImageType },
                { "ImageSize", order.ImageSize},
                { "Font", order.Font },
                { "Style", order.Style },
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

        private int update(Order order)
        {
            var commandSQL = @"UPDATE Orders SET TaobaoId = @TaobaoId, ImageType= @ImageType, ImageSize= @ImageSize, Font= @Font, Style= @Style, SubmitTime= @SubmitTime, 
                            [Status]= @Status, AuditTime= @AuditTime, Auditor= @Auditor, Productor= @Productor, ProductTime= @ProductTime, DeleteTime= @DeleteTime
                            WHERE Id = @Id";
            var parameters = new Dictionary<string, object>
            {
                {"Id", order.Id},
                { "TaobaoId", order.TaobaoId },
                { "ImageType", order.ImageType },
                { "ImageSize", order.ImageSize},
                { "Font", order.Font },
                { "Style", order.Style },
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