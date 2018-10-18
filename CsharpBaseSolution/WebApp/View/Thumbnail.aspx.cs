using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.View
{
    public partial class Thumbnail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpFileCollection files = Context.Request.Files;
            if (files.Count > 0)
            {
                HttpPostedFile file = files[0];

            }
        }
    }
}