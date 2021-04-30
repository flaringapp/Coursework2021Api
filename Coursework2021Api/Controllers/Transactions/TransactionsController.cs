using System;
using System.Collections.Generic;
using System.Linq;
using Coursework2021DB.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Coursework2021Api.Controllers.Transactions
{
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly CourseDBContext context;

        public TransactionsController(CourseDBContext context)
        {
            this.context = context;
        }

        [HttpGet("/api/transactions")]
        public ActionResult<List<TransactionResponse>> GetList(
            [FromQuery] string? managerId)
        {
            IQueryable<Transaction> query = context.Transactions;
            if (managerId != null)
            {
                var managerIdInt = int.Parse(managerId);
                query = query.Where(transaction => transaction.ManagerId == managerIdInt);
            }
            var transactions = query.Select(transaction => ResponseForModel(transaction))
                .ToList();
            return transactions;
        }

        [HttpGet("/api/transaction")]
        public ActionResult<TransactionResponse?> Get([FromQuery] string id)
        {
            var transaction = GetById(id);

            if (transaction == null) return BadRequest("No transaction found for id " + id);
            return ResponseForModel(transaction);
        }

        [HttpPut("/api/transaction")]
        public ActionResult<TransactionResponse> Put([FromBody] AddTransactionRequest request)
        {
            var rentalId = int.Parse(request.RentalId);
            var rental = context.RoomRentals.FirstOrDefault(r => r.Id == rentalId);
            if (rental == null) return BadRequest("Cannot find rental with id " + rentalId);

            var room = rental.Room;
            if (room == null) return BadRequest("Cannot find rental room with id " + rental.RoomId);

            var amount = room.PlacePrice * request.MonthsCount;

            var lastPaymentDate = rental.DatePaidUntil ?? rental.DateStart;
            var paidUntilDate = lastPaymentDate.AddMonths(request.MonthsCount);

            var transaction = CreateDBModel(request, amount, lastPaymentDate, paidUntilDate);
            context.Transactions.Add(transaction);
            SaveChanges(transaction);

            UpdateRentalPaidUntilDate(rental, paidUntilDate);

            return ResponseForModel(transaction);
        }

        [HttpDelete("/api/transaction")]
        public ActionResult Delete([FromQuery] string id)
        {
            var transaction = GetById(id);
            if (transaction == null) return BadRequest("Cannot find transaction by given id");

            context.Transactions.Remove(transaction);
            context.SaveChanges();

            return Ok();
        }

        private Transaction? GetById(string id)
        {
            var idInt = int.Parse(id);
            return context.Transactions.FirstOrDefault(t => t.Id == idInt);
        }

        private void SaveChanges(Transaction model)
        {
            context.SaveChanges();
            context.Entry(model).Reference(t => t.Manager).Load();
            context.Entry(model).Reference(t => t.Rent).Load();
        }

        private void UpdateRentalPaidUntilDate(RoomRental rental, DateTime paidUntil)
        {
            rental.DatePaidUntil = paidUntil;
            context.Update(rental);
            context.SaveChanges();
        }

        private static TransactionResponse ResponseForModel(Transaction transaction)
        {
            return new()
            {
                Id = transaction.Id.ToString(),
                RentalId = transaction.RentId.ToString(),
                UserId = transaction.Rent?.UserId.ToString(),
                UserFirstName = transaction.Rent?.User?.FirstName,
                UserLastName = transaction.Rent?.User?.LastName,
                RoomId = transaction.Rent?.RoomId.ToString(),
                RoomName = transaction.Rent?.Room?.Name,
                RoomType = transaction.Rent?.Room?.Type,
                ManagerId = transaction.ManagerId.ToString(),
                DateFrom = transaction.DatePaidFrom.ToString("yyyy-MM-dd"),
                DateTo = transaction.DatePaidTo.ToString("yyyy-MM-dd"),
                Amount = transaction.Amount,
                TimeCreated = transaction.TimeCreated.ToString("yyyy-MM-ddTHH:mm:ss")
            };
        }

        private Transaction CreateDBModel(
            AddTransactionRequest request,
            int amount,
            DateTime paidFrom,
            DateTime paidTo)
        {
            var transaction = context.Transactions.CreateProxy();
            transaction.ManagerId = int.Parse(request.ManagerId);
            transaction.RentId = int.Parse(request.RentalId);
            transaction.Amount = amount;
            transaction.DatePaidFrom = paidFrom;
            transaction.DatePaidTo = paidTo;
            transaction.TimeCreated = DateTime.UtcNow;
            return transaction;
        }
    }
}
