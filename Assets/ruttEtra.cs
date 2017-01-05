using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ruttEtra : MonoBehaviour {

	public int numberOfRows=100;
	public int samplePoints = 50;
	public float width=.058f;
	public Vector2 size;
	public MovieTexture texture;
	public GameObject[] lines;
	public Transform transformPlane;
	void Start() {
		lines = new GameObject[numberOfRows];
		texture.loop = true;
		texture.Play ();
		for (int i = 0; i < numberOfRows; i++) {
			GameObject line = new GameObject ();
			LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
			lineRenderer.material = new Material (Shader.Find("Unlit/LineShader"));
			float v = i/ (float)samplePoints;
			lineRenderer.material.SetFloat ("_rowCoord", v);
			lineRenderer.material.SetTexture ("_MainTex", texture);
			lineRenderer.widthMultiplier = width;
			lineRenderer.numPositions = samplePoints;
			GradientColorKey[] gradColorKeys = new GradientColorKey[samplePoints];
			//Color[] c = new Color[samplePoints];
			for (int j = 0; j < samplePoints; j++) {
				float u = (float)j / (float)samplePoints;
				//Color c =texture.GetPixelBilinear (u,v);
				//gradColorKeys[j]=new GradientColorKey(c,j/samplePoints);
				lineRenderer.SetPosition(j,new Vector3(j/10f,0,i/10f));
			}

			Gradient g = new Gradient ();
			/*g.SetKeys(gradColorKeys,
				new GradientAlphaKey[] { new GradientAlphaKey(1, 0.0f), new GradientAlphaKey(1, 1.0f) }
			);
			lineRenderer.colorGradient = g;*/
			lines [i] = line;
		}

		//lineRenderer.colorGradient = gradient;
	}

	void Update() {
		
		for (int i = 0; i < numberOfRows; i++) {
			LineRenderer lineRenderer = lines [i].GetComponent<LineRenderer> ();
			float v = i / (float)samplePoints;
			lineRenderer.material.SetFloat ("_rowCoord", v);
			lineRenderer.material.SetTexture ("_MainTex", texture);
			lineRenderer.material.SetVector ("_normal", transformPlane.forward);
			lineRenderer.widthMultiplier = width;
			lineRenderer.numPositions = samplePoints;
			GradientColorKey[] gradColorKeys = new GradientColorKey[samplePoints];
			//Color[] c = new Color[samplePoints];
			Vector3 steps = new Vector2 ();

			for (int j = 0; j < samplePoints; j++) {
				
				//float u = (float)j / (float)samplePoints;
				//Color c =texture.GetPixelBilinear (u,v);
				//gradColorKeys[j]=new GradientColorKey(c,j/samplePoints);
				Vector3 pos = transformPlane.position;
				pos += ((j-(float)samplePoints/2.0f) / 10.0f) * transformPlane.right + ((i-(float)samplePoints/2.0f) / 10.0f) * transformPlane.up;
				lineRenderer.SetPosition (j, pos);
			}
		}
	}


}