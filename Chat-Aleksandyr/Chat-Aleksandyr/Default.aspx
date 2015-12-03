<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Chat_Aleksandyr._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="wrapper">
        <form>
            <label for="nickName">Nick:</label>
            <input type="text" id="nickName" />
        </form>
        <div id="chat-container">
        
        </div>
        <div>
            <form>
                <input type="text" id="message" />
                <button id="send" onclick="SendMessage(); return false;">Send</button>
            </form>
        </div>
    </div>
    <script src="Scripts/WebSocket.js"></script>
</asp:Content>
