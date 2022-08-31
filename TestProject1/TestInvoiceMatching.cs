using  Topic.Invoice;
namespace TestProject1
{
    public class TestInvoiceMatching
    {
        public InvoiceMatching い贱祇布;
        [SetUp]
        public void Setup()
        {
             い贱祇布 = new InvoiceMatching();
        }

        [Test]
        public void 代刚肈ヘ1()
        {
            い贱祇布.肈ヘ1();
            Assert.Pass();
        }

        [Test]
        public void 代刚肈ヘ2()
        {
            い贱祇布.肈ヘ2();
            Assert.Pass();
        }

        [Test]
        public void 代刚肈ヘ3()
        {
            い贱祇布.肈ヘ3();
            Assert.Pass();
        }
    }
}