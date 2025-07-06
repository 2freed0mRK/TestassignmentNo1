# TestassignmentNo1
Welcome! In this repository, you'll find my solution for a test task, along with a video demonstration.

## Bottom Bar

https://github.com/user-attachments/assets/d54531f8-7a70-4a04-b5c8-c868deffe701

### Bottom Bar Features
* Modular Component Design: The project is built with a strong emphasis on component modularity. This approach ensures that individual UI elements and functionalities are independent, reusable, and easy to maintain or scale.
* Responsive Bottom Bar Navigation: The application features a bottom navigation bar with interactive buttons.
* Button State Management: Each button's script supports both an open and closed state, triggering distinct animations based on its current status for enhanced interactivity.
* Adaptive Animations: A core highlight is the responsiveness of animations across various screen resolutions and aspect ratios. Animations scale and adjust smoothly to provide a consistent user experience.
* Custom Button Behaviors: I've leveraged standard button behaviors and their triggers, enhancing them with custom logic and interactions to create a more dynamic user interface.
* Optimized Animation Updates: Animations utilize OnDidApplyAnimationProperties clips. This approach ensures that objects are updated only when their animated properties actually change, contributing to performance efficiency.


## Settings

https://github.com/user-attachments/assets/68c10b1f-16fd-466b-9b17-df094cede296

### Settings Screen Features
The settings screen demonstrates additional UI and animation capabilities:
* Animated Window Entry: The settings window features an animated entry sequence, a generic animation that can be reused for other windows or pop-ups.
* Button Animations with Overrides: Buttons on this screen showcase various animations for different sizes. Currently, these animations are handled using separate animation clips and Animator Overrides rather than adaptive scripts, demonstrating a flexible approach to animation management.
* Toggle Animations: Interactive toggles on the settings screen also feature their own distinct animations for a polished user experience.
* SDF Shader for Rounded Corners: The top of the settings window utilizes an SDF (Signed Distance Field) shader for smooth rounded corners. This shader uses a 128x100 uncompressed texture to ensure better filtering and visual quality.
* Animated Window Exit: The settings window includes a clean animated exit sequence, ensuring a smooth transition when closed.


## Reward Screen

https://github.com/user-attachments/assets/9a67461b-408c-4486-b3b7-b44e70060001

### Reward Screen Features
This screen highlights a celebratory user experience after completing a level:
* Reused Popup Animation: The reward screen utilizes the same animated popup opening sequence as the settings screen, showcasing reusability of UI components.
* Unique Reward Animation Layer: An additional animation layer is implemented for the reward-specific elements, creating a unique and engaging reveal sequence for various items on this screen.
* Particle Effects: Particle systems are incorporated to enhance the visual impact and celebratory feel of the reward presentation.
* Standard Button Animations: The buttons on this screen use standard animation behaviors, maintaining consistency with other UI elements.
* Custom "Level Completed" Text: A distinctive "Level Completed" text is created using multiple TextMeshPro components, allowing for complex visual effects and fine-tuned typography.

## Responsiveness & Adaptivity

https://github.com/user-attachments/assets/b532b6d9-1ed6-43ab-ab37-54d859b67db8

### Features
The UI is designed to be highly adaptive and provide a consistent experience across various devices:
* Bottom Bar Adaptive Animations: The animations for the bottom navigation bar fully support different screen resolutions, ensuring smooth transitions regardless of the device.
* Top Bar Layout for Tablets: The top resource bar is designed to anchor to the left edge on tablet devices, optimizing space utilization and readability on wider screens.
* Mobile SafeArea Support: Full support for mobile device Safe Area insets is implemented, preventing UI elements from being obscured by notches, punch-holes, or rounded corners.


## SDF Rounded Corners & Gradient

https://github.com/user-attachments/assets/37a51b34-07db-4a0a-9464-102253a1a2f4

### Features
* Chosen for its optimization benefits in terms of RAM and disk space, while maintaining high visual quality across various resolutions.
* A gradient effect was additionally implemented adjacent to the rounded corners, as per design mockups. This approach reduces draw calls and perfectly aligns with the artistic vision.

## Tile Shader (SDF-based)

https://github.com/user-attachments/assets/7b8b10a5-3118-425c-8b54-a2b1a1d15011

### Features
* SDF technology was also employed for displaying tiles, ensuring superior visual quality.
* Offsetting tiles between rows was implemented as per the design mockup, creating a visually distinct pattern.
* Supports custom tile rotation, providing flexibility in layout.
* Includes in-shader animations, further optimizing performance and visual effects.

##  General Enhancements & Project Structure
* Dynamic Bottom Bar Content: The bottom bar's icons and names are dynamically managed using an enum-based selection system. This demonstrates an understanding of in-game content population and the ability to easily support and implement new interactive elements.
* Dynamic Bar Visibility: The top and bottom bars feature animated show/hide sequences, enhancing interactivity and providing a cleaner user interface when not actively needed.
* Organized Project Structure: The project assets are meticulously organized into logical groups such as Prefabs, Textures, and Materials. This organization includes a preventive division into "Common" (for project-wide use) and "Feature-specific" categories. Feature-specific assets can be later moved to common if their usage becomes widespread, but remain isolated while unique to a particular feature, promoting clarity and maintainability.

Additional Materials
For more of my work, including other projects currently in progress, please check out my Google Drive:

Google Drive (WIP Projects): https://drive.google.com/drive/folders/1TWV0ApaxSiQEC4xshRLvxrZ3giRt22Vr?usp=drive_link

Email: raman.karabko@gmail.com

LinkedIn: https://www.linkedin.com/in/raman-karabko/

