namespace TransactionProcesor;

public enum TransactionType
{
    Deposit,
    Withdraw,
    Transfer
}

public class Transaction
{
    public TransactionType TransactionType { get; set; }    
    public Account SourceAccount { get; set; }
    public Account TargetAccount { get; set; }
    public decimal Amount { get; set; }

    //constructor for deposit and withdraw with same account
    public Transaction(TransactionType transactionType, Account account, decimal amount)
    {
        TransactionType = transactionType;
        SourceAccount = account;
        Amount = amount;
    }
    
    //constructor for transfer
    public Transaction(Account accountA, Account accountB, decimal amount)
    {
        TransactionType = TransactionType.Transfer;
        SourceAccount = accountA;
        TargetAccount = accountB;
        Amount = amount;
    }
}