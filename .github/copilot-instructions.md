# GitHub Copilot Instructions for AutopsyTable Mod (Continued)

## Mod Overview and Purpose
The **AutopsyTable** mod enhances the gameplay of RimWorld by allowing players to perform detailed autopsies on corpses. This is an update to the original mod by bodlosh, incorporating new features, enhanced support, and improved gameplay mechanics. The purpose of the mod is to provide more depth to post-mortem procedures by offering players the ability to extract organs, bionics, and other resources from corpses with varied outcomes based on their approach.

## Key Features and Systems

### New Autopsy Types
- **Normal Autopsy**: Allows extraction of organs and bionics without incurring a butcher debuff. The corpse is not consumed in the process.
- **Brutal Autopsy**: Generates skin and flesh parts along with a butcher debuff. 

### Ideology Support
- Additional functionality is added for players with the Ideology expansion, introducing a chance for skulls to spawn during autopsies.

### Compatibility with Other Mods
- Supports autopsies of creatures from the **Call of Cthulhu - Cosmic Horrors** mod.

### Factors Influencing Success
- Doctor skill level.
- Age of the corpse (after 24 hours, success is nearly impossible).
- Surgery success and infection chances based on the room and material quality, with silver tables offering enhanced performance.

## Coding Patterns and Conventions

### General Conventions
- Adhere to C# naming conventions: PascalCase for class names and methods, camelCase for method parameters and local variables.
- Use XML documentation comments to document methods and classes to maintain clear code documentation.

### Classes and Methods Overview
- Utilize internal classes for mod configurations such as `AutopsyTableMod` and `AutopsyTableSettings`.
- Extend RimWorld base classes like `Building_WorkTable` for new derived classes like `Building_AutopsyTable`.

### XML Integration
- Use XML files for configuration and localization to facilitate mod updates and translations. This setup enables seamless addition of new translations like the included Russian translation by jose.

## Harmony Patching
- Implement Harmony Patches for integrating new functionalities or modifying the game's base behavior without changing original source code.
- Example: Use a static class `WorkGiver_DoAutopsyBill` to patch and alter the base game's methods where necessary.

## Suggestions for Copilot
- **Mirroring Patterns**: Copilot can assist with generating repetitive code structures, such as creating new autopsy types by following the existing classes and methods like `Building_AutopsyTable`.
- **Documentation**: Leverage Copilot to fill in XML documentation comments for methods and classes where missing.
- **Localization**: Use Copilot to suggest additional translations or help format new XML translation files.
- **Harmony Patches**: Facilitate writing new patches by suggesting Harmony patch templates based on existing usage.

## Troubleshooting and Support
- Ensure that any persistent error is tested by isolating the mod with its requirements active and using the Log Uploader for detailed reporting.
- For best support, engage with the community on Discord channels dedicated to error-reporting instead of posting it as a discussion thread.
- Use RimSort for optimal mod organization to avoid conflicts.

## Tags
`organ harvesting`, `butchering`, `dissection`



This `.github/copilot-instructions.md` file provides comprehensive guidelines and insights into the mod’s features, coding principles, XML integration, and Harmony patching practices. It also outlines suggestions for using GitHub Copilot effectively in this context.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The .github/copilot-instructions.md file is included in the solution under the .github solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution .github copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.
