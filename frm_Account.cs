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
    public partial class frm_Account : Form
    {
        private Account loginAccount;

        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAccount(loginAccount); }
        }
        public frm_Account(Account acc)
        {
            InitializeComponent();
            LoginAccount = acc;
        }

        void ChangeAccount(Account acc)
        {
            txt_UseName.Text = loginAccount.UseName;
            txt_DisplayName.Text = loginAccount.DisplayName;
        }
        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_Account_Load(object sender, EventArgs e)
        {

        }

        void UpdateAccountInfo()
        {
            string displayName = txt_DisplayName.Text;
            string password = txt_PassWord.Text;
            string newpassword = txt_NewPassword.Text;
            string inputPass = txt_InputPass.Text;
            string userName = txt_UseName.Text;

            if (!newpassword.Equals(inputPass))
            {
                MessageBox.Show("Vui lòng xác nhận lại mật khẩu đúng với mật khẩu mới !!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (AccountDAO.Instance.UpdateAccount(userName, displayName, password, newpassword))
                {
                    MessageBox.Show("Cập nhập thành công !!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (updateAccount != null)
                        updateAccount(this, new AccountEvent(AccountDAO.Instance.GetAccountByUserName(userName)));
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu !!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private event EventHandler<AccountEvent> updateAccount;
        public event EventHandler<AccountEvent> UpdateAccount
        {
            add { updateAccount += value; } 
            remove { updateAccount -= value; }
        }
        private void btn_CapNhap_Click(object sender, EventArgs e)
        {
            UpdateAccountInfo();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
    public class AccountEvent : EventArgs
    {
        private Account acc;

        public Account Acc
        {
            get { return acc; } 
            set { acc = value; }
        }
        public AccountEvent(Account acc)
        {
            this.Acc = acc;
        }
    }
}
