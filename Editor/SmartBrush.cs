using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
using System;

[CustomGridBrush(false, false, false, "SmartBrush")]
public class SmartBrush : GridBrush {
	public override void Paint(GridLayout grid, GameObject brushTarget, Vector3Int position) {
		base.Paint(grid, brushTarget, position);
	}

	public override void Erase(GridLayout grid, GameObject brushTarget, Vector3Int position) {
		base.Erase(grid, brushTarget, position);
	}

	public override void FloodFill(GridLayout grid, GameObject brushTarget, Vector3Int position) {
		base.FloodFill(grid, brushTarget, position);
	}

	public override void BoxFill(GridLayout gridLayout, GameObject brushTarget, BoundsInt position) {
		base.BoxFill(gridLayout, brushTarget, position);
	}

	public override void Pick(GridLayout gridLayout, GameObject brushTarget, BoundsInt position, Vector3Int pickStart) {
		Tilemap component = brushTarget.GetComponent<Tilemap>();
		if(component == null) return;

		base.Pick(gridLayout, brushTarget, position, pickStart);

		foreach(Vector3Int tilePosition in position.allPositionsWithin) {
			if(component.GetTile(tilePosition) == null) continue;
			TilemapExtensions.AutoSelectPaintTarget();
			return;
		}
	}
}

[CustomEditor(typeof(SmartBrush))]
public class SmartBrushEditor : GridBrushEditor {
	protected override void OnEnable() {
		base.OnEnable();
		TilemapExtensions.FlushCache = true;
	}

	protected override void OnDisable() {
		base.OnDisable();
		TilemapExtensions.FlushCache = true;
	}

	public override GameObject[] validTargets {
		get {
			StageHandle currentStageHandle = StageUtility.GetCurrentStageHandle();
			if(brush.cells.Count() == 0) {
				Debug.LogWarning("No cells selected, can't set active Tilemap");
				return null;
			}

			return currentStageHandle
				.FindComponentsOfType<Tilemap>()
				.Where(
					x => x.gameObject.scene.isLoaded &&
					x.gameObject.activeInHierarchy &&
					Array.Exists(brush.cells, c =>
						c.tile != null &&
						c.tile.name == x.gameObject.name
					)
				)
				.Select(x => x.gameObject)
				.ToArray();
		}
	}
}
