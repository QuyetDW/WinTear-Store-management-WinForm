using DoAnChuyenNghanh1.DAO;
using DoAnChuyenNghanh1.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DoAnChuyenNghanh1
{
    public partial class frm_Admin : Form
    {
        BindingSource foodList = new BindingSource();
        BindingSource accountList = new BindingSource();

        public Account loginAccount;
        public frm_Admin()
        {
            InitializeComponent();
            Loadfrm();
        }
        void Loadfrm()
        {
            dgv_FoodDrink.DataSource = foodList;
            dgv_TaiKhoan.DataSource = accountList;

            LoadDateTimePickerBill();
            LoadListBillByDate(dtp_DateBegin.Value, dtp_DateEnd.Value);
            LoadListFood();
            LoadListCategory();
            LoadAccount();
            LoadCategoryIntoCbb(cbb_DanhMuc1);
            AddFoodBinding();
            AddAccountBinding();
            AddCategoryBinding();
        }
        void AddAccountBinding()
        {
            txt_TenTaiKhoan4.DataBindings.Add(new Binding("Text", dgv_TaiKhoan.DataSource, "UserName", true, DataSourceUpdateMode.Never));
            txt_TenHienThi4.DataBindings.Add(new Binding("Text", dgv_TaiKhoan.DataSource, "DisplayName", true, DataSourceUpdateMode.Never));
            nm_Type4.DataBindings.Add(new Binding("Value", dgv_TaiKhoan.DataSource, "Type", true, DataSourceUpdateMode.Never));
        }
        void LoadAccount()
        {
            accountList.DataSource = AccountDAO.Instance.GetlistAccount();
        }
        void LoadDateTimePickerBill()
        {
            DateTime today = DateTime.Now;
            dtp_DateBegin.Value = new DateTime(today.Year, today.Month, 1);
            dtp_DateEnd.Value = dtp_DateBegin.Value.AddMonths(1).AddDays(-1);
        }
        void AddCategoryBinding()
        {
            txt_TenDanhMuc2.DataBindings.Add(new Binding("Text", dgv_DanhMuc.DataSource, "name", true, DataSourceUpdateMode.Never));
            txt_ID2.DataBindings.Add(new Binding("Text", dgv_DanhMuc.DataSource, "id", true, DataSourceUpdateMode.Never));
        }
        void AddFoodBinding()
        {
            txt_TenMon1.DataBindings.Add(new Binding("Text", dgv_FoodDrink.DataSource, "name", true, DataSourceUpdateMode.Never));
            txt_ID1.DataBindings.Add(new Binding("Text", dgv_FoodDrink.DataSource, "id", true, DataSourceUpdateMode.Never));
            nm_Gia1.DataBindings.Add(new Binding("Value", dgv_FoodDrink.DataSource, "price", true, DataSourceUpdateMode.Never));
        }
        void LoadCategoryIntoCbb(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategory();
            cb.DisplayMember = "name";
        }
        private void frm_Admin_Load(object sender, EventArgs e)
        {

        }

        private void btn_Them3_Click(object sender, EventArgs e)
        {

        }

        private void btn_Xoa4_Click(object sender, EventArgs e)
        {
            string userName = txt_TenTaiKhoan4.Text;
            DeleteAccount(userName);
        }
        void LoadListBillByDate(DateTime checkIn, DateTime checkOut)
        {
            dgv_DoanhThu.DataSource = BillDAO.Instance.GetBillListByDate(checkIn, checkOut);
        }
        private void btn_ThongKe_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtp_DateBegin.Value, dtp_DateEnd.Value);
        }
        void LoadListFood()
        {
            foodList.DataSource = FoodDAO.Instance.GetListFood();
        }
        private void btn_Xem1_Click(object sender, EventArgs e)
        {
            LoadListFood();
        }

        private void txt_ID1_TextChanged(object sender, EventArgs e)
        {
            if (dgv_FoodDrink.SelectedCells.Count > 0)
            {
                int id = (int)dgv_FoodDrink.SelectedCells[0].OwningRow.Cells["categoryID"].Value;
                Category category = CategoryDAO.Instance.GetCategory(id);
                cbb_DanhMuc1.SelectedItem = category;
                int index = -1;
                int i = 0;

                foreach (Category item in cbb_DanhMuc1.Items)
                {
                    if (item.ID == category.ID)
                    {
                        index = i;
                        break;
                    }
                    i++;
                }
                cbb_DanhMuc1.SelectedIndex = index;
            }
        }

        private void btn_Them1_Click(object sender, EventArgs e)
        {
            string name = txt_TenMon1.Text;
            int categoryID = (cbb_DanhMuc1.SelectedItem as Category).ID;
            float price = (float)nm_Gia1.Value;

            if(FoodDAO.Instance.InsertFood(name, categoryID, price))
            {
                MessageBox.Show("Thêm món thành công !!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadListFood();
                if (insertFood != null)
                    insertFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Lỗi hệ thống !!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Sua1_Click(object sender, EventArgs e)
        {
            string name = txt_TenMon1.Text;
            int categoryID = (cbb_DanhMuc1.SelectedItem as Category).ID;
            float price = (float)nm_Gia1.Value;
            int id = Convert.ToInt32(txt_ID1.Text);

            if (FoodDAO.Instance.UpdateFood(id, name, categoryID, price))
            {
                MessageBox.Show("Sửa món thành công !!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadListFood();
                if (updateFood != null)
                    updateFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Lỗi hệ thống !!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Xoa1_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txt_ID1.Text);

            if (FoodDAO.Instance.DeleteFood(id))
            {
                MessageBox.Show("Xóa món thành công !!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadListFood();
                if (deleteFood != null)
                    deleteFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Lỗi hệ thống !!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private event EventHandler insertFood;
        public event EventHandler InsertFood
        {
            add { insertFood += value; }
            remove { insertFood -= value; }
        }
        private event EventHandler deleteFood;
        public event EventHandler DeleteFood
        {
            add { deleteFood += value; }
            remove { deleteFood -= value; }
        }
        private event EventHandler updateFood;
        public event EventHandler UpdateFood
        {
            add { updateFood += value; }
            remove { updateFood -= value; }
        }
        List<Food> SearchFoodByName(string name)
        {
            List<Food> listFood = FoodDAO.Instance.SearchFoodByName(name);

            return listFood;
        }
        private void btn_TimKiem1_Click(object sender, EventArgs e)
        {
            foodList.DataSource = SearchFoodByName(txt_TimKiem1.Text);
        }
        void AddAccount(string username, string displayname, int type)
        {
            if(AccountDAO.Instance.InsertAccount(username, displayname, type))
            {
                MessageBox.Show("Thêm tài khoản thành công !!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi hệ thống !!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadAccount();
        }
        void EditAccount(string username, string displayname, int type)
        {
            if (AccountDAO.Instance.UpdateAccount2(username, displayname, type))
            {
                MessageBox.Show("Cập nhập tài khoản thành công !!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi hệ thống !!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadAccount();
        }
        void DeleteAccount(string username)
        {
            if (loginAccount.UseName.Equals(username))
            {
                MessageBox.Show("Không thể xóa tài khoản đang đăng nhập !!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (AccountDAO.Instance.DeleteAccount(username))
            {
                MessageBox.Show("Xóa tài khoản thành công !!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi hệ thống !!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadAccount();
        }
        private void btn_Xem4_Click(object sender, EventArgs e)
        {

        }

        private void btn_Them4_Click(object sender, EventArgs e)
        {
            string userName = txt_TenTaiKhoan4.Text;
            string displayName = txt_TenHienThi4.Text;
            int type = (int)nm_Type4.Value;

            AddAccount(userName, displayName, type);
        }

        private void btn_Sua4_Click(object sender, EventArgs e)
        {
            string userName = txt_TenTaiKhoan4.Text;
            string displayName = txt_TenHienThi4.Text;
            int type = (int)nm_Type4.Value;

            EditAccount(userName, displayName, type);
        }
        void ResetPass(string userName)
        {
            if (AccountDAO.Instance.ResetPassword(userName))
            {
                MessageBox.Show("Đặt lại mật khẩu thành công !!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi hệ thống !!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btn_DatLaiMK_Click(object sender, EventArgs e)
        {
            string userName = txt_TenTaiKhoan4.Text;
            ResetPass(userName);
        }
        void LoadListCategory()
        {
            dgv_DanhMuc.DataSource = CategoryDAO.Instance.GetListCategory();
        }
        void AddFoodCategory(string name)
        {
            if (CategoryDAO.Instance.InsertCategory(name))
            {
                MessageBox.Show("Thêm danh mục thành công !!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi hệ thống !!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadListCategory();
        }
        void EditFoodCategory(int id, string name)
        {
            if (CategoryDAO.Instance.UpdateCategory(id, name))
            {
                MessageBox.Show("Sửa danh mục thành công !!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi hệ thống !!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadListCategory();
        }
        void DeleteFoodCategory(int id)
        {
            if (CategoryDAO.Instance.DeleteCategory(id))
            {
                MessageBox.Show("Xóa danh mục thành công !!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi hệ thống !!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadListCategory();
        }
        private void btn_Xem2_Click(object sender, EventArgs e)
        {
            LoadListCategory();
        }

        private void btn_Them2_Click(object sender, EventArgs e)
        {
            string name = txt_TenDanhMuc2.Text;
            AddFoodCategory(name);
        }

        private void btn_Sua2_Click(object sender, EventArgs e)
        {
            string name = txt_TenDanhMuc2.Text;
            int id = Convert.ToInt32(txt_ID2.Text);
            EditFoodCategory(id, name);
        }

        private void btn_Xoa2_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txt_ID2.Text);
            DeleteFoodCategory(id);
        }

        private void btn_LamMoi_Click(object sender, EventArgs e)
        {
            txt_ID1.Text = "";
            txt_TenMon1.Text = "";
            cbb_DanhMuc1.Text = "";
            nm_Gia1.Value = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txt_ID2.Text = "";
            txt_TenDanhMuc2.Text = "";
        }

        private void btn_LamMoi2_Click(object sender, EventArgs e)
        {
            txt_TenTaiKhoan4.Text = "";
            txt_TenHienThi4.Text = "";
            nm_Type4.Value = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
