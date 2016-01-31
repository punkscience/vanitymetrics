using UnityEngine;
using System.Collections;

public class BoatWaveData {

	protected float[] speeds = new float[3] {2.0F, 5.0F, 8.0F};
	protected float[] angles = new float[5] { -20.0F, -10.0F, 0.0F, 10.0F, 20.0F };

	public float speed;
	public float angle;
	public float releaseInterval;
	public float boatColorIndex;

	public BoatWaveData (int speedIndex, int rowIndex, float boatReleaseInterval) {
		boatColorIndex = speedIndex * 0.5F;
		speed = speeds [speedIndex];
		angle = angles [rowIndex];
		releaseInterval = boatReleaseInterval;
	}
}
