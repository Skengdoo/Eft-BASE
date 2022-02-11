using System.Collections.Generic;
using UnityEngine;
using EFT.Animals;
using RootMotion;
using System.Threading;
using System.Runtime.InteropServices;
using System;
using Microsoft.Win32.SafeHandles;
using System.IO;
using System.Text;
using EFT;
using BSG.CameraEffects;
using System.Linq;
using static EFTDLL.TarkovHooks;
using BSG.CameraEffects;
using EFT;
using EFT.Ballistics;
using EFT.Interactive;
using EFT.InventoryLogic;
using EFT.Weather;

namespace EFTDLL
{
    public class Menu : MonoBehaviour
    {

        public GameObject HookObject;
        private ObjectCache<EFT.Interactive.LootableContainer> container = new ObjectCache<EFT.Interactive.LootableContainer>();
        private ObjectCache<EFT.Interactive.ExfiltrationPoint> extraction = new ObjectCache<EFT.Interactive.ExfiltrationPoint>();
        private ObjectCache<EFT.Interactive.Corpse> Corpse = new ObjectCache<EFT.Interactive.Corpse>();
        private ObjectCache<EFT.Interactive.LootItem> Item = new ObjectCache<EFT.Interactive.LootItem>();
        private ObjectCache<EFT.Player> player = new ObjectCache<EFT.Player>();
        private ObjectCache<EFT.Player> LPplayer = new ObjectCache<EFT.Player>();
        private ObjectCache<TOD_Sky> sky = new ObjectCache<TOD_Sky>();
        private Color color;
        public static EFT.Player LocalPlayer = null;
        public static Vector2 vec2DrawLine = new Vector2(Screen.width / 2, Screen.height);
        public static Vector2 boxStuff = new Vector2(Screen.width / 2, Screen.height);
        public Vector3 NextBone { get; private set; }

       
        public void Start()
        {
            container.Init(this);
            Corpse.Init(this);
            extraction.Init(this);
            Item.Init(this);
            player.Init(this);
            LPplayer.Init(this);

        }


        private void Speeding()
        {
            if (Cfg.speedhack)
            {
                Time.timeScale = Cfg.speedValue;
                Menu.LocalPlayer.EnableSprint(true);
                Menu.LocalPlayer.MovementContext.EnableSprint(true);
                Menu.LocalPlayer.CurrentState.EnableSprint(true, false);
                return;
            }
            Time.timeScale = 1f;
        }

        private LayerMask Temp;
        private void FlyHack()
        {
            int mask = LayerMask.GetMask(new string[]
            {
                "Water",
                "Terrain",
                "HighPolyCollider",
                "TransparentCollider",
                "HitCollider"
            });

            if (Cfg.FloatOff)
            {
                EFTHardSettings.Instance.MOVEMENT_MASK = Temp;
            }
            else
            {
                EFTHardSettings.Instance.MOVEMENT_MASK = LayerMask.GetMask(new String[]
                {


                    "Water",
                    "Terrain",
                    "HighPolyCollider",
                    "TransparentCollider",
                    "HitCollider"
                });
            }


            if (Cfg.flyingHack)
            {
                int vis_mask = 1 << 12 | 1 << 16;
                LayerMask air = vis_mask;
                Menu.LocalPlayer.MovementContext.IsGrounded = true;
                Menu.LocalPlayer.MovementContext.FreefallTime = Cfg.flyingSpeed;
            }
        }


        private void MaxStats()
        {
            if (Cfg.MaxStats && Menu.LocalPlayer != null && Camera.main != null)
            {
                for (int i = 0; i < Menu.LocalPlayer.Skills.Skills.Count(); i++)
                {
                    if (Menu.LocalPlayer.Skills.Skills[i].Level != 51)
                    {
                        Menu.LocalPlayer.Skills.Skills[i].SetLevel(51);
                    }
                }
            }
        }



