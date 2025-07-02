<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="USCCuil.ascx.vb" Inherits="di.financiera.webResponsive.USCCuil" %>

<%@ Register TagPrefix="cc1" Namespace="ControlesWeb" Assembly="ControlesWeb" %>

<div class="form-group">
    <asp:Label ID="lblCuil" runat="server" CssClass="bmd-label-floating" EnableViewState="true" ClientIDMode="Static">CUIL</asp:Label>
    <div class="input-group" style="margin-bottom: 0px;">
        <cc1:TextBoxUp ID="intCuil1" Placeholder="20" runat="server" CssClass="form-control cuil-inicio  solonumeros" Width="100%" AutoComplete="False" ClientIDMode="Static" Visible="false"></cc1:TextBoxUp>
        <cc1:TextBoxUp ID="txtDocumentoCuil" UsaSeparadorPuntos="false"  Placeholder="32345678" runat="server" CssClass="form-control cuil-documento" Width="100%" AutoComplete="False" ClientIDMode="Static" Visible="false"></cc1:TextBoxUp>
        <cc1:TextBoxUp ID="intCuil2" runat="server" Placeholder="1" CssClass="form-control cuil-final solonumeros" Width="100%" AutoComplete="False" ClientIDMode="Static" Visible="false"></cc1:TextBoxUp>
    </div>
    <asp:RequiredFieldValidator ID="valCuil1" runat="server" CssClass="validator" ControlToValidate="intCuil1" ErrorMessage="El Cuil 1 es un campo requerido." Visible="false" ForeColor=" " ClientIDMode="Static" Style="display: none;">*</asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="valDocumentoCuil" runat="server" CssClass="validator" ControlToValidate="txtDocumentoCuil" ErrorMessage="El Documento es un campo requerido." Visible="false" ForeColor=" " ClientIDMode="Static" Style="display: none;">*</asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="valCuil2" runat="server" CssClass="validator" ControlToValidate="intCuil2" ErrorMessage="El Cuil 2 es un campo requerido." Visible="false" ForeColor=" " ClientIDMode="Static" Style="display: none;">*</asp:RequiredFieldValidator>
</div>