/*
 * Used code adapted from page 461 of the the Mission Demolition 
 * prototype to determine mouse position for adding new line points
 * 
 * Adapted code from page 476 to create the line renderer
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StrokeManager : MonoBehaviour {
	public bool drawing;
	public float maxMagnitude;
	public float minMagnitude;
	public int linesDrawn = 0;
	public int maxLinePoints = 150;
	public List<Vector2> currentPoints;
	public LineRenderer currentLine;
	public List<LineRenderer> lines;
	public Material brushMaterial;

	void Awake(){
		maxMagnitude = 10f;
		minMagnitude = 7f;
	}

	void FixedUpdate(){
		float draw = Input.GetAxisRaw ("Fire1");
		float erase = Input.GetAxisRaw ("Fire2");

		if (draw > 0) {
			StartLine ();	
		} else if (drawing) {
			StopLine ();
		}

		if (erase > 0) {
			EraseLines();
		}
	}

	public void StartLine(){

		Vector3 lastPoint = Vector3.zero;

		if (!drawing) {
			GameObject lineRendererGO = new GameObject ("LineRenderer" + linesDrawn);
			lineRendererGO.AddComponent<LineRenderer> ();
			//lineRendererGO.AddComponent<SpriteRenderer>().sharedMaterial = brushMaterial;
			lineRendererGO.tag = "LineRendererGO";
			currentLine = lineRendererGO.GetComponent<LineRenderer> ();
			Material[] materialArray = new Material[4];
			for (var i = 0; i < materialArray.Length; i++){
				materialArray[i] = brushMaterial;
			}
			currentLine.sharedMaterials = materialArray;
			lineRendererGO.SetActive (true);
			drawing = true;
		} 

		var mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector3 mouseDelta = mousePos - transform.position;

//		if (mouseDelta.magnitude > maxMagnitude || mouseDelta.magnitude < minMagnitude) {
//			mouseDelta.Normalize ();
//			mouseDelta *= maxMagnitude;
//		}
		mouseDelta.Normalize ();
		mouseDelta *= maxMagnitude;

		Vector3 newPointPos = transform.position + mouseDelta;

//		if (currentPoints.Count > 0) {
//			lastPoint = currentPoints[currentPoints.Count-1];
//			Vector3 normalNewPoint = newPointPos;
//			normalNewPoint.Normalize();
//			newPointPos = lastPoint + normalNewPoint;
//		}

		currentPoints.Add (newPointPos);

		if (currentPoints.Count < 2) {
			return;
		} else if (currentPoints.Count >= 10) {
			while (currentPoints.Count > maxLinePoints){
				currentPoints.RemoveAt(0);
			}
			currentLine.SetVertexCount(currentPoints.Count);
			for (var i = 0; i < currentPoints.Count; i++){
				currentLine.SetPosition(i,currentPoints[i]);
				currentLine.SetWidth(1.5f, 1.5f);
				if (!currentLine.enabled){
					currentLine.enabled = true;
				}
			}
			EdgeCollider2D edgeCollider = currentLine.GetComponentInParent<EdgeCollider2D>();
			if (edgeCollider == null){
				edgeCollider = currentLine.gameObject.AddComponent<EdgeCollider2D>();
			}
			//edgeCollider.isTrigger = true;
			edgeCollider.points = currentPoints.ToArray();
		}
	}

	public void StopLine(){

		if (currentPoints.Count <= 10) {
			Destroy (currentLine.gameObject);
		} else {
			lines.Add(currentLine);
			linesDrawn++;
		}

		currentPoints.Clear();
		Debug.Log ("Line ended");
		drawing = false;
	}

	public void EraseLines(){
		GameObject[] LineRenderers = GameObject.FindGameObjectsWithTag ("LineRendererGO");
		foreach (var GO in LineRenderers) {
			Destroy (GO);
		}
		Debug.Log (linesDrawn + " lines erased");
		linesDrawn = 0;
	}

//	void OnDrawGizmos(){
//		Gizmos.color = Color.green;
//		Vector3 gizmoPos = new Vector3(transform.position.x, transform.position.y, 1);
//		Gizmos.DrawWireSphere (gizmoPos, maxMagnitude);
//	}
	
}
