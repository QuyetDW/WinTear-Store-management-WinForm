using DoAnChuyenNghanh1.DAO;
using DoAnChuyenNghanh1.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnChuyenNghanh1
{
    public partial class frm_Login : Form
    {
        
        public frm_Login()
        {
            InitializeComponent();
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bạn muốn thoát chương trình ?", "Thông báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            Application.Exit();
        }

        private void btn_DangNhap_Click(object sender, EventArgs e)
        {
            string userName = txt_UseName.Text;
            string passWord = txt_PassWord.Text;

            if (Login(userName, passWord))
            {
                Account loginAccount = AccountDAO.Instance.GetAccountByUserName(userName);
                frm_MainMenu menu = new frm_MainMenu(loginAccount);
                this.Hide();
                menu.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Sai tên Tài khoản hoặc Mật khẩu !!!", "Thông báo",
                    MessageBoxButtons.OK ,MessageBoxIcon.Error);
            }      
        }
        bool Login(string userName, string passWord)
        {
            return AccountDAO.Instance.Login(userName, passWord);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void frm_Login_Load(object sender, EventArgs e)
        {

        }
    }
}
