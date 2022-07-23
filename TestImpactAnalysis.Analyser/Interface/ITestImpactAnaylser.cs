using System.Collections.Generic;

namespace TestImpactAnalysis.Analyser.Interface
{
    public interface ITestImpactAnaylser
    {
        List<string> AnalyseBranch(string branchName);
        List<string> AnalysePullRequest(string pullRequestId);
    }
}
