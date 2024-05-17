using RoR2;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace LarenthiosItems.Items
{
    public class EntropicPendulum : ItemDef
    {
        public Action<DamageReport> Proc;

        public EntropicPendulum()
        {
            name = "Entropic Pendulum";
            nameToken = "Entropic Pendulum";
            pickupToken = "Ebb and flow";
            descriptionToken = ":)";
            loreToken = "EXAMPLE_CLOAKONKILL_LORE";

            _itemTierDef = Addressables.LoadAssetAsync<ItemTierDef>("RoR2/Base/Common/LunarDef.asset").WaitForCompletion();
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
                report.damageDealt = report.victim.health > (report.victim.fullHealth / 2) && DateTime.Now.Second / 10 % 2 == 0 ? report.damageDealt * 2 : report.damageDealt / 2;
            };
        }
    }
}