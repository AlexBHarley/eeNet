using System;
using System.Collections.Generic;
using eeNet;
using NUnit.Framework;

namespace EventEmitterTests
{
    [TestFixture]
    public class EventEmitterTests
    {
        private int numberToTest = 0;
        private int numberToTest2 = 0;

        #region EmitTests

        [Test]
        public void DoesNotEmitIfEventDoesntExist()
        {
            var e = new EventEmitter();
            Assert.That(() => e.Emit("test_event", "test data"),
                Throws.Exception
                  .TypeOf<DoesNotExistException>()
                  .With.Message.EqualTo("Event [test_event] does not exist in the emitter. Consider calling EventEmitter.On"));
        }

        [Test]
        public void EmitCallsAllAttachedFunctions()
        {
            var e = new EventEmitter();
            numberToTest = 0;
            e.On("data", IncrementNumber);
            e.On("data", IncrementNumberBy2);
            e.Emit("data", numberToTest);
            Assert.AreEqual(3, numberToTest);
        }

        #endregion

        #region RemoveListenerTests

        [Test]
        public void ThrowsWhenRemovingFuncThatDoesNotExist()
        {
            var e = new EventEmitter();
            e.On("test_event", TestMethod);
            Assert.That(() => e.RemoveListener("test_event", IncrementNumber),
                Throws.Exception
                  .TypeOf<DoesNotExistException>()
                  .With.Message.EqualTo("Func [Void IncrementNumber(System.Object)] does not exist to be removed."));
        }

        [Test]
        public void RemoveListenerRemovesFunction()
        {
            var e = new EventEmitter();
            numberToTest = 0;
            e.On("data", IncrementNumber);
            e.On("data", IncrementNumberBy2);
            e.RemoveListener("data", IncrementNumberBy2);
            e.Emit("data", numberToTest);
            Assert.AreEqual(1, numberToTest);
        }

        [Test]
        public void RemoveFuncFromListenerThatDoesNotExistFails()
        {
            var e = new EventEmitter();
            Assert.That(() => e.RemoveListener("data", IncrementNumberBy2),
                Throws.Exception
                  .TypeOf<DoesNotExistException>()
                  .With.Message.EqualTo("Event [data] does not exist to have listeners removed."));
        }

        #endregion

        #region RemoveAllListeners

        [Test]
        public void RemoveAllListenersRemovesListeners()
        {
            var e = new EventEmitter();
            numberToTest = 0;
            e.On("data", IncrementNumber);
            e.On("data", IncrementNumberBy2);
            e.RemoveAllListeners("data");
            Assert.That(() => e.RemoveListener("data", IncrementNumberBy2),
                Throws.Exception
                  .TypeOf<DoesNotExistException>()
                  .With.Message.EqualTo("Func [Void IncrementNumberBy2(System.Object)] does not exist to be removed."));
        }

        #endregion

        private void TestMethod(object obj)
        {
        }

        private void IncrementNumber(object obj)
        {
            numberToTest++;
        }

        private void IncrementNumberBy2(object obj)
        {
            numberToTest += 2;
        }

        private void DecrementNumber(object obj)
        {
            numberToTest--;
        }
    }
}
