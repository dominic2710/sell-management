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
using System.Data;
using SellManagement.Model;
using System.Text.RegularExpressions;

namespace SellManagement.View
{
    /// <summary>
    /// Interaction logic for WINInputProduct.xaml
    /// </summary>
    public partial class WINInputProduct : Window
    {
        public WINInputProduct(CDatabase prmACCDB)
        {
            InitializeComponent();

            ACCDB = prmACCDB;
            TXT_PRDCD.Focus();

            GRD_PRODUCTINFO.DataContext = new mPRODUCT("");
        }

        public WINInputProduct(CDatabase prmACCDB, string prmPRDCD)
        {
            InitializeComponent();

            ACCDB = prmACCDB;
            TXT_PRDCD.Text = prmPRDCD;
            TXT_PRDCD.Focus();
        }

        CDatabase ACCDB;
        string WINDOW_MODE = "NEW";
        bool NotFirst = false;

        void CHECK_PRDCD()
        {
            if (TXT_PRDCD.Text.Trim() == "")
                return;

            string strCmd = string.Format("SELECT * FROM PRODUCT WHERE PRDCD = '{0}'", TXT_PRDCD.Text);
            DataTable DT = ACCDB.SELECT_DATA(strCmd);
            mPRODUCT pRODUCT;

            if (DT.Rows.Count > 0)
            {
                DataRow dr = DT.Rows[0];
                pRODUCT = new mPRODUCT(dr["PRDCD"].ToString(), dr["PRDNM"].ToString(), dr["UNTNM"].ToString(), dr["FORM"].ToString(), dr["CATEGORY"].ToString(),
                                        dr["STATUS"].ToString(), dr["NOTE"].ToString(), double.Parse(dr["PURCHASE_PRICE"].ToString()), double.Parse(dr["SALE_PRICE"].ToString()),
                                        double.Parse(dr["INVENTORY"].ToString()), double.Parse(dr["EXPIRYDAY"].ToString()),
                                        double.Parse(dr["WEIGHT"].ToString()));

                strCmd = string.Format("SELECT * FROM PRODUCT_SET WHERE PRDCD = '{0}' ORDER BY [NO]", TXT_PRDCD.Text);
                DT = ACCDB.SELECT_DATA(strCmd);
                if (DT.Rows.Count > 0)
                {
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        dr = DT.Rows[i];

                        strCmd = string.Format("SELECT * FROM PRODUCT WHERE PRDCD = '{0}'", dr["CHIL_PRDCD"].ToString());
                        DataTable dtw = ACCDB.SELECT_DATA(strCmd);
                        DataRow drw;

                        if (dtw.Rows.Count > 0)
                        {
                            drw = dtw.Rows[0];
                            pRODUCT.PRODUCTSETs.Add(new mPRODUCTSET(dr["NO"].ToString(), dr["CHIL_PRDCD"].ToString(), drw["PRDNM"].ToString(), double.Parse(dr["QUANTITY"].ToString()),
                                                                    double.Parse(drw["WEIGHT"].ToString()), double.Parse(drw["PURCHASE_PRICE"].ToString()), double.Parse(drw["SALE_PRICE"].ToString())));
                        }
                        else
                        {
                            pRODUCT.PRODUCTSETs.Add(new mPRODUCTSET(dr["NO"].ToString(), dr["CHIL_PRDCD"].ToString(), "", double.Parse(dr["QUANTITY"].ToString()), 0, 0, 0));
                        }
                    }
                }

                TXB_STATUS.Text = "";
            }
            else
            {
                pRODUCT = new mPRODUCT(TXT_PRDCD.Text);
                //for (int i = 1; i < 7; i++)
                //{
                //    pRODUCT.PRODUCTSETs.Add(new mPRODUCTSET(i.ToString()));
                //}
                TXB_STATUS.Text = "Sản phẩm mới";
            }

            GRD_PRODUCTINFO.DataContext = pRODUCT;
            LST_PRODUCTSET.ItemsSource = pRODUCT.PRODUCTSETs;

