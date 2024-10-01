using System.Collections.Concurrent;

namespace TransactionProcesor;

public class AccountService<TAccount> where TAccount : Account
{
    private readonly ConcurrentDictionary<long, Account> _accounts;
    private readonly ConcurrentDictionary<long, object> _accountLocks = new ConcurrentDictionary<long, object>();
    private readonly object _lock = new object();

    public AccountService(params TAccount[] accounts)
    {
        if (accounts == null)
        {
            throw new ArgumentNullException(nameof(accounts));
        }

        if (accounts.Length == 0)
        {
            throw new ArgumentException(nameof(accounts), $"{nameof(accounts)} cannot be empty");
        }
        _accounts = new ConcurrentDictionary<long, Account>();
        
        
        foreach (var account in accounts)
        {
            _accounts[account.Id] = account;
        }
    }
    
    public void Deposit(long accountId, decimal amount)
    {
        var account = GetAccount(accountId);
        
        var accountLock = _accountLocks.GetOrAdd(accountId, new object());
        lock (accountLock)
        {
            account.SetBalance(account.Balance + amount);
        }
    }

    public void Withdraw(long accountId, decimal amount)
    {
        var account = GetAccount(accountId);
        lock (_lock)
        {
            if (account.Balance < amount)
            {
                throw new InvalidOperationException("Insufficient funds");
            }
            account.SetBalance(account.Balance - amount);
        }
    }

    public void TransferTo(long fromAccountId, long toAccountId, decimal amount)
    {
        var fromAccount = GetAccount(fromAccountId);
        var toAccount = GetAccount(toAccountId);

        lock (_lock)
        { //lock both accounts
            if (fromAccount.Balance < amount)
            {
                throw new InvalidOperationException("Insufficient funds");
            }
            fromAccount.SetBalance(fromAccount.Balance - amount);
            toAccount.SetBalance(toAccount.Balance + amount);
        }
    }
    
    // Get an account by ID
    private Account GetAccount(long accountId)
    {
        if (_accounts.TryGetValue(accountId, out var account))
        {
            return account;
        }
        throw new InvalidOperationException("Account not found");
    }
}

