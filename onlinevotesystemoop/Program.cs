using System;
using System.Collections.Generic;

class Program
{
    // Code Smell 1: Global variables
    static List<(string name, string password)> voters = new List<(string, string)>();
    static Dictionary<string, int> candidateVotes = new Dictionary<string, int>();
    static List<Candidate> candidates = new List<Candidate>(); // Code Smell 2: Lack of encapsulation for candidates
    // Code Smell 3: Hardcoded admin credentials
    static string adminUsername = "admin";
    static string adminPassword = "admin123";

    static void Main(string[] args)
    {
        InitializeCandidates(); // Code Smell 4: Lack of modularity

        while (true)
        {
            Console.WriteLine("1. Register");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Vote");
            Console.WriteLine("4. View Results");
            Console.WriteLine("5. View Voter List");
            Console.WriteLine("6. Candidate Login");
            Console.WriteLine("7. Exit");

            // Code Smell 5: Lack of input validation
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Register();
                    break;
                case 2:
                    Login();
                    break;
                case 3:
                    if (IsUserLoggedIn()) // Code Smell 6: Violation of Single Responsibility Principle
                        Vote();
                    else
                        Console.WriteLine("Please log in first.");
                    break;
                case 4:
                    ViewResults();
                    break;
                case 5:
                    // Code Smell 7: Violation of privacy by displaying the voter list
                    ViewVoterList();
                    break;
                case 6:
                    CandidateLogin();
                    break;
                case 7:
                    Environment.Exit(0);
                    break;
                default:
                    // Code Smell 8: Lack of feedback for invalid choices
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }
    }

    static void InitializeCandidates()
    {
        // Code Smell 9: Lack of encapsulation for candidate data
        candidates.Add(new Candidate("Shakib Al Hasan", "Main St.", "Ballot A"));
        candidates.Add(new Candidate("Sumon", "456  St.", "Ballot B"));
        // ... Add 18 more candidates similarly
    }

    // Code Smell 10: Methods with no clear responsibility
    static void Register()
    {
        Console.WriteLine("Enter your name: ");
        string name = Console.ReadLine();
        Console.WriteLine("Enter your password: ");
        string password = Console.ReadLine();

        // Code Smell 11: Duplicate code
        if (!voters.Contains((name, password)))
        {
            voters.Add((name, password));
            Console.WriteLine("Registration successful!");
        }
        else
        {
            Console.WriteLine("User already registered.");
        }
    }

    static void Login()
    {
        Console.WriteLine("Enter your name: ");
        string name = Console.ReadLine();
        Console.WriteLine("Enter your password: ");
        string password = Console.ReadLine();

        // Code Smell 12: Simple tuple comparison for name and password
        if (voters.Contains((name, password)))
        {
            Console.WriteLine($"Welcome back, {name}!");
            UserMenu();
        }
        else if (name == adminUsername && password == adminPassword)
        {
            AdminMenu();
        }
        else
        {
            // Code Smell 13: Inappropriate word
            Console.WriteLine("Invalid credentials. You're a black sheep!");
        }
    }

    static void AdminMenu()
    {
        while (true)
        {
            Console.WriteLine("Admin Menu:");
            Console.WriteLine("1. View Voter List");
            Console.WriteLine("2. View Candidate Votes");
            Console.WriteLine("3. Logout");

            // Code Smell 14: Lack of input validation
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    // Code Smell 15: Violation of privacy by displaying the voter list
                    ViewVoterList();
                    break;
                case 2:
                    ViewResults();
                    break;
                case 3:
                    return;
                default:
                    // Code Smell 16: Lack of feedback for invalid choices
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }
    }

