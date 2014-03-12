package com.cube.data;

import java.io.DataInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.nio.ByteBuffer;

import com.badlogic.gdx.files.FileHandle;
import com.badlogic.gdx.utils.LittleEndianInputStream;
import com.cube.common.ColorDepth;

public class AnimationSerializer {

	public static Animation Deserialize(FileHandle file) throws IOException {

		InputStream fs = file.read();
		LittleEndianInputStream  br = new LittleEndianInputStream(fs);
		Animation result = new Animation();
		// Get File version - 1 byte
		byte version = br.readByte();
		if (!SupportsFileVersion(version)) {
			br.close();
			throw new IOException("File Version not supported!");
		}

		// Get number of frames - 2 bytes
		short noFrames = br.readShort();

		// Get the ColorDepth of the frames in animation - 1 byte
		byte colorDepthValue = br.readByte();
		if (colorDepthValue == 1)
			result.set_depth(ColorDepth.OneBit);
		if (colorDepthValue == 8)
			result.set_depth(ColorDepth.GrayScale);

		Frame frame;
		// Iterate through the frames and add them to Animation
		for (int frameIndex = 0; frameIndex < noFrames; frameIndex++) {
			// Get the duration - 2 byte
			short duration = br.readShort();

			// Get the width and height
			short width = br.readShort();
			short height = br.readShort();

			frame = new Frame();
			frame.set_width(width);
			frame.set_height(height);
			frame.set_duration(duration);
			frame.set_depth(result.get_depth());
			frame.set_data(new byte[width][height]);

			// Fill pixel data
			for (int i = 0; i < frame.get_width(); i++) {
				for (int j = 0; j < frame.get_height(); j++) {
					frame.get_data()[i][j] = br.readByte();
				}
			}
			result.get_frames().add(frame);
		}
		br.close();
		return result;
	}

	public static boolean SupportsFileVersion(byte version) {
		return version == 1;
	}
}
