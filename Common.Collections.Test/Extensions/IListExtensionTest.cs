﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Common.Collections.Extensions;

namespace Common.Collections.Test.Extensions
{
    [TestClass]
    public class Collections_IListExtensionTest
    {
        [TestMethod]
        public void Shuffle()
        {

            int[] array = new int[10];
            for (int i = 0; i < 10; i++)
                array[i] = i;

            Random rnd = new Random(0);
            array.Shuffle(rnd);

            int[] result = new int[]
            {
                0,4,5,8,2,1,3,6,9,7
            };

            CollectionAssert.AreEqual(result, array);

        }
    }
}
