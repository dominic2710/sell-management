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
using System.Text.RegularExpressions;

namespace SellManagement.View
{
    /// <summary>
    /// Interaction logic for WINEditProductSet.xaml
    /// </summary>
    public partial class WINEditProductSet : Window
    {
        public WINEditProductSet()
        {
            InitializeComponent();
        }

        public WINEditProductSet(mORDERITEM prmOrderItem, CDatabase prmACCDB)
        {
            InitializeComponent();

            //oRDERITEM = prmOrderItem;
            //.Clone();
            oRDERITEM = new mORDERITEM("1");
            ACCDB = prmACCDB;
        }

        CDatabase ACCDB;
        public mORDERITEM oRDERITEM;
        public bool dialogResult = false;

        void DERIVE_PURCHASEPRICE()
        {
            double WK_PURCHASEPRICE = 0;
            foreach (mPRODUCTSET lvItem in LST_PRODUCTSET.Items)
            {
                WK_PURCHASEPRICE += lvItem.PURCHASEPRICE * lvItem.QUANTITY;
            }

            (GRD_PRODUCTINFO.DataContext as mORDERITEM).PURCHASEPRICE = WK_PURCHASEPRICE;
            TXT_PURCHASEPRICE.Text = WK_PURCHASEPRICE.ToString();
        }
        void DERIVE_SALEPRICE()
        {
            double WK_SALEPRICE = 0;
            foreach (mPRODUCTSET lvItem in LST_PRODUCTSET.Items)
            {
                WK_SALEPRICE += lvItem.SALEPRICE * lvItem.QUANTITY;
            }

            (GRD_PRODUCTINFO.DataContext as mORDERITEM).SALEPRICE = WK_SALEPRICE;
            TXT_SALEPRICE.Text = WK_SALEPRICE.ToString();
        }
        void DERIVE_WEIGHT()
        {
            double WK_WEIGHT = 0;
            foreach (mPRODUCTSET lvItem in LST_PRODUCTSET.Items)
            {
                WK_WEIGHT += lvItem.WEIGHT * lvItem.QUANTITY;
            }

            (GRD_PRODUCTINFO.DataContext as mORDERITEM).WEIGHT = WK_WEIGHT;
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

        private void BTN_UPDATE_Click(object sender, RoutedEventArgs e)
        {
            dialogResult = true;
            this.Close();
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

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GRD_PRODUCTINFO.DataContext = oRDERITEM;
            LST_PRODUCTSET.ItemsSource = oRDERITEM.PRODUCTSETs;
        }

        private void BTN_CANCLE_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