            if (pRODUCT.FORM =="Bánh lẻ")
            {
                LST_PRODUCTSET.IsEnabled = false;
                TXT_WEIGHT.IsEnabled = true;
            }
            else
            {
                LST_PRODUCTSET.IsEnabled = true;
                TXT_WEIGHT.IsEnabled = false;
            }

            BTN_UPDATE.IsEnabled = true;
            BTN_DELETE.IsEnabled = true;

            WINDOW_MODE = "UPDATE";
        }
        void DERIVE_PURCHASEPRICE()
        {
            double WK_PURCHASEPRICE = 0;
            foreach (mPRODUCTSET lvItem in LST_PRODUCTSET.Items)
            {
                WK_PURCHASEPRICE += lvItem.PURCHASEPRICE * lvItem.QUANTITY;
            }

            (GRD_PRODUCTINFO.DataContext as mPRODUCT).PURCHASEPRICE = WK_PURCHASEPRICE;
            TXT_PURCHASEPRICE.Text = WK_PURCHASEPRICE.ToString();
        }
        void DERIVE_SALEPRICE()
        {
            double WK_SALEPRICE = 0;
            foreach (mPRODUCTSET lvItem in LST_PRODUCTSET.Items)
            {
                WK_SALEPRICE += lvItem.SALEPRICE * lvItem.QUANTITY;
            }

            (GRD_PRODUCTINFO.DataContext as mPRODUCT).SALEPRICE = WK_SALEPRICE;
            TXT_SALEPRICE.Text = WK_SALEPRICE.ToString();
        }
        void DERIVE_WEIGHT()
        {
            double WK_WEIGHT = 0;
            foreach (mPRODUCTSET lvItem in LST_PRODUCTSET.Items)
            {
                WK_WEIGHT += lvItem.WEIGHT * lvItem.QUANTITY;
            }

            (GRD_PRODUCTINFO.DataContext as mPRODUCT).WEIGHT = WK_WEIGHT;
            TXT_WEIGHT.Text = WK_WEIGHT.ToString();
        }
        void CHECK_CHILPRDCD(TextBox textBox)
        {
            WrapPanel wrapPanel = textBox.Parent as WrapPanel;

            string strCmd = string.Format("SELECT * FROM PRODUCT WHERE PRDCD = '{0}'", textBox.Text);
            DataTable DT = ACCDB.SELECT_DATA(strCmd);

            if (DT.Rows.Count > 0)
            {
                DataRow dr = DT.Rows[0];

                (wrapPanel.Children[2] as TextBlock).Text = dr["PRDNM"].ToString();
                (wrapPanel.Children[3] as TextBox).Text = "1";
                (wrapPanel.Children[4] as TextBlock).Text = dr["WEIGHT"].ToString();
                (wrapPanel.Children[5] as TextBlock).Text = dr["PURCHASE_PRICE"].ToString();
                (wrapPanel.Children[6] as TextBlock).Text = dr["SALE_PRICE"].ToString();
            }
            else
            {
                (wrapPanel.Children[2] as TextBlock).Text = "";
                (wrapPanel.Children[3] as TextBox).Text = "0";
                (wrapPanel.Children[4] as TextBlock).Text = "0";
                (wrapPanel.Children[5] as TextBlock).Text = "0";
                (wrapPanel.Children[6] as TextBlock).Text = "0";
            }

            DERIVE_WEIGHT();
            DERIVE_PURCHASEPRICE();
            DERIVE_SALEPRICE();
        }

