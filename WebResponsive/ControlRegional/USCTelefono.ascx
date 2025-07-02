<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="USCTelefono.ascx.vb" Inherits="di.financiera.webResponsive.USCTelefono" %>

<%@ Register TagPrefix="cc1" Namespace="ControlesWeb" Assembly="ControlesWeb" %>

<div class="form-group">
    <asp:Label ID="lblTelefono" runat="server" CssClass="bmd-label-floating" EnableViewState="true" ClientIDMode="AutoID">Teléfono</asp:Label>
    <div class="input-group" style="margin-bottom: 0px;">
        <cc1:IntegerBox ID="intTelefonoCodigoArea" runat="server" CssClass="form-control tel-cod-area" EnableViewState="true" UsaSeparadorPuntos="false" Width="100%" ClientIDMode="Static" ></cc1:IntegerBox>
        <cc1:IntegerBox ID="intTelefonoCaracteristica" runat="server" CssClass="form-control tel-carac" EnableViewState="true" UsaSeparadorPuntos="false"  Width="100%" ClientIDMode="Static" ></cc1:IntegerBox>
        <cc1:IntegerBox ID="intTelefonoNumero" runat="server" CssClass="form-control tel-numero" EnableViewState="true" UsaSeparadorPuntos="false" Width="100%" ClientIDMode="Static" ></cc1:IntegerBox>

        <asp:RequiredFieldValidator ID="valTelefonoCodigoArea" runat="server" CssClass="validator" EnableViewState="true" ControlToValidate="intTelefonoCodigoArea" ErrorMessage="El codigo de area es un campo requerido." Visible="false" ForeColor=" " ClientIDMode="Static" Style="display: none;">*</asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="valTelefonoCaracteristica" runat="server" CssClass="validator" EnableViewState="true" ControlToValidate="intTelefonoCaracteristica" ErrorMessage="La caracteristica es un campo requerido." Visible="false" ForeColor=" " ClientIDMode="Static" Style="display: none;">*</asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="valTelefonoNumero" runat="server" CssClass="validator" EnableViewState="true" ControlToValidate="intTelefonoNumero" ErrorMessage="El numero es un campo requerido." Visible="false" ForeColor=" " ClientIDMode="Static" Style="display: none;">*</asp:RequiredFieldValidator>
    </div>
</div>

<ajaxToolkit:AutoCompleteExtender ID="intTelefonoCodigoArea_AutoCompleteExtender" ClientIDMode="Static"
    runat="server" Enabled="false"
    ServiceMethod="obtenerCodigoArea"
    ServicePath="~/ServiciosAJAX/AutocompletarTelefono.asmx"
    MinimumPrefixLength="1"
    EnableCaching="false"
    UseContextKey="true"
    CompletionSetCount="1"
    CompletionInterval="2"
    TargetControlID=""
    FirstRowSelected="True"
    CompletionListCssClass="completionList"
    CompletionListHighlightedItemCssClass="itemHighlighted"
    CompletionListItemCssClass="listItem">
</ajaxToolkit:AutoCompleteExtender>
<ajaxToolkit:AutoCompleteExtender ID="intTelefonoCaracteristica_AutoCompleteExtender" ClientIDMode="Static"
    runat="server" Enabled="false"
    ServiceMethod="ObtenerCaracteristica"
    ServicePath="~/ServiciosAJAX/AutocompletarTelefono.asmx"
    MinimumPrefixLength="1"
    UseContextKey="true"
    EnableCaching="false"
    CompletionSetCount="1"
    CompletionInterval="2"
    TargetControlID=""
    FirstRowSelected="True"
    CompletionListCssClass="completionList"
    CompletionListHighlightedItemCssClass="itemHighlighted"
    CompletionListItemCssClass="listItem">
</ajaxToolkit:AutoCompleteExtender>