using DA_JsonLibrary_CS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProjectJson_CS
{
    [TestClass]
    public class UnitTestParse
    {
        [TestMethod]
        public void TestJBaseParseJArrayEmpty()
        {
            object testObj = JArray.Parse("[]");
            Assert.AreEqual(testObj.ToString(), "[]");
        }

        [TestMethod]
        public void TestJBaseParseJArrayExtraCommas()
        {
            object testObj = JArray.Parse("[ , , , ]");
            Assert.AreEqual(testObj.ToString(), "[]");
        }

        [TestMethod]
        public void TestJBaseParseJObjectEmpty()
        {
            object testObj = JObject.Parse("{}");
            Assert.AreEqual(testObj.ToString(), "{}");
        }

        [TestMethod]
        public void TestJBaseParseJObjectExtraCommas()
        {
            object testObj = JObject.Parse("{ , , , }");
            Assert.AreEqual(testObj.ToString(), "{}");
        }

        [TestMethod]
        public void TestJBaseParseJArrayWhitespace1()
        {
            object testObj = JArray.Parse(" [ ] ");
            Assert.AreEqual(testObj.ToString(), "[]");
        }

        [TestMethod]
        public void TestJBaseParseJArrayWhitespace2()
        {
            object testObj = JArray.Parse(" [ 1 ] ");
            Assert.AreEqual(testObj.ToString(), "[1]");
        }

        [TestMethod]
        public void TestJBaseParseJArrayWhitespace3()
        {
            object testObj = JArray.Parse(" [ 1 , 2 , 3 ] ");
            Assert.AreEqual(testObj.ToString(), "[1,2,3]");
        }

        [TestMethod]
        public void TestJBaseParseJArrayWhitespace4()
        {
            object testObj = JArray.Parse(" [ 1 , 2 , 3 , ] ");
            Assert.AreEqual(testObj.ToString(), "[1,2,3]");
        }

        [TestMethod]
        public void TestJBaseParseJObjectWhitespace1()
        {
            object testObj = JObject.Parse(" { } ");
            Assert.AreEqual(testObj.ToString(), "{}");
        }

        [TestMethod]
        public void TestJBaseParseJObjectWhitespace2()
        {
            object testObj = JObject.Parse(" { \"key\" : null } ");
            Assert.AreEqual(testObj.ToString(), "{\"key\":null}");
        }

        [TestMethod]
        public void TestJBaseParseJObjectWhitespace3()
        {
            object testObj = JObject.Parse(" { \"key1\" : 1 , \"key2\" : 2 , \"key3\" : 3 } ");
            Assert.AreEqual(testObj.ToString(), "{\"key1\":1,\"key2\":2,\"key3\":3}");
        }

        [TestMethod]
        public void TestJBaseParseJObjectWhitespace4()
        {
            object testObj = JObject.Parse(" { \"key1\" : 1 , \"key2\" : 2 , \"key3\" : 3 } , ");
            Assert.AreEqual(testObj.ToString(), "{\"key1\":1,\"key2\":2,\"key3\":3}");
        }
    }
}
