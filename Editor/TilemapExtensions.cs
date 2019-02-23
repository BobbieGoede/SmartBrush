using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class TilemapExtensions {
	public static bool FlushCache = true;
	static Type gridPaintingStateType;
	static MethodInfo autoSelectPaintTarget;
	static object instance;
	static FieldInfo flushPaintTargetCache;
	static PropertyInfo scenePaintTarget;

	static void CacheReflectionFields() {
		gridPaintingStateType = Type.GetType("UnityEditor.GridPaintingState,UnityEditor");
		instance = gridPaintingStateType
			.GetProperty("instance", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
			.GetValue(gridPaintingStateType, null);
		flushPaintTargetCache = gridPaintingStateType.GetField("m_FlushPaintTargetCache", BindingFlags.NonPublic | BindingFlags.Instance);
		autoSelectPaintTarget = gridPaintingStateType.GetMethod("AutoSelectPaintTarget", BindingFlags.Public | BindingFlags.Static);
		scenePaintTarget = gridPaintingStateType.GetProperty("scenePaintTarget", BindingFlags.Public | BindingFlags.Static);
		FlushCache = false;
	}

	public static void AutoSelectPaintTarget() {
		if(FlushCache) CacheReflectionFields();

		flushPaintTargetCache.SetValue(instance, true);
		autoSelectPaintTarget.Invoke(null, null);

		Selection.activeGameObject = (GameObject)scenePaintTarget.GetValue(instance);
	}
}