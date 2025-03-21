﻿using p3rpc.femc.yukaributfemctrueadachi.Configuration;
using p3rpc.femc.yukaributfemctrueadachi.Template;
using Reloaded.Hooks.ReloadedII.Interfaces;
using Reloaded.Mod.Interfaces;
using UnrealEssentials.Interfaces;
using Unreal.ObjectsEmitter.Interfaces;

namespace p3rpc.femc.yukaributfemctrueadachi
{
	/// <summary>
	/// Your mod logic goes here.
	/// </summary>
	public class Mod : ModBase // <= Do not Remove.
	{
		/// <summary>
		/// Provides access to the mod loader API.
		/// </summary>
		private readonly IModLoader _modLoader;

		/// <summary>
		/// Provides access to the Reloaded.Hooks API.
		/// </summary>
		/// <remarks>This is null if you remove dependency on Reloaded.SharedLib.Hooks in your mod.</remarks>
		private readonly IReloadedHooks? _hooks;

		/// <summary>
		/// Provides access to the Reloaded logger.
		/// </summary>
		private readonly ILogger _logger;

		/// <summary>
		/// Entry point into the mod, instance that created this class.
		/// </summary>
		private readonly IMod _owner;

		/// <summary>
		/// Provides access to this mod's configuration.
		/// </summary>
		private Config _configuration;

		/// <summary>
		/// The configuration of the currently executing mod.
		/// </summary>
		private readonly IModConfig _modConfig;
		private AssetRedirector _assetRedirector;
		private string modName;
		private IUnreal unreal;

		public Mod(ModContext context)
		{
			_modLoader = context.ModLoader;
			_hooks = context.Hooks;
			_logger = context.Logger;
			_owner = context.Owner;
			_configuration = context.Configuration;
			_modConfig = context.ModConfig;
			_assetRedirector = new AssetRedirector(unreal, modName);
			_assetRedirector.RedirectPlayerAssets();
			modName = _modConfig.ModName;

			var modDir = _modLoader.GetDirectoryForModId(_modConfig.ModId);
			var enabledMods = this._modLoader.GetAppConfig().EnabledMods;
			var femcEnabled = enabledMods.Contains("p3rpc.femc");
			var unrealController = _modLoader.GetController<IUnreal>();
			var unrealEssentialsController = _modLoader.GetController<IUnrealEssentials>();
			
			if (unrealEssentialsController == null || !unrealEssentialsController.TryGetTarget(out var unrealEssentials))
			{
				_logger.WriteLine($"Unable to get controller for Unreal Essentials, please be sure to ping @DniweTamp in the femc reloaded server.", System.Drawing.Color.Pink);
				return;
			}
			if (unrealController == null || !unrealController.TryGetTarget(out unreal))
			{
				_logger.WriteLine($"Unable to get Unreal interface.", System.Drawing.Color.Pink);
				return;
			}

			List<string> foldersToAdd = new List<string>();
			if (!femcEnabled)
			{
				foldersToAdd.Add("TestFolder");
			}

			foreach (var folderName in foldersToAdd)
			{
				var filesPath = Path.Combine(modDir, folderName);
				_logger.WriteLine($"Loading folder: {folderName}", System.Drawing.Color.Pink);
				unrealEssentials.AddFromFolder(filesPath);
			}
		}
		#region Standard Overrides
		public override void ConfigurationUpdated(Config configuration)
		{
			// Apply settings from configuration.
			_configuration = configuration;
			_logger.WriteLine($"[{_modConfig.ModId}] Config Updated: Applying");
		}
		#endregion

		#region For Exports, Serialization etc.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public Mod() { }
#pragma warning restore CS8618
		#endregion
	}
}
