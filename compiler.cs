using System;
using System.Diagnostics;

namespace CSharpBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("C# Builder");
            Console.WriteLine("Enter the file name (without extension):");
            string fileName = Console.ReadLine();

            string command = "cmd /c del " + fileName + ".exe 2>NUL && csc /nologo /out:" + fileName + ".exe " + fileName + ".cs";
Process process = new Process();
            process.StartInfo.FileName = "C:\\Windows\\System32\\cmd.exe";
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.Start();

            process.StandardInput.WriteLine(command);
            process.StandardInput.Close();

            string output = process.StandardOutput.ReadToEnd();
            Console.WriteLine(output);

            process.WaitForExit();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
