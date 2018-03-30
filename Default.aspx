<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="row margin-fix">
            <div class="col-md-3 animated fadeInLeft">
                Search By Employee Name:<br />
                <asp:TextBox ID="txtSearchStr" runat="server" CssClass=" form-control textbox asp"></asp:TextBox>
                <br />
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Search" OnClientClick="load();" CssClass="btn btn-default margin-fix" />
                <div id="num_results">&nbsp;</div>
                <div id="employee_loading">
                    <asp:UpdateProgress ID="updateProgress" runat="server">
                        <ProgressTemplate>
                            <div style="text-align: center">
                                <div class="jqueryloading">&nbsp;</div>
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </div>
                    <asp:UpdatePanel ID="Find_an_employee_updatePanel" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Button1" />
                        </Triggers>
                        <ContentTemplate>
                               <asp:Label ID="lblResults" runat="server" Text="" CssClass="animated fadeInUp"></asp:Label>
                            <label id="lblaction"></label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
            </div>
        </div>
    </form>
</body>
</html>
