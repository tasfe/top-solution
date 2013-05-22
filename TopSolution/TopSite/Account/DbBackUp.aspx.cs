using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TopLogic;

namespace TopSite.Account
{
    public partial class DbBackUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Request.QueryString["action"] == "doback")
            {
                Backup();
            }
        }

        private void Backup()
        {
            try
            {
                using (DataBaseManagerLogic logic = new DataBaseManagerLogic())
                {
                    logic.BackupDataBase();
                }
                Response.Write("备份成功");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);

            }
            Response.End();
        }
    }
}