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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;

namespace SellManagement.UC
{
    /// <summary>
    /// Interaction logic for UCListProduct.xaml
    /// </summary>
    public partial class UCListProduct : UserControl
    {
        public UCListProduct()
        {
            InitializeComponent();
            ACCDB = new CDatabase();

            LOAD_ALL_PRODUCT();
        }

        CDatabase ACCDB;
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

                DTG_PRODUCTSET.ItemsSource = pRODUCTs;
            }
        }

        private void DTG_PRODUCTSETRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            View.WINInputProduct wINInputProduct = new View.WINInputProduct(ACCDB, ((sender as DataGridRow).DataContext as mPRODUCT).PRDCD);
            wINInputProduct.ShowDialog();
        }

        private void BTN_ADD_Click(object sender, RoutedEventArgs e)
        {
            //View.WINInputProduct wINInputProduct = new View.WINInputProduct(ACCDB);
            //wINInputProduct.ShowDialog();

            View.WINRecieveOrder wINRecieveOrder = new View.WINRecieveOrder(ACCDB);
            wINRecieveOrder.ShowDialog();
        }
    }
}
