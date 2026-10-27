# GitHub Copilot Instructions for AutopsyTable (Continued) Mod

## Mod Overview and Purpose

AutopsyTable (Continued) is an updated version of the original AutopsyTable mod. It provides players with the ability to perform autopsies on corpses to retrieve valuable organs and bionics. The mod introduces two types of autopsies: a normal autopsy that only gives organs/bionics without a butchering debuff, and a brutal autopsy that also gives skin/flesh but comes with a butchering debuff. If Ideology is installed, autopsies have a chance of spawning skulls. Additionally, the mod supports the autopsy of creatures from the Call of Cthulhu - Cosmic Horrors mod. It also includes mod-options to control the consumption of the corpse after autopsy and allows for multiple autopsies of the same corpse.

## Key Features and Systems

- **Normal and Brutal Autopsies**: Players can choose between two types of autopsies, each with its own rewards and drawbacks.
- **Skull Spawning**: If Ideology is installed, autopsies have a chance to spawn skulls.
- **Cosmic Horrors Support**: The mod supports autopsy of creatures from the Call of Cthulhu - Cosmic Horrors mod.
- **Mod-Options**: Players can control the consumption of the corpse after autopsy and allow for multiple autopsies of the same corpse.

## Coding Patterns and Conventions

- **Mod Structure**: The mod is organized into several C# files, each responsible for different aspects of the mod's functionality.
- **XML Integration**: The mod uses XML files to define various game elements such as recipes, thing filters, and work givers.
- **Harmony Patching**: The mod uses Harmony, a popular modding framework for RimWorld, to patch and extend vanilla game functionality.

## XML Integration

The mod includes the following XML files:

- `Defs/RecipeDefs/Recipes_Autopsy.xml`: Defines the recipes for performing autopsies.
- `Defs/SpecialThingFilterDefs/SpecialThingFilters_Autopsy.xml`: Defines special thing filters for autopsies.
- `Defs/ThingDefs/Building_AutopsyTable.xml`: Defines the autopsy table building.
- `Defs/WorkGiverDefs/WorkGivers.xml`: Defines work givers for autopsies.
- `Patches/Patch_CosmicHorrors.xml`: Patches for compatibility with the Call of Cthulhu - Cosmic Horrors mod.

## Harmony Patching

The mod uses Harmony to patch various vanilla game methods to extend functionality. For example, the `Corpse_ButcherProducts` class includes both `Prefix` and `Postfix` methods to modify the behavior of butchering and autopsy processes.

## Suggestions for Copilot

- **Code Snippets**: Use Copilot to generate code snippets for new features or bug fixes. For example, creating a new recipe or modifying an existing method.
- **XML Definitions**: Use Copilot to generate XML definitions for new game elements such as recipes, thing filters, or work givers.
- **Harmony Patches**: Use Copilot to generate Harmony patches for existing vanilla methods. This can help in extending or modifying the behavior of the game.
- **Mod-Options UI**: Use Copilot to generate the UI for mod-options, allowing players to customize the behavior of the mod.

## Error Reporting

If you encounter any errors while using the mod, please follow these steps:

1. Ensure that only the AutopsyTable (Continued) mod and its dependencies are active.
2. If the error persists, try adding your other mods one by one until the error occurs again.
3. Post your log using the Log Uploader.
4. For best support, please use the Discord-channel for error-reporting.
5. Do not report errors by making a discussion-thread, as the author will not receive any notification.
6. If you have the solution for a problem, please post it to the GitHub repository.
7. Use RimSort to sort your mods.

## Tags

- organ harvesting
- butchering
- dissection

By following these guidelines, you can effectively use GitHub Copilot to assist with your AutopsyTable (Continued) mod development.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

