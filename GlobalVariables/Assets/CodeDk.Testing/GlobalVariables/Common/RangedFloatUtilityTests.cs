using NUnit.Framework;

namespace CodeDk
{

    public class RangedFloatUtilityTests
    {
        public const bool DID_BREACH = true;
        public const bool DID_NOT_BREACH = false;

        public const float MIN_VALUE = 0.0f;
        public const float MAX_VALUE = 10.0f;

        public const float DELTA = 1.0f;

        public class TheClampMethod
        {
            [Test]
            public void ReturnsDidBreach_ForValueLessThanMin()
            {
                // Arrange
                const float VALUE = MIN_VALUE - DELTA;

                FloatClampingResult EXPECTED_RESULT = new FloatClampingResult(DID_BREACH, MIN_VALUE);

                // Act
                FloatClampingResult Result = FloatClamping.Clamp(VALUE, MIN_VALUE, MAX_VALUE);

                // Assert
                Assert.That(EXPECTED_RESULT.DidBreachRange == Result.DidBreachRange);
                Assert.That(EXPECTED_RESULT.Result == Result.Result);
            }

            [Test]
            public void ReturnsDidBreach_ForValueGreaterThanMax()
            {
                // Arrange
                const float VALUE = MAX_VALUE + DELTA;

                FloatClampingResult EXPECTED_RESULT = new FloatClampingResult(DID_BREACH, MAX_VALUE);

                // Act
                FloatClampingResult Result = FloatClamping.Clamp(VALUE, MIN_VALUE, MAX_VALUE);

                // Assert
                Assert.That(EXPECTED_RESULT.DidBreachRange == Result.DidBreachRange);
                Assert.That(EXPECTED_RESULT.Result == Result.Result);
            }

            [Test]
            public void ReturnsDidNotBreach_ForMinValue()
            {
                // Arrange
                const float VALUE = MIN_VALUE;

                FloatClampingResult EXPECTED_RESULT = new FloatClampingResult(DID_NOT_BREACH, VALUE);

                // Act
                FloatClampingResult Result = FloatClamping.Clamp(VALUE, MIN_VALUE, MAX_VALUE);

                // Assert
                Assert.That(EXPECTED_RESULT.DidBreachRange == Result.DidBreachRange);
                Assert.That(EXPECTED_RESULT.Result == Result.Result);
            }

            [Test]
            public void ReturnsDidNotBreach_ForMaxValue()
            {
                // Arrange
                const float VALUE = MAX_VALUE;

                FloatClampingResult EXPECTED_RESULT = new FloatClampingResult(DID_NOT_BREACH, VALUE);

                // Act
                FloatClampingResult Result = FloatClamping.Clamp(VALUE, MIN_VALUE, MAX_VALUE);

                // Assert
                Assert.That(EXPECTED_RESULT.DidBreachRange == Result.DidBreachRange);
                Assert.That(EXPECTED_RESULT.Result == Result.Result);
            }

            [Test]
            public void ReturnsDidNotBreach_ForValueInBetween()
            {
                // Arrange
                const float VALUE = 0.5f * (MAX_VALUE + MIN_VALUE);

                FloatClampingResult EXPECTED_RESULT = new FloatClampingResult(DID_NOT_BREACH, VALUE);

                // Act
                FloatClampingResult Result = FloatClamping.Clamp(VALUE, MIN_VALUE, MAX_VALUE);

                // Assert
                Assert.That(EXPECTED_RESULT.DidBreachRange == Result.DidBreachRange);
                Assert.That(EXPECTED_RESULT.Result == Result.Result);
            }
        }

        public class TheRepeatMethod
        {
            [Test]
            public void ReturnsDidBreach_ForValueLessThanMin()
            {
                // Arrange
                const float VALUE = MIN_VALUE - DELTA;

                FloatClampingResult EXPECTED_RESULT = new FloatClampingResult(DID_BREACH, MAX_VALUE - DELTA);

                // Act
                FloatClampingResult Result = FloatClamping.Repeat(VALUE, MIN_VALUE, MAX_VALUE);

                // Assert
                Assert.That(EXPECTED_RESULT.DidBreachRange == Result.DidBreachRange);
                Assert.That(EXPECTED_RESULT.Result == Result.Result);
            }

            [Test]
            public void ReturnsDidBreach_ForValueGreaterThanMax()
            {
                // Arrange
                const float VALUE = MAX_VALUE + DELTA;

                FloatClampingResult EXPECTED_RESULT = new FloatClampingResult(DID_BREACH, MIN_VALUE + DELTA);

                // Act
                FloatClampingResult Result = FloatClamping.Repeat(VALUE, MIN_VALUE, MAX_VALUE);

                // Assert
                Assert.That(EXPECTED_RESULT.DidBreachRange == Result.DidBreachRange);
                Assert.That(EXPECTED_RESULT.Result == Result.Result);
            }

