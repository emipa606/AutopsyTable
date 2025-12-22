# GitHub Copilot Instructions for AutopsyTable (Continued)

## Mod Overview and Purpose

**AutopsyTable (Continued)** is a RimWorld mod that enhances gameplay by introducing advanced mechanics for dealing with corpses. It allows colonists to perform autopsies and remove organs or bionics without incurring the usual butchering debuff, thereby making the process more civilization-friendly. The mod has been updated with additional translations and improved support for other mods and game expansions.

## Key Features and Systems

- **Bifurcated Autopsy Options:** 
  - **Normal Autopsy:** Yields only organs and bionics without a butchering debuff.
  - **Brutal Autopsy:** Provides additional resources like skin/flesh but induces a butchering debuff.
  
- **Integration with Ideology Expansion:**
  - Autopsies may result in skulls appearing, reflecting game lore and ideology mechanics.

- **Support for Cosmic Horrors:** 
  - Provides compatibility for autopsying creatures from the "Call of Cthulhu - Cosmic Horrors" mod.

- **Dynamic Success Calculations:**
  - Success of autopsies is affected by factors such as the doctor's skill, corpse age, room conditions, and the quality/material of the autopsy table.

## Coding Patterns and Conventions

- **Framework Versioning:** The mod uses multiple `.NETFramework` versions (e.g., v4.7.2, v4.8.1, v4.8), ensuring compatibility and leveraging the latest features where applicable.

- **Class and Method Design:** 
  - Use of static helper classes (e.g., `Corpse_ButcherProducts`) for modular functionality that serves the mod's key features.
  - Focus on clear method definitions with public visibility for essential functionality and private or internal for helper functions.

## XML Integration

- XML files define in-game objects and behaviors that the C# code interacts with. Proper XML structures ensure compatibility with RimWorld's mod loading system.
- XML naming conventions should reflect in-game terminology for easier understanding and consistency.

## Harmony Patching

- Harmony is utilized for method patching to maintain mod compatibility with game updates and other mods.
- Proper notation of Harmony patches should be included to ensure maintainability, and arranged to preserve game stability.

## Suggestions for Copilot

When using GitHub Copilot to assist with additional functionalities or bug fixes:

1. **Code Consistency:** Ensure newly generated code adheres to existing coding styles and conventions used within the mod.
   
2. **Patching Instructions:** Leverage Copilot to draft initial Harmony patches but always review and test them thoroughly for errors or conflicts.
   
3. **Unit Testing:** Use Copilot to aid in creating unit tests that verify the functionality of critical methods such as `getMaxHours()` in `Building_AutopsyTable`.

4. **Documentation:** Encourage Copilot to generate appropriate documentation comments that detail method behaviors and their use cases. This is particularly useful for methods with public access.

5. **Localization Enhancements:** Utilize Copilot for expanding translations or improving localization features by generating translation keys and values.

By following these guidelines, contributors can effectively leverage GitHub Copilot to enhance and maintain the AutopsyTable mod while ensuring high coding standards and maintainability.
