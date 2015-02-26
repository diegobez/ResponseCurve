using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using UnityEngine;

namespace UnityTest
{
  [TestFixture]
  [Category("Unit Tests")]
  internal class ResponseCurveTest
  {
    [Test]
    [Category("Response Curve Tests")]
    [MaxTime(100)]
    public void ExceptionTest()
    {
      ResponseCurve responseCurve = (new GameObject()).AddComponent<ResponseCurve>();
      responseCurve.Initialize( new float[]{ 0, 3, 4, 7, 10 }, 
                                new float[]{ 0, 3, 1, -4, 3 });

      Assert.AreEqual(responseCurve.GetValue(1.5f), 1.5f);
      Assert.AreEqual(responseCurve.GetValue(3.0f), 3.0f);
      Assert.AreEqual(responseCurve.GetValue(0.0f), 0.0f);
      Assert.AreEqual(responseCurve.GetValue(10.0f), 3.0f);
      Assert.AreEqual(responseCurve.GetValue(-3.0f), 0.0f);
      Assert.AreEqual(responseCurve.GetValue(26.0f), 3.0f);
      Assert.AreEqual(responseCurve.GetValue(3.7f),  3 + ( 1 - 3 ) * ( 3.7f - 3 ) /( 4 - 3 ) );
      Assert.AreEqual(responseCurve.GetValue(8.5f),  -0.5f);
      Assert.Pass();
    }    
  }
}
