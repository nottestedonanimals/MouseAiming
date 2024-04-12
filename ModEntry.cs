using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;

namespace MouseAiming {

    internal sealed class ModEntry : Mod {

        public override void Entry(IModHelper helper) {
            helper.Events.Input.ButtonsChanged += this.OnButtonChange;
        }

        private void OnButtonChange(object? sender, ButtonsChangedEventArgs e) {

            // Player direction
            // 0 = Up
            // 1 = Right
            // 2 = Down
            // 3 = Left
            // Game1.player.faceDirection(3);

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
                float slope = rise/run;

                if (((slope > 1 || slope < -1) && run < 0 && rise < 0) || ((slope < -1) && run > 0 && rise < 0) || (slope > 1 && run > 0 && rise < 0)) {

                    player.faceDirection(0);

                } else if ((slope > -1 && run > 0 && rise < 0) || (slope < 1 && run > 0 && rise > 0)) {

                    player.faceDirection(1);

                }
                
                else if (((slope > 1 || slope < -1) && run > 0 && rise > 0) || ((slope < -1) && run < 0 && rise > 0) || (slope > 1 && run > 0 && rise < 0)) {

                    player.faceDirection(2);

                } else if ((slope > -1 && run < 0 && rise > 0) || (slope < 1 && run < 0 && rise < 0)) {

                    player.faceDirection(3);

                }
               
                else {
                    player.faceDirection(player.FacingDirection);
                }

            }

        }
    }
}