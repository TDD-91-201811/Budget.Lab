using System;
using System.Collections.Generic;
using System.Linq;

namespace BudgetTest
{
    public class BudgetService
    {
        private IBudgetRepository _budgetRepository;
        private double _totalAmount = 0;

        public BudgetService(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public double TotalAmount(DateTime start, DateTime end)
        {
            var thisMon = start;
            int thisEndMon = end.Month;
            while (thisMon <= end)
            {
                Budget budget = _budgetRepository.GetAll().FirstOrDefault(p => p.YearMonth == thisMon.ToString("yyyyMM"));
                if (budget != null)
                {
                    double oneDayAmount = budget.Amount / DateTime.DaysInMonth(thisMon.Year, thisMon.Month);
                    DateTime thisMonthStartDay = start == thisMon ? start : new DateTime(thisMon.Year, thisMon.Month, 1);

                    DateTime thisMonthEndDay = end.Year == thisMon.Year && end.Month == thisMon.Month ? end : CaculateMonthLastDate(thisMon);

                    int thisMonthTotalDays = thisMonthEndDay.Day - thisMonthStartDay.Day + 1;

                    _totalAmount += oneDayAmount * thisMonthTotalDays;
                }
                else
                {
                    _totalAmount += 0;
                }
                thisMon = thisMon.AddMonths(1);
            }

            return _totalAmount;
        }

        private static DateTime CaculateMonthLastDate(DateTime start)
        {
            DateTime theMonthLastDay = new DateTime(start.Year, start.Month + 0, 1).AddDays(-1);
            return theMonthLastDay;
        }
    }

    public interface IBudgetRepository
    {
        List<Budget> GetAll();
    }

    public class Budget
    {
        public string YearMonth { get; set; }
        public int Amount { get; set; }
    }
}