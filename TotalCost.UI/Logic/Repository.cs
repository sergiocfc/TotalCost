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

        private Dictionary<TimeIntervalType, Func<Payment, bool>> InIntervalDic = new Dictionary<TimeIntervalType, Func<Payment, bool>>()
        {
            {TimeIntervalType.Day, p => p.Date >= DateTime.Today },
            {TimeIntervalType.Week, (p) => {
                var now = DateTime.Now;
                return (p.Date >= new DateTime(now.Year, now.Month, now.Day) - new TimeSpan((int)now.DayOfWeek, 0, 0, 0));
            } },
            {TimeIntervalType.Month, (p) => {
                var now = DateTime.Now;
                return (p.Date >= new DateTime(now.Year, now.Month, 1));
            } },
            {TimeIntervalType.Year, (p) => p.Date.Year == DateTime.Now.Year },
            {TimeIntervalType.Custom, null }
        };

        public Dictionary<Group, double> GetSumByGroupsDuring(TimeIntervalType timeIntervalType, DateTime dateIn)
        {
            throw new NotImplementedException();
        }

        public List<StatByGroupViewModel> GetStatByGroups(TimeIntervalType tType)
        {
            if (tType == TimeIntervalType.Custom)
                throw new ArgumentException("Интервал не может быть custom");

            var query = from p in c.Payments
                        where InIntervalDic[tType](p)
                        group p by p.Group into stat
                        select new StatByGroupViewModel
                        {
                            Group = stat.Key,
                            Min = stat.Min(p => p.Sum),
                            Average = stat.Average(p => p.Sum),
                            Max = stat.Max(p => p.Sum)
                        };

            return query.ToList();
        }

        public Dictionary<Group, double> GetSumByGroupsDuring()
        {
            return GetSumByGroupsDuring(TimeIntervalType.Day);
        }

        public Dictionary<Group, double> GetSumByGroupsDuring(TimeIntervalType tType)
        {
            if (InIntervalDic[TimeIntervalType.Custom] == null)
                throw new ArgumentNullException("Тип интервала custom не определен");

            var query = from p in c.Payments
                        where InIntervalDic[tType](p)
                        group p.Sum by p.Group into g
                        select new
                        {
                            Group = g.Key,
                            Total = g.Sum(s => s)
                        };

            return query.ToDictionary(a => a.Group, a => a.Total);
        }

        public Dictionary<Group, double> GetSumByGroupsDuring(DateTime startDate, DateTime endDate)
        {
            InIntervalDic[TimeIntervalType.Custom] = (p) => (p.Date >= startDate && p.Date <= endDate);
            return GetSumByGroupsDuring(TimeIntervalType.Custom);
        }

        public double GetSumExceed(Group _group)
        {
            throw new NotImplementedException();
        }

        public void RemoveBill(Bill billToRemove, Bill saveBill)
        {
            if (c.Bills.Count() == 1)
                throw new ArgumentOutOfRangeException("Количество счетов не может быть равно 0");
            if (billToRemove == saveBill)
                throw new ArgumentException("Счета должны быть разными");

            // TODO: делать это через платеж.
            saveBill.Sum += billToRemove.Sum;
            c.Bills.Remove(billToRemove);
            c.SaveChanges();
        }

        public void RemoveGroup(Group groupToRemove, Group saveGroup)
        {
            if (groupToRemove == saveGroup)
                throw new ArgumentException("Группы должны быть разными");

            saveGroup.Payments.AddRange(groupToRemove.Payments);
            c.Groups.Remove(groupToRemove);
            c.SaveChanges();
        }

        public void RemoveLimit(Limit limit)
        {
            throw new NotImplementedException();
        }

        public void RemovePayment(Payment payment)
        {
            c.Payments.Remove(payment);
            c.SaveChanges();
        }

        public void TransferMoney(Bill billFrom, Bill billTo, double sum)
        {
            throw new NotImplementedException();
        }
    }
}
