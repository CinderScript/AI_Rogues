using System.Collections.Generic;

using UnityEngine;

namespace AIRogue.GameObjects
{
	/// <summary>
	/// Encapsulates all Weapons on the ship and provides method for activating weapons.  
	/// This controller makes sure only one weapon is fired at a time so audio
	/// clips don't play at the same time causing audio artifacts.
	/// </summary>
	class WeaponsGroupController : MonoBehaviour
	{
		private const float TIME_BETWEEN_SHOTS = 0.05f;

		private List<Weapon> weapons;
		private float cooldownTimer = -1;

		public void Initialize(List<Weapon> unitWeapons)
		{
			// deep copy
			weapons = new List<Weapon>();
			foreach (var weap in unitWeapons)
			{
				weapons.Add( weap );
			}
		}
		public void FireWeapons()
		{
			if (cooldownTimer < 0)
			{
				cooldownTimer = TIME_BETWEEN_SHOTS;
				FireOneShot();
			}
		}

		void Update()
		{
			cooldownTimer -= Time.deltaTime;
		}

		/// <summary>
		/// Tries to fire each weapon in the list until a weapon
		/// is fired.  Rearanges the weapons in the list so that
		/// priority is given to unfired weapons.
		/// </summary>
		void FireOneShot()
		{
			var firedIndex = -1;
			var fired = false;

			for (int i = 0; i < weapons.Count; i++)
			{
				fired = weapons[i].FireWeapon();
				if (fired)
				{
					firedIndex = i;
					break;
				}
			}

			int lastIndex = weapons.Count - 1;

			// if the weapon that fired was not the last in the list,
			// then send it to the end of the list so it does not have
			// priority next time this method is called.
			if (fired)
			{
				if (firedIndex < lastIndex)
				{
					// move to end
					var weap = weapons[firedIndex];
					weapons.RemoveAt( firedIndex );
					weapons.Add( weap );
				}
			}
		}
	}
}