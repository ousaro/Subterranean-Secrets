# Subterranean-Secrets

This project is a demonstration of my journey in learning Unity and its various components. It represents my exploration of game development concepts, including scripting, design patterns, built-in Unity functionalities, and multiple aspects of game mechanics.

<img src="/screenshots.jpg" alt="Home Screen" width="200" style="display: inline-block; margin-right: 20px;"/>

---

## Game Preview
Watch a preview of the game [here](https://youtu.be/gCIBulQ_Vxg).


## Project Overview

This project serves as a **learning tool** where I focused on understanding and implementing:

- Unity **components** and their functionalities.
- **2D Game Development** concepts, such as handling collisions, triggers, and animations.
- Unity’s **built-in functions** and their **order of execution**.
- Core beginner-level game development skills, including:
  - Using **Rigidbody2D** for physics-based interactions.
  - Working with **Sprite Renderer** for character and environment visuals.
  - Designing user interfaces with the **UI Canvas system**.
  - Implementing smooth camera behavior with **Cinemachine**.
  - Integrating and managing **audio components**.
  - Creating transitions and controlling logic with **Animator** and animation triggers.
  - Implementing tile-based environments using **Tilemap** and **Tilemap Collider**.

---

## Key Features and Concepts Explored

### 1. **Player Management**
- Utilized the **Singleton pattern** to ensure a single instance of the player object.
- Managed player states such as movement, jumping, and interactions using **Rigidbody2D** and **Collider2D**.

### 2. **Physics and Movement**
- Learned how to use Unity's **Rigidbody2D** for realistic physics-based movement.
- Applied **colliders** for detecting environmental boundaries and obstacles.

### 3. **Tilemap and Tilemap Collider**
- Designed and implemented 2D environments using Unity's **Tilemap** system, creating dynamic levels.
- Utilized **Tilemap Collider 2D** to detect and respond to collisions with tiles in the environment.
- Learned to combine **Tilemap** with **Rigidbody2D** and **Collider2D** for smooth interactions between the player and the environment.

### 4. **Built-In Unity Functions**
- Explored and utilized Unity’s built-in methods:
  - **Start()** and **Awake()** for initialization logic.
  - **Update()** for frame-by-frame gameplay mechanics.
  - Implemented **OnTriggerEnter2D** and **OnTriggerExit2D** to handle collisions and environmental interactions.

### 5. **Sprite Renderer and Visual Elements**
- Used the **Sprite Renderer** component to display characters and environmental assets.

### 6. **Animations**
- Created and managed animations for characters using **Unity’s Animation system**.
- Configured **Animator Controllers** to manage state transitions.
- Used **parameters** and **triggers** in Animator to control animations during gameplay.

### 7. **UI Development**
- Designed and implemented interactive menus, health bars, and other UI elements using the **Canvas system**.
- Linked UI elements to scripts for dynamic updates (e.g., updating the health bar as the player takes damage).

### 8. **Audio Integration**
- Implemented background music and sound effects using **Audio Sources**.

### 9. **Cinemachine Camera System**
- Integrated Unity’s **Cinemachine** to create smooth and dynamic camera behavior.

### 10. **Package Manager**
- Explored Unity’s **Package Manager** to add and manage third-party tools and plugins.
- Installed and configured **Cinemachine** and other essential Unity packages.

### 11. Debugging
- Debugged scripts using Unity’s **console** and learned to identify and fix common errors.
---


## Getting Started

1. Clone this repository:
   ```bash
   git clone <repository-url>
   ```
2. Open the project in Unity.
3. Play the game in the Unity Editor to explore the implemented features.

---

## Acknowledgments

This project is part of my learning journey as I strive to become proficient in Unity and game development. It has helped me build a solid foundation and inspired me to dive deeper into creating more complex and polished games.
