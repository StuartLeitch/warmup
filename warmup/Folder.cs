// Copyright 2007-2010 The Apache Software Foundation.
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
using System.Globalization;
namespace warmup
{
    using System;
    using System.IO;

    internal class Folder :
        IExporter
    {
        public void Export(string sourceControlWarmupLocation, string templateName, TargetDir targetDir)
        {
            string baseDir = Path.Combine(sourceControlWarmupLocation, templateName);
            Console.WriteLine("Copying to: {0}", targetDir.FullPath);
            CopyDirectory(baseDir, targetDir.FullPath);
        }

        public static void CopyDirectory(string source, string destination)
        {
            if (source.EndsWith("\\build")) return;
            if (source.EndsWith("\\bin")) return;
            if (source.EndsWith("\\debug")) return;
            if (source.EndsWith("\\obj")) return;
            if (source.Contains("ReSharper")) return;
            if (source.EndsWith("\\DeploymentPackages")) return;
            if (source.EndsWith("\\.git") || source.Contains("\\.hg")) return;
            if (destination[destination.Length - 1] != Path.DirectorySeparatorChar)
                destination += Path.DirectorySeparatorChar;
            if (!Directory.Exists(destination)) Directory.CreateDirectory(destination);
            string[] files = Directory.GetFileSystemEntries(source);
            foreach (var element in files)
            {
                // Sub directories

                if (element.EndsWith(".obj", true, new CultureInfo("en-US"))) continue;
                if (element.EndsWith(".pdb", true, new CultureInfo("en-US"))) continue;
                if (element.EndsWith(".user", true, new CultureInfo("en-US"))) continue;
                if (element.EndsWith(".suo", true, new CultureInfo("en-US"))) continue;
                if (element.EndsWith(".bak", true, new CultureInfo("en-US"))) continue;
                if (element.EndsWith(".cache", true, new CultureInfo("en-US"))) continue;
                if (element.EndsWith(".log", true, new CultureInfo("en-US"))) continue;
                if (element.EndsWith(".zip", true, new CultureInfo("en-US"))) continue;
                if (element.EndsWith(".user", true, new CultureInfo("en-US"))) continue;
                if (element.EndsWith(".local.xml", true, new CultureInfo("en-US"))) continue;
                if (element.EndsWith(".Local.testSettings", true, new CultureInfo("en-US"))) continue;
                if (element.EndsWith(".Publish.xml", true, new CultureInfo("en-US"))) continue;
                if (Directory.Exists(element))
                    CopyDirectory(element, destination + Path.GetFileName(element));
                    // Files in directory

                else
                    File.Copy(element, destination + Path.GetFileName(element), true);
            }
        }
    }
}