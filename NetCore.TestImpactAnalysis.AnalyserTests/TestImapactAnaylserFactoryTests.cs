using NetCore.TestImpactAnalysis.AnalyserTests.Helpers;
using NUnit.Framework;
using System;
using TestImpactAnalysis.Analyser.DataTransferObjects;
using TestImpactAnalysis.Analyser.Implementation;
using TestImpactAnalysis.Analyser.Interface;

namespace TestImpactAnalysis.AnalyserTests
{
    [TestFixture]
    public class TestImapactAnaylserFactoryTests
    {
        private const string repoName = "TestImpactAnalysis.DemoSolution";
        private const string repoPath = @"C:\Airport\Programming\Projects\TestImpactAnalysis.DemoSolution";

        [Test]
        public void TestImapactAnaylserFactory_CreateAnalyser_Sucssful()
        {
            // Arrange - Create analyzerConfiguration object
            var analyzerConfiguration = CreateAnalyzerCongifuation();

            // Act - Create Analyser instance
            var testImpactAnalyserFactory = new TestImpactAnaylserFactory();
            var testImpactAnalyser = testImpactAnalyserFactory.CreateAnalyzerForRepo(analyzerConfiguration);

            // Assert
            Assert.IsNotNull(testImpactAnalyser);
        }

        #region Private Methods

        private static IAnalyserConfiguration CreateAnalyzerCongifuation()
        {
            var gitRepository = new GitRepository(repoName, repoPath);
            var analyzerConfigurationClass = typeof(IAnalyserConfiguration).CreateClassWithPublicProperties("temp", "TestImpactAnalysis.Analyser");
            var analyzerConfiguration = (IAnalyserConfiguration)Activator.CreateInstance(analyzerConfigurationClass);
            analyzerConfiguration.GitRepository = gitRepository;
            return analyzerConfiguration;
        }

        #endregion
    }
}
