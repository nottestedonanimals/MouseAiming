using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;

namespace MouseAiming {

    internal sealed class ModEntry : Mod {

        private ModConfig Config;

        public const string scytheId = "47";
        public const string goldenScytheId = "53";
        public const string iridiumScytheID = "66";

        public override void Entry(IModHelper helper) {
            
            this.Config = this.Helper.ReadConfig<ModConfig>();

            helper.Events.Input.ButtonsChanged += this.OnButtonChange;
        }

        private void OnButtonChange(object? sender, ButtonsChangedEventArgs e) {

            // Player direction
            // 0 = Up
            // 1 = Right
            // 2 = Down
            // 3 = Left

            if (!Context.IsWorldReady)
                return;

            Farmer player = Game1.player;
            SButton actionButton = new InputButton(true).ToSButton();
            ICursorPosition cursorPosition = this.Helper.Input.GetCursorPosition();

            

            if (actionButton.IsUseToolButton() && this.Helper.Input.GetState(actionButton) == SButtonState.Pressed && Game1.activeClickableMenu == null
            && player.CurrentTool is StardewValley.Tools.MeleeWeapon && !Game1.eventUp && player.canMove && Game1.player.ActiveObject == null &&
            !Game1.player.isEating && Game1.player.CurrentTool != null) {

                Vector2 playerStanding = player.getStandingPosition();

                float rise = cursorPosition.AbsolutePixels.Y - playerStanding.Y;
                float run = cursorPosition.AbsolutePixels.X - playerStanding.X;
                float slope = rise / run;

                if ((!this.Config.includeScythe) && ((Game1.player.CurrentTool.ItemId == "47") || (Game1.player.CurrentTool.ItemId == "53") || (Game1.player.CurrentTool.ItemId == "66"))) {

                } else {

                    if (((slope > 1 || slope < -1) && run < 0 && rise < 0) || ((slope < -1) && run > 0 && rise < 0) || (slope > 1 && run > 0 && rise < 0)) {

                        player.faceDirection(0);

                    } else if ((slope > -1 && run > 0 && rise < 0) || (slope < 1 && run > 0 && rise > 0)) {

                        player.faceDirection(1);

                    } else if (((slope > 1 || slope < -1) && run > 0 && rise > 0) || ((slope < -1) && run < 0 && rise > 0) || (slope > 1 && run > 0 && rise < 0)) {

                        player.faceDirection(2);

                    } else if ((slope > -1 && run < 0 && rise > 0) || (slope < 1 && run < 0 && rise < 0)) {

                        player.faceDirection(3);

                    } else {
                        player.faceDirection(player.FacingDirection);
                    }

                }

            }

        }
    }
}