        private void TXT_PRDCD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter )
            {
                CHECK_PRDCD();
            }
            else if (e.Key == Key.F5)
            {
                WINListProduct wINListProduct = new WINListProduct(ACCDB);
                wINListProduct.ShowDialog();

                if (wINListProduct.ListDialogResult)
                {
                    TXT_PRDCD.Text = wINListProduct.SELECTED_PRDCD;
                    CHECK_PRDCD();
                }
            }
        }

        private void BTN_UPDATE_Click(object sender, RoutedEventArgs e)
        {
            if (WINDOW_MODE == "")
                return;

            mPRODUCT pRODUCT = GRD_PRODUCTINFO.DataContext as mPRODUCT;
            List<mPRODUCTSET> pRODUCTSETs = LST_PRODUCTSET.ItemsSource as List<mPRODUCTSET>;
            string strCmd = string.Format("SELECT * FROM PRODUCT WHERE PRDCD = '{0}'", pRODUCT.PRDCD);

            if (ACCDB.CHECK_DATA_EXISTED(strCmd))
            {
                strCmd = string.Format("UPDATE PRODUCT SET PRDNM = '{0}', " +
                                                            "UNTNM = '{1}', " +
                                                            "FORM = '{2}', " +
                                                            "CATEGORY = '{3}'," +
                                                            "STATUS = '{4}', " +
                                                            "PURCHASE_PRICE = {5}, " +
                                                            "SALE_PRICE = {6}, " +
                                                            "EXPIRYDAY = {7}, " +
                                                            "WEIGHT = {8}, " +
                                                            "[NOTE] = '{9}'" +
                                                        "WHERE PRDCD = '{10}'",
                                                            pRODUCT.PRDNM, pRODUCT.UNTNM, pRODUCT.FORM, pRODUCT.CATEGORY, pRODUCT.STATUS,
                                                            pRODUCT.PURCHASEPRICE, pRODUCT.SALEPRICE, pRODUCT.EXPIRYDAY, pRODUCT.WEIGHT, pRODUCT.NOTE, pRODUCT.PRDCD);

                if (!ACCDB.UPDATE_DATA(strCmd))
                    return;

                foreach (mPRODUCTSET pRODUCTSET in pRODUCTSETs)
                {
                    strCmd = string.Format("UPDATE PRODUCT_SET SET CHIL_PRDCD = '{0}', " +
                                                                    "QUANTITY = {1} " +
                                                                "WHERE PRDCD = '{2}' " +
                                                                    "AND [NO] = '{3}'", 
                                                                    pRODUCTSET.PRDCD, pRODUCTSET.QUANTITY, pRODUCT.PRDCD, pRODUCTSET.NO);
                    if (!ACCDB.UPDATE_DATA(strCmd))
                        return;
                }
            }
            else
            {
                strCmd = string.Format("INSERT INTO PRODUCT(PRDCD, PRDNM, WEIGHT, UNTNM, FORM, CATEGORY, STATUS, PURCHASE_PRICE, SALE_PRICE, SUPPLIERCD, EXPIRYDAY, INVENTORY, [NOTE]) " +
                                        "VALUES('{0}', '{1}', {2}, '{3}', '{4}', '{5}', '{6}', {7}, {8}, '{9}', {10}, {11}, '{12}')",
                                          pRODUCT.PRDCD, pRODUCT.PRDNM,pRODUCT.WEIGHT, pRODUCT.UNTNM, pRODUCT.FORM, pRODUCT.CATEGORY, pRODUCT.STATUS, pRODUCT.PURCHASEPRICE,
                                          pRODUCT.SALEPRICE, "",pRODUCT.EXPIRYDAY, pRODUCT.INVENTORY, pRODUCT.NOTE);

                if (!ACCDB.INSERT_DATA(strCmd))
                    return;

                foreach (mPRODUCTSET pRODUCTSET in pRODUCTSETs)
                {
                    strCmd = string.Format("INSERT INTO PRODUCT_SET(PRDCD, [NO], CHIL_PRDCD, QUANTITY) VALUES('{0}', '{1}', '{2}', {3})", pRODUCT.PRDCD, pRODUCTSET.NO, pRODUCTSET.PRDCD, pRODUCTSET.QUANTITY);
                    if (!ACCDB.INSERT_DATA(strCmd))
                        return;
                }
            }

            MessageBox.Show("Cập nhật thành công", "Thông tin sản phẩm", MessageBoxButton.OK, MessageBoxImage.Information);
            BTN_UPDATE.IsEnabled = false;
            BTN_DELETE.IsEnabled = false;

            pRODUCT = new mPRODUCT(TXT_PRDCD.Text);
            GRD_PRODUCTINFO.DataContext = pRODUCT;
            LST_PRODUCTSET.ItemsSource = null;
            TXT_PRDCD.Focus();

            WINDOW_MODE = "";
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }

        private void TXT_CHILPRDCD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CHECK_CHILPRDCD(sender as TextBox);
            }     
            else if (e.Key == Key.F5)
            {
                WINListProduct wINListProduct = new WINListProduct(ACCDB);
                wINListProduct.ShowDialog();

                if (wINListProduct.ListDialogResult)
                {
                    (sender as TextBox).Text = wINListProduct.SELECTED_PRDCD;
                    CHECK_CHILPRDCD(sender as TextBox);
                }
            }
        }

        private void BTN_DELETE_Click(object sender, RoutedEventArgs e)
        {
            if (WINDOW_MODE == "")
                return;

            string strCmd = string.Format("SELECT * FROM PRODUCT WHERE PRDCD = '{0}'", TXT_PRDCD.Text);

            if (ACCDB.CHECK_DATA_EXISTED(strCmd))
            {
                strCmd = string.Format("DELETE FROM PRODUCT WHERE PRDCD = '{0}'", TXT_PRDCD.Text);
                if (!ACCDB.DELETE_DATA(strCmd))
                    return;

                strCmd = string.Format("DELETE FROM PRODUCT_SET WHERE PRDCD = '{0}'", TXT_PRDCD.Text);
                if (!ACCDB.DELETE_DATA(strCmd))
                    return;

                MessageBox.Show("Xóa thành công", "Thông tin sản phẩm", MessageBoxButton.OK, MessageBoxImage.Information);

                BTN_UPDATE.IsEnabled = false;
                BTN_DELETE.IsEnabled = false;
                TXB_STATUS.Text = "";

                mPRODUCT pRODUCT = new mPRODUCT(TXT_PRDCD.Text);
                GRD_PRODUCTINFO.DataContext = pRODUCT;
                LST_PRODUCTSET.ItemsSource = null;
                TXT_PRDCD.Focus();

                WINDOW_MODE = "";

            }
        }

        private void CMB_FORM_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LST_PRODUCTSET == null)
                return;

            if (!NotFirst)
            {
                NotFirst = true;
                return;
            }

            if (CMB_FORM.SelectedIndex == 0)
            {
                LST_PRODUCTSET.IsEnabled = false;
                TXT_WEIGHT.IsEnabled = true;

                LST_PRODUCTSET.ItemsSource = null;
            }
            else
            {
                LST_PRODUCTSET.IsEnabled = true;
                TXT_WEIGHT.IsEnabled = false;

                List<mPRODUCTSET> pRODUCTSETs = new List<mPRODUCTSET>();
                for (int i = 1; i < 5; i++)
                {
                    pRODUCTSETs.Add(new mPRODUCTSET((i.ToString())));
                }
                LST_PRODUCTSET.ItemsSource = pRODUCTSETs;
            }
        }

        private void TXT_QUANTITY_TextChanged(object sender, TextChangedEventArgs e)
        {
            DERIVE_WEIGHT();
            DERIVE_PURCHASEPRICE();
            DERIVE_SALEPRICE();
        }

        private void TXT_QUANTITY_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        private void BTN_CLEAR_Click(object sender, RoutedEventArgs e)
        {
            mPRODUCT pRODUCT = new mPRODUCT("");
            GRD_PRODUCTINFO.DataContext = pRODUCT;
            LST_PRODUCTSET.ItemsSource = null;
            TXT_PRDCD.Focus();

            WINDOW_MODE = "";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CHECK_PRDCD();
        }
    }
}
