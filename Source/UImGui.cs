using ImGuiNET;
using UImGui.Assets;
using UImGui.Platform;
using UImGui.Renderer;
using UnityEngine;
using UnityEngine.Rendering;

namespace UImGui
{
	// TODO: Check Multithread run.
	public class UImGui : MonoBehaviour
	{
		// TODO: Implement.
		public event System.Action Layout;  // Layout event for *this* ImGui instance.
		[SerializeField]
		private bool _doGlobalLayout = true; // Do global/default Layout event too.

		private Context _context;
		private IRenderer _renderer;
		private IPlatform _platform;
		private CommandBuffer _renderCommandBuffer;
		private bool _usingURP;

		[SerializeField]
		private Camera _camera = null;

		[SerializeField]
		private RenderImGui _renderFeature = null;

		[SerializeField]
		private RenderType _rendererType = RenderType.Mesh;

		[SerializeField]
		private InputType _platformType = InputType.InputManager;

		[Tooltip("Null value uses default imgui.ini file.")]
		[SerializeField]
		private IniSettingsAsset _iniSettings = null;

		[Header("Configuration")]

		[SerializeField]
		private UIOConfig _initialConfiguration = new UIOConfig
		{
			ImGuiConfig = ImGuiConfigFlags.DockingEnable | ImGuiConfigFlags.NavEnableKeyboard,

			DoubleClickTime = 0.30f,
			DoubleClickMaxDist = 6.0f,

			DragThreshold = 6.0f,

			KeyRepeatDelay = 0.250f,
			KeyRepeatRate = 0.050f,

			FontGlobalScale = 1.0f,
			FontAllowUserScaling = false,

			DisplayFramebufferScale = Vector2.one,

			MouseDrawCursor = false,
			TextCursorBlink = false,

			ResizeFromEdges = true,
			MoveFromTitleOnly = true,
			ConfigMemoryCompactTimer = 1f,
		};

		[SerializeField]
		private FontAtlasConfigAsset _fontAtlasConfiguration = null;

		[Header("Customization")]
		[SerializeField]
		private ShaderResourcesAsset _shaders = null;

		[SerializeField]
		private StyleAsset _style = null;

		[SerializeField]
		private CursorShapesAsset _cursorShapes = null;

		private void Awake()
		{
			_context = UImGuiUtility.CreateContext();
		}

		private void OnDestroy()
		{
			UImGuiUtility.DestroyContext(_context);
		}

		private void OnEnable()
		{
			void Fail(string reason)
			{
				enabled = false;
				throw new System.Exception($"Failed to start: {reason}.");
			}

			if (_camera == null)
			{
				Fail(nameof(_camera));
			}

			_usingURP = RenderUtils.IsUsingURP();
			if (_renderFeature == null && _usingURP)
			{
				Fail(nameof(_renderFeature));
			}

			_renderCommandBuffer = RenderUtils.GetCommandBuffer(Constants.UImGuiCommandBuffer);

			if (_usingURP)
			{
				_renderFeature.CommandBuffer = _renderCommandBuffer;
			}
			else
			{
				_camera.AddCommandBuffer(CameraEvent.AfterEverything, _renderCommandBuffer);
			}

			UImGuiUtility.SetCurrentContext(_context);

			ImGuiIOPtr io = ImGui.GetIO();

			_initialConfiguration.ApplyTo(io);
			_style?.ApplyTo(ImGui.GetStyle());

			_context.TextureManager.BuildFontAtlas(io, _fontAtlasConfiguration);
			_context.TextureManager.Initialize(io);

			//SetPlatform(Platform.Create(_platformType, _cursorShapes, _iniSettings), io);
			if (_platform == null)
			{
				Fail(nameof(_platform));
			}

			//SetRenderer(RenderUtils.Create(_rendererType, _shaders, _context.textures), io);
			if (_renderer == null)
			{
				Fail(nameof(_renderer));
			}
		}

		private void OnDisable()
		{
			//ImGuiUn.SetUnityContext(_context);
			//ImGuiIOPtr io = ImGui.GetIO();

			//SetRenderer(null, io);
			//SetPlatform(null, io);

			//ImGuiUn.SetUnityContext(null);

			//_context.textures.Shutdown();
			//_context.textures.DestroyFontAtlas(io);

			//if (_usingURP)
			//{
			//	if (_renderFeature != null)
			//		_renderFeature.commandBuffer = null;
			//}
			//else
			//{
			//	if (_camera != null)
			//		_camera.RemoveCommandBuffer(CameraEvent.AfterEverything, _cmd);
			//}

			//if (_cmd != null)
			//	RenderUtils.ReleaseCommandBuffer(_cmd);
			//_cmd = null;
		}

		private void Reset()
		{
			_camera = Camera.main;
			_initialConfiguration.SetDefaults();
		}

		private void Update()
		{
			//ImGuiUn.SetUnityContext(_context);
			//ImGuiIOPtr io = ImGui.GetIO();

			//s_prepareFramePerfMarker.Begin(this);
			//_context.textures.PrepareFrame(io);
			//_platform.PrepareFrame(io, _camera.pixelRect);
			//ImGui.NewFrame();
			//s_prepareFramePerfMarker.End();

			//s_layoutPerfMarker.Begin(this);
			//try
			//{
			//	if (_doGlobalLayout)
			//		ImGuiUn.DoLayout();   // ImGuiUn.Layout: global handlers
			//	Layout?.Invoke();     // this.Layout: handlers specific to this instance
			//}
			//finally
			//{
			//	ImGui.Render();
			//	s_layoutPerfMarker.End();
			//}

			//s_drawListPerfMarker.Begin(this);
			//_cmd.Clear();
			//_renderer.RenderDrawLists(_cmd, ImGui.GetDrawData());
			//s_drawListPerfMarker.End();
		}

		private void SetRenderer(IRenderer renderer, ImGuiIOPtr io)
		{
			_renderer?.Shutdown(io);
			_renderer = renderer;
			_renderer?.Initialize(io);
		}

		private void SetPlatform(IPlatform platform, ImGuiIOPtr io)
		{
			_platform?.Shutdown(io);
			_platform = platform;
			_platform?.Initialize(io);
		}
	}
}