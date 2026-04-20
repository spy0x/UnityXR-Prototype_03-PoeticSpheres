# 🌌 Poetic Spheres — XR Meditation Experience

![Poetic Spheres GIF](./poetic_spheres.gif "Poetic Spheres GIF")

Poetic Spheres is a mixed-reality Unity prototype where interactive floating spheres bring poems into your physical space. Using hand interactions, each sphere reveals poem metadata and plays spoken-word audio to create a calm, immersive experience.

## ✨ Concept

> *"What if poetry could surround you—literally? A sanctuary where verses float in your space, waiting to be touched and heard."*

This project explores how XR can:
- Enhance mindfulness through immersive, multisensory interaction
- Make digital poetry feel tactile and spatial
- Transform real rooms into responsive art environments

## 🌟 Core Features

- **Dynamic sphere placement** with Meta MR Utility Kit room-aware spawn positions
- **Hand-based interaction** for hover/select behavior and proximity feedback
- **Poem playback system** that triggers spatial audio clips per sphere
- **Adaptive passthrough mood changes** during poem playback
- **Reusable poem data model** via ScriptableObject assets (`Assets/Data`)

## 🧰 Tech Stack

- **Unity** `6000.0.28f1`
- **Meta XR SDK (All)** `71.0.0`
- **URP** `17.0.3`
- **XR Management / Oculus XR Plugin**

See dependencies in: `Packages/manifest.json`

## 📁 Key Project Structure

- `Assets/Scenes/MainGame.unity` — main playable scene (in build settings)
- `Assets/Scripts/GameManager.cs` — poem allocation and passthrough transitions
- `Assets/Scripts/PoeticSphere.cs` — sphere behavior, poem triggering, label UI
- `Assets/Scripts/PoemPlayer.cs` — central audio playback control
- `Assets/Scripts/HandProximity.cs` — close-range interaction behavior
- `Assets/Scripts/Data/Poem.cs` — poem ScriptableObject definition
- `Assets/Data/` — poem content/audio assets

## 🚀 Getting Started

1. Install **Unity 6 (6000.0.28f1)**.
2. Clone this repository.
3. Open the project folder in Unity.
4. Let Package Manager resolve dependencies.
5. Open `Assets/Scenes/MainGame.unity`.
6. Connect a compatible Meta Quest device and run via Play Mode/Build.

## 🎮 Interaction Flow

- Spheres are spawned in valid room locations.
- Hovering can show poem title/author labels.
- Selecting/expanding a sphere triggers poem audio.
- While audio plays, passthrough brightness fades for atmosphere.
- Spheres restore after playback and can be re-engaged.

## 📝 Notes

- The repository includes Meta sample content under `Assets/Samples/`.
- Current build settings include `MainGame.unity` as the enabled scene.

## 📜 License

No explicit license file is currently included in this repository.