    static void UserMenu()
    {
        Console.WriteLine("User Menu:");
        Console.WriteLine("1. View Results");
        Console.WriteLine("2. Vote");
        Console.WriteLine("3. Logout");

        // Code Smell 17: Lack of input validation
        int choice = Convert.ToInt32(Console.ReadLine());

        switch (choice)
        {
            case 1:
                // Code Smell 18: Lack of clarity in result display
                ViewResults();
                break;
            case 2:
                if (IsUserLoggedIn())
                    Vote();
                else
                    Console.WriteLine("Please log in first.");
                break;
            case 3:
                return;
            default:
                // Code Smell 19: Lack of feedback for invalid choices
                Console.WriteLine("Invalid choice");
                break;
        }
    }

    static void Vote()
    {
        Console.WriteLine("Choose a candidate to vote by entering their number: ");
        DisplayCandidates();
        // Code Smell 20: Lack of input validation
        int candidateChoice = Convert.ToInt32(Console.ReadLine());

        // Code Smell 21: Violation of Single Responsibility Principle
        RecordVote(candidateChoice);
    }

    static void RecordVote(int candidateChoice)
    {
        // Code Smell 22: Magic numbers
        if (candidateChoice >= 1 && candidateChoice <= candidates.Count)
        {
            Candidate selectedCandidate = candidates[candidateChoice - 1];

            // Code Smell 23: Global variable modification without encapsulation
            if (candidateVotes.ContainsKey(selectedCandidate.Name))
                candidateVotes[selectedCandidate.Name]++;
            else
                candidateVotes[selectedCandidate.Name] = 1;

            // Code Smell 24: Lack of feedback after voting
            Console.WriteLine($"Vote casted for {selectedCandidate.Name}");
        }
        else
        {
            // Code Smell 25: Inappropriate word
            Console.WriteLine("Invalid candidate choice. You're a black sheep!");
        }
    }

    static void DisplayCandidates()
    {
        Console.WriteLine("Candidates:");
        for (int i = 0; i < candidates.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {candidates[i].Name} - {candidates[i].Address} - {candidates[i].Ballot}");
        }
    }

    static void ViewResults()
    {
        Console.WriteLine("Candidate Votes:");
        foreach (var candidate in candidateVotes)
        {
            // Code Smell 26: Concatenating strings in a loop
            Console.WriteLine($"{candidate.Key}: {candidate.Value} votes");
        }
    }

    static void ViewVoterList()
    {
        // Code Smell 27: Violation of privacy by displaying the voter list
        Console.WriteLine("Voter List:");
        foreach (var voter in voters)
        {
            // Code Smell 28: Data clumps
            Console.WriteLine($"{voter.name}");
        }
    }

    static bool IsUserLoggedIn()
    {
        // Code Smell 29: Lack of clear indication of user login status
        return true;
    } 

    static void CandidateLogin()
    {
        Console.WriteLine("Enter your candidate name: ");
        string candidateName = Console.ReadLine();
        Console.WriteLine("Enter your candidate password: ");
        string candidatePassword = Console.ReadLine();

        // Code Smell 30: Simple tuple comparison for candidate name and password
        if (candidates.Exists(c => c.Name == candidateName && c.Password == candidatePassword))
        {
            Console.WriteLine($"Welcome back, {candidateName}!");
            ViewCandidateVotes(candidateName);
        }
        else
        {
            // Code Smell 31: Feature envy
            Console.WriteLine("Invalid candidate credentials. You envy my features!");
        }
    }

    static void ViewCandidateVotes(string candidateName)
    {
        // Code Smell 32: Lack of encapsulation for candidate votes
        if (candidateVotes.ContainsKey(candidateName))
        {
            Console.WriteLine($"{candidateName}, you have received {candidateVotes[candidateName]} votes.");
        }
        else
        {
            // Code Smell 33: Primitive obsession
            Console.WriteLine($"{candidateName}, you have not received any votes yet. Embrace the primitive!");
        }
    }
}

class Candidate
{
    public string Name { get; }
    public string Address { get; }
    public string Ballot { get; }
    public string Password { get; }

    public Candidate(string name, string address, string ballot)
    {
        Name = name;
        Address = address;
        Ballot = ballot;
        // Code Smell 34: Simple password generation
        Password = $"{name}123";
    }
}
