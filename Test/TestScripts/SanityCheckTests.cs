namespace TestScripts.Client
{
    [TestFixture]
    public class SanityCheckTests
    {
        public SanityCheckTests() { }
        // No data-driven test cases in this file, but class structure is now consistent with the pattern.
        [Test]
        public void Test_Pipeline_Sanity()
        {
            Assert.That(true, "Pipeline sanity test passed.");
            int result = 2 + 3;
            Assert.That(result, Is.EqualTo(5), "Addition test passed.");
        }
    }
}