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

        public int deleteAccount(string ID)
        {
            return new DAL.AccountAccess().DeleteAccount(ID);
        }

        public int UpdateAccountName(string id, string name)
        {
            return new DAL.AccountAccess().UpdateAccountName(id, name);
        }
        public int UpdateAccountPhone(string id, string phone)
        {
            return new DAL.AccountAccess().UpdateAccountPhone(id, phone);
        }
        public int UpdateAccountBirth(string id, DateTime birth)
        {
            return new DAL.AccountAccess().UpdateAccountBirth(id, birth);
        }

        public int UpdateAccountEmail(string id, string email)
        {
            return new DAL.AccountAccess().UpdateAccountEmail(id, email);
        }
        public int UpdateAccountPassword(string id, string password)
        {
            return new DAL.AccountAccess().UpdateAccountPassword(id, password);
        }

    }
}
