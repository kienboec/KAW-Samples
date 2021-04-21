using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Composition;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using PluginArchitectureInDotNetCore.MEF.Common;

namespace PluginArchitectureInDotNetCore.MEF.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Run();
        }

        public void Run()
        {
            Compose();
            string consoleInput;
            do
            {
                Console.WriteLine("enter command (exec, exit)");
                Console.Write("command> ");
                consoleInput = Console.ReadLine();
                Console.WriteLine();
                
                if (consoleInput == "exec")
                {
                    Commands.ToList().ForEach(command => command.Execute());
                }
            } while (consoleInput != "exit");
        }

        private List<Assembly> GetAssemblies()
        {
            var executableLocation = Assembly.GetEntryAssembly()?.Location;
            var path = Path.Combine(Path.GetDirectoryName(executableLocation), "plugins");
            var allAssemblyFilePaths = Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories);
            var allAssemblies = allAssemblyFilePaths.Select(x => AssemblyLoadContext.Default.LoadFromAssemblyPath(x)).ToList();

            return allAssemblies;
        }

        [ImportMany]
        public IEnumerable<ICommand> Commands { get; set; }

        private void Compose()
        {
            using var container = new ContainerConfiguration().WithAssemblies(GetAssemblies()).CreateContainer();
            Commands = container.GetExports<ICommand>().ToList();
        }

        // https://docs.microsoft.com/en-us/dotnet/core/tutorials/creating-app-with-plugin-support
        private void Compose2()
        {
            Commands = GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => typeof(ICommand).IsAssignableFrom(type))
                .Select(type => (ICommand)Activator.CreateInstance(type))
                .ToList();
        }
    }
}