using  Topic.Invoice;
namespace TestProject1
{
    public class TestInvoiceMatching
    {
        public InvoiceMatching �����o��;
        [SetUp]
        public void Setup()
        {
             �����o�� = new InvoiceMatching();
        }

        [Test]
        public void �����D��1()
        {
            �����o��.�D��1();
            Assert.Pass();
        }

        [Test]
        public void �����D��2()
        {
            �����o��.�D��2();
            Assert.Pass();
        }

        [Test]
        public void �����D��3()
        {
            �����o��.�D��3();
            Assert.Pass();
        }
    }
}