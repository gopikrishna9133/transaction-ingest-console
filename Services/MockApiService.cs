using System.Text.Json;
using TransactionIngest.Models;

namespace TransactionIngest.Services
{
    public class MockApiService
    {
        public List<Transaction> GetTransactions()
        {
            var json = File.ReadAllText("mockdata.json");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<List<Transaction>>(json, options) ?? new List<Transaction>();
        }
    }
}