namespace TransactionProcesor;

public class Processor
{
    private readonly AccountService<Account> _accountService;
    public Processor(AccountService<Account> accountService)
    {
        _accountService = accountService;
    }

    private void ProcessTransaction(Transaction transaction)
    {
        switch (transaction.TransactionType)
        {
            case TransactionType.Deposit:
                _accountService.Deposit(transaction.SourceAccount.Id, transaction.Amount);
                break;
            case TransactionType.Withdraw:
                _accountService.Withdraw(transaction.SourceAccount.Id, transaction.Amount);
                break;
            case TransactionType.Transfer:
                _accountService.TransferTo(transaction.SourceAccount.Id, transaction.TargetAccount.Id, transaction.Amount);
                break;
            default:
                throw new ArgumentOutOfRangeException("Unknown transaction type");
        }
    }
    public void Process(List<Transaction> transactions)
    {
        foreach (var transaction in transactions)
        {
            ProcessTransaction(transaction);
        }
    }

    public async Task ProcessTransactionsConcurrently(List<Transaction> transactions)
    {
       /*THIS FAILED BECAUSE
        
        Since Parallel.ForEach already runs operations concurrently,
         there's no need to add an additional Task.Run inside it.
         REMOVE TASK.RUN() as below
         This ensures that transactions are still processed in parallel but without the unnecessary extra task creation that could be leading to the balance inconsistency.
        */
       // Parallel.ForEach(transactions, (transaction) =>
       // {
            // Task.Run(() =>
            // {
            //     ProcessTransaction(transaction);
            // });
            //   });   
            
            Parallel.ForEach(transactions, ProcessTransaction);
            
            /**AWAIT ALL ALSO WORKS **/
          // await Task.WhenAll(transactions.Select(transaction => Task.Run(() => ProcessTransaction(transaction))));
     
        
        
    }
}