        public void Update()
        {
            this.Speeding();
            this.FlyHack();
            this.MaxStats();

            // menu stuff
            if (Input.GetKeyUp(KeyCode.Insert))
            {
                Cfg.MenuToggle = !Cfg.MenuToggle;

                if (Cfg.MenuToggle)
                {
                    Cfg.OldCursorVisible = Cursor.visible;
                    Cfg.OldCursorLockMode = Cursor.lockState;
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
                else
                {
                    Cursor.visible = Cfg.OldCursorVisible;
                    Cursor.lockState = Cfg.OldCursorLockMode;
                }
            }

            if(Input.GetKeyUp(KeyCode.Home))
            {
                Loader.Unload();
            }


            // thermal on
            Camera.main.GetComponent<ThermalVision>().On = Cfg.thermal;

            foreach (EFT.Player LPplayer in LPplayer.Objects)
            {
                if (Cfg.stamina)
                {
                    LPplayer.Physical.StaminaParameters.SprintDrainRate = 0f;
                    LPplayer.Physical.StaminaParameters.JumpConsumption = 0f;
                    LPplayer.Physical.StaminaParameters.OxygenRestoration = 9999f;
                    LPplayer.Physical.StaminaParameters.ProneConsumption = 0f;
                    LPplayer.Physical.StaminaParameters.SitToStandConsumption = 0f;
                   
                }
                if(Cfg.ExperiemntFeature)
                {
                    LPplayer.GetComponent<Player.FirearmController>().Item.Template.Ergonomics = 100;
                    LPplayer.GetComponent<Player.FirearmController>().Item.Template.bHearDist = 1;
                    LPplayer.ProceduralWeaponAnimation.Mask = EFT.Animations.EProceduralAnimationMask.ForceReaction;
                    LPplayer.Weapon.CurrentAmmoTemplate.Tracer = true;
                    LPplayer.Weapon.CurrentAmmoTemplate.TracerColor = JsonType.TaxonomyColor.violet;
                    LPplayer.Weapon.CurrentAmmoTemplate.PenetrationPower = 1000;
                }

                if (Cfg.recoil)
                {
                    LPplayer.ProceduralWeaponAnimation.Shootingg.RecoilStrengthXy = new Vector2(0, 0);
                    LPplayer.ProceduralWeaponAnimation.Shootingg.RecoilStrengthZ = new Vector2(0, 0);
                    LPplayer.ProceduralWeaponAnimation.Shootingg.Intensity = 0;
                }
                if (Cfg.sway)
                {
                    LPplayer.ProceduralWeaponAnimation.MotionReact.SwayFactors.x = 0;
                    LPplayer.ProceduralWeaponAnimation.MotionReact.SwayFactors.y = 0;
                    LPplayer.ProceduralWeaponAnimation.MotionReact.SwayFactors.z = 0;
                }
                if (Cfg.restrictions)
                {
                    LPplayer.ProceduralWeaponAnimation.Breath.Intensity = 0;
                    LPplayer.ProceduralWeaponAnimation.Walk.Intensity = 0;
                    LPplayer.ProceduralWeaponAnimation.Shootingg.Stiffness = 0;
                    LPplayer.ProceduralWeaponAnimation.ForceReact.Intensity = 0;
                    LPplayer.ProceduralWeaponAnimation.WalkEffectorEnabled = false;
                    LPplayer.ProceduralWeaponAnimation.MotionReact.Intensity = 0;
                    LPplayer.ProceduralWeaponAnimation.Sprint = true;
                }
            }
        }

        public void OnGUI()
        {
            GUI.backgroundColor = Color.blue;
            GUILayout.Label("Insert Menu Key");

            if (!Cfg.MenuToggle)
            {
                Cfg.WindowRect = GUI.Window(0, Cfg.WindowRect, Draw, "Niggas");
            }

            foreach (EFT.Player player in player.Objects)
            {
                if (Cfg.chams)
                {
                    var skinDictionary = player?.PlayerBody?.BodySkins;
                    if (skinDictionary != null)
                    {
                        foreach (var skin in skinDictionary.Values)
                        {
                            if (skin == null)
                                continue;

                            foreach (var renderer in skin.GetRenderers())
                            {
                                if (renderer?.material?.shader == null)
                                    continue;

                                renderer.material.shader = null;
                            }
                        }
                    }
                }
                if (Cfg.playerESP)
                {
                    EFT.PlayerOwner ownPlayer = player.GetComponent<PlayerOwner>();
                    if (ownPlayer != null)
                    {
                        LocalPlayer = player;
                        continue;
                    }
                    if (!player.isActiveAndEnabled) continue;
                    if (!player.IsVisible) continue;

                    Vector3 vector = Camera.main.WorldToScreenPoint(player.Transform.position);
                    float dist = Vector3.Distance(Camera.main.transform.position, player.Transform.position);
                    int distance = (int)dist;
                    var AI = player.Profile.Info.RegistrationDate <= 0;
                    int Level = player.Profile.Info.Level;
                    var Name = AI ? "Scav" : player.Profile.Info.Nickname;
                    int Health = (int)player.HealthController.GetBodyPartHealth(EBodyPart.Common).Current;


                    if (vector.z > 0)
                    {

                        // player esp + dist
                        GUI.Label(new Rect(vector.x - 50f, Screen.height - vector.y, 200f, 100f), Name + " [" + $"{distance}m" + "]");

                        // player health
                        if (Cfg.health)
                        {
                            GUI.Label(new Rect(vector.x - 50f, Screen.height - vector.y + 15f, 200f, 100f), "HP: [" + Health.ToString() + "]");
                        }
                        // player lines
                        if (Cfg.snaplines)
                        {
                            draw.DrawLine(player, vec2DrawLine); 
                        }



                        // player bones, yes its messy af :c
                        if (Cfg.skelton)
                        {
                            var playerRightPalmVector = new Vector3(
                                Camera.main.WorldToScreenPoint(player.PlayerBones.RightPalm.position).x,
                                Camera.main.WorldToScreenPoint(player.PlayerBones.RightPalm.position).y,
                                Camera.main.WorldToScreenPoint(player.PlayerBones.RightPalm.position).z);
                            var playerLeftPalmVector = new Vector3(
                                Camera.main.WorldToScreenPoint(player.PlayerBones.LeftPalm.position).x,
                                Camera.main.WorldToScreenPoint(player.PlayerBones.LeftPalm.position).y,
                                Camera.main.WorldToScreenPoint(player.PlayerBones.LeftPalm.position).z);
                            var playerLeftShoulderVector = new Vector3(
                                Camera.main.WorldToScreenPoint(player.PlayerBones.LeftShoulder.position).x,
                                Camera.main.WorldToScreenPoint(player.PlayerBones.LeftShoulder.position).y,
                                Camera.main.WorldToScreenPoint(player.PlayerBones.LeftShoulder.position).z);
                            var playerRightShoulderVector = new Vector3(
                                Camera.main.WorldToScreenPoint(player.PlayerBones.RightShoulder.position).x,
                                Camera.main.WorldToScreenPoint(player.PlayerBones.RightShoulder.position).y,
                                Camera.main.WorldToScreenPoint(player.PlayerBones.RightShoulder.position).z);
                            var playerNeckVector = new Vector3(
                                Camera.main.WorldToScreenPoint(player.PlayerBones.Neck.position).x,
                                Camera.main.WorldToScreenPoint(player.PlayerBones.Neck.position).y,
                                Camera.main.WorldToScreenPoint(player.PlayerBones.Neck.position).z);
                            var playerCenterVector = new Vector3(
                                Camera.main.WorldToScreenPoint(player.PlayerBones.Pelvis.position).x,
                                Camera.main.WorldToScreenPoint(player.PlayerBones.Pelvis.position).y,
                                Camera.main.WorldToScreenPoint(player.PlayerBones.Pelvis.position).z);
                            var playerRightThighVector = new Vector3(
                                Camera.main.WorldToScreenPoint(player.PlayerBones.RightThigh2.position).x,
                                Camera.main.WorldToScreenPoint(player.PlayerBones.RightThigh2.position).y,
                                Camera.main.WorldToScreenPoint(player.PlayerBones.RightThigh2.position).z);
                            var playerLeftThighVector = new Vector3(
                                Camera.main.WorldToScreenPoint(player.PlayerBones.LeftThigh2.position).x,
                                Camera.main.WorldToScreenPoint(player.PlayerBones.LeftThigh2.position).y,
                                Camera.main.WorldToScreenPoint(player.PlayerBones.LeftThigh2.position).z);
                            var playerRightFootVector = new Vector3(
                                Camera.main.WorldToScreenPoint(player.PlayerBones.KickingFoot.position).x,
                                Camera.main.WorldToScreenPoint(player.PlayerBones.KickingFoot.position).y,
                                Camera.main.WorldToScreenPoint(player.PlayerBones.KickingFoot.position).z);
                            var playerBoundingVector = new Vector3(
                                Camera.main.WorldToScreenPoint(player.Transform.position).x,
                                Camera.main.WorldToScreenPoint(player.Transform.position).y,
                                Camera.main.WorldToScreenPoint(player.Transform.position).z);
                            var playerHeadVector = new Vector3(
                                Camera.main.WorldToScreenPoint(player.PlayerBones.Head.position).x,
                                Camera.main.WorldToScreenPoint(player.PlayerBones.Head.position).y,
                                Camera.main.WorldToScreenPoint(player.PlayerBones.Head.position).z);
                            var playerLeftFootVector = new Vector3(
                                Camera.main.WorldToScreenPoint(define.GetBonePosByID(player, 18)).x,
                                Camera.main.WorldToScreenPoint(define.GetBonePosByID(player, 18)).y,
                                Camera.main.WorldToScreenPoint(define.GetBonePosByID(player, 18)).z
                                );
                            var playerLeftElbow = new Vector3(
                                Camera.main.WorldToScreenPoint(define.GetBonePosByID(player, 91)).x,
                                Camera.main.WorldToScreenPoint(define.GetBonePosByID(player, 91)).y,
                                Camera.main.WorldToScreenPoint(define.GetBonePosByID(player, 91)).z
                                );
                            var playerRightElbow = new Vector3(
                                Camera.main.WorldToScreenPoint(define.GetBonePosByID(player, 112)).x,
                                Camera.main.WorldToScreenPoint(define.GetBonePosByID(player, 112)).y,
                                Camera.main.WorldToScreenPoint(define.GetBonePosByID(player, 112)).z
                                );
                            var playerLeftKnee = new Vector3(
                                Camera.main.WorldToScreenPoint(define.GetBonePosByID(player, 17)).x,
                                Camera.main.WorldToScreenPoint(define.GetBonePosByID(player, 17)).y,
                                Camera.main.WorldToScreenPoint(define.GetBonePosByID(player, 17)).z
                                );
                            var playerRightKnee = new Vector3(
                                Camera.main.WorldToScreenPoint(define.GetBonePosByID(player, 22)).x,
                                Camera.main.WorldToScreenPoint(define.GetBonePosByID(player, 22)).y,
                                Camera.main.WorldToScreenPoint(define.GetBonePosByID(player, 22)).z
                                );

                            // draw skeleton lines
                            Color playerColor = Color.white;
                            draw.Line(new Vector2(playerNeckVector.x, (float)Screen.height - playerNeckVector.y), new Vector2(playerCenterVector.x, (float)Screen.height - playerCenterVector.y), playerColor, 2);
                            draw.Line(new Vector2(playerLeftShoulderVector.x, (float)Screen.height - playerLeftShoulderVector.y), new Vector2(playerLeftElbow.x, (float)Screen.height - playerLeftElbow.y), playerColor, 2);
                            draw.Line(new Vector2(playerRightShoulderVector.x, (float)Screen.height - playerRightShoulderVector.y), new Vector2(playerRightElbow.x, (float)Screen.height - playerRightElbow.y), playerColor, 2);
                            draw.Line(new Vector2(playerLeftElbow.x, (float)Screen.height - playerLeftElbow.y), new Vector2(playerLeftPalmVector.x, (float)Screen.height - playerLeftPalmVector.y), playerColor, 2);
                            draw.Line(new Vector2(playerRightElbow.x, (float)Screen.height - playerRightElbow.y), new Vector2(playerRightPalmVector.x, (float)Screen.height - playerRightPalmVector.y), playerColor, 2);
                            draw.Line(new Vector2(playerRightShoulderVector.x, (float)Screen.height - playerRightShoulderVector.y), new Vector2(playerLeftShoulderVector.x, (float)Screen.height - playerLeftShoulderVector.y), playerColor, 2);
                            draw.Line(new Vector2(playerLeftKnee.x, (float)Screen.height - playerLeftKnee.y), new Vector2(playerCenterVector.x, (float)Screen.height - playerCenterVector.y), playerColor, 2);
                            draw.Line(new Vector2(playerRightKnee.x, (float)Screen.height - playerRightKnee.y), new Vector2(playerCenterVector.x, (float)Screen.height - playerCenterVector.y), playerColor, 2);
                            draw.Line(new Vector2(playerLeftKnee.x, (float)Screen.height - playerLeftKnee.y), new Vector2(playerLeftFootVector.x, (float)Screen.height - playerLeftFootVector.y), playerColor, 2);
                            draw.Line(new Vector2(playerRightKnee.x, (float)Screen.height - playerRightKnee.y), new Vector2(playerRightFootVector.x, (float)Screen.height - playerRightFootVector.y), playerColor, 2);
                        }
                    }  
                }
            }

            // probs worst way to do item esp but fuck it.
            foreach (EFT.Interactive.LootableContainer Container in container.Objects)
            {
                if (!Container.isActiveAndEnabled) continue;
                Vector3 vector = Camera.main.WorldToScreenPoint(Container.transform.position);
                float dist = Vector3.Distance(Camera.main.transform.position, Container.transform.position);
                int distance = (int)dist;
                if (vector.z > 0)
                {
                    if (dist < 500) // 500 dist max
                    {
                        // duffle bag
                        if (Cfg.duffleBag && Container.name == "lootable")
                        {
                            GUI.Label(new Rect(vector.x, Screen.height - vector.y, 200f, 100f), "Duffle Bag" + " [" + $"{distance}m" + "]");
                        }
                        // weapon box
                        else if (Cfg.weaponBox && Container.name == "cover_64")
                        {
                            GUI.Label(new Rect(vector.x, Screen.height - vector.y, 200f, 100f), "Weapon Box" + " [" + $"{distance}m" + "]");
                        }
                        else if (Cfg.weaponBox && Container.name == "cover_140")
                        {
                            GUI.Label(new Rect(vector.x, Screen.height - vector.y, 200f, 100f), "Weapon Box" + " [" + $"{distance}m" + "]");
                        }
                        else if (Cfg.weaponBox && Container.name == "cover_110x45")
                        {
                            GUI.Label(new Rect(vector.x, Screen.height - vector.y, 200f, 100f), "Weapon Box" + " [" + $"{distance}m" + "]");
                        }
                        // tool box
                        else if (Cfg.toolBox && Container.name == "Toolbox_Door")
                        {
                            GUI.Label(new Rect(vector.x, Screen.height - vector.y, 200f, 100f), "Tool Box" + " [" + $"{distance}m" + "]");
                        }
                        // supply Box
                        else if (Cfg.supplyBox && Container.name == "container_supplyBox_opened_Cap")
                        {
                            GUI.Label(new Rect(vector.x, Screen.height - vector.y, 200f, 100f), "Supply Box" + " [" + $"{distance}m" + "]");
                        }
                        // wooden Crate
                        else if (Cfg.woodenCrate && Container.name == "weapon_box_cover")
                        {
                            GUI.Label(new Rect(vector.x, Screen.height - vector.y, 200f, 100f), "Wooden Crate" + " [" + $"{distance}m" + "]");
                        }
                        // Stash
                        else if (Cfg.Stash && Container.name == "scontainer_Blue_Barrel_Base_Cap")
                        {
                            GUI.Label(new Rect(vector.x, Screen.height - vector.y, 200f, 100f), "Stash" + " [" + $"{distance}m" + "]");
                        }
                        else if (Cfg.Stash && Container.name == "scontainer_wood_CAP")
                        {
                            GUI.Label(new Rect(vector.x, Screen.height - vector.y, 200f, 100f), "Stash" + " [" + $"{distance}m" + "]");
                        }
                        // ammo Crate
                        else if (Cfg.ammoCrate && Container.name == "Ammo_crate_Cap")
                        {
                            GUI.Label(new Rect(vector.x, Screen.height - vector.y, 200f, 100f), "Ammo Crate" + " [" + $"{distance}m" + "]");
                        }
                        // Money Box
                        else if (Cfg.MoneyBox && Container.name == "item_static_container_moneyBag_03_cover")
                        {
                            GUI.Label(new Rect(vector.x, Screen.height - vector.y, 200f, 100f), "Money Box" + " [" + $"{distance}m" + "]");
                        }
                        // Grenade Box
                        else if (Cfg.grenadeBox && Container.name == "Grenade_box_Door")
                        {
                            GUI.Label(new Rect(vector.x, Screen.height - vector.y, 200f, 100f), "Grenade Box" + " [" + $"{distance}m" + "]");
                        }
                        // Safe
                        else if (Cfg.Safe && Container.name == "boor_safe")
                        {
                            GUI.Label(new Rect(vector.x, Screen.height - vector.y, 200f, 100f), "Safe" + " [" + $"{distance}m" + "]");
                        }
                    }
                }
            }

            if (Cfg.itemground)
            {
                foreach (EFT.Interactive.LootItem loot in Item.Objects)
                {
                    if (!loot.isActiveAndEnabled) continue;
                    Vector3 vector = Camera.main.WorldToScreenPoint(loot.transform.position);
                    float dist = Vector3.Distance(Camera.main.transform.position, loot.transform.position);
                    int distance = (int)dist;
                    if (vector.z > 0)
                    {
                        if (dist < 350)
                        {
                            // will display all items on ground. for testing only, not done.
                            GUI.Label(new Rect(vector.x, Screen.height - vector.y, 200f, 100f), loot.name);
                        }
                    }
                }
            }


            if (Cfg.extraction)
            {
                foreach (EFT.Interactive.ExfiltrationPoint Extraction in extraction.Objects)
                {
                    if (!Extraction.isActiveAndEnabled) continue;
                    Vector3 vector = Camera.main.WorldToScreenPoint(Extraction.transform.position);
                    float dist = Vector3.Distance(Camera.main.transform.position, Extraction.transform.position);
                    int distance = (int)dist;
                    if (vector.z > 0)
                    {
                        string name = Extraction.name;
                        string simpleName = define.extractionNametoSimpleName(name);
                        GUI.Label(new Rect(vector.x, Screen.height - vector.y, 200f, 100f), simpleName + " [" + $"{distance}m" + "]");
                    }
                }
            }

            if (Cfg.corpse)
            {
                foreach (EFT.Interactive.Corpse corpse in Corpse.Objects)
                {
                    if (!corpse.isActiveAndEnabled) continue;
                    Vector3 vector = Camera.main.WorldToScreenPoint(corpse.transform.position);
                    float dist = Vector3.Distance(Camera.main.transform.position, corpse.transform.position);
                    int distance = (int)dist;
                    if (vector.z > 0)
                    {
                        GUI.Label(new Rect(vector.x - 50f, Screen.height - vector.y, 200f, 100f), "Corpse" + " [" + $"{distance}m" + "]");
                    }
                }
            }
        }

        // menu
        public void Draw(int id)
        {
            GUI.DragWindow(new Rect(0, 0, 0x1000, 30));
            GUI.Label(new Rect(10, 20, 120, 25), "Display Exits");
            Cfg.extraction = GUI.Toggle(new Rect(10, 40, 120, 25), Cfg.extraction, " Extraction");
            GUI.Label(new Rect(10, 60, 120, 25), "Display Loot");
            Cfg.duffleBag = GUI.Toggle(new Rect(10, 80, 120, 25), Cfg.duffleBag, " Duffle Bag");
            Cfg.weaponBox = GUI.Toggle(new Rect(10, 100, 120, 25), Cfg.weaponBox, " Weapon Box");
            Cfg.toolBox = GUI.Toggle(new Rect(10, 120, 120, 25), Cfg.toolBox, " Tool Box");
            Cfg.supplyBox = GUI.Toggle(new Rect(10, 140, 120, 25), Cfg.supplyBox, " Supply Box");
            Cfg.woodenCrate = GUI.Toggle(new Rect(10, 160, 120, 25), Cfg.woodenCrate, " Wooden Crate");
            Cfg.Stash = GUI.Toggle(new Rect(10, 180, 120, 25), Cfg.Stash, " Stash");
            Cfg.ammoCrate = GUI.Toggle(new Rect(10, 200, 120, 25), Cfg.ammoCrate, " Ammo Crate");
            Cfg.MoneyBox = GUI.Toggle(new Rect(10, 220, 120, 25), Cfg.MoneyBox, " Money Box");
            Cfg.grenadeBox = GUI.Toggle(new Rect(10, 240, 120, 25), Cfg.grenadeBox, " Grenade Box");
            Cfg.Safe = GUI.Toggle(new Rect(10, 260, 120, 25), Cfg.Safe, " Safe");
            GUI.Label(new Rect(10, 280, 120, 25), "Display Corpse");
            Cfg.corpse = GUI.Toggle(new Rect(10, 300, 120, 25), Cfg.corpse, " Corpse");
            GUI.Label(new Rect(10, 320, 120, 25), "Display Items");
            Cfg.itemground = GUI.Toggle(new Rect(10, 340, 120, 25), Cfg.itemground, " Items");

            GUI.Label(new Rect(10, 360, 120, 25), "Display Players");
            Cfg.playerESP = GUI.Toggle(new Rect(10, 380, 120, 25), Cfg.playerESP, " Player");
            Cfg.health = GUI.Toggle(new Rect(10, 400, 120, 25), Cfg.health, " Health");
            Cfg.skelton = GUI.Toggle(new Rect(10, 420, 120, 25), Cfg.skelton, " Skeleton");
            Cfg.snaplines = GUI.Toggle(new Rect(10, 440, 120, 25), Cfg.snaplines, " Snaplines");
            Cfg.chams = GUI.Toggle(new Rect(10, 460, 120, 25), Cfg.chams, " Chams");
            Cfg.drawPlayerBox = GUI.Toggle(new Rect(10, 480, 120, 25), Cfg.drawPlayerBox, "Draw Box");



            GUI.Label(new Rect(10, 495, 120, 25), "Features");
            Cfg.thermal = GUI.Toggle(new Rect(10, 520, 120, 25), Cfg.thermal, " Thermal");
            Cfg.stamina = GUI.Toggle(new Rect(10, 540, 120, 25), Cfg.stamina, " Stamina");
            Cfg.recoil = GUI.Toggle(new Rect(10, 560, 120, 25), Cfg.recoil, " Recoil");
            Cfg.sway = GUI.Toggle(new Rect(10, 580, 120, 25), Cfg.sway, " Sway");
            Cfg.restrictions = GUI.Toggle(new Rect(10, 600, 120, 25), Cfg.restrictions, " Movement");
            Cfg.day = GUI.Toggle(new Rect(10, 620, 120, 25), Cfg.day, " Always Day");
            Cfg.speedhack = GUI.Toggle(new Rect(10, 640, 120, 25), Cfg.speedhack, "SpeedHack"); //Cfg.flyingHack
            Cfg.flyingHack = GUI.Toggle(new Rect(10, 660, 120, 25), Cfg.flyingHack, "Flyhack"); //Cfg.flyingHack
            Cfg.MaxStats = GUI.Toggle(new Rect(10, 680, 120, 25), Cfg.MaxStats, "Max Stats"); //Cfg.flyingHack //Cfg.ExperiemntFeature
            Cfg.ExperiemntFeature = GUI.Toggle(new Rect(10, 700, 120, 25), Cfg.ExperiemntFeature, "Test"); //Cfg.flyingHack




        }
    }
}



