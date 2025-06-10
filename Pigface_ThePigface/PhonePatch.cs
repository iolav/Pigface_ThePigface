using BepInEx;
using HarmonyLib;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace Pigface_ThePigface.Patches
{
    [HarmonyPatch(typeof(Cellphone), "Awake")]
    internal class PhoneAwakePatch
    {
        static void Postfix(Cellphone __instance)
        {
            var Field = typeof(Cellphone).GetField("cellPhoneObj", BindingFlags.NonPublic | BindingFlags.Instance);
            GameObject PhoneObj = (GameObject)Field.GetValue(__instance);
            MeshRenderer ScreenRenderer = PhoneObj.transform.Find("first_person_armature/Bone.002/Bone.001/phone_screen").GetComponent<MeshRenderer>();

            Texture2D NewTexture = Plugin.Bundle.LoadAsset<Texture2D>("screen_texture");

            foreach (Material Mat in ScreenRenderer.materials)
            {
                Mat.mainTexture = NewTexture;
                Mat.SetTexture("_MainTex", NewTexture);
                Mat.SetTexture("_EmissionMap", NewTexture);
            }
        }
    }

    [HarmonyPatch(typeof(Cellphone), "OpenPhone", new System.Type[] { typeof(bool) })]
    internal class PhoneOpenPatch
    {
        static void Postfix(Cellphone __instance)
        {
            Plugin.Log!.LogInfo("hello jp!");

            AudioClip NewSound = Plugin.Bundle.LoadAsset<AudioClip>("call_sound");

            var Field = typeof(Cellphone).GetField("cellPhoneObj", BindingFlags.NonPublic | BindingFlags.Instance);
            GameObject PhoneObj = (GameObject)Field.GetValue(__instance);

            Transform AudioChild = PhoneObj.transform.Find("JohnPork_AudioSource");

            AudioSource Audio;

            if (AudioChild != null)
            {
                Audio = AudioChild.GetComponent<AudioSource>();
            }
            else
            {
                GameObject AudioObj = new GameObject("JohnPork_AudioSource");
                AudioObj.transform.SetParent(PhoneObj.transform);
                AudioObj.transform.localPosition = Vector3.zero;

                Audio = AudioObj.AddComponent<AudioSource>();
            }

            Audio.clip = NewSound;
            Audio.Play();
        }
    }
}