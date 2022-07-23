using TestImpactAnalysis.Analyser.Interface;

namespace TestImpactAnalysis.Analyser.Implementation
{
    public class TestImapatAnaylserFactory
    {
        public ITestImpactAnaylser CreateAnalyzerForRepo(IAnalyserConfiguration analyserConfiguration)
        {
            var analyser = new TestImpactAnaylser(analyserConfiguration.GitRepository);
            return analyser;
        }
    }
}
