using RapidFireTest.Core;
using RapidFireTest.Core.Extension;
using RapidFireTest.Model;

namespace Test
{
    [TestClass]
    public class TestSample : ITestBase<Calculator>
    {
        public override void SetDataRow(DataRowsRF DataRows)
        {
            DataRows.Set(TestMethod1, 10, 5, 5)
                .AddRow(12, 6, 6)
                .AddRow(12, 6, 7);
        }
        RapidFireTest unit = new(TestType.Unit);

        [TestMethodRF]
        public void TestMethod1(int res, int a, int b)
        {
            unit.Run(new TestCase
            {
                AcceptanceCriteriaId = "STR-1",
                Invoke = Invoke.Action(x => new Calculator().Add(a, b)),
                Assert = TestAssert.Value.EqualTo(res),
            });
        }
    }
}