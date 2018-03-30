using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.DirectoryServices;
using System.Web.UI.WebControls;
using System.DirectoryServices.AccountManagement;
using System.Web.Security;
using System.Configuration;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;


/// <summary>
/// Summary description for Github
/// </summary>
public class Github
{
    public Github()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public class Users
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string PhoneNumber { get; set; }
        public bool isMapped { get; set; }
        public string EmpPhoto { get; set; }
        public string Manager { get; set; }
        public string Level2Manager { get; set; }
        public string office { get; set; }
        public string EmployeeTitle { get; set; }
        public string Department { get; set; }
        public string Mobile { get; set; }
        public string EmployeeID { get; set; }
    }
    public static List<Users> GetADUsers(string username)
    {
        try
        {
            List<Users> lstADUsers = new List<Users>();

            //Add your domain path e.g LDAP://blah;
            string DomainPath = "";

            DirectoryEntry searchRoot = new DirectoryEntry(DomainPath);
            DirectorySearcher search = new DirectorySearcher(searchRoot);


            if (username == "ALL")
            {
                search.Filter = "(&(objectClass=user)(objectCategory=person))";
            }

            else
            {

                search.Filter = "(&(objectClass=user)(objectCategory=person)(telephoneNumber=*)(sAMAccountName=" + username + "*)(!userAccountControl:1.2.840.113556.1.4.803:=2))";
            }
            search.PageSize = 10000;
            search.PropertiesToLoad.Add("sAMAccountName");
            search.PropertiesToLoad.Add("employeeID");
            search.PropertiesToLoad.Add("mail");
            search.PropertiesToLoad.Add("title");
            search.PropertiesToLoad.Add("usergroup");
            search.PropertiesToLoad.Add("telephoneNumber");
            search.PropertiesToLoad.Add("displayName");
            search.PropertiesToLoad.Add("thumbnailPhoto");
            search.PropertiesToLoad.Add("physicalDeliveryOfficeName");
            search.PropertiesToLoad.Add("department");
            search.PropertiesToLoad.Add("mobile");
            search.PropertiesToLoad.Add("manager");

            SearchResult result;
            SearchResultCollection resultCol = search.FindAll();
            if (resultCol != null)
            {
                for (int counter = 0; counter < resultCol.Count; counter++)
                {

                    Users objSurveyUsers = new Users();
                    string UserNameEmailString = string.Empty;
                    result = resultCol[counter];


                    //get current login username Manager
                    if (result.Properties.Contains("manager"))
                    {

                        string MgrName = "";
                        MgrName = result.Properties["Manager"][0].ToString();
                        var input = result.Properties["Manager"][0].ToString();
                        var manager2 = input.Split(',').Select(pair => pair.Split('=').LastOrDefault()).ToArray().GetValue(0);
                        MgrName = manager2.ToString();
                        MgrName = MgrName.Replace(" ", ".");

                        objSurveyUsers.Manager = MgrName;

                        //get current login username Senior Manager
                        string Level2MgrName = Get2ndManager(MgrName);
                        objSurveyUsers.Level2Manager = Level2MgrName;
                    }
                    else
                    {
                        objSurveyUsers.Manager = "N/A";
                      
                    }




                    //get current login username EmpID
                    if (result.Properties.Contains("employeeID"))
                    {
                        objSurveyUsers.EmployeeID = (String)result.Properties["employeeID"][0];
                    }
                    else
                    {
                        objSurveyUsers.EmployeeID = "N/A";

                    }
                   
                    //get current login username Email Address
                    if (result.Properties.Contains("mail"))
                    {
                        objSurveyUsers.Email = (String)result.Properties["mail"][0];
                    }
                    else
                    {
                        objSurveyUsers.Email = "N/A";
                   
                    }
                    //get current login username Emp Job Title
                    if (result.Properties.Contains("title"))
                    {
                        objSurveyUsers.EmployeeTitle = (String)result.Properties["title"][0];
                    }
                    else
                    {
                        objSurveyUsers.EmployeeTitle = "N/A";
                    }

                    //get current login username cn name
                    if (result.Properties.Contains("sAMAccountName"))
                    {
                        objSurveyUsers.UserName = (String)result.Properties["sAMAccountName"][0];
                    }
                    else
                    {
                        objSurveyUsers.UserName = "N/A";
                    }
                    //get current login username display Name
                    if (result.Properties.Contains("displayName"))
                    {
                        objSurveyUsers.DisplayName = (String)result.Properties["displayName"][0];
                    }
                    else
                    {
                        objSurveyUsers.DisplayName = "N/A";
                    }
                    //get current login username Phone Number
                    if (result.Properties.Contains("telephoneNumber"))
                    {
                        objSurveyUsers.PhoneNumber = (String)result.Properties["telephoneNumber"][0];
                    }
                    else
                    {
                        objSurveyUsers.PhoneNumber = "N/A";
                    }
                    //get current login username Location
                    if (result.Properties.Contains("physicalDeliveryOfficeName"))
                    {
                        objSurveyUsers.office = (String)result.Properties["physicalDeliveryOfficeName"][0];
                    }
                    else
                    {
                        objSurveyUsers.office = "N/A";
                    }
                    //get current login username Mobile
                    if (result.Properties.Contains("mobile"))
                    {
                        objSurveyUsers.Mobile = (String)result.Properties["mobile"][0];
                    }
                    else
                    {
                        objSurveyUsers.Mobile = "N/A";
                    }
                    //get current login username Department
                    if (result.Properties.Contains("department"))
                    {
                        objSurveyUsers.Department = (String)result.Properties["department"][0];
                   }
                    else
                    {
                        objSurveyUsers.Department = "N/A";
                    }

                    //get current login username Photo
                    if (result.Properties.Contains("thumbnailPhoto"))
                    {
                        byte[] bytes = (byte[])result.Properties["thumbnailPhoto"][0];
                        string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                        objSurveyUsers.EmpPhoto = base64String;
                        lstADUsers.Add(objSurveyUsers);
                    }
                    else
                    {
                        lstADUsers.Add(objSurveyUsers);
                    }

                }
            }
            else
            {

            }
            return lstADUsers;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
}