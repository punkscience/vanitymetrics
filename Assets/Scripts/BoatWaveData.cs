using UnityEngine;
using System.Collections;

public class BoatWaveData {

	protected float[] speeds = new float[3] {2.0F, 4.0F, 6.0F};
	protected float[] angles = new float[5] { -20.0F, -10.0F, 0.0F, 10.0F, 20.0F };

	public float speed;
	public float angle;
	public float releaseInterval;
	public int boatTypeIndex;

	public BoatWaveData (int speedIndex, int rowIndex, float boatReleaseInterval) {
		boatTypeIndex = speedIndex;
		speed = speeds [speedIndex];
		angle = angles [rowIndex];
		releaseInterval = boatReleaseInterval;
	}
}
