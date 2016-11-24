<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="DemoSebastienBouchard._Default" %>

<%@ Register assembly="DevExpress.Web.v16.1, Version=16.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Grille d'entrée de temps</title>
</head>
<body>
    <form id="form1" runat="server">
        <dx:ASPxRoundPanel ID="rpTitle" runat="server" ShowHeader="false" Height="16px" ShowCollapseButton="true" Width="100%" Theme="Glass">
            <PanelCollection>
<dx:PanelContent runat="server">
    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="GRILLE D'ENTRÉE DE TEMPS" Theme="iOS" Width="100%" Style="text-align:center;font-weight:800">
    </dx:ASPxLabel>
                </dx:PanelContent>
</PanelCollection>
        </dx:ASPxRoundPanel>
        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <dx:ASPxRoundPanel ID="rpUsers" runat="server" HeaderText="Consultants" Width="100%" Theme="Glass">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <dx:ASPxComboBox ID="cboUsers" runat="server" AutoPostBack="True" Theme="Glass">
                            </dx:ASPxComboBox>
                            <asp:LinkButton ID="lnkCreateNewUser" runat="server">Créer un nouveau consultant</asp:LinkButton>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxRoundPanel>
                <dx:ASPxRoundPanel ID="panCreateUser" runat="server" Visible="False" ShowHeader="false" Theme="Glass" >
                     <PanelCollection>
                        <dx:PanelContent runat="server">
                    <dx:ASPxLabel ID="lblNewUserName" runat="server" Text="Nom du consultant :">
                    </dx:ASPxLabel>
                    <br />
                    <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                    <asp:Label ID="lblUserNameRequired" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                    <br />
                    <asp:Button ID="btnConfirmCreateUser" runat="server" Text="Créer" />
                    <asp:Button ID="btnCancelCreateUser" runat="server" Text="Annuler" />
                            </dx:PanelContent>
                         </PanelCollection>
                </dx:ASPxRoundPanel>
        <br />
                <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" HeaderText="Grille d'entrées de temps" Theme="Glass" Width="100%">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <dx:ASPxGridView ID="gvEntries" runat="server" AutoGenerateColumns="False" KeyFieldName="EntryId" Theme="Glass" ValidateRequestMode="Enabled" Width="100%">
                                <SettingsContextMenu EnableFooterMenu="False">
                                </SettingsContextMenu>
                                <SettingsPager PageSize="20">
                                </SettingsPager>
                                <SettingsEditing Mode="Inline">
                                </SettingsEditing>
                                <SettingsBehavior ConfirmDelete="True" />
                                <SettingsCommandButton>
                                    <ShowAdaptiveDetailButton ButtonType="Image">
                                    </ShowAdaptiveDetailButton>
                                    <HideAdaptiveDetailButton ButtonType="Image">
                                    </HideAdaptiveDetailButton>
                                    <NewButton Text="Ajouter">
                                    </NewButton>
                                    <UpdateButton Text="Confirmer">
                                    </UpdateButton>
                                    <CancelButton Text="Annuler">
                                    </CancelButton>
                                    <EditButton Text="Modifier">
                                    </EditButton>
                                    <DeleteButton Text="Supprimer">
                                    </DeleteButton>
                                </SettingsCommandButton>
                                <SettingsText ConfirmDelete="Êtes-vous sûr de vouloir effacer cette entrée?" />
                                <Columns>
                                    <dx:GridViewDataComboBoxColumn Caption="Projet" FieldName="Project_Id" Name="cboProject" ShowInCustomizationForm="True" VisibleIndex="0" Width="15%">
                                        <PropertiesComboBox>
                                            <ValidationSettings CausesValidation="True">
                                                <RequiredField ErrorText="Un projet associé est requis" IsRequired="True" />
                                            </ValidationSettings>
                                        </PropertiesComboBox>
                                    </dx:GridViewDataComboBoxColumn>
                                    <dx:GridViewDataComboBoxColumn Caption="Tâche" FieldName="Task_Id" Name="dcTask" ShowInCustomizationForm="True" VisibleIndex="1" Width="15%">
                                        <PropertiesComboBox>
                                            <ValidationSettings CausesValidation="True">
                                                <RequiredField ErrorText="Une tâche associée est requise" IsRequired="True" />
                                            </ValidationSettings>
                                        </PropertiesComboBox>
                                    </dx:GridViewDataComboBoxColumn>
                                    <dx:GridViewDataTextColumn FieldName="Description" Name="dcDescription" ShowInCustomizationForm="True" VisibleIndex="2" Width="15%"> 
                                        <PropertiesTextEdit>
                                            <ValidationSettings CausesValidation="True">
                                                <RequiredField ErrorText="La description est requise" IsRequired="True" />
                                            </ValidationSettings>
                                        </PropertiesTextEdit>
                                        <EditFormSettings Caption="Entrer la description" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Nombre d'heures" FieldName="NbHours" Name="dcNbHours" ShowInCustomizationForm="True" VisibleIndex="3" Width="15%">
                                        <PropertiesTextEdit>
                                            <ValidationSettings CausesValidation="True">
                                                <RequiredField ErrorText="Le nombre d'heures est requis" IsRequired="True" />
                                            </ValidationSettings>
                                        </PropertiesTextEdit>
                                        <EditFormSettings Caption="Entrer le nombre d'heures" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataDateColumn Caption="Date d'entrée" FieldName="EntryDate" Name="dcEntryDate" ShowInCustomizationForm="True" VisibleIndex="4" Width="15%">
                                        <PropertiesDateEdit>
                                            <ValidationSettings CausesValidation="True">
                                                <RequiredField ErrorText="La date d'entrée est requise" IsRequired="True" />
                                            </ValidationSettings>
                                        </PropertiesDateEdit>
                                        <SettingsHeaderFilter Mode="DateRangePicker">
                                        </SettingsHeaderFilter>
                                        <EditFormSettings Caption="Choisir date d'entrée" />
                                    </dx:GridViewDataDateColumn>
                                    <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" Visible="False" VisibleIndex="5" Width="20%">
                                    </dx:GridViewCommandColumn>
                                </Columns>
                            </dx:ASPxGridView>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxRoundPanel>
                <br />
                <br />
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
