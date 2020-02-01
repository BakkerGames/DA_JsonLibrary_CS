﻿using DA_JsonLibrary_CS;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UnitTestProjectJson_CS
{
    [TestClass]
    public class UnitTestJObject
    {
        [TestMethod]
        public void TestNullJObjectWithWhitespace()
        {
            // arrange
            string actualValue;
            string expectedValue = "{}";
            JObject jo = new JObject();
            // act
            actualValue = jo.ToString(JsonFormat.Space);
            // assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void TestNullJObjectNoWhitespace()
        {
            // arrange
            string actualValue;
            string expectedValue = "{}";
            JObject jo = new JObject();
            // act
            actualValue = jo.ToString();
            // assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void TestNullJObjectDefaultWhitespace()
        {
            // arrange
            string actualValue;
            string expectedValue = "{}";
            JObject jo = new JObject();
            // act
            actualValue = jo.ToString();
            // assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void TestJObjectNullValue()
        {
            // arrange
            string actualValue;
            string expectedValue = "{\"key\":null}";
            JObject jo = new JObject();
            jo.Add("key", null);
            // act
            actualValue = jo.ToString();
            // assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void TestJObjectFalseValue()
        {
            // arrange
            string actualValue;
            string expectedValue = "{\"key\":false}";
            JObject jo = new JObject();
            jo.Add("key", false);
            // act
            actualValue = jo.ToString();
            // assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void TestJObjectTrueValue()
        {
            // arrange
            string actualValue;
            string expectedValue = "{\"key\":true}";
            JObject jo = new JObject();
            jo.Add("key", true);
            // act
            actualValue = jo.ToString();
            // assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void TestJObjectStringValue()
        {
            // arrange
            string actualValue;
            string expectedValue = "{\"key\":\"abc\"}";
            JObject jo = new JObject();
            jo.Add("key", "abc");
            // act
            actualValue = jo.ToString();
            // assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void TestJObjectStringValueCtrlChars()
        {
            // arrange
            string actualValue;
            string expectedValue = "{\"key\":\"\\r\\n\\t\\b\\f\\u1234\"}";
            JObject jo = new JObject();
            jo.Add("key", "\r\n\t\b\f\u1234");
            // act
            actualValue = jo.ToString();
            // assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void TestJObjectIntValue()
        {
            // arrange
            string actualValue;
            string expectedValue = "{\"key\":123}";
            JObject jo = new JObject();
            jo.Add("key", 123);
            // act
            actualValue = jo.ToString();
            // assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void TestJObjectIntValueWhitespace()
        {
            // arrange
            string actualValue;
            string expectedValue = "{\r\n  \"key\": 123\r\n}";
            JObject jo = new JObject();
            jo.Add("key", 123);
            // act
            actualValue = jo.ToString(JsonFormat.Space);
            // assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void TestJObjectDoubleValue()
        {
            // arrange
            string actualValue;
            string expectedValue = "{\"key\":123.45}";
            JObject jo = new JObject();
            jo.Add("key", 123.45);
            // act
            actualValue = jo.ToString();
            // assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void TestJObjectDoubleExponentValue()
        {
            // arrange
            double actualValue;
            double expectedValue = 1.2345e50;
            JObject jo = JObject.Parse("{\"key\":1.2345e50}");
            // act
            actualValue = (double)jo["key"];
            // assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void TestJObjectDoubleExponentNegativeValue()
        {
            // arrange
            double actualValue;
            double expectedValue = 1.2345e-50;
            JObject jo = JObject.Parse("{\"key\":1.2345e-50}");
            // act
            actualValue = (double)jo["key"];
            // assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void TestJObjectDoubleExponentUpperValue()
        {
            // arrange
            double actualValue;
            double expectedValue = 1.2345E50;
            JObject jo = JObject.Parse("{\"key\":1.2345E50}");
            // act
            actualValue = (double)jo["key"];
            // assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void TestJObjectDoubleExponentUpperNegativeValue()
        {
            // arrange
            double actualValue;
            double expectedValue = 1.2345E-50;
            JObject jo = JObject.Parse("{\"key\":1.2345E-50}");
            // act
            actualValue = (double)jo["key"];
            // assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void TestJObjectDateValue()
        {
            // arrange
            string actualValue;
            string expectedValue = "{\"key\":\"2017-01-02\"}";
            JObject jo = new JObject();
            jo.Add("key", DateTime.Parse("01/02/2017"));
            // act
            actualValue = jo.ToString();
            // assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void TestJObjectDatetimeValue()
        {
            // arrange
            string actualValue;
            string expectedValue = "{\"key\":\"2017-01-02 16:42:25\"}"; // difference, has no T or .0000000
            JObject jo = new JObject();
            jo.Add("key", DateTime.Parse("01/02/2017 16:42:25"));
            // act
            actualValue = jo.ToString();
            // assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void TestJObjectJObjectValue()
        {
            // arrange
            string actualValue;
            string expectedValue = "{\"key\":{\"newkey\":456}}";
            JObject jo = new JObject();
            JObject jo2 = new JObject();
            jo2.Add("newkey", 456);
            jo.Add("key", jo2);
            // act
            actualValue = jo.ToString();
            // assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void TestJObjectJArrayValue()
        {
            // arrange
            string actualValue;
            string expectedValue = "{\"key\":[\"newkey\",456]}";
            JObject jo = new JObject();
            JArray ja = new JArray();
            ja.Add("newkey");
            ja.Add(456);
            jo.Add("key", ja);
            // act
            actualValue = jo.ToString();
            // assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void TestJObjectMultiValue()
        {
            // arrange
            string actualValue;
            string expectedValue = "{\"key\":123,\"otherkey\":789.12}";
            JObject jo = new JObject();
            jo.Add("key", 123);
            jo.Add("otherkey", 789.12);
            // act
            actualValue = jo.ToString();
            // assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void TestJObjectMultiValueWhitespace()
        {
            // arrange
            string actualValue;
            string expectedValue = "{\r\n  \"key\": 123,\r\n  \"otherkey\": 789.12\r\n}";
            JObject jo = new JObject();
            jo.Add("key", 123);
            jo.Add("otherkey", 789.12);
            // act
            actualValue = jo.ToString(JsonFormat.Space);
            // assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void TestJObjectNewEmpty()
        {
            // arrange
            string actualValue;
            string expectedValue = "{}";
            JObject jo = JObject.Parse(expectedValue);
            // act
            actualValue = jo.ToString(JsonFormat.Space);
            // assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void TestJObjectNewValues()
        {
            // arrange
            string actualValue;
            string expectedValue = "{\r\n  \"key\": 123,\r\n  \"otherkey\": 789.12\r\n}";
            JObject jo = JObject.Parse(expectedValue);
            // act
            actualValue = jo.ToString(JsonFormat.Space);
            // assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void TestJObjectItemArray()
        {
            // arrange
            string actualValue;
            string expectedValue = "{\"array\":[1,2,3,4]}";
            int[] ia = { 1, 2, 3, 4 };
            JObject jo = new JObject();
            jo.Add("array", ia);
            // act
            actualValue = jo.ToString();
            // assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void TestJObjectItemList()
        {
            // arrange
            string actualValue;
            string expectedValue = "{\"list\":[1,2,3,4]}";
            List<int> ia = new List<int> { 1, 2, 3, 4 };
            JObject jo = new JObject();
            jo.Add("list", ia);
            // act
            actualValue = jo.ToString();
            // assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void TestJObjectMerge()
        {
            // arrange
            string actualValue;
            string expectedValue = "{\"firstitem\":1,\"seconditem\":2,\"thirditem\":3,\"fourthitem\":4}";
            JObject jo1 = new JObject()
            {
                { "firstitem", 1 },
                { "seconditem", 2 }
            };
            JObject jo2 = new JObject(jo1)
            {
                { "thirditem", 3 },
                { "fourthitem", 4 }
            };
            // act
            actualValue = jo2.ToString();
            // assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void TestJObjectMergeWithUpdate()
        {
            // arrange
            string actualValue;
            string expectedValue = "{\"firstitem\":3,\"seconditem\":2,\"fourthitem\":4}";
            JObject jo1 = new JObject()
            {
                { "firstitem", 1 },
                { "seconditem", 2 }
            };
            JObject jo2 = new JObject()
            {
                { "firstitem", 3 },
                { "fourthitem", 4 }
            };
            jo1.Merge(jo2);
            // act
            actualValue = jo1.ToString();
            // assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void TestJObjectWithComments()
        {
            // arrange
            string actualValue;
            string expectedValue = "{\"key\":\"value\"}";
            JObject jo = JObject.Parse("//comment\r\n{//comment\r\n\"key\"/*comment*/:/*comment*/\"value\"//comment\r\n}//comment");
            // act
            actualValue = jo.ToString();
            // assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void TestJObjectWhitespace()
        {
            // arrange
            string actualValue;
            string expectedValue = "{\"key1\":\"value\",\"key2\":1234,\"key3\":false}";
            JObject jo = JObject.Parse(" { \"key1\" : \"value\" , \"key2\" : 1234 , \"key3\" : false } ");
            // act
            actualValue = jo.ToString();
            // assert
            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}
