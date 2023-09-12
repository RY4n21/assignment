using System;

// Base class Account
class Account
{
    // Properties
    public int AccountNumber { get; private set; }
    private double Balance;

    // Constructor
    public Account(int accountNumber)
    {
        AccountNumber = accountNumber;
        Balance = 0;
    }

    // Methods
    public void Deposit(double amount)
    {
        if (amount > 0)
        {
            Balance += amount;
            Console.WriteLine($"Deposited ${amount}. New balance: ${Balance}");
        }
        else
        {
            Console.WriteLine("Invalid deposit amount.");
        }
    }

    public virtual void Withdraw(double amount)
    {
        if (amount > 0 && Balance >= amount)
        {
            Balance -= amount;
            Console.WriteLine($"Withdrawn ${amount}. New balance: ${Balance}");
        }
        else
        {
            Console.WriteLine("Invalid withdrawal amount or insufficient balance.");
        }
    }

    public void DisplayAccountDetails()
    {
        Console.WriteLine($"Account Number: {AccountNumber}");
        Console.WriteLine($"Balance: ${Balance}");
    }
}

// Subclass SavingsAccount
class SavingsAccount : Account
{
    // Additional Property
    public double InterestRate { get; private set; }

    // Constructor
    public SavingsAccount(int accountNumber, double interestRate) : base(accountNumber)
    {
        InterestRate = interestRate;
    }

    // Method to calculate and add interest
    public void CalculateInterest()
    {
        double interestAmount = Balance * (InterestRate / 100);
        Deposit(interestAmount);
        Console.WriteLine($"Interest calculated and added: ${interestAmount}");
    }

    public override void Withdraw(double amount)
    {
        base.Withdraw(amount);
    }
}

// Subclass CheckingAccount
class CheckingAccount : Account
{
    // Additional Property
    public double OverdraftLimit { get; private set; }

    // Constructor
    public CheckingAccount(int accountNumber, double overdraftLimit) : base(accountNumber)
    {
        OverdraftLimit = overdraftLimit;
    }

    // Override Withdraw method to allow limited overdraft
    public override void Withdraw(double amount)
    {
        if (amount > 0 && (Balance + OverdraftLimit) >= amount)
        {
            Balance -= amount;
            Console.WriteLine($"Withdrawn ${amount}. New balance: ${Balance}");
        }
        else
        {
            Console.WriteLine("Invalid withdrawal amount or exceeding overdraft limit.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create instances of SavingsAccount and CheckingAccount
        SavingsAccount savingsAccount = new SavingsAccount(101, 2.5); // 2.5% interest rate
        CheckingAccount checkingAccount = new CheckingAccount(102, 500); // $500 overdraft limit

        // Deposit and withdraw from both accounts
        savingsAccount.Deposit(1000);
        savingsAccount.CalculateInterest();
        savingsAccount.Withdraw(200);
        savingsAccount.DisplayAccountDetails();

        checkingAccount.Deposit(500);
        checkingAccount.Withdraw(300);
        checkingAccount.Withdraw(800);
        checkingAccount.DisplayAccountDetails();
    }
}
