using System;
using System.Linq;
using System.Net.Http;

namespace V1.Di.FunctionApp.Functions.FunctionOptions
{
    /// <summary>
    /// This represents the options entity for the <see cref="AutofacGitHubRepositoriesHttpTrigger"/> class.
    /// </summary>
    public class GitHubRepositoriesHttpTriggerOptions : FunctionOptionsBase
    {
        private readonly HttpRequestMessage _req;

        /// <summary>
        /// Initializes a new instance of the <see cref="GitHubRepositoriesHttpTriggerOptions"/> class.
        /// </summary>
        /// <param name="req"></param>
        public GitHubRepositoriesHttpTriggerOptions(HttpRequestMessage req)
        {
            this._req = req;
        }

        /// <summary>
        /// Gets the repository type - users or organisations.
        /// </summary>
        public string Type => this.GetRepositoryType();

        /// <summary>
        /// Gets the repository name.
        /// </summary>
        public string Name => this.GetRepositoryName();

        private string GetRepositoryType()
        {
            var type = this._req.GetQueryNameValuePairs()
                                .SingleOrDefault(p => p.Key.Equals("type", StringComparison.CurrentCultureIgnoreCase))
                                .Value;

            return type;
        }

        private string GetRepositoryName()
        {
            var name = this._req.GetQueryNameValuePairs()
                                .SingleOrDefault(p => p.Key.Equals("name", StringComparison.CurrentCultureIgnoreCase))
                                .Value;

            return name;
        }
    }
}