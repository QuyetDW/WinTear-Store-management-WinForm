using DoAnChuyenNghanh1.DAO;
using DoAnChuyenNghanh1.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnChuyenNghanh1
{
    public partial class frm_MainMenu : Form
    {
        private Account loginAccount;

        public Account LoginAccount 
        { 
            get { return loginAccount; }
            set { loginAccount = value; ChangeAccount(loginAccount.Type); }
        }

        public frm_MainMenu(Account acc)
        {
            InitializeComponent();
            
            this.LoginAccount = acc;

            LoadTable();
            LoadCategory();
            LoadComboboxTable(cbb_ChuyenBan);
        }
        void ChangeAccount(int type)
        {
            adminToolStripMenuItem.Enabled = type == 1;
            thôngTinTàiKhoảnToolStripMenuItem.Text += " (" + LoginAccount.DisplayName + ")";
        }
        void LoadCategory()
        {
            List<Category> listCategory = CategoryDAO.Instance.GetListCategory();
            comboBox1.DataSource = listCategory;
            comboBox1.DisplayMember = "Name";
        }

        void LoadFoodListByCategoryID(int id)
        {
            List<Food> listFood = FoodDAO.Instance.GetFoodByCategoryID(id);
            comboBox2.DataSource = listFood;
            comboBox2.DisplayMember = "Name";
        }


        void LoadTable()
        {
            flp_Table.Controls.Clear();

            List<Table> tablelist = TableDAO.Instance.LoadTableList();
            foreach (Table item in tablelist) 
            { 
                Button btn = new Button() { Width = TableDAO.TableWidth, Height = TableDAO.TableHeight};
                btn.Text = item.Name + Environment.NewLine + item.Status;
                btn.Click += Btn_Click;
                btn.Tag = item;

                if(item.Status == "Trống")
                {
                    btn.BackColor = Color.SteelBlue;
                }
                else
                {
                    btn.BackColor = Color.LightPink;
                }
                flp_Table.Controls.Add(btn);
            }
        }

        void ShowBill(int id)
        {
            listView1.Items.Clear();
            List<DoAnChuyenNghanh1.DTO.Menu> listBillInfo = MenuDAO.Instance.GetListMenuByTable(id);
            float totalPrice = 0;

            foreach (DoAnChuyenNghanh1.DTO.Menu item in listBillInfo)
            {
                ListViewItem listViewItem = new ListViewItem(item.FoodName.ToString());
                listViewItem.SubItems.Add(item.Count.ToString());
                listViewItem.SubItems.Add(item.Price.ToString());
                listViewItem.SubItems.Add(item.TotalPrice.ToString());
                totalPrice += item.TotalPrice;

                listView1.Items.Add(listViewItem);
            }
            CultureInfo culture = new CultureInfo("vi-VN");
            //Thread.CurrentThread.CurrentCulture = culture;
            txt_Total.Text = totalPrice.ToString("c", culture);
        }

        void LoadComboboxTable(ComboBox cd)
        {
            cbb_ChuyenBan.DataSource = TableDAO.Instance.LoadTableList();
            cbb_ChuyenBan.DisplayMember = "Name";
        }

        private void Btn_Click(object sender, EventArgs e)
        {

            int tableID = ((sender as Button).Tag as Table).ID;
            listView1.Tag = (sender as Button).Tag;

            ShowBill(tableID);
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_Account account = new frm_Account(LoginAccount);
            account.UpdateAccount += f_UpdateAccount;
            account.ShowDialog();
        }

        void f_UpdateAccount(object sender, AccountEvent e)
        {
            thôngTinTàiKhoảnToolStripMenuItem.Text = "Xin chào: (" + e.Acc.DisplayName + ")";
        }
        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_Admin admin = new frm_Admin();
            admin.loginAccount = LoginAccount;
            admin.InsertFood += f_InsertFood;
            admin.DeleteFood += f_DeleteFood;
            admin.UpdateFood += f_UpdateFood;
            this.Hide();
            admin.ShowDialog();
            this.Show();
        }

        private void f_UpdateFood(object sender, EventArgs e)
        {
            LoadFoodListByCategoryID((comboBox1.SelectedItem as Category).ID);
            if(listView1.Tag != null)
            ShowBill((listView1.Tag as Table).ID);
        }

        private void f_DeleteFood(object sender, EventArgs e)
        {
            LoadFoodListByCategoryID((comboBox1.SelectedItem as Category).ID);
            if (listView1.Tag != null)
                ShowBill((listView1.Tag as Table).ID);
            LoadTable();
        }

        private void f_InsertFood(object sender, EventArgs e)
        {
            LoadFoodListByCategoryID((comboBox1.SelectedItem as Category).ID);
            if (listView1.Tag != null)
                ShowBill((listView1.Tag as Table).ID);
        }

        private void frm_MainMenu_Load(object sender, EventArgs e)
        {

        }

        private void trợGiúpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help help = new Help();
            this.Hide();
            help.ShowDialog();
            this.Show();
        }

        private void txt_Total_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;

            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem == null)
                return;

            Category selected = cb.SelectedItem as Category;
            id = selected.ID;

            LoadFoodListByCategoryID(id);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btn_ThemMon_Click(object sender, EventArgs e)
        {
            Table table = listView1.Tag as Table;

            if (table == null)
            {
                MessageBox.Show("Hãy chọn bàn !!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int idBill = BillDAO.Instance.GetUncheckBillIDByTableID(table.ID);
            int foodID = (comboBox2.SelectedItem as Food).ID;
            int count = (int)nud_ThemMon.Value;

            if (idBill == -1)
            {
                BillDAO.Instance.InsertBill(table.ID);
                BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.GetMaxIDBill(), foodID, count);
            }
            else
            {
                BillInfoDAO.Instance.InsertBillInfo(idBill, foodID, count);
            }
            ShowBill(table.ID);

            LoadTable();
        }

        private void btn_ThanhToan_Click(object sender, EventArgs e)
        {
            Table table = listView1.Tag as Table;

            int idBill = BillDAO.Instance.GetUncheckBillIDByTableID(table.ID);
            int discount = (int)nud_GiamGia.Value;

            double total = Convert.ToDouble(txt_Total.Text.Split(',')[0].Replace(".", ""));
            double finalTotal = total - ((total / 100) * discount);

            if (idBill != -1)
            {
                if (MessageBox.Show(string.Format("Bạn có chắc muốn thanh toán cho {0}\n Tổng tiền: {1}\n Giảm giá: {2}%\n Thành tiền: {3} ", table.Name, total, discount, finalTotal),"Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                {
                    BillDAO.Instance.CheckOut(idBill, discount, (float)finalTotal);
                    ShowBill(table.ID);

                    LoadTable();
                }
            }
        }

        private void btn_ChuyenBan_Click(object sender, EventArgs e)
        {
            int id1 = (listView1.Tag as Table).ID;
            int id2 = (cbb_ChuyenBan.SelectedItem as Table).ID;

            if (MessageBox.Show(string.Format("Bạn có chắc muốn chuyển {0} qua bàn {1}", (listView1.Tag as Table).Name, (cbb_ChuyenBan.SelectedItem as Table).Name), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            TableDAO.Instance.SwitchTable(id1, id2);
            LoadTable();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_GiamGia_Click(object sender, EventArgs e)
        {

        }
    }
}
