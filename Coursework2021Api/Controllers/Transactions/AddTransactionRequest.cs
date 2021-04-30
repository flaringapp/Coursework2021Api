using System;

namespace Coursework2021Api.Controllers.Transactions
{
    public class AddTransactionRequest
    {
        public string RentalId { get; set; }
        public string ManagerId { get; set; }
        public int MonthsCount { get; set; }
    }
}