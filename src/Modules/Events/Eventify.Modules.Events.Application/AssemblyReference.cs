using System.Reflection;

namespace Eventify.Modules.Events.Application;
public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
