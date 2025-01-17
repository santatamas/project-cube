﻿using System;
using System.IO;
using CubeProject.Data.Entities;
using CubeProject.Infrastructure.Enums;
using CubeProject.Infrastructure.Interfaces;

namespace CubeProject.Data.Serializers
{
    public class AnimationSerializer : IBinarySerializer<Animation>
    {
        public Stream Serialize(Animation data)
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);

            // Write out file version - 1 byte
            bw.Write((byte)FileVersion.V1);

            // Write out the number of frames - 2 bytes
            bw.Write((short)data.Frames.Count);

            // Write out color depth - 1 byte
            bw.Write((byte)data.ColorDepth);

            for (int index = 0; index < data.Frames.Count; index++)
            {
                // Get current frame
                var frame = data.Frames[index];

                // Write out current frame duration - 2 byte
                bw.Write((short)data.FrameDurations[index]);

                // Write out current Frame width and height
                bw.Write((short)frame.Width);
                bw.Write((short)frame.Height);

                // Write out pixel data
                for (int i = 0; i < frame.Width; i++)
                {
                    for (int j = 0; j < frame.Height; j++)
                    {
                         bw.Write((byte)frame[i, j]);
                    }
                }
            }
            return ms;

        }

        public Animation Deserialize(Stream stream)
        {
            BinaryReader br = new BinaryReader(stream);
            Animation result = new Animation();

            // Get File version - 1 byte
            FileVersion version = (FileVersion)br.ReadByte();
            if (!SupportsFileVersion(version))
            {
                br.Close();
                throw new NotSupportedException("File Version not supported!");
            }

            // Get number of frames - 2 bytes
            var noFrames = br.ReadInt16();

            // Get the ColorDepth of the frames in animation - 1 byte
            ColorDepth colorDepth = (ColorDepth) br.ReadByte();
            result.ColorDepth = colorDepth;

            Frame<byte> frame;
            // Iterate through the frames and add them to Animation
            for (int frameIndex = 0; frameIndex < noFrames; frameIndex++)
            {
                // Get the duration - 2 byte
                result.FrameDurations.Add(br.ReadInt16());

                // Get the width and height 
                var width  = br.ReadInt16();
                var height = br.ReadInt16();

                frame = new Frame<byte>(width, height);

                // Fill pixel data
                for (int i = 0; i < frame.Width; i++)
                {
                    for (int j = 0; j < frame.Height; j++)
                    {
                        frame[i, j] = br.ReadByte();
                    }
                }
                result.Frames.Add(frame);
            }

            return result;
        }

        public bool SupportsFileVersion(FileVersion version)
        {
            return version == FileVersion.V1;
        }
    }
}
