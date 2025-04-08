# 🌌 Poetic Spheres - XR Meditation Experience  

## ✨ Idea  
**Elevator Pitch:**  
*"What if poetry could surround you—literally? A sanctuary where verses float in your space, waiting to be touched and heard."*  

This prototype demonstrates how XR can:  
- 🧠 **Enhance mindfulness** through multi-sensory immersion  
- 🖐️ **Humanize tech** with tactile poetry interactions  
- 🌠 **Transform any room** into a dynamic art installation  

**Why XR?**  
- Leverages **passthrough** to blend digital poetry with real-world familiarity  
- Uses **hand tracking** for intimate, controller-free engagement  
- Adapts to **any physical space** (thanks to Meta SDK's Room Presence)  

---

## 🌟 Core Features  
*Essential components to create this meditative experience:*  

| Feature | Tech Used | Purpose |  
|---------|-----------|---------|  
| **🌀 Glowing Poetry Spheres** | `Meta SDK RoomPresence.FindSpawnPositions()` | Dynamically places floating poems in user's environment |  
| **👆 Touch-Activated Audio** | `Meta SDK HandTracking` + `Unity AudioSource` | Plays poems when spheres are touched (hand-tracking) |  
| **🌓 Adaptive Ambient Lighting** | `Meta SDK EffectMesh` + `Passthrough` | Creates mood-matched lighting for immersion |  
| **🎧 Spatial Audio Poems** | `Unity Spatializer` | Makes verses feel physically present in the room |  

---

##
