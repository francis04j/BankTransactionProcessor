namespace TransactionProcesor;

public class Account
{
    public long Id { get; set; }
    public decimal Balance { get; private set; }

    public Account(long id, decimal balance)
    {
        Id = id;
        Balance = balance;
    }

    internal void SetBalance(decimal balance)
    {
        Balance = balance;
    }
}