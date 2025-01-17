package com.cube.main;

import com.badlogic.gdx.ApplicationListener;
import com.badlogic.gdx.Game;
import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.assets.AssetManager;
import com.cube.common.VirtualScreen;
import com.cube.main.Screens.LoadingScreen;

public class CubeGame extends Game implements ApplicationListener {
	
	public static AssetManager AssetManager = new AssetManager();
	private LoadingScreen _loadingScreen;
	
	@Override
	public void create() {	
		VirtualScreen.ScreenWidth = Gdx.graphics.getWidth();
		VirtualScreen.ScreenHeight = Gdx.graphics.getHeight();
		
		VirtualScreen.PixelWidthRatio = VirtualScreen.ScreenWidth / VirtualScreen.ReferenceScreenWidth;
		VirtualScreen.PixelHeightRatio = VirtualScreen.ScreenHeight / VirtualScreen.ReferenceScreenHeight;
				
		_loadingScreen = new LoadingScreen(this);
		setScreen(_loadingScreen);
	}

	@Override
	public void dispose() {
		_loadingScreen.dispose();
		AssetManager.dispose();
	}
}
