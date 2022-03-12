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

namespace SellManagement.View
{
    /// <summary>
    /// Interaction logic for WINListProduct.xaml
    /// </summary>
    public partial class WINListProduct : Window
    {
        public WINListProduct()
        {
            InitializeComponent();
        }

        public WINListProduct(CDatabase prmACCDB)
        {
            InitializeComponent();

            ACCDB = prmACCDB;
            LOAD_ALL_PRODUCT();
        }

        CDatabase ACCDB;
        public bool ListDialogResult = false;
        public string SELECTED_PRDCD = "";

        void LOAD_ALL_PRODUCT()
        {
            List<mPRODUCT> pRODUCTs = new List<mPRODUCT>();
            string strCmd = string.Format("SELECT * FROM PRODUCT");

            DataTable dt = ACCDB.SELECT_DATA(strCmd);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    pRODUCTs.Add(new mPRODUCT(dr["PRDCD"].ToString(), dr["PRDNM"].ToString(), dr["UNTNM"].ToString(), dr["FORM"].ToString(), dr["CATEGORY"].ToString(),
                                            dr["STATUS"].ToString(), dr["NOTE"].ToString(), double.Parse(dr["PURCHASE_PRICE"].ToString()), double.Parse(dr["SALE_PRICE"].ToString()),
                                            double.Parse(dr["INVENTORY"].ToString()), double.Parse(dr["EXPIRYDAY"].ToString()),
                                            double.Parse(dr["WEIGHT"].ToString())));
                }

                LST_PRODUCT.ItemsSource = pRODUCTs;
                LST_PRODUCT.SelectedIndex = 0;
            }
            else
                MessageBox.Show("Không có data");
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListDialogResult = true;
            SELECTED_PRDCD = ((sender as ListViewItem).DataContext as mPRODUCT).PRDCD;

            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key ==Key.Escape)
            {
                ListDialogResult = false;
                this.Close();
            }
        }

        private void WrapPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
                DragMove();
        }
    }
}
