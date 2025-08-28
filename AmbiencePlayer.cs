using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace TerrariaAmbience
{
    public class AmbiencePlayer : ModPlayer
    {
        private string lastBiome = "";
        private SlotId currentSoundSlot;
        private ActiveSound activeSound; // Reference to the playing sound
        private bool soundPlaying = false;

        // Predefined looped sounds
        private static readonly SoundStyle JungleSound = new SoundStyle("TerrariaAmbience/Sounds/Custom/JungleRany")
        {
            IsLooped = true,
            Volume = 0.7f
        };
        private static readonly SoundStyle SnowSound = new SoundStyle("TerrariaAmbience/Sounds/Custom/SnowRany")
        {
            IsLooped = true,
            Volume = 0.7f
        };
        private static readonly SoundStyle DesertSound = new SoundStyle("TerrariaAmbience/Sounds/Custom/DesertRany")
        {
            IsLooped = true,
            Volume = 0.7f
        };
        private static readonly SoundStyle ForestSound = new SoundStyle("TerrariaAmbience/Sounds/Custom/ForestRany")
        {
            IsLooped = true,
            Volume = 0.7f
        };

        public override void PostUpdateMiscEffects()
        {
            string currentBiome = "";

            if (Player.ZoneJungle)
                currentBiome = "Jungle";
            else if (Player.ZoneSnow)
                currentBiome = "Snow";
            else if (Player.ZoneDesert)
                currentBiome = "Desert";
            else
                currentBiome = "Forest";

            if (currentBiome != "" && currentBiome != lastBiome)
            {
                Main.NewText("You are now in: " + currentBiome, 200, 200, 255);

                // Stop previous sound if one is playing
                if (soundPlaying && activeSound != null && activeSound.IsPlaying)
                {
                    activeSound.Stop();
                }

                // Play new biome sound and keep reference
                switch (currentBiome)
                {
                    case "Jungle":
                        currentSoundSlot = SoundEngine.PlaySound(JungleSound, Player.Center);
                        break;
                    case "Snow":
                        currentSoundSlot = SoundEngine.PlaySound(SnowSound, Player.Center);
                        break;
                    case "Desert":
                        currentSoundSlot = SoundEngine.PlaySound(DesertSound, Player.Center);
                        break;
                    case "Forest":
                        currentSoundSlot = SoundEngine.PlaySound(ForestSound, Player.Center);
                        break;
                }

                if (SoundEngine.TryGetActiveSound(currentSoundSlot, out var sound))
                {
                    activeSound = sound;
                }

                soundPlaying = true;
                lastBiome = currentBiome;
            }

            // Make sure the sound follows the player
            if (soundPlaying && activeSound != null && activeSound.IsPlaying)
            {
                activeSound.Position = Player.Center;
            }
        }
    }
}
