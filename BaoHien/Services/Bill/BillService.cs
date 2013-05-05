using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BaoHien.Services.Base;
using BaoHien.Model;

namespace BaoHien.Services.Bills
{
    public class BillService : BaseService<Bill>
    {
        public Bill GetBill(System.Int32 id)
        {
            Bill bill = OnGetItem<Bill>(id.ToString());
            return bill;
        }

        public List<Bill> GetBills()
        {
            List<Bill> bills = OnGetItems<Bill>();
            return bills.OrderByDescending(x => x.CreatedDate).ToList();
        }

        public bool AddBill(Bill bill)
        {
            return OnAddItem<Bill>(bill);
        }

        public bool DeleteBill(System.Int32 id)
        {
            return OnDeleteItem<Bill>(id.ToString());
        }

        public bool UpdateBill(Bill bill)
        {
            return OnUpdateItem<Bill>(bill, bill.Id.ToString());
        }

        public List<Bill> SearchingBill(BillSearchCriteria billSearchCriteria)
        {
            List<Bill> bills = OnGetItems<Bill>();
            if (billSearchCriteria != null)
            {
                if (billSearchCriteria.CreatedBy.HasValue)
                {
                    bills = bills.Where(pr => pr.UserId == billSearchCriteria.CreatedBy.Value).ToList();
                }
                if (billSearchCriteria.CustId.HasValue)
                {
                    bills = bills.Where(pr => pr.CustId == billSearchCriteria.CustId.Value).ToList();
                }
                if (billSearchCriteria.Code != "")
                {
                    bills = bills.Where(pr => pr.BillCode.ToLower().Contains(billSearchCriteria.Code)).ToList();
                }
                if (billSearchCriteria.To.HasValue && billSearchCriteria.From.HasValue)
                {
                    bills = bills.
                        Where(pr => pr.CreatedDate.CompareTo(billSearchCriteria.From.Value) >= 0
                            && pr.CreatedDate.CompareTo(billSearchCriteria.To.Value) <= 0)
                            .ToList();
                }
            }

            return bills;
        }
    }
}
