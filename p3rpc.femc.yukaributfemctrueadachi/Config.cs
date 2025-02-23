using p3rpc.femc.yukaributfemctrueadachi.Template.Configuration;
using Reloaded.Mod.Interfaces.Structs;
using System.ComponentModel;

namespace p3rpc.femc.yukaributfemctrueadachi.Configuration
{
	public class Config : Configurable<Config>
	{

		[DisplayName("AOA Options")]
		[Description("The AOA Image.")]
		[Category("2D Options")]
		[DefaultValue(AOAType.esaadrien)]
		public AOAType AOATrue { get; set; } = AOAType.esaadrien;

		public enum AOAType
		{
			Ely,
			Chrysanthie,
			Fernando,
			Monica,
			RonaldReagan,
			esaadrien,
			mekki,
			shiosakana,
			shiosakanaAlt,
			Nami,
			AngieDaGorl
		}

		[DisplayName("AOA Text Options")]
		[Description("The AOA Foreground Text.")]
		[Category("2D Options")]
		[DefaultValue(AOATextType.SorryBoutThat)]
		public AOATextType AOAText { get; set; } = AOATextType.SorryBoutThat;

		public enum AOATextType
		{
			DontLookBack,
			SorryBoutThat,
			PerfectlyAccomplished
		}


		[DisplayName("Glass Shard")]
		[Description("The Glass Shard in that one menu when pausing.")]
		[Category("2D Options")]
		[DefaultValue(ShardType.Esa)]
		public ShardType ShardTrue { get; set; } = ShardType.Esa;

		public enum ShardType
		{
			Esa,
			Ely,
			ElyAlt,
			Shiosakana,
			namiweiko,
			AngieDaGorl
		}

		[DisplayName("Cutin")]
		[Description("Cutin Movie")]
		[Category("2D Options")]
		[DefaultValue(CutinType.Mekki)]
		public CutinType CutinTrue { get; set; } = CutinType.Mekki;

		public enum CutinType
		{
			berrycha,
			ElyandPatmandx,
			Mekki,
			shiosakana
		}

		/// <summary>
		/// Allows you to override certain aspects of the configuration creation process (e.g. create multiple configurations).
		/// Override elements in <see cref="ConfiguratorMixinBase"/> for finer control.
		/// </summary>
		public class ConfiguratorMixin : ConfiguratorMixinBase
		{
			// 
		}
	}
}