using TestImpactAnalysis.Analyser.Interface;

namespace TestImpactAnalysis.Analyser.Implementation
{
    public class TestImpactAnaylserFactory
    {
        public ITestImpactAnaylser CreateAnalyzerForRepo(IAnalyserConfiguration analyserConfiguration)
        {
            var analyser = new TestImpactAnaylser(analyserConfiguration.GitRepository);
            return analyser;
        }
    }
}
