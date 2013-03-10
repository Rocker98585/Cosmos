﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cosmos.Build.MSBuild;
using Microsoft.Build.Framework;
using System.Diagnostics;

namespace DebugCompiler
{
  class Program
  {
    static void Main(string[] args)
    {
      var xTimespans = new List<TimeSpan>();
      #region Bench
      for (int i = 0; i < 3; i++)
      {
        var xSW = Stopwatch.StartNew();
        try
        {
          var xTask = new IL2CPUTask();
          xTask.DebugEnabled = true;
          xTask.DebugMode = "Source";
          xTask.TraceAssemblies = "User";
          xTask.DebugCom = 1;
          xTask.UseNAsm = true;
          xTask.OutputFilename = @"C:\Data\Sources\Cosmos\source2\users\Matthijs\Playground\bin\Debug\PlaygroundBoot.asm";
          xTask.EnableLogging = true;
          xTask.EnableLogging = false;
          xTask.EmitDebugSymbols = true;
          xTask.IgnoreDebugStubAttribute = false;
          xTask.References = GetReferences();
          xTask.OnLogError = (m) => Console.WriteLine("Error: {0}", m);
          xTask.OnLogWarning = (m) => Console.WriteLine("Warning: {0}", m);
          xTask.OnLogMessage = (m) => Console.WriteLine("Message: {0}", m);
          xTask.OnLogException = (m) => Console.WriteLine("Exception: {0}", m.ToString());
          if (xTask.Execute())
          {
            Console.WriteLine("Executed OK");
          }
          else
          {
            Console.WriteLine("Errorred");
          }
          xSW.Stop();
          xTimespans.Add(xSW.Elapsed);

        }
        catch (Exception E)
        {
          Console.WriteLine(E.ToString());
          Console.ReadLine();
          return;
        }
      }
      #endregion Bench
      for (int i = 0; i < xTimespans.Count; i++)
      {
        Console.WriteLine("Run {0} took {1}", i + 1, xTimespans[i].ToString());
      }
    }

    private static ITaskItem[] GetReferences()
    {
      return new ITaskItem[]{
        new TaskItemImpl(@"c:\Data\Sources\Cosmos\source2\Users\Matthijs\Playground\bin\Debug\Playground.dll"),
        new TaskItemImpl(@"c:\Data\Sources\Cosmos\source2\Kernel\System\Hardware\Core\Cosmos.Core.Plugs\bin\x86\Debug\Cosmos.Core.Plugs.dll"),
        new TaskItemImpl(@"c:\Data\Sources\Cosmos\source2\Kernel\Debug\Cosmos.Debug.Kernel.Plugs\bin\x86\Debug\Cosmos.Debug.Kernel.Plugs.dll"),
        new TaskItemImpl(@"c:\Data\Sources\Cosmos\source2\Kernel\System\Cosmos.System.Plugs.System\bin\x86\Debug\Cosmos.System.Plugs.System.dll")
      };
    }

    private class TaskItemImpl : ITaskItem
    {
      private string path;
      public TaskItemImpl(string path)
      {
        this.path = path;
      }
      public System.Collections.IDictionary CloneCustomMetadata()
      {
        throw new NotImplementedException();
      }

      public void CopyMetadataTo(ITaskItem destinationItem)
      {
        throw new NotImplementedException();
      }

      public string GetMetadata(string metadataName)
      {
        if (metadataName == "FullPath")
        {
          return path;
        }
        throw new NotImplementedException();
      }

      public string ItemSpec
      {
        get
        {
          throw new NotImplementedException();
        }
        set
        {
          throw new NotImplementedException();
        }
      }

      public int MetadataCount
      {
        get
        {
          return MetadataNames.Count;
        }
      }

      public System.Collections.ICollection MetadataNames
      {
        get
        {
          return new String[] { "FullPath" };
        }
      }

      public void RemoveMetadata(string metadataName)
      {
        throw new NotImplementedException();
      }

      public void SetMetadata(string metadataName, string metadataValue)
      {
        throw new NotImplementedException();
      }
    }
  }
}