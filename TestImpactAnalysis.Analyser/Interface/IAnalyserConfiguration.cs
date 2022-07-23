using TestImpactAnalysis.Analyser.DataTransferObjects;

namespace TestImpactAnalysis.Analyser.Interface
{
    public interface IAnalyserConfiguration
    {
        GitRepository GitRepository { get; set; }
    }
}
