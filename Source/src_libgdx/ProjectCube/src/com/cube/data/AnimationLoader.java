package com.cube.data;

import java.io.IOException;
import java.nio.file.Path;

import com.badlogic.gdx.assets.AssetDescriptor;
import com.badlogic.gdx.assets.AssetManager;
import com.badlogic.gdx.assets.loaders.AsynchronousAssetLoader;
import com.badlogic.gdx.assets.loaders.FileHandleResolver;
import com.badlogic.gdx.files.FileHandle;
import com.badlogic.gdx.utils.Array;

public class AnimationLoader extends AsynchronousAssetLoader<Animation, AnimationParameter> {
	
	Animation animation;
	
	public AnimationLoader (FileHandleResolver resolver) {
		super(resolver);
	}

	@Override
	public void loadAsync (AssetManager manager, String fileName, FileHandle file, AnimationParameter parameter) {
		try {
			String extension = "";
			int i = fileName.lastIndexOf('.');
			if (i > 0) {
			    extension = fileName.substring(i+1);
			}
			
			boolean isCompressed = extension.equals("pmz");
			animation = AnimationSerializer.Deserialize(file,isCompressed);
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

	@Override
	public Animation loadSync (AssetManager manager, String fileName, FileHandle file, AnimationParameter parameter) {
		return animation;
	}

	@Override
	public Array<AssetDescriptor> getDependencies (String fileName, FileHandle file, AnimationParameter parameter) {
		return null;
	}
}