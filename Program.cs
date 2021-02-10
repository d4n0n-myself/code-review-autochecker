using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using LibGit2Sharp;

namespace CodeReviewAutochecker
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            const string path = "E:\\GitHub\\super-duper-test";
            var fileToCheckPaths = GetFilesToBeAnalysed(path);

            var outputFilePath = RunAnalysis(path, fileToCheckPaths);

            var report = ReadResult(outputFilePath);

            // var resultStr = PrettyPrint(report)
            
            // do what ever u want with this analysis
            
            // todo: 
            // 1) add full resharper object API from docs
            // 2) separate this method by extracting an interface with a property of enum to hold the provider of analysis
            // 3) implement pretty print for resharper
            // 4) make config to read variables from
        }

        private static string RunAnalysis(string path, string[] fileToCheckPaths)
        {
            var pathToSolution = Directory
                .GetFiles(path, "*.sln", SearchOption.AllDirectories)
                .Single();
            var desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var outputFilePath = $"{desktop}\\result.xml";
            var command =
                $"/C jb inspectcode {pathToSolution}" +
                $" --output={outputFilePath}" +
                $" --no-swea" +
                $" --include={string.Join(';', fileToCheckPaths)}";

            try
            {
                using var process = System.Diagnostics.Process.Start("CMD.exe", command);
                if (process == null)
                    throw new Exception("gfy");

                while (true)
                {
                    if (process.HasExited)
                        break;
                    process.WaitForExit(1000);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return outputFilePath;
        }

        private static string[] GetFilesToBeAnalysed(string path)
        {
            var discover = Repository.Discover(path);
            if (discover == null)
                throw new ArgumentException("No git in that folder and no git in the upper folders!");

            using var repo = new Repository(discover);

            var oldTree = repo.Branches["master"].Tip.Tree;
            var newTree = repo.Branches["oh-no-pls-test"].Tip.Tree;
            var compareOptions = new CompareOptions
            {
                // IncludeUnmodified = false
            };
            var changeCollection = repo.Diff.Compare<Patch>(oldTree, newTree,
                compareOptions);
            var applicableStatuses = new[] {ChangeKind.Added, ChangeKind.Copied, ChangeKind.Modified, ChangeKind.TypeChanged};
            var bannedExtensions = new[] {".sln", ".gitignore", ".git", ".csproj"};
            return changeCollection
                .Where(x => applicableStatuses.Contains(x.Status))
                .Select(x => "**" + x.Path)
                .Where(x => bannedExtensions.All(e => !x.EndsWith(e)))
                .ToArray();
        }

        private static Report ReadResult(string outputFilePath)
        {
            var text = File.ReadAllText(outputFilePath);
            var serializer = new XmlSerializer(typeof(Report));
            using var reader = new StringReader(text);
            return (Report) serializer.Deserialize(reader);
        }
    }
}