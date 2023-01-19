using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
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
		private static readonly Dictionary<Type, List<NPCVariation>> _db = new();

		public static void AddVariation(NPCVariation variation)
		{
			List<NPCVariation> variationsList;

			if (!_db.TryGetValue(typeof(T), out variationsList!))
			{
				variationsList = new();
				_db.Add(typeof(T), variationsList);
			}
			/*
			 * At this point variationsList will either be a new list
			 * or the one that already existed in the dictionary (outputted by TryGetValue)
			 */

			variationsList.Add(variation);
		}

		public static List<NPCVariation> AllVariations
		{
			get
			{
				_db.TryGetValue(typeof(T), out var variationsList);
				return variationsList ?? new();
			}
		}

		/// <summary>
		/// Shorthand for AllVariations.Count
		/// </summary>
		public static int VariationsCount => AllVariations.Count;

		public static List<NPCVariation> AvailableVariations
		{
			get
			{
				var variations = AllVariations;
				return (from variation in variations
					   where variation.ConditionIsMet
					   select variation).ToList();
			}
		}

		/// <summary>
		/// Returns a random variation of the NPC, selecting only from the ones which condition
		/// is met at the moment of the method call
		/// </summary>
		/// <param name="index">A unique number identifying the picked variation, will be -1 in all
		/// cases in which a valid variation can't be returned</param>
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
	}

	public class NPCVariation
	{
		public string Name { get; init; }

		public Asset<Texture2D> Texture { get; init; }

		public Func<bool> Condition { get; init; } = () => true;

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
		/// Shorthand for .Condition, with incorporated null-checking of the condition itself
		/// </summary>
		public bool ConditionIsMet => Condition?.Invoke() ?? true;
	}
}
