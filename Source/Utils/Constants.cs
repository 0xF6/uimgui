﻿using Unity.Profiling;

namespace UImGui
{
	internal static class Constants
	{
		public static readonly uint Version = (0 << 16) | (0 << 8) | (5);

		internal static readonly string UImGuiCommandBuffer = "UImGui";

		// TODO: Test all profile markers.
		internal static readonly ProfilerMarker PrepareFrameMarker = new ProfilerMarker("DearImGui.PrepareFrame");
		internal static readonly ProfilerMarker LayoutfMarker = new ProfilerMarker("DearImGui.Layout");
		internal static readonly ProfilerMarker DrawListMarker = new ProfilerMarker("DearImGui.RenderDrawLists");

		internal static readonly ProfilerMarker UpdateMeshPerfMarker = new ProfilerMarker("DearImGui.RendererMesh.UpdateMesh");
		internal static readonly ProfilerMarker CreateDrawComandsPerfMarker = new ProfilerMarker("DearImGui.RendererMesh.CreateDrawCommands");

		internal static readonly string ExecuteDrawComandsPerfMarker = "DearImGui.ExecuteDrawCommands";
	}
}