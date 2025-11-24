<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="phoneDirectory.aspx.cs" Inherits="soha_f6269.Demo.phoneDirectory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br/> <br/>
   <div>
       <li><a runat="server" href="https://member5-1.smarterasp.net/cp/cp_screen">Download</a></li>
       <asp:Label ID="lblOutput" runat="server" Text=""></asp:Label>
   </div>
    <asp:DropDownList ID="ddlfName" runat="server">
    </asp:DropDownList>
    <br/>

    <asp:GridView ID="gvContact" runat="server" AutoGenerateColumns="False" DataKeyNames="contactId" >
        <Columns>
            <asp:BoundField DataField="contactId" HeaderText="contactId" ReadOnly="True" SortExpression="contactId" />
            <asp:BoundField DataField="fName" HeaderText="fName" SortExpression="fName" />
            <asp:BoundField DataField="lName" HeaderText="lName" SortExpression="lName" />
            <asp:BoundField DataField="cell" HeaderText="cell" SortExpression="cell" />
            <asp:BoundField DataField="email" HeaderText="email" SortExpression="email" />
            <asp:BoundField DataField="country" HeaderText="country" SortExpression="country" />
        </Columns>
    </asp:GridView>
</asp:Content>