            [Test]
            public void ReturnsDidNotBreach_ForMinValue()
            {
                // Arrange
                const float VALUE = MIN_VALUE;

                FloatClampingResult EXPECTED_RESULT = new FloatClampingResult(DID_NOT_BREACH, VALUE);

                // Act
                FloatClampingResult Result = FloatClamping.Repeat(VALUE, MIN_VALUE, MAX_VALUE);

                // Assert
                Assert.That(EXPECTED_RESULT.DidBreachRange == Result.DidBreachRange);
                Assert.That(EXPECTED_RESULT.Result == Result.Result);
            }

            [Test]
            public void ReturnsDidNotBreach_ForMaxValue()
            {
                // Arrange
                const float VALUE = MAX_VALUE;

                FloatClampingResult EXPECTED_RESULT = new FloatClampingResult(DID_NOT_BREACH, VALUE);

                // Act
                FloatClampingResult Result = FloatClamping.Repeat(VALUE, MIN_VALUE, MAX_VALUE);

                // Assert
                Assert.That(EXPECTED_RESULT.DidBreachRange == Result.DidBreachRange);
                Assert.That(EXPECTED_RESULT.Result == Result.Result);
            }

            [Test]
            public void ReturnsDidNotBreach_ForValueInBetween()
            {
                // Arrange
                const float VALUE = 0.5f * (MAX_VALUE + MIN_VALUE);

                FloatClampingResult EXPECTED_RESULT = new FloatClampingResult(DID_NOT_BREACH, VALUE);

                // Act
                FloatClampingResult Result = FloatClamping.Repeat(VALUE, MIN_VALUE, MAX_VALUE);

                // Assert
                Assert.That(EXPECTED_RESULT.DidBreachRange == Result.DidBreachRange);
                Assert.That(EXPECTED_RESULT.Result == Result.Result);
            }
        }

        public class ThePingPongMethod
        {
            [Test]
            public void ReturnsDidBreach_ForValueLessThanMin()
            {
                // Arrange
                const float VALUE = MIN_VALUE - DELTA;

                FloatClampingResult EXPECTED_RESULT = new FloatClampingResult(DID_BREACH, MIN_VALUE + DELTA);

                // Act
                FloatClampingResult Result = FloatClamping.PingPong(VALUE, MIN_VALUE, MAX_VALUE);

                // Assert
                Assert.That(EXPECTED_RESULT.DidBreachRange == Result.DidBreachRange);
                Assert.That(EXPECTED_RESULT.Result == Result.Result);
            }

            [Test]
            public void ReturnsDidBreach_ForValueGreaterThanMax()
            {
                // Arrange
                const float VALUE = MAX_VALUE + DELTA;

                FloatClampingResult EXPECTED_RESULT = new FloatClampingResult(DID_BREACH, MAX_VALUE - DELTA);

                // Act
                FloatClampingResult Result = FloatClamping.PingPong(VALUE, MIN_VALUE, MAX_VALUE);

                // Assert
                Assert.That(EXPECTED_RESULT.DidBreachRange == Result.DidBreachRange);
                Assert.That(EXPECTED_RESULT.Result == Result.Result);
            }

            [Test]
            public void ReturnsDidNotBreach_ForMinValue()
            {
                // Arrange
                const float VALUE = MIN_VALUE;

                FloatClampingResult EXPECTED_RESULT = new FloatClampingResult(DID_NOT_BREACH, VALUE);

                // Act
                FloatClampingResult Result = FloatClamping.PingPong(VALUE, MIN_VALUE, MAX_VALUE);

                // Assert
                Assert.That(EXPECTED_RESULT.DidBreachRange == Result.DidBreachRange);
                Assert.That(EXPECTED_RESULT.Result == Result.Result);
            }

            [Test]
            public void ReturnsDidNotBreach_ForMaxValue()
            {
                // Arrange
                const float VALUE = MAX_VALUE;

                FloatClampingResult EXPECTED_RESULT = new FloatClampingResult(DID_NOT_BREACH, VALUE);

                // Act
                FloatClampingResult Result = FloatClamping.PingPong(VALUE, MIN_VALUE, MAX_VALUE);

                // Assert
                Assert.That(EXPECTED_RESULT.DidBreachRange == Result.DidBreachRange);
                Assert.That(EXPECTED_RESULT.Result == Result.Result);
            }

            [Test]
            public void ReturnsDidNotBreach_ForValueInBetween()
            {
                // Arrange
                const float VALUE = 0.5f * (MAX_VALUE + MIN_VALUE);

                FloatClampingResult EXPECTED_RESULT = new FloatClampingResult(DID_NOT_BREACH, VALUE);

                // Act
                FloatClampingResult Result = FloatClamping.PingPong(VALUE, MIN_VALUE, MAX_VALUE);

                // Assert
                Assert.That(EXPECTED_RESULT.DidBreachRange == Result.DidBreachRange);
                Assert.That(EXPECTED_RESULT.Result == Result.Result);
            }
        }

    }


}
