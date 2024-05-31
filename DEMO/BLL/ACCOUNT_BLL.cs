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

        public bool AuthenticateAccount(string email, string password, out int permissionID)
        {
            AccountAccess accountDAL = new AccountAccess();

            if (accountDAL.CheckAccountExists(email))
            {
                permissionID = accountDAL.GetPermissionID(email, password);
                return permissionID != 0;
            }
            else
            {
                permissionID = 0;
                return false;
            }
        }
    }
}
