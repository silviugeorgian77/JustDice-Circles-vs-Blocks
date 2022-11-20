# JustDice-Circles-vs-Blocks

## Acceptance Criteria
- Screen Dimension: Mobile Portrait
- Scene: one color background, in the middle of the screen is a block (“the
- enemy”)
- Core Loop: Tapping on the block increases the player’s gold
- Upgrades: Gold per Tap can be increased by incremental upgrades
- Gold per Tap Scaling: goldPerTap = 5*upgradeLevel^2.1
- Upgrade Cost Scaling: upgradeCost = 5 * 1.08^upgradeLevel
- Passive Damage: Player can buy circles (helpers) that “attack” the block and
- generate passive income
- Up to 5 circles can be bought
- First circle costs 100 gold
- Each following circle costs 10 times as much as the previous one
- Each circle “attacks” once every second
- Each circle can be upgraded by investing gold
- Same scaling formulas as for gold per tap
- Remote config: Scaling formulas are configured on a remote file on a server
- File is downloaded on game start
- Attack / Shooting animation when circles attack the block
- Hit animation when player taps on the block
- Make it pretty. Nice, harmonic colors and effects
- Game runs on actual Android or iOS phone

## Achitecture
As this is an idle game, this should be a data oriented game.
Idle games offer many upgrades and purhcases. Each time something is upgraded or purchased, it affects a couple of areas of the game, thus, it would be a good idea to design the architecture in a way that is easy to make UI updates upon data changes.

The **UserData** class contains properties for each field. On the setters it fires events. Every part of the game interested in those property changes should subscribe to those.

Since our app has a couple of different layers, we must use dependency injection and make sure that each layer is completely independent of the others. Check out **MainSceneManager**, the class that starts the game.

Data persistency. Idle games change the user data very often. We cannot save it after every change, because of power consumption reasons, so data is saved at regual intervals and when the application is sent to background.

## Data Provider
- Responsible for providing the newest game configuration
- Priorities of the data:
  - Online data
    - Timout: 10 seconds
  - Last online data
  - Data that shipped with the app
When new online data is received, it gets cached. Next time, if no internet, the last available data is used.
- If the app never had internet, when we open it, it uses by default the pre-shipped data.

## Rewarded video ideas
- Technical aspects:
  - Use a mediation platform
  - Add at least 4 ad platforms in order to test the incomes, but also to make sure that there is always an ad available
- Placement ideas:
  - Since this is an idle game, we can provide free upgrades via rewarded videos. In this implementation for example, in the expanded menu, from time to time, we can have a rewarded ad button, instead of showing the price.
  - 2X income. In the main screen of the game, from time to time, a TV button can appear from the side with the text "Double income for 10 minutes"
  - 6X income. After the 2X income has been activate, just add to that
  - Add an extra helper Circle for 5 minutes if you watch a rewarded add
