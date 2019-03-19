using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Repositories;
using Models;
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
            string strQuery = @"SELECT o1.*, u1.`Name` as 'AuditorName',u2.`Name` as 'ProductorName' FROM `orders` o1
                                    left join users as u1 on o1. auditor = u1.id
                                    left join users as u2 on o1.Productor = u2.id
                                    where o1.`Status` <> 4
                                    order by o1.id desc ";
          
            var rows = contexto.ExecuteCommandSQL(strQuery, null);
            foreach (var row in rows)
            {
                Order tempOrder = new Order();
                tempOrder.Id = int.Parse(row["Id"].ToString());
                tempOrder.TaobaoId = row["TaobaoId"].ToString();
                tempOrder.SampleId = int.Parse(row["SampleId"].ToString());
                tempOrder.Sample = new SampleService().GetSample(tempOrder.SampleId,false);
                tempOrder.MainText = row["MainText"].ToString();
                tempOrder.SmallText = row["SmallText"].ToString();
                tempOrder.ImageUrl = row["ImageUrl"].ToString();
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

        public List<Order> List(int[] status)
        {
            var Orders = new List<Order>();
            string strQuery = @"SELECT o1.*, u1.`Name` as 'AuditorName',u2.`Name` as 'ProductorName' FROM `orders` o1
                                    left join users as u1 on o1. auditor = u1.id
                                    left join users as u2 on o1.Productor = u2.id 
                                    where o1.`Status` in ({0}) order by o1.id desc";
            strQuery = string.Format(strQuery, string.Join(",", status)); 
            var parameters = new Dictionary<string, object>
            {
                { "Status", status }
            };
            var rows = contexto.ExecuteCommandSQL(strQuery, parameters);
            foreach (var row in rows)
            { 
                Order tempOrder = new Order();
                tempOrder.Id = int.Parse(row["Id"].ToString());
                tempOrder.TaobaoId = row["TaobaoId"].ToString();
                tempOrder.SampleId = int.Parse(row["SampleId"].ToString());
                tempOrder.Sample = new SampleService().GetSample(tempOrder.SampleId, false);
                tempOrder.MainText = row["MainText"].ToString();
                tempOrder.SmallText = row["SmallText"].ToString();
                tempOrder.ImageUrl = row["ImageUrl"].ToString();
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
            Order tempOrder = new Order();
            tempOrder.Id = int.Parse(row["Id"].ToString());
            tempOrder.TaobaoId = row["TaobaoId"].ToString();
            tempOrder.SampleId = int.Parse(row["SampleId"].ToString());
            tempOrder.Sample = new SampleService().GetSample(tempOrder.SampleId, false);
            tempOrder.MainText = row["MainText"].ToString();
            tempOrder.SmallText = row["SmallText"].ToString();
            tempOrder.ImageUrl = row["ImageUrl"].ToString();
            tempOrder.SubmitTime = DateTime.Parse(row["SubmitTime"].ToString());
            tempOrder.Status = int.Parse(row["Status"].ToString());
            tempOrder.StatusName = ((EnumStatus)int.Parse(row["Status"].ToString())).ToString();
            tempOrder.Auditor = int.Parse(row["Auditor"].ToString());
            tempOrder.AuditorName = row["AuditorName"] == null ? "" : row["AuditorName"].ToString();
            tempOrder.Productor = int.Parse(row["Productor"].ToString());
            tempOrder.ProductorName = row["ProductorName"] == null ? "" : row["ProductorName"].ToString();
            tempOrder.ProductTime = DateTime.Parse(row["ProductTime"].ToString());
            tempOrder.DeleteTime = DateTime.Parse(row["DeleteTime"].ToString()); 
            return tempOrder;
        }

        public Order GetOrderByTaobaoId(string taobaoId)
        {
            var Orders = new List<Order>();
            const string strQuery = @"SELECT o1.*, u1.`Name` as 'AuditorName',u2.`Name` as 'ProductorName' FROM `orders` o1
                                    left join users as u1 on o1. auditor = u1.id
                                    left join users as u2 on o1.Productor = u2.id 
                                    where o1.taobaoId = @taobaoId and o1.Status <> 4";
            var parameters = new Dictionary<string, object>
            {
                { "taobaoId", taobaoId }
            };
            var rows = contexto.ExecuteCommandSQL(strQuery, parameters);
            var row = rows.FirstOrDefault();
            if (row == null)
            {
                return null;
            }
            Order tempOrder = new Order();
            tempOrder.Id = int.Parse(row["Id"].ToString());
            tempOrder.TaobaoId = row["TaobaoId"].ToString();
            tempOrder.SampleId = int.Parse(row["SampleId"].ToString());
            tempOrder.Sample = new SampleService().GetSample(tempOrder.SampleId, false);
            tempOrder.MainText = row["MainText"].ToString();
            tempOrder.SmallText = row["SmallText"].ToString();
            tempOrder.ImageUrl = row["ImageUrl"].ToString();
            tempOrder.SubmitTime = DateTime.Parse(row["SubmitTime"].ToString());
            tempOrder.Status = int.Parse(row["Status"].ToString());
            tempOrder.StatusName = ((EnumStatus)int.Parse(row["Status"].ToString())).ToString();
            tempOrder.Auditor = int.Parse(row["Auditor"].ToString());
            tempOrder.AuditorName = row["AuditorName"] == null ? "" : row["AuditorName"].ToString();
            tempOrder.Productor = int.Parse(row["Productor"].ToString());
            tempOrder.ProductorName = row["ProductorName"] == null ? "" : row["ProductorName"].ToString();
            tempOrder.ProductTime = DateTime.Parse(row["ProductTime"].ToString());
            tempOrder.DeleteTime = DateTime.Parse(row["DeleteTime"].ToString());
            return tempOrder;
        }

        public int Insert(Order order)
        {
            const string commandSQL = @"INSERT into orders(TaobaoId, SampleId, ImageUrl, SubmitTime, Status, AuditTime, ProductTime, DeleteTime, MainText,SmallText)  
                                        VALUES(@TaobaoId, @SampleId, @ImageUrl, @SubmitTime, @Status, @AuditTime, @ProductTime, @DeleteTime, @MainText,@SmallText)";
            if (order.SmallText == null)
                order.SmallText = "";
            var parameters = new Dictionary<string, object>
            { 
                { "TaobaoId", order.TaobaoId },
                { "SampleId", order.SampleId },
                { "ImageUrl", order.ImageUrl},
                { "SubmitTime", order.SubmitTime },
                { "Status", order.Status }, 
                { "MainText", order.MainText},
                { "SmallText", order.SmallText},
                { "AuditTime", order.AuditTime }, 
                { "Auditor", order.Auditor },
                { "Productor", order.Productor },
                { "ProductTime", order.ProductTime },
                { "DeleteTime", order.DeleteTime },
            };
            return contexto.ExecuteCommand(commandSQL, parameters);
        }

        private int Update(Order order)
        {
            var commandSQL = @"UPDATE Orders SET TaobaoId = @TaobaoId, SampleId=@SampleId, ImageUrl = @ImageUrl, SubmitTime= @SubmitTime, Status= @Status, 
                            MainText= @MainText, SmallText= @SmallText, AuditTime= @AuditTime, Auditor= @Auditor, Productor= @Productor, ProductTime= @ProductTime, DeleteTime= @DeleteTime 
                            WHERE Id = @Id";
            var parameters = new Dictionary<string, object>
            {
                {"Id", order.Id},
                { "TaobaoId", order.TaobaoId },
                 { "SampleId", order.SampleId },
                { "ImageUrl", order.ImageUrl},
                { "SubmitTime", order.SubmitTime },
                { "Status", order.Status },
                { "MainText", order.MainText},
                { "SmallText", order.SmallText},
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
                Update(Order);
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