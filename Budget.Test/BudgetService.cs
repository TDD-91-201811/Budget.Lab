using System;
using System.Collections.Generic;
using System.Linq;

namespace BudgetTest
{
    public class BudgetService
    {
        private IBudgetRepository _budgetRepository;

        public BudgetService(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public double TotalAmount(DateTime start, DateTime end)
        {
            var data = _budgetRepository.GetAll().FirstOrDefault(p => p.YearMonth == start.ToString("yyyyMM"));

            if (data != null)
            {
                double onedayAmount = data.Amount / DateTime.DaysInMonth(start.Year, start.Month);
                var totalDay = end.Day - start.Day + 1;
                return onedayAmount * totalDay;
            }
            return 0;
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