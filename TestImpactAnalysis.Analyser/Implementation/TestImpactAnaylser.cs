using System.Collections.Generic;
using TestImpactAnalysis.Analyser.DataTransferObjects;
using TestImpactAnalysis.Analyser.Interface;

namespace TestImpactAnalysis.Analyser.Implementation
{
    public class TestImpactAnaylser : ITestImpactAnaylser
    {
        private GitRepository _gitRepository;

        public TestImpactAnaylser(GitRepository gitRepository)
        {
            _gitRepository = gitRepository;
        }

        public List<string> AnalyseBranch(string branchName)
        {
            var result = new List<string>();

            if (string.IsNullOrWhiteSpace(branchName))
            {
                return result;
            }

            return result;
        }

        public List<string> AnalysePullRequest(string pullRequestId)
        {
            var result = new List<string>();

            if (string.IsNullOrWhiteSpace(pullRequestId))
            {
                return result;
            }

            return result;
        }
    }
}
