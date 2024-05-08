using Microsoft.Win32;
using System;

class Program
{
    static void Main()
    {
        string keyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\DateTime\Servers";
        string valueName = "0";
        string valueData = "time.windows.com";

        RegistryKey key = null;

        try
        {
            key = Registry.LocalMachine.OpenSubKey(keyPath, true);
            if (key == null)
            {
                // If the key does not exist, create it
                key = Registry.LocalMachine.CreateSubKey(keyPath);
            }

            // Set the registry value
            key.SetValue(valueName, valueData, RegistryValueKind.String);

            Console.WriteLine("Registry value updated successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        finally
        {
            // Make sure to close the key properly if it's not null
            if (key != null)
            {
                key.Close();
            }
        }
    }
}
