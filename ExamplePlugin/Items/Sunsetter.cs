using R2API.Utils;
using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine; 
using UnityEngine.AddressableAssets;
using UnityEngine.Yoga;

namespace LarenthiosItems.Items
{
    public class Sunsetter : ItemDef
    {
        public Action<DamageReport> Proc;

        public Sunsetter()
        {
            name = "Sunsetter";
            nameToken = "Sunsetter";
            pickupToken = "The reptile tail ripped from its back";
            descriptionToken = "Deal increased damage based on target missing health";
            loreToken = "EXAMPLE_CLOAKONKILL_LORE";

            _itemTierDef = Addressables.LoadAssetAsync<ItemTierDef>("RoR2/Base/Common/Tier2Def.asset").WaitForCompletion();
            pickupIconSprite = Addressables.LoadAssetAsync<Sprite>("RoR2/Base/Common/MiscIcons/texMysteryIcon.png").WaitForCompletion();
            pickupModelPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Mystery/PickupMystery.prefab").WaitForCompletion();

            canRemove = true;
            hidden = false;

            Proc = report =>
            {
                if (!report.victim || !report.victimBody)
                {
                    return;
                }
                Log.Debug($"Damage dealt before: {report.damageDealt}");
                float victimHealth = (report.victim.fullHealth - report.victim.health) / report.victim.fullHealth;
                report.damageDealt += (report.damageDealt) * victimHealth * (10 + (5 * report.attackerBody.inventory.GetItemCount(itemIndex) - 1) / 100);
                Log.Debug($"Damage dealt after: {report.damageDealt}");
            };
        }
    }
}