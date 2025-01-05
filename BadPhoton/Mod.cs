using MelonLoader;
using HarmonyLib;
using Il2CppFusion.Photon.Realtime;

namespace BadPhoton
{
    public class Mod : MelonMod
    {
        private MelonPreferences_Category badPhotonCategory;
        private MelonPreferences_Entry<string> fusionAppId;
        private MelonPreferences_Entry<string> chatAppId;

        [HarmonyPatch(typeof(LoadBalancingClient), "Connect")]
        [HarmonyPatch(typeof(LoadBalancingClient), "ConnectUsingSettings")]
        static class AuthMethodPatch
        {
            public static void Prefix(LoadBalancingClient __instance) {
                __instance.AuthValues = null;
            }
        }

        public override void OnInitializeMelon()
        {
            badPhotonCategory = MelonPreferences.CreateCategory("Bad Photon");
            fusionAppId = badPhotonCategory.CreateEntry("AppIdFusion", "5d868f5e-bdcc-4bd8-9f0f-aa3ad527fbc0");
            chatAppId = badPhotonCategory.CreateEntry("AppIdChat", "3bb88f25-2d6c-4029-af41-da0d917e95c9");
            // Default are official Aska app ids
        }

        public override void OnLateInitializeMelon()
        {
            PhotonAppSettings.Instance.AppSettings.AppIdFusion = fusionAppId.Value;
            PhotonAppSettings.Instance.AppSettings.AppIdChat = chatAppId.Value;
            LoggerInstance.Msg($"Set Photon Fusion app id to {fusionAppId.Value}");
            LoggerInstance.Msg($"Set Photon Chat app id to {chatAppId.Value}");
        }
    }
}
