using R2API;
using RoR2;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace LarenthiosItems.Items
{
    public class Sunsetter : ItemDef
    {
        public Action<DamageReport> Proc;

        public Sunsetter()
        {
            name = "LARRY_SUNSETTER_NAME";
            nameToken = "LARRY_SUNSETTER_NAME";
            pickupToken = "LARRY_SUNSETTER_PICKUP";
            descriptionToken = "LARRY_SUNSETTER_DESC";
            loreToken = "LARRY_SUNSETTER_LORE";

            _itemTierDef = Addressables.LoadAssetAsync<ItemTierDef>("RoR2/Base/Common/Tier2Def.asset").WaitForCompletion();
            pickupIconSprite = Addressables.LoadAssetAsync<Sprite>("RoR2/Base/Common/MiscIcons/texMysteryIcon.png").WaitForCompletion();
            pickupModelPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Mystery/PickupMystery.prefab").WaitForCompletion();

            canRemove = true;
            hidden = false;

            Proc = report =>
            {
                if (!report.victim || !report.victimBody || !report.attackerBody || report.attackerBody.inventory.GetItemCount(itemIndex) == 0)
                {
                    return;
                }
                float missingHealth = (report.victim.fullHealth - report.victim.health) / report.victim.fullHealth;
                float stacks = report.attackerBody.inventory.GetItemCount(itemIndex) - 1;

                report.damageDealt += (report.damageDealt) * missingHealth * ((10f + (5f * stacks)) / 100f);
            };
        }
    }
}