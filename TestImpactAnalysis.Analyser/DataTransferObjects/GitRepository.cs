namespace TestImpactAnalysis.Analyser.DataTransferObjects
{
    public class GitRepository
    {
        public string Name { get; }
        public string Path { get; }

        public GitRepository(string name, string path)
        {
            Name = name;
            Path = path;
        }
    }
}
