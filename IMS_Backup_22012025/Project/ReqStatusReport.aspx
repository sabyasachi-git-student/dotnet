<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Project.master" AutoEventWireup="true" CodeFile="ReqStatusReport.aspx.cs" Inherits="Project_ReqStatusReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="w3-container">
        <div class="w3-panel w3-card-4 w3-white">
            <div class="w3-blue w3-card-4">
                <h3 align="center">Requisition Status Report</h3>
            </div>
            <br />

            <div align="center">
                 <table>

                     <tr>
                        <td>
                            <asp:TextBox runat="server" BackColor="Red" Width="50px" Enabled="false"></asp:TextBox>
                        </td>
                        <td>Reject</td>
                        <td>
                            <asp:TextBox runat="server" BackColor="LightBlue" Width="50px" Enabled="false"></asp:TextBox>
                        </td>
                        <td>Initiate</td>
                        <td>
                            <asp:TextBox runat="server" BackColor="Orange" Width="50px" Enabled="false"></asp:TextBox>
                        </td>
                        <td>Work In Progress</td>
                         <td>
                            <asp:TextBox runat="server" BackColor="LightGreen" Width="50px" Enabled="false"></asp:TextBox>
                        </td>
                        <td>Work Done</td>
                    </tr>
                    </table>
                </div>
                <br />
             <div align="left">
                <table>
                    <tr>
                        <td style="font-weight: bold;">Requisition Id : <span style="color: red;">*</span>
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="ddlRequisitionId" runat="server"
                                DataSourceID="SqlDataSource2" TextField="ReqNOCId" AutoPostBack="true" OnTextChanged="ddlStockInId_TextChanged"
                                TextFormatString="{0} {1}" Theme="Office2003Blue" ValueField="ReqNOCId" Height="30px" Width="250px">
                                <%--<Columns>
                                    <dx:ListBoxColumn Caption="ReqPopId" FieldName="ReqPopId" />
                                    <dx:ListBoxColumn Caption="PO No" FieldName="POID" />
                                </Columns>--%>
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                                ConnectionString="<%$ ConnectionStrings:Profit %>"
                                SelectCommand="select ReqNOCId from tbl_RequisitionNOC union all select ReqProId from tbl_RequisitionProject">

                                <SelectParameters>
                                    <asp:SessionParameter SessionField="BranchId" Name="BranchId" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                        <td>

                        </td>
                        <td>
                             <asp:Button runat="server" ID="btnSave" Text="ViewDetails" CssClass="w3-btn w3-green" />
                        </td>
                    </tr>
                </table>
            </div>

            <br />
            <div align="left" style="overflow: auto; width: 1300px;">
                <table cellspacing="15px" align="center">

                    <tr>
                        <td>
                            <asp:TextBox runat="server" ID="TextBox1" Height="90px" Width="150px" Style="border-radius: 10px; text-align: center; border-color:black;" Enabled="false" Text="Requisition from NOC"></asp:TextBox>
                            <img style="height: 100px; width: 100px; margin-bottom: -40px;" src="../Images/arrow1.png" />
                            <asp:TextBox runat="server" ID="TextBox2" Height="90px" Width="200px" Style="border-radius: 10px; text-align: center; border-color:black;" Enabled="false" Text="Action taken by NOC in-charge"></asp:TextBox>
                            <img style="height: 100px; width: 630px; margin-bottom: -40px;" src="../Images/blue-lines.png" />
                            <img style="height: 180px; width: 100px; margin-bottom: -175px; margin-left: -78px;" src="../Images/arrow12.png" />
                        </td>
                    </tr>
                    
                        <tr>
                            <td>
                                <asp:TextBox runat="server" ID="TextBox4" Height="90px" Width="200px" Style="border-radius: 10px; text-align: center; border-color:black;" Enabled="false" Text="Requisition from Project Manager"></asp:TextBox>
                                <img style="height: 100px; width: 850px; margin-bottom: -40px;" src="../Images/blue-lines.png" />
                                <img style="height: 65px; width: 100px; margin-bottom: -60px; margin-left: -82px;" src="../Images/arrow12.png" />
                                <asp:TextBox runat="server" Visible="false" ID="TextBox8" Height="90px" Width="200px" Style="border-radius: 10px; text-align: center;" Text="Requisition from Project Manager"></asp:TextBox>
                                <img style="height: 100px; width: 250px; margin-bottom: -40px; visibility: hidden" src="../Images/arrow1.png" />
                                <img style="height: 100px; width: 100px; margin-bottom: -40px; visibility: hidden" src="../Images/arrow1.png" />
                                <asp:TextBox runat="server" Visible="false" ID="TextBox9" Height="90px" Width="200px" Style="border-radius: 10px; text-align: center;" Text="Requisition from Project Manager"></asp:TextBox>
                                <img style="height: 100px; width: 250px; margin-bottom: -40px; visibility: hidden" src="../Images/arrow1.png" />
                                <asp:TextBox runat="server" Visible="false" ID="TextBox10" Height="90px" Width="200px" Style="border-radius: 10px; text-align: center;" Text="Requisition from Project Manager"></asp:TextBox>

                            </td>
                        </tr>



                    <tr>
                        <td>
                            <asp:TextBox runat="server" ID="TextBox3" Height="90px" Width="200px" Style="border-radius: 10px; text-align: center; border-color:black;" Enabled="false" Text="Requisition from PoP EF"></asp:TextBox>
                            <img style="height: 100px; width: 100px; margin-bottom: -40px;" src="../Images/arrow1.png" />
                            <asp:TextBox runat="server" ID="TextBox5" Height="90px" Width="200px" Style="border-radius: 10px; text-align: center; resize: none; padding-top: 30px; margin-bottom: -35px; border-width: 2px; border-color: black;" TextMode="MultiLine" Enabled="false" Text="Requisition from Section In-charge or action taken by Section In-charge"></asp:TextBox>
                            <img style="height: 100px; width: 100px; margin-bottom: -40px;" src="../Images/arrow1.png" />
                            <asp:TextBox runat="server" ID="TextBox6" Height="90px" Width="200px" Style="border-radius: 10px; text-align: center; resize: none; padding-top: 30px; margin-bottom: -35px; border-width: 2px; border-color: black;" TextMode="MultiLine" Enabled="false" Text="Requisition from Section TM or action taken by TM"></asp:TextBox>
                            <img style="height: 100px; width: 100px; margin-bottom: -40px;" src="../Images/arrow1.png" />
                            <asp:TextBox runat="server" ID="TextBox7" Height="90px" Width="200px" Style="border-radius: 10px; text-align: center; resize: none; padding-top: 30px; margin-bottom: -35px; border-width: 2px; border-color: black;" TextMode="MultiLine" Enabled="false" Text="Requisition received and action taken by Regional Store"></asp:TextBox>
                             <img style="height: 100px; width: 100px; margin-bottom: -135px; margin-left: -108px;" src="../Images/arrow12.png" />
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:TextBox runat="server" ID="TextBox11" Height="200px" Width="180px" Style="text-align: center; border-width: 0;"></asp:TextBox>
                            <asp:TextBox runat="server" ID="TextBox12" Height="200px" Width="180px" Style="text-align: center; border-width: 0;"></asp:TextBox>
                            <img style="height: 130px; width: 100px;" src="../Images/blue-lines12.png" />
                            <img style="height: 95px; width: 320px; margin-bottom: -43px; margin-left: -81px" src="../Images/blue-lines.png" />
                            <img style="height: 140px; width: 100px; margin-left: -56px;" src="../Images/blue-lines12.png" />
                            <img style="height: 95px; width: 300px; margin-bottom: -51px; margin-left: -64px" src="../Images/arrow1.png" />
                            <asp:TextBox runat="server" ID="TextBox14" Height="90px" Width="150px" Style="border-radius: 10px; text-align: center; resize: none; padding-top: 30px; margin-bottom: -35px; border-width: 2px; border-color: black;" TextMode="MultiLine" Enabled="false" Text="Material issued"></asp:TextBox>
                            <img style="height: 100px; width:100px; margin-bottom: -40px;" src="../Images/arrow1.png" />
                            <asp:TextBox runat="server" ID="TextBox13" Height="90px" Width="150px" Style="border-radius: 10px; text-align: center; border-color:black;" Enabled="false" Text="Material Shipped"></asp:TextBox>
                            <img style="height: 100px; width: 100px; margin-bottom: -40px;" src="../Images/arrow1.png" />
                            <asp:TextBox runat="server" ID="TextBox15" Height="90px" Width="150px" Style="border-radius: 10px; text-align: center; resize: none; padding-top: 30px; margin-bottom: -35px; border-width: 2px; border-color: black;" TextMode="MultiLine" Enabled="false" Text="Material Received by the requisitioner"></asp:TextBox>
                            <asp:TextBox runat="server" ID="TextBox16" Height="200px" Width="180px" Style="text-align: center; border-width: 0;"></asp:TextBox>

                        </td>
                    </tr>
                    
                </table>
            </div>

        </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

