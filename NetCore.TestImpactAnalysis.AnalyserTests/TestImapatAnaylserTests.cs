using NetCore.TestImpactAnalysis.AnalyserTests.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TestImpactAnalysis.Analyser.DataTransferObjects;
using TestImpactAnalysis.Analyser.Implementation;
using TestImpactAnalysis.Analyser.Interface;

namespace TestImpactAnalysis.AnalyserTests
{
    [TestFixture]
    public class TestImapatAnaylserTests
    {
        private const string repoName = "TestImpactAnalysis.DemoSolution";
        private const string repoPath = @"C:\Airport\Programming\Projects\TestImpactAnalysis.DemoSolution";
        private const string CommitName = "";
        private const string BranchName = "";
        private const string PullRequestId = "";

        [Test]
        public void AnalyseCommit_NotExistingCommit_ReturnEmptyListOfTests()
        {
            // Arrange - Create Analyser instance
            var analyzerConfiguration = CreateAnalyzerCongifuation();
            var testImpactAnalyserFactory = new TestImapatAnaylserFactory();
            var testImpactAnalyser = testImpactAnalyserFactory.CreateAnalyzerForRepo(analyzerConfiguration);

            // Act
            List<string> testsToRun = testImpactAnalyser.AnalyseBranch(CommitName);

            // Assert
            CollectionAssert.IsEmpty(testsToRun);
        }

        [Test]
        public void AnalyseBranch_NotExistingBranch_ReturnEmptyListOfTests()
        {
            // Arrange - Create Analyser instance
            var analyzerConfiguration = CreateAnalyzerCongifuation();
            var testImpactAnalyserFactory = new TestImapatAnaylserFactory();
            var testImpactAnalyser = testImpactAnalyserFactory.CreateAnalyzerForRepo(analyzerConfiguration);

            // Act
            List<string> testsToRun = testImpactAnalyser.AnalyseBranch(BranchName);

            // Assert
            CollectionAssert.IsEmpty(testsToRun);
        }

        [Test]
        public void AnalysePullRequest_NotExistingPullRequest_ReturnEmptyListOfTests()
        {
            // Arrange - Create Analyser instance
            var analyzerConfiguration = CreateAnalyzerCongifuation();
            var testImpactAnalyserFactory = new TestImapatAnaylserFactory();
            var testImpactAnalyser = testImpactAnalyserFactory.CreateAnalyzerForRepo(analyzerConfiguration);

            // Act
            List<string> testsToRun = testImpactAnalyser.AnalysePullRequest(PullRequestId);

            // Assert
            CollectionAssert.IsEmpty(testsToRun);
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
