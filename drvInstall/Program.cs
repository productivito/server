// Type: drvInstall.Program
// Assembly: drvInstall, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// Assembly location: J:\share\New folder\drvInstall.exe

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace drvInstall
{
  internal class Program
  {
    public static string StartupPath
    {
      get
      {
        return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
      }
    }

    public static bool Is64Bit()
    {
      return IntPtr.Size == 8;
    }

    public static string GetHttpServiceName()
    {
      return !Program.Is64Bit() ? "netfilter2" : "netfilter2_64";
    }

    private static void Main(string[] args)
    {
      try
      {
        if (Environment.CommandLine.EndsWith("doinstall"))
        {
          Program.InstallHTTPDriver();
        }
        else
        {
          if (!Environment.CommandLine.EndsWith("douninstall"))
            return;
          Program.UninstallHTTPDriver();
        }
      }
      catch (Exception ex)
      {
        Trace.WriteLine((object) ex);
        throw;
      }
    }

    private static void InstallHTTPDriver()
    {
      string destFileName = Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), string.Format("System32\\drivers\\{0}.sys", (object) Program.GetHttpServiceName()));
      if (Program.Is64Bit())
      {
        Trace.WriteLine("Install system 64");
        string sourceFileName = Path.Combine(Program.StartupPath, "netfilter2_64.sys");
        Trace.WriteLine(string.Format("{0} -> {1} ", (object) sourceFileName, (object) destFileName));
        try
        {
          File.Copy(sourceFileName, destFileName, true);
        }
        catch (Exception ex)
        {
          Trace.WriteLine("Fail to copy driver files: " + ex.Message);
        }
      }
      else
      {
        Trace.WriteLine("Install system 32");
        File.Copy(Path.Combine(Program.StartupPath, "netfilter2.sys"), destFileName, true);
      }
    }

    private static void UninstallHTTPDriver()
    {
      string path = Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), string.Format("System32\\drivers\\{0}.sys", (object) Program.GetHttpServiceName()));
      if (File.Exists(path))
        File.Delete(path);
      else
        Trace.WriteLine("Cannot find driver file to delete");
    }
  }
}
