using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SellManagement.Model;
using System.Data;
using System.Globalization;

namespace SellManagement.View
{
    /// <summary>
    /// Interaction logic for WINRecieveOrder.xaml
    /// </summary>
    public partial class WINRecieveOrder : Window
    {
        public WINRecieveOrder(CDatabase prmACCDB)
        {
            InitializeComponent();

            ACCDB = prmACCDB;
        }

        CDatabase ACCDB;

        void DERIVE_PRICE(TextBox textBOX)
        {
            if (textBOX.Text.Trim() != "")
            {
                WrapPanel wrapPanel = textBOX.Parent as WrapPanel;
                (wrapPanel.Children[6] as TextBlock).Text = (int.Parse((wrapPanel.Children[5] as TextBlock).Text,System.Globalization.NumberStyles.AllowThousands) * int.Parse(textBOX.Text)).ToString();
            }
        }
        void DERIVE_WEIGHT(TextBox textBOX)
        {
            if (textBOX.Text.Trim() != "")
            {
                WrapPanel wrapPanel = textBOX.Parent as WrapPanel;
                (wrapPanel.Children[4] as TextBlock).Text = (int.Parse((wrapPanel.Children[7] as TextBlock).Text, System.Globalization.NumberStyles.AllowThousands) * int.Parse(textBOX.Text)).ToString();
            }
        }
        void DERIVE_TOTALWEIGHT()
        {
            double WK_TOTALWEIGHT = 0;
            foreach (mORDERITEM lvItem in LST_PRODUCT.Items)
            {
                WK_TOTALWEIGHT += lvItem.WEIGHT * lvItem.QUANTITY; 
            }

            TXB_TOTALWEIGHT.Text = WK_TOTALWEIGHT.ToString("#,###,###;;#0", CultureInfo.InvariantCulture);
        }
        void DERIVE_TOTALQUANTITY()
        {
            double WK_TOTALQUANTITY = 0;
            foreach (mORDERITEM lvItem in LST_PRODUCT.Items)
            {
                WK_TOTALQUANTITY += lvItem.QUANTITY;
            }

            TXB_TOTALQUANTITY.Text = WK_TOTALQUANTITY.ToString("#,###,###;;#0", CultureInfo.InvariantCulture);
        }
        void DERIVE_TOTALPRICE()
        {
            double WK_TOTALPRICE = 0;
            foreach (mORDERITEM lvItem in LST_PRODUCT.Items)
            {
                WK_TOTALPRICE += lvItem.SALEPRICE * lvItem.QUANTITY; ;
            }

            TXB_TOTALPRICE.Text = WK_TOTALPRICE.ToString("#,###,###;;#0", CultureInfo.InvariantCulture);
        }

