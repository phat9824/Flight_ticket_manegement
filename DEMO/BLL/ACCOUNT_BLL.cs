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


        public List<ACCOUNT> List_acc(ACCOUNT dto)
        {
            return new DAL.AccountAccess().GetMember(dto);
        }

    }
}
