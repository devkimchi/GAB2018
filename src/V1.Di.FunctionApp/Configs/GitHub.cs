using System;

namespace V1.Di.FunctionApp.Configs
{
    /// <summary>
    /// This represents the config entity for GitHub API.
    /// </summary>
    public class GitHub
    {
        /// <summary>
        /// Gets or sets the base URL.
        /// </summary>
        public virtual string BaseUrl => Environment.GetEnvironmentVariable("BaseUrl");

        /// <summary>
        /// Gets or sets the set of endpoints.
        /// </summary>
        public virtual Endpoints Endpoints => new Endpoints();
    }

    /// <summary>
    /// This represents the config entity for GitHub API endpoints.
    /// </summary>
    public class Endpoints
    {
        /// <summary>
        /// Gets or sets the repository endpoint.
        /// </summary>
        public virtual string Repositories => Environment.GetEnvironmentVariable("Repositories");
    }
}