        void CHECK_PRDCD(TextBox textBox)
        {
            WrapPanel wrapPanel = textBox.Parent as WrapPanel;
            TextBlock textBlock = (wrapPanel.Parent as Grid).Children[1] as TextBlock;
            mORDERITEM oRDERITEM = wrapPanel.DataContext as mORDERITEM;

            string strCmd = string.Format("SELECT * FROM PRODUCT WHERE PRDCD = '{0}'", textBox.Text);
            DataTable DT = ACCDB.SELECT_DATA(strCmd);
            string strSetcm = "";

            if (DT.Rows.Count > 0)
            {
                DataRow dr = DT.Rows[0];

                oRDERITEM.CATEGORY = dr["CATEGORY"].ToString();
                oRDERITEM.FORM = dr["FORM"].ToString();
                oRDERITEM.UNTNM = dr["UNTNM"].ToString();
                oRDERITEM.PURCHASEPRICE =int.Parse( dr["PURCHASE_PRICE"].ToString());

                (wrapPanel.Children[2] as TextBlock).Text = dr["PRDNM"].ToString();
                (wrapPanel.Children[3] as TextBox).Text = "1";
                (wrapPanel.Children[4] as TextBlock).Text = dr["WEIGHT"].ToString();
                (wrapPanel.Children[5] as TextBlock).Text = dr["SALE_PRICE"].ToString();
                (wrapPanel.Children[6] as TextBlock).Text = dr["SALE_PRICE"].ToString();
                (wrapPanel.Children[7] as TextBlock).Text = dr["WEIGHT"].ToString();

                strCmd = string.Format("SELECT * FROM PRODUCT_SET WHERE PRDCD = '{0}' ORDER BY [NO]", textBox.Text);
                DT = ACCDB.SELECT_DATA(strCmd);
                if (DT.Rows.Count > 0)
                {
                    foreach (DataRow drw in DT.Rows)
                    {
                        strSetcm = strSetcm + drw["CHIL_PRDCD"].ToString();

                        strCmd = string.Format("SELECT * FROM PRODUCT WHERE PRDCD = '{0}'", drw["CHIL_PRDCD"].ToString());
                        DataTable dtw = ACCDB.SELECT_DATA(strCmd);
                        DataRow drw_2;
                        if (dtw.Rows.Count > 0)
                        {
                            strSetcm = strSetcm + " - " + dtw.Rows[0]["PRDNM"].ToString() + "; ";

                            drw_2 = dtw.Rows[0];
                            oRDERITEM.PRODUCTSETs.Add(new mPRODUCTSET(drw["NO"].ToString(), drw["CHIL_PRDCD"].ToString(), drw_2["PRDNM"].ToString(), double.Parse(drw["QUANTITY"].ToString()),
                                                                    double.Parse(drw_2["WEIGHT"].ToString()), double.Parse(drw_2["PURCHASE_PRICE"].ToString()), double.Parse(drw_2["SALE_PRICE"].ToString())));

                        }
                        else
                        {
                            oRDERITEM.PRODUCTSETs.Add(new mPRODUCTSET(drw["NO"].ToString()));
                        }
                    }
                    if (strSetcm.Length <= 5)
                        strSetcm = "Chưa chọn sản phẩm";
                    else
                        strSetcm = strSetcm.Substring(0, strSetcm.Length - 2);
                }
                textBlock.Text = strSetcm;
            }
            else
            {
                (wrapPanel.Children[2] as TextBlock).Text = "";
                (wrapPanel.Children[3] as TextBox).Text = "0";
                (wrapPanel.Children[4] as TextBlock).Text = "0";
                (wrapPanel.Children[5] as TextBlock).Text = "0";
                (wrapPanel.Children[6] as TextBlock).Text = "0";
                textBlock.Text = "";
            }

            DERIVE_TOTALWEIGHT();
            DERIVE_TOTALQUANTITY();
            DERIVE_TOTALPRICE();
        }

        private void TXT_ORDERNO_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TXT_QUANTITY_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void TXT_QUANTITY_TextChanged(object sender, TextChangedEventArgs e)
        {
            DERIVE_PRICE(sender as TextBox);
            DERIVE_WEIGHT(sender as TextBox);
            DERIVE_TOTALWEIGHT();
            DERIVE_TOTALQUANTITY();
            DERIVE_TOTALPRICE();           
        }

        private void TXT_QUANTITY_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mORDERDETAIL orderDetail = new mORDERDETAIL("0001", "001", "Nguyễn Văn A", "01687726348", "hgahkdashdgsadkjqwegwhdgsahdgsahdgjsadgsad", new List<mORDERITEM>());

            orderDetail.ORDERITEMS.Add(new mORDERITEM("1"));
            orderDetail.ORDERITEMS.Add(new mORDERITEM("2"));
            orderDetail.ORDERITEMS.Add(new mORDERITEM("3"));
            orderDetail.ORDERITEMS.Add(new mORDERITEM("4"));
            orderDetail.ORDERITEMS.Add(new mORDERITEM("5"));

            GRD_ORDERDETAIL.DataContext = orderDetail;
            LST_PRODUCT.ItemsSource = orderDetail.ORDERITEMS;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }

        private void TXT_PRDCD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CHECK_PRDCD(sender as TextBox);
            }
            else if (e.Key == Key.F5)
            {
                WINListProduct wINListProduct = new WINListProduct(ACCDB);
                wINListProduct.ShowDialog();

                if (wINListProduct.ListDialogResult)
                {
                    (sender as TextBox).Text = wINListProduct.SELECTED_PRDCD;
                    CHECK_PRDCD(sender as TextBox);
                }
            }
        }

        private void BTN_UPDATE_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTN_DELETE_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTN_CLEAR_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LST_PRODUCTRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            mORDERITEM oRDERITEM = (sender as ListViewItem).DataContext as mORDERITEM;

            WINEditProductSet wINEditProductSet = new WINEditProductSet(oRDERITEM, ACCDB);
            wINEditProductSet.ShowDialog();

            if (wINEditProductSet.dialogResult)
            {
                oRDERITEM = wINEditProductSet.oRDERITEM;
            }
        }
    }
}
