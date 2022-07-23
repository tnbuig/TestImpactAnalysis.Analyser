using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace NetCore.TestImpactAnalysis.AnalyserTests.Helpers
{
    public static class RoslynExtensions
    {
        public static Type CreateClassWithPublicProperties(this Type baseType, string newClassName, string newNamespace = "Magic")
        {
            var concreteProperties = baseType.GetProperties().Select(
               property =>
               {
                   var methodString = $"public {property.Name} {property.Name} {{get; set;}}";
                   return methodString.Trim();
               });

            var concreteMethodsString = concreteProperties
                                        .ToString(Environment.NewLine + Environment.NewLine);

            var classCode = $@"
            using System;

            namespace {newNamespace}
            {{
                public class {newClassName}: {baseType.FullName}
                {{
                    public {newClassName}()
                    {{
                    }}

                    {concreteMethodsString}
                }}
            }}
            ".Trim();

            classCode = FormatUsingRoslyn(classCode);

            Assembly[] assembliesCollection = AppDomain
            .CurrentDomain
            .GetAssemblies();

            List<PortableExecutableReference> filterdAssemblies = new List<PortableExecutableReference>();

            foreach (var assem in assembliesCollection)
            {
                try
                {
                    if (!string.IsNullOrEmpty(assem.Location))
                    {
                        filterdAssemblies.Add(MetadataReference.CreateFromFile(assem.Location));
                    }
                }
                catch (Exception)
                {


                }
            }

            PortableExecutableReference[] assemblies = filterdAssemblies.ToArray();

            var syntaxTree = CSharpSyntaxTree.ParseText(classCode);

            var compilation = CSharpCompilation
                                .Create(newNamespace)
                                .AddSyntaxTrees(syntaxTree)
                                .AddReferences(assemblies)
                                .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using (var ms = new MemoryStream())
            {
                var result = compilation.Emit(ms);
                //compilation.Emit($"C:\\Temp\\{newNamespace}.dll");

                if (result.Success)
                {
                    ms.Seek(0, SeekOrigin.Begin);
                    Assembly assembly = Assembly.Load(ms.ToArray());

                    var newTypeFullName = $"{newNamespace}.{newClassName}";

                    var type = assembly.GetType(newTypeFullName);
                    return type;
                }
                else
                {
                    IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
                        diagnostic.IsWarningAsError ||
                        diagnostic.Severity == DiagnosticSeverity.Error);

                    foreach (Diagnostic diagnostic in failures)
                    {
                        Console.Error.WriteLine("{0}: {1}", diagnostic.Id, diagnostic.GetMessage());
                    }

                    return null;
                }
            }
        }

        public static string ToString(this IEnumerable<string> list, string separator)
        {
            string result = string.Join(separator, list);
            return result;
        }

        public static string FormatUsingRoslyn(string csCode)
        {
            var tree = CSharpSyntaxTree.ParseText(csCode);
            var root = tree.GetRoot().NormalizeWhitespace();
            var result = root.ToFullString();
            return result;
        }
    }

}