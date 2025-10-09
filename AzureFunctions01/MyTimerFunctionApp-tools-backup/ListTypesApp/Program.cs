using System;
using System.Reflection;

class Program {
    static void Main() {
        var asm = Assembly.LoadFrom("src\\Microsoft.Azure.Functions.Worker.Extensions.Timer.dll");
        foreach (var t in asm.GetExportedTypes()){
            Console.WriteLine(t.FullName);
            foreach(var m in t.GetMethods(BindingFlags.Public|BindingFlags.Static|BindingFlags.Instance|BindingFlags.DeclaredOnly)){
                Console.WriteLine("  " + m.Name + " (" + (m.IsStatic?"static":"instance") + ")");
            }
        }
    }
}
