using TransactionIngest.Services;
using TransactionIngest.Data;

Console.WriteLine("Starting transaction ingestion...");

using var db = new AppDbContext();
db.Database.EnsureCreated();

var api = new MockApiService();
var transactions = api.GetTransactions();

var service = new TransactionService();
service.ProcessTransactions(transactions);

Console.WriteLine("Processing completed.");