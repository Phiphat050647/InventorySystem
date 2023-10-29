using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace InventorySystem.Pages.Siam
{
    public class CreateSiamModel : PageModel
    {
        public StockInfo stockInfo = new StockInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
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
                    String sql = "INSERT INTO stocks" +
                         "(item, storeid, supplier, amount) VALUES " +
                         "(@item, @storeid, @supplier, @amount);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@item", stockInfo.item);
                        command.Parameters.AddWithValue("@storeid", stockInfo.storeid);
                        command.Parameters.AddWithValue("@supplier", stockInfo.supplier);
                        command.Parameters.AddWithValue("@amount", stockInfo.amount);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            stockInfo.item = "";
            stockInfo.storeid = "";
            stockInfo.supplier = "";
            stockInfo.amount = "";
            successMessage = "New Item added Correctly";

            Response.Redirect("/Siam/IndexSiam");
        }
    }
}