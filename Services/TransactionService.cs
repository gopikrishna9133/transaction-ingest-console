using TransactionIngest.Data;
using TransactionIngest.Models;

namespace TransactionIngest.Services
{
    public class TransactionService
    {
        public void ProcessTransactions(List<Transaction> incoming)
        {
            using var db = new AppDbContext();

            foreach (var tx in incoming)
            {
                var existing = db.Transactions
                    .FirstOrDefault(x => x.TransactionId == tx.TransactionId);

                if (existing == null)
                {
                    db.Transactions.Add(tx);
                }
                else
                {
                    if (existing.Amount != tx.Amount)
                    {
                        db.Audits.Add(new TransactionAudit
                        {
                            TransactionId = existing.TransactionId,
                            FieldChanged = "Amount",
                            OldValue = existing.Amount.ToString(),
                            NewValue = tx.Amount.ToString()
                        });

                        existing.Amount = tx.Amount;
                    }

                    if (existing.ProductName != tx.ProductName)
                    {
                        db.Audits.Add(new TransactionAudit
                        {
                            TransactionId = existing.TransactionId,
                            FieldChanged = "ProductName",
                            OldValue = existing.ProductName,
                            NewValue = tx.ProductName
                        });

                        existing.ProductName = tx.ProductName;
                    }

                    if (existing.LocationCode != tx.LocationCode)
                    {
                        db.Audits.Add(new TransactionAudit
                        {
                            TransactionId = existing.TransactionId,
                            FieldChanged = "LocationCode",
                            OldValue = existing.LocationCode,
                            NewValue = tx.LocationCode
                        });

                        existing.LocationCode = tx.LocationCode;
                    }

                    existing.UpdatedAt = DateTime.UtcNow;
                }
            }

            var ids = incoming.Select(x => x.TransactionId).ToList();
            var last24Hours = DateTime.UtcNow.AddHours(-24);

            var revokedTransactions = db.Transactions
                .Where(t => t.TransactionTime > last24Hours &&
                            !ids.Contains(t.TransactionId))
                .ToList();

            foreach (var r in revokedTransactions)
            {
                r.Status = "Revoked";
            }

            db.SaveChanges();
        }
    }
}