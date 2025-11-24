<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ServiceControl.aspx.cs" Inherits="soha_f6269.Demo.ServiceControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--<p>--%>
        <br />
        <table class="nav-justified">
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblOutput" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="text-right" style="color: #0033CC; font-size: small; font-weight: bold">Contact ID&nbsp;&nbsp; </td>
                <td>
                    <asp:TextBox ID="txtContactId" runat="server" Width="128px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="text-right" style="color: #0033CC; font-size: small; font-weight: bold"><strong>Fist Name&nbsp;&nbsp; </strong></td>
                <td>
                    <asp:TextBox ID="txtfName" runat="server" Width="128px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="text-right" style="height: 20px; font-weight: bold; font-size: small; color: #0033CC">Last Name&nbsp;&nbsp; </td>
                <td style="height: 20px">
                    <asp:TextBox ID="txtlName" runat="server" Width="128px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="text-right" style="height: 20px; font-weight: bold; font-size: small; color: #0033CC">Cell&nbsp;&nbsp; </td>
                <td style="height: 20px">
                    <asp:TextBox ID="txtCell" runat="server" Width="128px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="text-right" style="font-weight: bold; font-size: small; color: #0033CC">Email&nbsp;&nbsp; </td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server" Width="128px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="text-right" style="font-weight: bold; font-size: small; color: #0033CC">Country&nbsp;&nbsp; </td>
                <td>
                    <asp:DropDownList ID="ddlCountry" runat="server" Width="128px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
                    <asp:Button ID="btnShowContactInfo" runat="server" OnClick="btnShowContactInfo_Click" Text="Show Contact Info" Width="232px" />
                    <asp:Button ID="btnUbdate" runat="server" OnClick="btnUbdate_Click" Text="Ubdate" />
                    <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <br />
                    <br />
                </td>
            </tr>
        </table>
   <%-- </p>--%>
    <div>
        <asp:GridView ID="gvContact" runat="server"></asp:GridView>
    </div>
</asp:Content>
