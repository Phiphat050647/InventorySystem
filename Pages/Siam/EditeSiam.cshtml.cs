using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace InventorySystem.Pages.Siam
{
    public class EditeSiamModel : PageModel
    {
        public StockInfo stockInfo = new StockInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String itemid = Request.Query["itemid"];

            try
            {
                String conectionString = "Server=tcp:inventory191321.database.windows.net,1433;Initial Catalog=inventorysystem;Persist Security Info=False;User ID=Phiphat.deep;Password=0803340674Za;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(conectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM stocks WHERE itemid=@itemid";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@itemid", itemid);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                stockInfo.itemid = reader.GetInt32(0).ToString();
                                stockInfo.item = reader.GetString(1);
                                stockInfo.storeid = reader.GetString(2);
                                stockInfo.supplier = reader.GetString(3);
                                stockInfo.amount = reader.GetString(4);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
        public void OnPost()
        {
            stockInfo.itemid = Request.Form["itemid"];
            stockInfo.item = Request.Form["item"];
            stockInfo.storeid = Request.Form["storeid"];
            stockInfo.supplier = Request.Form["supplier"];
            stockInfo.amount = Request.Form["amount"];

            if (stockInfo.item.Length == 0 || stockInfo.storeid.Length == 0 ||
                stockInfo.supplier.Length == 0 || stockInfo.amount.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            try
            {
                String conectionString = "Server=tcp:inventory191321.database.windows.net,1433;Initial Catalog=inventorysystem;Persist Security Info=False;User ID=Phiphat.deep;Password=0803340674Za;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(conectionString))
                {
                    connection.Open();
                    String sql = "UPDATE stocks " +
                        "SET item=@item, storeid=@storeid, supplier=@supplier, amount=@amount " +
                        "WHERE itemid=@itemid"; // ????? @itemid ???????? SQL
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@item", stockInfo.item);
                        command.Parameters.AddWithValue("@storeid", stockInfo.storeid);
                        command.Parameters.AddWithValue("@supplier", stockInfo.supplier);
                        command.Parameters.AddWithValue("@amount", stockInfo.amount);
                        command.Parameters.AddWithValue("@itemid", stockInfo.itemid); // ???????????????? @itemid

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Siam/IndexSiam");
        }
    }
}
