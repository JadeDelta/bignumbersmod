# 🧩 Big Numbers Mod

> A lightweight economy scaling mod that dynamically adjusts item and shop values as you progress — fully configurable and multiplayer-aware.

![Unity](https://img.shields.io/badge/Unity-2021%2B-black?logo=unity)
![BepInEx](https://img.shields.io/badge/BepInEx-6.0+-blue)
![Harmony](https://img.shields.io/badge/Harmony-Patching-success)
![License](https://img.shields.io/badge/License-MIT-green)

---

## 💡 Overview

**Big Numbers Mod** enhances the in-game economy by scaling item and shop values with each level you complete.  
It’s simple, configurable, and designed to keep progression exciting without breaking balance.

All multipliers are editable through the config file — so you can fine-tune your economy from “slightly boosted” to “absolutely absurd”.

---

## ⚙️ Features

- **📈 Item Scaling:**  
  Increases item value ranges each level by a configurable percentage.

- **🏪 Shop Scaling:**  
  Shop prices grow based on a base multiplier and an additional per-level multiplier.

- **🔧 Fully Configurable:**  
  Adjust multipliers easily via the generated `.cfg` file.

- **👥 Multiplayer Aware:**  
  Automatically applies a slight discount for each additional player.

---

## 🧰 Configuration

Once the mod runs for the first time, it generates a config file at:
```
BepInEx/config/com.river.GameScaling.cfg
```

**Default values:**
```ini
[Valuables]
MultiplierPerLevel = 0.2   # +20% item value per level after the first

[Shop]
BaseMultiplier = 4.0       # Base shop price multiplier
MultiplierPerLevel = 0.2   # +20% shop price per level after the first
```

You can freely tweak these to fit your preferred difficulty or pacing.

---

## 📦 Installation

1. Install **[Thunderstore Mod Manager](https://www.overwolf.com/app/Thunderstore-Thunderstore_Mod_Manager)** or **r2modman**.  
2. Download the latest release of **BigNumbersMod.zip**.  
3. Extract the archive into your game’s folder:  
   ```
   /BepInEx/plugins/
   ```
4. Launch the game — the mod loads automatically.  
5. (Optional) Edit the generated config to customize multipliers.

---

## 📁 Folder Structure

```
BigNumbersMod/
├── src/
│   └── GameScaling.cs
├── refs/
│   ├── BepInEx.dll
│   ├── 0Harmony.dll
│   └── UnityEngine.dll
├── build.bat
├── README.md
└── icon.png (optional)
```

All source code lives in `/src`, with dependencies in `/refs`.  
Run `build.bat` to compile the plugin.

---

## 🧠 Technical Details

- **Frameworks:** BepInEx 6, Harmony Patching  
- **Plugin ID:** `com.river.GameScaling`  
- **Main Class:** `GameScaling.cs`  
- **Version:** 1.0.0  

---

## ☕ Support

If you like the mod or my work, consider supporting me on Ko-fi ❤️  
👉 [**ko-fi.com/jadedelta**](https://ko-fi.com/jadedelta)

---

## 📜 License

This project is licensed under the **MIT License** — you’re free to modify and share it, just give credit.

---

### 🖋️ Credits

Developed by **JadeDelta**  
Made with ❤️ and too much coffee.
