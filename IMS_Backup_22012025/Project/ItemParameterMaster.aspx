<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="ItemParameterMaster.aspx.cs" Inherits="Project_ItemParameterMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">Item Parameter Master</h3>
            </div>
            <br />

            <div align="center">
                <fieldset>
                    <legend style="font-size: large; color: red;">Make Master</legend>
                    <table cellspacing="15px" align="center">
                        <tr>
                            <td>Make :</td>
                            <td>
                                <asp:TextBox ID="txtMake" runat="server" Height="30px" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnMake" runat="server" Text="Save" OnClick="btnMake_Click" CssClass="w3-btn w3-blue" />
                            </td>
                        </tr>

                        <tr>
                            <td colspan="2" align="center">
                                <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Theme="Office2003Blue" OnRowCommand="ASPxGridView1_RowCommand" KeyFieldName="RowId">
                                    <Columns>

                                        <dx:GridViewDataTextColumn FieldName="Make" Caption="Make" ShowInCustomizationForm="True">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Caption="Edit" Width="0px">
                                            <DataItemTemplate>
                                                <asp:Button ID="btnMakeEdit" runat="server" CommandName="MakeEdit" Text="Edit" CssClass="w3-btn w3-orange" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <Settings ShowFilterRowMenuLikeItem="True" ShowGroupFooter="VisibleAlways" ShowHeaderFilterButton="True" ShowFilterBar="Auto" ShowFilterRow="True" ShowFilterRowMenu="True" />
                                </dx:ASPxGridView>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="select * from tbl_MakeMaster order by rowid desc"></asp:SqlDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </fieldset>
                <br />
                <fieldset>
                    <legend style="font-size: large; color: red;">Model Master</legend>
                    <table cellspacing="15px" align="center">
                        <tr>
                            <td>Model :</td>
                            <td>
                                <asp:TextBox ID="txtModel" runat="server" Height="30px" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnModel" runat="server" Text="Save" OnClick="btnModel_Click" CssClass="w3-btn w3-blue" />

                            </td>
                        </tr>

                        <tr>
                            <td colspan="2" align="center">
                                <dx:ASPxGridView ID="ASPxGridView2" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource2" OnRowCommand="ASPxGridView2_RowCommand" Theme="Office2003Blue" KeyFieldName="RowId">
                                    <Columns>

                                        <dx:GridViewDataTextColumn FieldName="Model" Caption="Model"  CellStyle-Wrap="True" ShowInCustomizationForm="True">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Caption="Edit" Width="0px">
                                            <DataItemTemplate>
                                                <asp:Button ID="btnModelEdit" runat="server" CommandName="ModelEdit" Text="Edit" CssClass="w3-btn w3-orange" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>

                                    </Columns>
                                    <Settings ShowFilterRowMenuLikeItem="True" ShowGroupFooter="VisibleAlways" ShowHeaderFilterButton="True" ShowFilterBar="Auto" ShowFilterRow="True" ShowFilterRowMenu="True" />
                                </dx:ASPxGridView>
                                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="select * from tbl_ModelMaster order by rowid desc"></asp:SqlDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </fieldset>

                <br />
                <fieldset>
                    <legend style="font-size: large; color: red;">Unit Master</legend>
                    <table cellspacing="15px" align="center">
                        <tr>
                            <td>Unit :</td>
                            <td>
                                <asp:TextBox ID="txtUnit" runat="server" Height="30px" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnUnit" runat="server" Text="Save" OnClick="btnUnit_Click" CssClass="w3-btn w3-blue" />

                            </td>
                        </tr>

                        <tr>
                            <td colspan="2" align="center">
                                <dx:ASPxGridView ID="ASPxGridView3" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource3" OnRowCommand="ASPxGridView3_RowCommand" Theme="Office2003Blue" KeyFieldName="RowId">
                                    <Columns>

                                        <dx:GridViewDataTextColumn FieldName="Unit" Caption="Unit" ShowInCustomizationForm="True">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Caption="Edit" Width="0px">
                                            <DataItemTemplate>
                                                <asp:Button ID="btnUnitEdit" runat="server" CommandName="UnitEdit" Text="Edit" CssClass="w3-btn w3-orange" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>

                                    </Columns>
                                    <Settings ShowFilterRowMenuLikeItem="True" ShowGroupFooter="VisibleAlways" ShowHeaderFilterButton="True" ShowFilterBar="Auto" ShowFilterRow="True" ShowFilterRowMenu="True" />
                                </dx:ASPxGridView>
                                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="select * from tbl_UnitMaster order by rowid desc"></asp:SqlDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </fieldset>
                <br />
                <fieldset>
                    <legend style="font-size: large; color: red;">Item Code Master</legend>
                    <table cellspacing="15px" align="center">
                        <tr>
                            <td>Item Code :</td>
                            <td>
                                <asp:TextBox ID="txtItemCode" runat="server" Height="30px" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnItemCode" runat="server" Text="Save" OnClick="btnItemCode_Click" CssClass="w3-btn w3-blue" />

                            </td>
                        </tr>

                        <tr>
                            <td colspan="2" align="center">
                                <dx:ASPxGridView ID="ASPxGridView4" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource4" Theme="Office2003Blue" OnRowCommand="ASPxGridView4_RowCommand" KeyFieldName="RowId">
                                    <Columns>

                                        <dx:GridViewDataTextColumn FieldName="ItemCode" Caption="Item Code" ShowInCustomizationForm="True">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Caption="Edit" Width="0px">
                                            <DataItemTemplate>
                                                <asp:Button ID="btnItemCodeEdit" runat="server" CommandName="ItemCodeEdit" Text="Edit" CssClass="w3-btn w3-orange" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>

                                    </Columns>
                                    <Settings ShowFilterRowMenuLikeItem="True" ShowGroupFooter="VisibleAlways" ShowHeaderFilterButton="True" ShowFilterBar="Auto" ShowFilterRow="True" ShowFilterRowMenu="True" />
                                </dx:ASPxGridView>
                                <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="select * from tbl_ItemCodeMaster order by rowid desc"></asp:SqlDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </fieldset>
                <br />
                <fieldset>
                    <legend style="font-size: large; color: red;">Power Supply Master</legend>
                    <table cellspacing="15px" align="center">
                        <tr>
                            <td>Power Supply :</td>
                            <td>
                                <asp:TextBox ID="txtPowerSupply" runat="server" Height="30px" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnPowerSupply" runat="server" Text="Save" OnClick="btnPowerSupply_Click" CssClass="w3-btn w3-blue" />

                            </td>
                        </tr>

                        <tr>
                            <td colspan="2" align="center">
                                <dx:ASPxGridView ID="ASPxGridView5" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource5" OnRowCommand="ASPxGridView5_RowCommand" Theme="Office2003Blue" KeyFieldName="RowId">
                                    <Columns>

                                        <dx:GridViewDataTextColumn FieldName="PowerSupply" Caption="Power Supply" ShowInCustomizationForm="True">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Caption="Edit" Width="0px">
                                            <DataItemTemplate>
                                                <asp:Button ID="btnPowerSupplyEdit" runat="server" CommandName="PowerSupplyEdit" Text="Edit" CssClass="w3-btn w3-orange" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>

                                    </Columns>
                                    <Settings ShowFilterRowMenuLikeItem="True" ShowGroupFooter="VisibleAlways" ShowHeaderFilterButton="True" ShowFilterBar="Auto" ShowFilterRow="True" ShowFilterRowMenu="True" />
                                </dx:ASPxGridView>
                                <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:Profit %>" SelectCommand="select * from tbl_PowerSupplyMaster order by rowid desc"></asp:SqlDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </fieldset>

            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

