using System;

using V2.Di.FunctionApp.Modules;

namespace V2.Di.FunctionApp.Containers
{
    /// <summary>
    /// This provides interfaces to the <see cref="ContainerBuilder"/> class.
    /// </summary>
    public interface IContainerBuilder
    {
        /// <summary>
        /// Registers a dependency collection module.
        /// </summary>
        /// <param name="module"><see cref="IModule"/> instance.</param>
        /// <returns>Returns <see cref="IContainerBuilder"/> instance.</returns>
        IContainerBuilder RegisterModule(IModule module = null);

        /// <summary>
        /// Builds the dependency container.
        /// </summary>
        /// <returns>Returns <see cref="IServiceProvider"/> instance.</returns>
        IServiceProvider Build();
    }
}