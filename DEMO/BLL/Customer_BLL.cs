using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Customer_BLL
    {
        public string Get_ID()
        {
            return new DAL.CustomerAsccess().AutoID();
        }
        public string Add_Customer(List<CustomerDTO> customer)
        {
            string kq = "";
            foreach (CustomerDTO dto in customer)
            {
                kq = new DAL.CustomerAsccess().Add_Customer(dto);
            }
            return kq;
        }
    }
}
