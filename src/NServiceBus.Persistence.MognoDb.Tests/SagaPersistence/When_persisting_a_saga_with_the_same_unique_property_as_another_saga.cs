﻿using System;
using MongoDB.Driver;
using NUnit.Framework;

namespace NServiceBus.Persistence.MognoDb.Tests.SagaPersistence
{
    public class When_persisting_a_saga_with_the_same_unique_property_as_another_saga : MongoFixture
    {
        [Test]
        public void It_should_enforce_uniqueness()
        {
            var saga1 = new SagaWithUniqueProperty
            {
                Id = Guid.NewGuid(),
                UniqueString = "whatever"
            };

            var saga2 = new SagaWithUniqueProperty
            {
                Id = Guid.NewGuid(),
                UniqueString = "whatever"
            };

            SaveSaga(saga1);
            try
            {
                SaveSaga(saga2);
                Assert.Fail("SaveSaga should throw an exception");
            }
            catch (AggregateException aggEx)
            {
                Assert.AreEqual(typeof(MongoWriteException), aggEx.GetBaseException().GetType());
            }
            catch (Exception ex)
            {
                Assert.Fail("Incorrect exception thrown.");
            }
            
        }
    }
}