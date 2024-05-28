using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
using System.Data.SqlClient;
using System.Data;
using System.Security.Principal;

namespace BLL
{

    public class ACCOUNT_BLL
    {
        AccountAccess accAccess = new AccountAccess();
        public string CheckLogic(ACCOUNT acc)
        {
            if (acc.Email == "")
            {
                return "required tk_email";
            }
            if (acc.Email == "")
            {
                return "required pass";
            }

            string info = accAccess.CheckLogic(acc);
            return info;

        }
        public void SignUp(ACCOUNT User, ref string kq)
        {
            AccountAccess accountAccess = new AccountAccess();
            kq = accountAccess.SignUp(User);
        }

    }
}
