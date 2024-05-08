using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        Console.WriteLine("Choose an option:");
        Console.WriteLine("1. Program 1 (Static IP: 10.0.0.1, Subnet Mask: 255.255.255.0, Default Gateway: 10.0.0.254)");
        Console.WriteLine("2. Program 2 (Static IP: 192.168.1.100, Subnet Mask: 255.255.255.0, Default Gateway: 192.168.1.1)");
        Console.WriteLine("3. Program 3 (Static IP: 172.16.0.1, Subnet Mask: 255.255.0.0, Default Gateway: 172.16.0.254)");
        Console.WriteLine("4. Program 4 (Automatically obtain IP and DNS)");
        Console.WriteLine("S. Settings");

        ConsoleKeyInfo keyInfo = Console.ReadKey();

        string command;
        switch (keyInfo.KeyChar)
        {
            case '1':
                command = "netsh interface ipv4 set address name=\"Ethernet\" static 10.0.0.1 255.255.255.0 10.0.0.254";
                break;
            case '2':
                command = "netsh interface ipv4 set address name=\"Ethernet\" static 192.168.1.100 255.255.255.0 192.168.1.1";
                break;
            case '3':
                command = "netsh interface ipv4 set address name=\"Ethernet\" static 172.16.0.1 255.255.0.0 172.16.0.254";
                break;
            case '4':
                command = "netsh interface ipv4 set address name=\"Ethernet\" dhcp && netsh interface ipv4 set dnsservers name=\"Ethernet\" source=dhcp";
                break;
            case 'S':
            case 's':
                SetCustomSettings();
                return;
            default:
                Console.WriteLine("\nInvalid choice.");
                return; // Exit the program if the choice is invalid
        }

        ExecuteCommand(command);
    }

    static void SetCustomSettings()
    {
        Console.WriteLine("\nCustom Settings:");
        Console.WriteLine("Enter IP address (leave empty to keep current):");
        string ip = Console.ReadLine();
        Console.WriteLine("Enter Subnet Mask (leave empty to keep current):");
        string subnet = Console.ReadLine();
        Console.WriteLine("Enter Default Gateway (leave empty to keep current):");
        string gateway = Console.ReadLine();

        string command = "netsh interface ipv4 set address name=\"Ethernet\"";
        if (!string.IsNullOrEmpty(ip))
            command += " static " + ip;
        if (!string.IsNullOrEmpty(subnet))
            command += " " + subnet;
        if (!string.IsNullOrEmpty(gateway))
            command += " " + gateway;

        ExecuteCommand(command);
    }

    static void ExecuteCommand(string command)
    {
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = @"C:\WINDOWS\system32\cmd.exe",
            Verb = "runas",
            Arguments = "/C \"" + command + "\"",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = false
        };

        Process process = new Process
        {
            StartInfo = startInfo
        };
        process.Start();

        string output = process.StandardOutput.ReadToEnd();

        process.WaitForExit();

        Console.WriteLine(output);

        Environment.Exit(0);
    }
}
