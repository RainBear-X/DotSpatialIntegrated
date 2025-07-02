using Core;
using DotSpatial.Controls;

namespace Modules
{
    public interface IRunnableModule
    {
        string Name { get; }
        string Category { get; }
        void Run();
    }
}