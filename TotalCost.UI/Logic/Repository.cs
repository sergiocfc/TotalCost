using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TotalCost.UI.Entity;
using TotalCost.UI.ViewModels;

namespace TotalCost.UI.Logic
{
    class Repository : IDataRepository
    {
        private Context c = new Context();

        public bool AddBill(Bill newBill)
        {
            if (c.Bills.Count() >= 4)
                return false;
            if (string.IsNullOrWhiteSpace(newBill.Name))
                throw new ArgumentNullException("Название счета не может быть null или состоять только из пустых символов.");
            if (c.Bills.SingleOrDefault(b => b.Name == newBill.Name) != null)
                throw new ArgumentException("Название счета должно быть уникальным.");
            if (newBill.Sum < 0)
                throw new ArgumentOutOfRangeException("Баланс нового счета не может быть отрицательным.");

            c.Bills.Add(newBill);
            c.SaveChanges();

            return true;
        }

        public void AddGroup(Group newGroup)
        {
            if (string.IsNullOrWhiteSpace(newGroup.Name))
                throw new ArgumentNullException("Название группы не может быть null или состоять только из пустых символов.");

            c.Groups.Add(newGroup);
        }

        public void AddLimit(Limit newLimit)
        {
            if (newLimit.StartDate > newLimit.EndDate)
                throw new ArgumentException("Дата начала не может быть позже даты завершения.");
            if (newLimit.Sum <= 0)
                throw new ArgumentException("Ограничение не может равнять нулю или быть отрицательным.");
            if (newLimit.Group == null)
                throw new NullReferenceException("Ограничение должно быть привязано к группе.");

            c.Limits.Add(newLimit);
        }

        public void AddPayment(Payment newPayment)
        {
            if (newPayment.Sum <= 0)
                throw new ArgumentException("Платеж не может быть равным нулю или быть отрицательным.");
            if (newPayment.Group == null)
                throw new NullReferenceException("Платеж должен быть привязан к группе.");
            if (newPayment.Bill == null)
                throw new NullReferenceException("Платеж должен быть привязан к счету.");

            c.Payments.Add(newPayment);
        }

        public void Dispose()
        {
            c.Dispose();
        }

        public double GetAccountBalance(Bill bill)
        {
            var groups = (from p in c.Payments
                          where p.Bill == bill
                          group p by p.Group.Type into g
                          select g);

            var balance = groups.Single(g => g.Key == GroupType.Income).Sum(g => g.Sum) -
                          groups.Single(g => g.Key == GroupType.Consumption).Sum(g => g.Sum); 

            return balance;
        }

        public List<StatByGroupViewModel> GetStatByGroups(TimeIntervalType timeInvtervalType)
        {
            throw new NotImplementedException();
        }

        public Dictionary<Group, double> GetSumByGroupsDuring()
        {
            throw new NotImplementedException();
        }

        public Dictionary<Group, double> GetSumByGroupsDuring(TimeIntervalType timeIntervalType)
        {
            throw new NotImplementedException();
        }

        public Dictionary<Group, double> GetSumByGroupsDuring(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public double GetSumExceed(Group group)
        {
            throw new NotImplementedException();
        }

        public double GetTotalSumOfGroup(Group group)
        {
            throw new NotImplementedException();
        }

        public void RemoveBill(Bill billToRemove, Bill saveBill)
        {
            throw new NotImplementedException();
        }

        public void RemoveGroup(Group groupToRemove, Group saveGroup)
        {
            throw new NotImplementedException();
        }

        public void RemoveLimit(Limit limit)
        {
            throw new NotImplementedException();
        }

        public void RemovePayment(Payment payment)
        {
            throw new NotImplementedException();
        }

        public void TransferMoney(Bill billFrom, Bill billTo, double sum)
        {
            throw new NotImplementedException();
        }
    }
}
