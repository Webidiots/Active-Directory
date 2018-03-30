using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.DirectoryServices;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Add onkeydown attribute to specific textbox
        txtSearchStr.Attributes.Add("onkeydown", "if ((event.which && event.which == 13) || (event.keyCode && event.keyCode == 13)) {document.getElementById('ctl00_ContentPlaceHolder1_Button1').click(); return false;} else return true;");
        if (Request.QueryString["keyword"] != null)
        {
            if (txtSearchStr.Text.Length == 0)
            {
                txtSearchStr.Text = Request.QueryString["keyword"];
                getPeeps();
            }
        }
        else
        {
        }
    }
    protected void getPeeps()
    {
        System.Web.HttpBrowserCapabilities browser = Request.Browser;
        List<Github.Users> lst = Github.GetADUsers(txtSearchStr.Text);
        string rsults = string.Empty;
        for (int i = 0; i < lst.Count; i++)
        {
            if (browser.Browser == "IE" || browser.Browser == "Mozilla" || browser.Browser == "InternetExplorer" || browser.Browser == "Chrome")
            {
                if (((Github.Users)lst[i]).EmpPhoto == null)
                {
                    rsults = rsults + "<div class='card' style='width:25rem;'><div class='margin-fix'><span class='icon-size'><img class='card-img-top img-responsive rounded-circle' src='../Assets/O14_person_placeHolder_192_small.png' width='87' height='87' alt='Card image cap' /></span><h4 class='card-title'>" + ((Github.Users)lst[i]).DisplayName + "</h4><h6 class='card-title'>" + ((Github.Users)lst[i]).Department + " Department" + "<br /> " + ((Github.Users)lst[i]).EmployeeTitle + ", <i class='fa fa-map-marker'></i> " + ((Github.Users)lst[i]).office + "</h6> <b>Reports to: </b> " + ((Github.Users)lst[i]).Manager + "</h6> <br /><b>Level 2 Manager: </b> " + ((Github.Users)lst[i]).Level2Manager + "</h6><br /><i class='fa fa-phone-square'></i> " + ((Github.Users)lst[i]).PhoneNumber + " <br /><i class='fa fa-mobile'></i> " + ((Github.Users)lst[i]).Mobile + "</div></div><br />";
                  
                }
                else
                {
                    rsults = rsults + "<div class='card' style='width:25rem;'><div class='margin-fix'><span class='icon-size'><img class='card-img-top img-responsive rounded-circle' src='data:image/png;base64," + ((Github.Users)lst[i]).EmpPhoto + "' width='87' height='87' alt='Card image cap' /></span><h4 class='card-title'>" + ((Github.Users)lst[i]).DisplayName + "</h4><h6 class='card-title'>" + ((Github.Users)lst[i]).Department + " Department" + "<br /> " + ((Github.Users)lst[i]).EmployeeTitle + ", <i class='fa fa-map-marker'></i> " + ((Github.Users)lst[i]).office + "</h6><b>Reports to: </b> " + ((Github.Users)lst[i]).Manager + "</h6><br /> <b>Level 2 Manager: </b> " + ((Github.Users)lst[i]).Level2Manager + "</h6><br /><i class='fa fa-phone-square'></i> " + ((Github.Users)lst[i]).PhoneNumber + "<br /><i class='fa fa-mobile'></i> " + ((Github.Users)lst[i]).Mobile + "</div></div><br />";
                  
                }
            }
            else
            {

            }
        }
        lblResults.Text = rsults;
    }
}