using NUnit.Framework;

using VectorMain;

namespace VectorTest
{
    public class VectorTests
    {
        private Vector _vector1;
        private Vector _vector2;

        [SetUp]
        public void Setup()
        {
            _vector1 = new Vector(new double[] { 4, 3, 2, 1, 2 });
            _vector2 = new Vector(new double[] { 1, 1, 1, 1, 1 });
        }

        [Test]
        public void TestAdd()
        {
            _vector1.Add(_vector2);
            Assert.AreEqual(new Vector(new double[] { 5, 4, 3, 2, 3 }), _vector1);
        }

        [Test]
        public void TestGetScalarProduct()
        {
            Assert.AreEqual(12.0, Vector.GetScalarProduct(_vector1, _vector2), 0.000001, "Результат произведения не верный");
        }
    }
}