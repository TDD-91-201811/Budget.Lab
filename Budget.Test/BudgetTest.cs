using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Castle.Components.DictionaryAdapter;
using NSubstitute;

namespace BudgetTest
{
    [TestClass]
    public class BudgetTest
    {
        private BudgetService budgetService;
        public BudgetTest()
        {
            var budgetRepository = Substitute.For<IBudgetRepository>();
            budgetRepository.GetAll().Returns(new EditableList<Budget>()
            {
                new Budget() {YearMonth = "201801", Amount = 310},
            });
            budgetService = new BudgetService(budgetRepository);
        }


        [TestMethod]
        public void ThisMonNoBudget_OneDay()
        {
            BudgetShouldBe(0, new DateTime(2018, 3, 1), new DateTime(2018, 3, 1));
        }

        [TestMethod]
        public void ThisMonHasBudget_OneDay()
        {
            BudgetShouldBe(10, new DateTime(2018, 1, 1), new DateTime(2018, 1, 1));
        }

        private void BudgetShouldBe(int expected, DateTime start, DateTime end)
        {
            Assert.AreEqual(expected, budgetService.TotalAmount(start, end));
        }
    }
}