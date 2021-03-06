
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonSharp.Tests
{
    [AddComponentMenu("Udon Sharp/Tests/LocalFunctionTest")]
    public class LocalFunctionTest : UdonSharpBehaviour
    {
        [System.NonSerialized]
        public IntegrationTestSuite tester;

        int eventCounter;
        LocalFunctionTest selfReference;

        public void ExecuteTests()
        {
            selfReference = this;
            TestCustomEvents();
            TestFunctionParameters();
            TestIntermediateReturn();
        }

        int GetCountAmount() => 4;

        public void IncrementCounter()
        {
            eventCounter += GetCountAmount();
        }

        void TestCustomEvents()
        {
            eventCounter = 0;

            SendCustomEvent("IncrementCounter");
            SendCustomEvent("IncrementCounter");
            SendCustomEvent("IncrementCounter");

            tester.TestAssertion("Custom Event Calls", eventCounter == 12);

            selfReference.SendCustomEvent("IncrementCounter");
            tester.TestAssertion("Reference Custom Event Calls", eventCounter == 16);
        }

        int AddIntegers(int a, int b)
        {
            ++a;
            return a + b;
        }

        void TestFunctionParameters()
        {
            int a = 4;
            int addResult = AddIntegers(a, 6);
            tester.TestAssertion("Function parameters", addResult == 11 && a == 4);
        }

        int AddIntegers2(int a, int b)
        {
            return a + b;
        }

        void TestIntermediateReturn()
        {
            int result = AddIntegers2(2, 4) + AddIntegers2(6, 9);
            tester.TestAssertion("Method Intermediate Return Value", result == 21);
        }
    }
}
