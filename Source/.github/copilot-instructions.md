# Copilot Instructions

## Project Guidelines
- In this repository, relevant mod XML files are included as Solution Items under the XML folder, and decompiled game source is available as a read-only project named Assembly-CSharp for reference during implementation.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace. When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- Keep this instructions file updated when functional behavior changes so documentation matches the current implementation.

## Current Feature Notes
- Normal flow: `AutopsyHumanoid`
- Alternate flow: `AutopsyHumanoidBrutal`
- Settings used by normal flow:
  - `KeepInputItem`
  - `AllowRepeatUntilDepleted`
- Patch points in current implementation:
  - `Toils_Recipe_ConsumeIngredients`
  - `WorkGiver_DoBill_IsUsableIngredient_Patch`
  - `Corpse_ButcherProducts`
- `WorkGiver_DoBill_IsUsableIngredient_Patch` uses `HasAnyHarvestableAutopsyProducts` to avoid selecting depleted input items for `AutopsyHumanoid`.
- `Corpse_ButcherProducts` updates extracted implant state and uses `AllowRepeatUntilDepleted` to control depletion behavior for the normal flow.