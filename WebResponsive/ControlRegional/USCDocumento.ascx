<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="USCDocumento.ascx.vb" Inherits="di.financiera.webResponsive.USCDocumento" %>

<%@ Register TagPrefix="cc1" Namespace="ControlesWeb" Assembly="ControlesWeb" %>

<script src="../assets/site/documento.js"></script>

<div class="form-group">
    <asp:Label ID="lblDocumento" runat="server" CssClass="bmd-label-floating" EnableViewState="true" ClientIDMode="Static">Documento</asp:Label>
    <cc1:IntegerBox ID="intDocumento" runat="server" CssClass="form-control" Width="100%" AutoComplete="False" ClientIDMode="Static" Visible="false"></cc1:IntegerBox>
    <asp:RequiredFieldValidator ID="valintDocumento" runat="server" CssClass="validator" ControlToValidate="intDocumento" ErrorMessage="El Documento es un campo requerido." Visible="false" ForeColor=" " ClientIDMode="Static" Style="display: none;">*</asp:RequiredFieldValidator>
</div>