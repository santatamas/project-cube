﻿using System.Collections.Generic;
using System.IO;
using CubeProject.Data.Entities;
using CubeProject.Data.Serializers;
using CubeProject.Infrastructure.Enums;
using NUnit.Framework;

namespace CubeProject.Data.UnitTests
{
    [TestFixture]
    public class AnimationSerializerTests
    {
        private Animation _animation;

        [SetUp]
        public void Setup()
        {
            _animation = new Animation
            {
                ColorDepth = ColorDepth.Onebit
            };

            var frame = new Frame<byte>(2,2);
            frame.Data[0, 0] = 1;
            frame.Data[0, 1] = 1;
            frame.Data[1, 0] = 1;
            frame.Data[1, 1] = 1;

            _animation.Frames.Add(frame);
            _animation.FrameDurations.Add(500);

            var frame2 = new Frame<byte>(2, 2);
            frame2.Data[0, 0] = 0;
            frame2.Data[0, 1] = 0;
            frame2.Data[1, 0] = 0;
            frame2.Data[1, 1] = 0;

            _animation.Frames.Add(frame2);
            _animation.FrameDurations.Add(500);
        }

        [Test]
        public void TestAnimationSerialization()
        {
            AnimationSerializer serializer = new AnimationSerializer();
            Stream contentStream = serializer.Serialize(_animation);

            contentStream.Position = 0;

            Animation result = serializer.Deserialize(contentStream);

            // pretty poor validation, but it'll do.
            Assert.AreEqual(_animation.Frames.Count, result.Frames.Count);
        }
    }
}
