using BepInEx;
using LarenthiosItems.Items;
using R2API;
using RoR2;
using UnityEngine;

namespace LarenthiosItems
{
    [BepInDependency(ItemAPI.PluginGUID)]
    [BepInDependency(LanguageAPI.PluginGUID)]
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]

    public class Plugin : BaseUnityPlugin
    {
        public const string PluginAuthor = "Larenthios";
        public const string PluginName = "LarenthiosItems";
        public const string PluginVersion = "1.0.0";
        public const string PluginGUID = PluginAuthor + "." + PluginName;

        private static AirbornePathogen airbornePathogen;

        public void Awake()
        {
            Log.Init(Logger);
            airbornePathogen = ScriptableObject.CreateInstance<AirbornePathogen>();

            var displayRules = new ItemDisplayRuleDict(null);

            ItemAPI.Add(new CustomItem(airbornePathogen, displayRules));

            GlobalEventManager.onCharacterDeathGlobal += airbornePathogen.Proc;
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F2))
            {
                PlayerCharacterMasterController.instances[0].master.inventory.GiveItem(airbornePathogen.itemIndex);

                PlayerCharacterMasterController.instances[0].master.inventory.GiveItem(RoR2Content.Items.SlowOnHit.itemIndex);

                PlayerCharacterMasterController.instances[0].master.inventory.GiveItem(RoR2Content.Items.BleedOnHit.itemIndex);
                PlayerCharacterMasterController.instances[0].master.inventory.GiveItem(RoR2Content.Items.BleedOnHit.itemIndex);
                PlayerCharacterMasterController.instances[0].master.inventory.GiveItem(RoR2Content.Items.BleedOnHit.itemIndex);
                PlayerCharacterMasterController.instances[0].master.inventory.GiveItem(RoR2Content.Items.BleedOnHit.itemIndex);
                PlayerCharacterMasterController.instances[0].master.inventory.GiveItem(RoR2Content.Items.BleedOnHit.itemIndex);
                PlayerCharacterMasterController.instances[0].master.inventory.GiveItem(RoR2Content.Items.BleedOnHit.itemIndex);
                PlayerCharacterMasterController.instances[0].master.inventory.GiveItem(RoR2Content.Items.BleedOnHit.itemIndex);
                PlayerCharacterMasterController.instances[0].master.inventory.GiveItem(RoR2Content.Items.BleedOnHit.itemIndex);
                PlayerCharacterMasterController.instances[0].master.inventory.GiveItem(RoR2Content.Items.BleedOnHit.itemIndex);
                PlayerCharacterMasterController.instances[0].master.inventory.GiveItem(RoR2Content.Items.BleedOnHit.itemIndex);
            }
        }
    }
}
