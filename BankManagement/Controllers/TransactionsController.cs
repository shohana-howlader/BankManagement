using BankManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly BankContext _context;

        public TransactionsController(BankContext context)
        {
            _context = context;
        }

        [HttpPost("deposit")]
        public IActionResult Deposit(Transaction transaction)
        {
            var account = _context.Accounts.Find(transaction.AccountId);
            if (account == null)
                return NotFound("Account not found.");

            account.Balance += transaction.Amount;
            transaction.TransactionType = "Deposit";
            transaction.Date = DateTime.UtcNow;

            _context.Transactions.Add(transaction);
            _context.SaveChanges();

            return Ok("Deposit successful.");
        }

        [HttpPost("withdraw")]
        public IActionResult Withdraw(Transaction transaction)
        {
            var account = _context.Accounts.Find(transaction.AccountId);
            if (account == null || account.Balance < transaction.Amount)
                return BadRequest("Insufficient funds or account not found.");

            account.Balance -= transaction.Amount;
            transaction.TransactionType = "Withdrawal";
            transaction.Date = DateTime.UtcNow;

            _context.Transactions.Add(transaction);
            _context.SaveChanges();

            return Ok("Withdrawal successful.");
        }
    }
}
