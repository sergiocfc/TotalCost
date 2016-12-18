using System;
using System.Collections.Generic;
using System.Linq;
using TotalCost.Lib.ViewModels;

namespace TotalCost.UI.Lib
{
    public class Repository : IDataRepository
    {
        private Context c = new Context();
        public Action<Payment> OnPaymentAdd { get; set; }
        public Action<Payment> OnPaymentRemove { get; set; }

        public bool AddBill(string name, BillType type, double sum)
        {
            if (c.Bills.Count() >= 4)
                return false;
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("Название счета не может быть null или состоять только из пустых символов.");
            if (c.Bills.SingleOrDefault(x => x.Name == name) != null)
                throw new ArgumentException("Название счета должно быть уникальным.");
            if (sum < 0)
                throw new ArgumentOutOfRangeException("Баланс нового счета не может быть отрицательным.");

            Bill b = new Bill
            {
                Name = name,
                Type = type
            };

            Payment p = new Payment
            {
                Sum = sum,
                Date = DateTime.Now,
                Bill = b,
                Group = c.Groups.Single(g => g.Name == "Base"),
                Note = "Начальная сумма на счете"
            };

            c.Payments.Add(p);
            c.Bills.Add(b);

            c.SaveChanges();

            return true;
        }

        public void AddGroup(Group newGroup)
        {
            if (string.IsNullOrWhiteSpace(newGroup.Name))
                throw new ArgumentNullException("Название группы не может быть null или состоять только из пустых символов.");
            if (c.Groups.SingleOrDefault(g => g.Name == newGroup.Name) != null)
                throw new ArgumentException("Название группы должно быть уникальным");

            c.Groups.Add(newGroup);
            c.SaveChanges();
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
            c.SaveChanges();
            OnPaymentAdd?.Invoke(newPayment);
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

        private Dictionary<TimeIntervalType, Func<Payment, DateTime, bool>> InIntervalDic 
            = new Dictionary<TimeIntervalType, Func<Payment, DateTime, bool>>()
        {
            {TimeIntervalType.Day, (p, d) => p.Date >= new DateTime(d.Year, d.Month, d.Day, 0, 0, 0)
                && p.Date  <= new DateTime(d.Year, d.Month, d.Day, 23, 59, 59)},
            {TimeIntervalType.Week, (p, d) => {
                var startWeek = new DateTime(d.Year, d.Month, d.Day) - new TimeSpan((int)d.DayOfWeek + 1, 0, 0, 0);
                var endWeek = new DateTime(d.Year, d.Month, d.Day) + new TimeSpan(7 - (int)d.DayOfWeek, 23, 59, 59);
                return (p.Date >= startWeek && p.Date <= endWeek);
            } },
            {TimeIntervalType.Month, (p, d) => {
                var startMonth = new DateTime(d.Year, d.Month, 1);
                var endMonth = new DateTime(d.Year, d.Month, 31);
                return (p.Date >= startMonth && p.Date <= endMonth);
            } },
            {TimeIntervalType.Year, (p, d) => p.Date.Year == d.Year },
            {TimeIntervalType.Custom, null }
        };

        public List<StatByGroupViewModel> GetStatByGroups(TimeIntervalType tType, DateTime dateIn)
        {
            if (tType == TimeIntervalType.Custom)
                throw new ArgumentException("Интервал не может быть custom");

            var query = from p in c.Payments
                        where InIntervalDic[tType](p, dateIn)
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

        public List<StatByGroupViewModel> GetStatByGroups(TimeIntervalType tType)
        {
            return GetStatByGroups(tType, DateTime.Now);
        }

        public Dictionary<Group, double> GetSumByGroupsDuring()
        {
            return GetSumByGroupsDuring(TimeIntervalType.Day);
        }

        public Dictionary<Group, double> GetSumByGroupsDuring(TimeIntervalType tType, DateTime dateIn)
        {
            if (InIntervalDic[TimeIntervalType.Custom] == null)
                throw new ArgumentNullException("Тип интервала custom не определен");

            var query = from p in c.Payments
                        where InIntervalDic[tType](p, dateIn)
                        group p.Sum by p.Group into g
                        select new
                        {
                            Group = g.Key,
                            Total = g.Sum(s => s)
                        };

            return query.ToDictionary(a => a.Group, a => a.Total);
        }

        public Dictionary<Group, double> GetSumByGroupsDuring(TimeIntervalType tType)
        {
            return GetSumByGroupsDuring(tType, DateTime.Now);
        }

        public Dictionary<Group, double> GetSumByGroupsDuring(DateTime startDate, DateTime endDate)
        {
            InIntervalDic[TimeIntervalType.Custom] = (p, d) => (p.Date >= startDate && p.Date <= endDate);
            return GetSumByGroupsDuring(TimeIntervalType.Custom);
        }

        public double GetSumExceed(Group group)
        {
            var sum = GetSumByGroupsDuring(TimeIntervalType.Month)[group];
            return group.Limit - sum;
        }

        public void RemoveBill(Bill billToRemove, Bill saveBill)
        {
            if (c.Bills.Count() == 1)
                throw new ArgumentOutOfRangeException("Количество счетов не может быть равно 0");
            if (billToRemove == saveBill)
                throw new ArgumentException("Счета должны быть разными");

            saveBill.Payments.AddRange(billToRemove.Payments);
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

        public void RemovePayment(Payment payment)
        {
            c.Payments.Attach(payment);
            c.Payments.Remove(payment);
            c.SaveChanges();
            OnPaymentRemove?.Invoke(payment);
        }

        public void TransferMoney(Bill billFrom, Bill billTo, double sum)
        {
            if (billFrom == billTo)
                throw new ArgumentException("Счета должны быть разными");
            if (sum <= 0)
                throw new ArgumentOutOfRangeException("Сумма должна быть больше 0");

            var pTo = new Payment{
                Sum =  sum,
                Group = c.Groups.Single(g => g.Name == "TransferTo"),
                Bill = billTo,
                Note = $"Перевод со счета '{billFrom.Name}'"
            };

            var pFrom = new Payment
            {
                Sum = sum,
                Group = c.Groups.Single(g => g.Name == "TransferFrom"),
                Bill = billFrom,
                Note = $"Перевод на счет '{billTo.Name}'"
            };

            c.Payments.Add(pTo);
            c.Payments.Add(pFrom);

            c.SaveChanges();
        }
    }
}
