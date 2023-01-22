using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace TheConfectionRebirth.NPCs
{
	/// <summary>
	/// Helper class to manage texture variations for NPCs
	/// </summary>
	/// <typeparam name="T">The type of the NPC desired to be managed</typeparam>
	static class VariationsManager<T> where T : ModNPC
	{
		private static readonly Dictionary<Type, VariationDBEntry> _db = new();

		public static void AddVariation(NPCVariation variation) => Entry.AddVariation(variation);

		public static void AddVariationRange(params NPCVariation[] variations)
		{
			foreach (var variation in variations)
				AddVariation(variation);
		}

		private static VariationDBEntry Entry
		{
			get
			{
				if (!_db.TryGetValue(typeof(T), out var dbEntry))
				{
					dbEntry = new();
					_db.Add(typeof(T), dbEntry);
				}

				return dbEntry;
			}
		}

		public static List<NPCVariation> AllVariations => Entry.Variations.ToList();

		/// <summary>
		/// Shorthand for AllVariations.Count
		/// </summary>
		public static int VariationsCount => AllVariations.Count;

		public static List<NPCVariation> AvailableVariations
		{
			get
			{
				var dbEntry = Entry;

				// If all the variations don't have any condition skip the LINQ query
				if (dbEntry._variations.Count == dbEntry._unconditionalVariations.Count)
					return dbEntry.UnconditionalVariations.ToList();

				return (from variation in Entry.Variations
						where variation.ConditionIsMet
				        select variation).ToList();
			}
		}

		/// <summary>
		/// Returns a random variation of the NPC, selecting only from the ones which condition
		/// is met at the moment of the method call
		/// </summary>
		/// <param name="index">A unique number identifying the picked variation, will be -1 in all
		/// cases where valid variation can't be returned</param>
		/// <returns>An NPCVariation object containing information about the drawn variation,
		/// null in every case where no variations are available (e.g. when none have their
		/// condition satisfied)</returns>
		public static NPCVariation? GetRandomVariation(out int index)
		{
			index = -1;
			var variations = AvailableVariations;
			if (variations.Count == 0)
				return null;
			index = Main.rand.Next(variations.Count);
			return variations[index];
		}

		/// <summary>
		/// Returns a variation of the NPC at the specified index
		/// </summary>
		/// <param name="index">A unique number identifying the desired variation</param>
		/// <returns>An NPCVariation object containing information about the desired variation,
		/// null in every case where no variations are available or the index is invalid</returns>
		public static NPCVariation? GetVariationByIndex(int index)
		{
			try
			{
				return AllVariations[index];
			}
			catch (IndexOutOfRangeException)
			{
				return null;
			}
		}

		public static void ClearVariations() => _db.Remove(typeof(T));

		/// <summary>
		/// Helper struct to speed up the process of variations retrieving
		/// </summary>
		private readonly struct VariationDBEntry
		{
			internal readonly List<NPCVariation> _variations = new();

			public ReadOnlyCollection<NPCVariation> Variations => _variations.AsReadOnly();

			internal readonly List<NPCVariation> _unconditionalVariations = new();

			public ReadOnlyCollection<NPCVariation> UnconditionalVariations
				=> _unconditionalVariations.AsReadOnly();

			public VariationDBEntry()
			{
				_variations = new();
				_unconditionalVariations = new();
			}

			public void AddVariation(NPCVariation variation)
			{
				_variations.Add(variation);

				if (variation.Unconditional)
					_unconditionalVariations.Add(variation);
			}

			public override int GetHashCode() => Variations.GetHashCode();

			public override bool Equals(object? obj)
			{
				return obj is VariationDBEntry entry &&
					   EqualityComparer<List<NPCVariation>>.Default.Equals(_variations, entry._variations);
			}

			public static bool operator ==(VariationDBEntry left, VariationDBEntry right)
				=> left.Equals(right);

			public static bool operator !=(VariationDBEntry left, VariationDBEntry right)
				=> !(left == right);
		}

	}

	public class NPCVariation
	{
		public string Name { get; init; }

		public Asset<Texture2D> Texture { get; init; }

		public Func<bool>? Condition { get; init; }

		public NPCVariation(string name, Asset<Texture2D> texture)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
			Texture = texture ?? throw new ArgumentNullException(nameof(texture));
		}

		public NPCVariation(string name, Asset<Texture2D> texture, Func<bool> condition)
			: this(name, texture)
		{
			Condition = condition;
		}

		/// <summary>
		/// Shorthand for .Condition() with incorporated null-checking of the condition itself
		/// </summary>
		public bool ConditionIsMet => Condition?.Invoke() ?? true;

		/// <summary>
		/// Shorthand for .Condition is null
		/// </summary>
		public bool Unconditional => Condition is null;
	}
}
