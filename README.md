# VR Room Project — Create with VR

A room-scale VR experience built with Unity and the XR Interaction Toolkit, completed as part of the [Create with VR](https://learn.unity.com/course/create-with-vr) Unity Learn course.

---

## Lessons Completed

- **Unit 1 – Architecture Prototype**: Set up XR Origin, locomotion, teleportation, and object interaction in a 3D architectural walkthrough scene.
- **Unit 2 – 3D Painting Prototype**: Implemented controller-based 3D drawing using ray interactors, trail rendering, and spawn-from-list mechanics.
- **Unit 3 – Training Simulation Prototype**: Built an interactive training environment with buttons, levers, knobs, joysticks, and socket interactors.
- **Final Project – Audrey's VR Room**: Designed a complete, immersive VR room environment combining all course concepts with custom additions.

---

## Key Features

### 1. Analog Wall Clock
- Displays real-world time using `DateTime.Now` from the system clock.
- Hour, minute, and second hands rotate in real-time via script (`WallClock.cs`).
- Rotation is calculated mathematically in `Update()` — no Unity Animation window used.
- Accurate smooth motion: minute hand accounts for seconds, hour hand accounts for minutes.

### 2. Custom Animated Hand Models
- Default controller hands replaced with custom VR hand models (Blue, Gray, Yellow, Colorful variants).
- Hands animate in response to controller inputs (grip and trigger).
- Left and right hand prefabs properly aligned to XR controller transforms.

### 3. Scene Design & Interactivity
- Fully furnished VR room with couches, tables, shelving, lighting, plants, and decorative objects.
- Grabbable and interactable objects using XR Grab Interactable components.
- Teleportation system using both Teleport Area (free movement) and Teleport Anchor (fixed points).
- Tunneling vignette for comfort during locomotion.

### 4. Additional Features
- **Tennis Mini-Game** (`Tennis-Room.unity`): A separate scene with a racket, ball physics, and an auto-player opponent.
- **3D Spatial Audio**: Audio sources placed in the environment for ambient immersion.
- **Particle Effects**: Visual effects integrated into the scene.
- **Video Playback**: In-scene video player support.
- **FPS Display**: Performance monitoring overlay (`DisplayFPS.cs`).
- **Multiple Skyboxes**: AllSkyFree skybox collection for varied environment moods.
- **Scene Transitions**: `LoadScene.cs` enables navigation between scenes.

---

## XR Components Used

| Component | Purpose |
|---|---|
| XR Origin (VR) | Camera rig and controller tracking |
| XR Ray Interactor | Long-range object selection and teleportation |
| XR Direct Interactor | Close-range grabbing |
| XR Grab Interactable | Makes objects grabbable |
| Teleportation Area | Free teleport surface |
| Teleportation Anchor | Fixed teleport destination point |
| XR Interaction Manager | Manages all interactor/interactable matching |
| Input Action Manager | Handles controller button/axis bindings |

---

## Teleportation Setup

For teleportation to work, the following conditions must be met:
1. XR Origin has a **Teleport Interactor** on the controller.
2. The destination object has a **Teleportation Area** or **Teleportation Anchor** component.
3. The destination surface has a **Teleportation Layer** assigned in the Layer Mask.
4. The **Locomotion System** component is present on the XR Origin.
5. The player activates the teleport ray (typically by holding the thumbstick forward).

**Teleport Anchor vs Teleport Area:**
- **Teleport Anchor** — teleports the player to a specific fixed point and orientation. Used for precise destinations (e.g., in front of a table).
- **Teleport Area** — allows the player to teleport anywhere on a surface within the collider bounds. Used for open floor areas.

---

## Performance Techniques

- **Baked Lighting**: Lightmaps pre-baked for the main scene to reduce real-time light calculations.
- **URP (Universal Render Pipeline)**: Lightweight render pipeline optimized for VR/mobile.
- **Object Pooling**: `SpawnPooledObjects.cs` used to reuse objects instead of instantiating/destroying.
- **Occlusion Culling**: Configured to avoid rendering objects outside the player's view.
- **Burst Compiler**: Enabled via Unity's Burst package for optimized math-heavy operations.
- **Android Build Target**: Project built and tested as an APK for standalone VR headset deployment.

---

## Project Structure

```
Assets/
├── _Course Library/       # Course-provided scripts, prefabs, audio, materials
│   ├── Scripts/           # Action, Condition, Control, and custom scripts
│   └── _Prefabs/          # VR hands, room furniture, interactables
├── Challenges/            # Unit challenge scenes (Architecture, 3DPainting, Training)
├── Scenes/
│   ├── Audrey-Room.unity  # Main final project scene
│   └── Tennis-Room.unity  # Bonus mini-game scene
├── Samples/               # XR Interaction Toolkit starter assets
└── Settings/              # URP and XR configuration assets
```
