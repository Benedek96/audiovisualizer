﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AudioVisualizer;

namespace test.managed
{
    [TestClass]
    public class DataClassTests
    {
        [TestMethod]
        public void ScalarData()
        {
            var data = new ScalarData(2);
            Assert.AreEqual(2, data.Count());
            Assert.AreEqual(2, data.Count);
            Assert.AreEqual(ScaleType.Linear, data.AmplitudeScale);
            Assert.AreEqual(0.0f,data[0]);
            Assert.AreEqual(0.0f, data[1]);
            CollectionAssert.AreEqual(new float[] { 0.0f, 0.0f }, data.AsEnumerable().ToArray());

            var logData = data.ConvertToLogAmplitude(-50, 0);
            Assert.AreEqual(2, logData.Count());
            Assert.AreEqual(2, data.Count);
            Assert.AreEqual(ScaleType.Logarithmic, logData.AmplitudeScale);
            Assert.AreEqual(-50.0f, logData[0]);
            Assert.AreEqual(-50.0f, logData[1]);
            CollectionAssert.AreEqual(new float[] { -50.0f, -50.0f }, logData.AsEnumerable().ToArray());
        }
    }
}