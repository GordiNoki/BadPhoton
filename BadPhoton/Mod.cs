using MelonLoader;
using HarmonyLib;
using Il2CppPhoton.Realtime;

namespace BadPhoton
{
    public class Mod : MelonMod
    {
        private MelonPreferences_Category badPhotonCategory;
        private MelonPreferences_Entry<string> appId;

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
            appId = badPhotonCategory.CreateEntry("AppId", "17f87ebc-2af8-4da0-9e9f-a9e75a67e030"); // Default is official pico park 2 app id
        }

        public override void OnLateInitializeMelon()
        {
            Il2CppGame.ReleaseAppId.AppIdInternal = Il2CppCommon.AesEncryptor.EncryptString(appId.Value);
            LoggerInstance.Msg($"Set Photon app id to encrypted {appId.Value}: {Il2CppGame.ReleaseAppId.AppIdInternal}");
        }
    }
}
