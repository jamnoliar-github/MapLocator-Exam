<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MapLocator._Default" %>

<%@ Register Assembly="GMaps" Namespace="Subgurim.Controles" TagPrefix="cc1" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h3>Restaurants in Cebu</h3>
    <asp:DropDownList ID="ddlType" runat="server" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" AutoPostBack="true">
        
    </asp:DropDownList>

    <div>
        <cc1:GMap ID="GMap1" runat="server" Width="1000px" Height="1000px" />
    </div>


    <script>
        function ShowDirections(lat, long) {
            var param1 = lat.toString();
            var param2 = long.toString();
            $.ajax({
                type: "POST",
                url: 'Default.aspx/GetDirections',
                data: {
                    latitude: param1,
                    longitude: param2
                },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    alert("success");
                },
                error: function (e) {
                    alert("Something Wrong." + e);
                }
            });
        }
    </script>

</asp:Content>


