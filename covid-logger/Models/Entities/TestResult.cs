using Google.Protobuf.Reflection;

namespace ResidentLog.Models.Entities
{
    public class TestResult
    {
        public int TestResultType { get; set; }
        public string TestResultDescription { get; set; }

        public TestResult(int type, string description)
        {
            TestResultType = type;
            TestResultDescription = description;
        }
    }
}