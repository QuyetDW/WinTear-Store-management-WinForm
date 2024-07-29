using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnChuyenNghanh1.DTO
{
    public class Account
    {
        public Account(string useName, string displayName, int type, string password = null) 
        { 
            this.UseName = useName;
            this.DisplayName = displayName;
            this.Type = type;
            this.Password = password;
        }

        public Account(DataRow row)
        {
            this.UseName = row["UserName"].ToString();
            this.DisplayName = row["DisplayName"].ToString();
            this.Type = (int)row["Type"];
            this.Password = row["Password"].ToString();
        }

        private int type;
        private string password;
        private string displayName;
        private string useName;

        public string UseName { get => useName; set => useName = value; }
        public string DisplayName { get => displayName; set => displayName = value; }
        public string Password { get => password; set => password = value; }
        public int Type { get => type; set => type = value; }
    }
}
