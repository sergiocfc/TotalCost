using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TotalCost.UI.Entity;
using TotalCost.UI.ViewModels;

namespace TotalCost.UI.Logic
{
    interface IDataRepository : IDisposable
    {
        /// <summary>
        /// Добавить счет. Максимальное количество - 4.
        /// </summary>
        /// <param name="newBill"></param>
        bool AddBill(string name, BillType type, double sum);
        /// <summary>
        /// Получить остаток на счете.
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        double GetAccountBalance(Bill bill);
        /// <summary>
        /// Перевести деньги с одного счета на другой.
        /// </summary>
        /// <param name="billFrom"></param>
        /// <param name="billTo"></param>
        /// <param name="sum"></param>
        void TransferMoney(Bill billFrom, Bill billTo, double sum);
        /// <summary>
        /// Удалить счет.
        /// </summary>
        /// <param name="billToRemove"></param>
        /// <param name="saveBill"></param>
        void RemoveBill(Bill billToRemove, Bill saveBill);
        /// <summary>
        /// Добавить платеж.
        /// </summary>
        /// <param name="newPayment"></param>
        void AddPayment(Payment newPayment);
        /// <summary>
        /// Получить доходы по группам за текущий день.
        /// </summary>
        /// <returns></returns>
        Dictionary<Group, double> GetSumByGroupsDuring();
        /// <summary>
        /// Получить доходы по группам за текущий промежуток времени.
        /// </summary>
        /// <param name="timeIntervalType">Тип текущего периода.</param>
        /// <returns>Возвращает список пар ключ-значение сумм доходов по группам</returns>
        Dictionary<Group, double> GetSumByGroupsDuring(TimeIntervalType timeIntervalType);
        /// <summary>
        /// Получить доходы по группам за промежуток времени.
        /// </summary>
        /// <param name="timeIntervalType">Тип текущего периода.</param>
        /// <returns>Возвращает список пар ключ-значение сумм доходов по группам</returns>
        Dictionary<Group, double> GetSumByGroupsDuring(TimeIntervalType timeIntervalType, DateTime dateIn);
        /// <summary>
        /// Получить доход по группам за промежуток времени.
        /// </summary>
        /// <param name="startDate">День начала отсчета.</param>
        /// <param name="endDate">День завершения отсчета (включается).</param>
        /// <returns></returns>
        Dictionary<Group, double> GetSumByGroupsDuring(DateTime startDate, DateTime endDate);
        /// <summary>
        /// Получить минимальное, максимальное и среднее значение для каждой группы за промежуток времени.
        /// </summary>
        /// <param name="timeInvtervalType"></param>
        /// <returns></returns>
        List<StatByGroupViewModel> GetStatByGroups(TimeIntervalType timeInvtervalType);
        /// <summary>
        /// Получить минимальное, максимальное и среднее значение для каждой группы за промежуток времени.
        /// </summary>
        /// <param name="timeInvtervalType"></param>
        /// <returns></returns>
        List<StatByGroupViewModel> GetStatByGroups(TimeIntervalType timeInvtervalType, DateTime dateIn);
        /// <summary>
        /// Удалить платеж.
        /// </summary>
        /// <param name="payment"></param>
        void RemovePayment(Payment payment);
        /// <summary>
        /// Добавить группу.
        /// </summary>
        /// <param name="newGroup"></param>
        void AddGroup(Group newGroup);
        /// <summary>
        /// Удалить группу.
        /// </summary>
        /// <param name="groupToRemove"></param>
        /// <param name="saveGroup"></param>
        void RemoveGroup(Group groupToRemove, Group saveGroup);
        /// <summary>
        /// Добавить предел.
        /// </summary>
        /// <param name="newLimit"></param>
        double GetSumExceed(Group group);
    }
}
