using System;
using System.Collections.Generic;

class Account
{
    public string AccountNumber { get; }
    public string Name { get; }
    public decimal Balance { get; private set; }

    public Account(string accountNumber, string name, decimal initialDeposit)
    {
        AccountNumber = accountNumber;
        Name = name;
        Balance = initialDeposit;
    }

    public void Deposit(decimal amount)
    {
        Balance += amount;
    }

    public bool Withdraw(decimal amount)
    {
        if (amount > Balance)
            return false;
        Balance -= amount;
        return true;
    }
}

class Bank
{
    private Dictionary<string, Account> accounts = new Dictionary<string, Account>();
    private int lastAccountNumber = 1000;

    public void CreateAccount()
    {
        Console.Write("Enter name: ");
        string name = Console.ReadLine();
        Console.Write("Enter initial deposit: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal initialDeposit))
        {
            string accountNumber = (++lastAccountNumber).ToString();
            accounts[accountNumber] = new Account(accountNumber, name, initialDeposit);
            Console.WriteLine($"Account created. Your account number is: {accountNumber}");
        }
        else
        {
            Console.WriteLine("Invalid amount. Account creation failed.");
        }
    }

    public void Deposit()
    {
        Console.Write("Enter account number: ");
        string accountNumber = Console.ReadLine();
        if (accounts.TryGetValue(accountNumber, out Account account))
        {
            Console.Write("Enter deposit amount: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                account.Deposit(amount);
                Console.WriteLine($"Deposit successful. New balance: {account.Balance}");
            }
            else
            {
                Console.WriteLine("Invalid amount.");
            }
        }
        else
        {
            Console.WriteLine("Account not found.");
        }
    }

    public void Withdraw()
    {
        Console.Write("Enter account number: ");
        string accountNumber = Console.ReadLine();
        if (accounts.TryGetValue(accountNumber, out Account account))
        {
            Console.Write("Enter withdrawal amount: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                if (account.Withdraw(amount))
                    Console.WriteLine($"Withdrawal successful. New balance: {account.Balance}");
                else
                    Console.WriteLine("Insufficient funds.");
            }
            else
            {
                Console.WriteLine("Invalid amount.");
            }
        }
        else
        {
            Console.WriteLine("Account not found.");
        }
    }

    public void CheckBalance()
    {
        Console.Write("Enter account number: ");
        string accountNumber = Console.ReadLine();
        if (accounts.TryGetValue(accountNumber, out Account account))
        {
            Console.WriteLine($"Account Holder: {account.Name}");
            Console.WriteLine($"Balance: {account.Balance}");
        }
        else
        {
            Console.WriteLine("Account not found.");
        }
    }

    public void Transfer()
    {
        Console.Write("Enter your account number: ");
        string fromAccountNumber = Console.ReadLine();
        Console.Write("Enter recipient's account number: ");
        string toAccountNumber = Console.ReadLine();

        if (accounts.TryGetValue(fromAccountNumber, out Account fromAccount) &&
            accounts.TryGetValue(toAccountNumber, out Account toAccount))
        {
            Console.Write("Enter transfer amount: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                if (fromAccount.Withdraw(amount))
                {
                    toAccount.Deposit(amount);
                    Console.WriteLine("Transfer successful.");
                    Console.WriteLine($"Your new balance: {fromAccount.Balance}");
                }
                else
                {
                    Console.WriteLine("Insufficient funds.");
                }
            }
            else
            {
                Console.WriteLine("Invalid amount.");
            }
        }
        else
        {
            Console.WriteLine("One or both accounts not found.");
        }
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("\n--- Banking System ---");
            Console.WriteLine("1. Create Account");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Withdraw");
            Console.WriteLine("4. Check Balance");
            Console.WriteLine("5. Transfer");
            Console.WriteLine("6. Exit");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateAccount();
                    break;
                case "2":
                    Deposit();
                    break;
                case "3":
                    Withdraw();
                    break;
                case "4":
                    CheckBalance();
                    break;
                case "5":
                    Transfer();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Bank bank = new Bank();
        bank.Run();
    }
}
