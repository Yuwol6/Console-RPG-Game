# ConsoleRPG

A console-based role-playing game written in C#, featuring turn-based combat, explicit game state management and core programming fundamentals.
The project focuses on clean object-oriented design, testable game logic and well-documented APIs using XML documentation comments.

## Installation

1. Clone the repository from GitHub.
2. Open the project in **Visual Studio 2022**.
3. Run the project (Ctrl + F5) to build and run the game in the console.

## How to Play

1. Enter your hero's name.
2. Choose whether each party is controlled by a human player or computer.
3. Take turns selecting actions for your heroes to attack enemies, equip gear, or use potions. The default setup is human player for the hero party, and a computer for the enemy party.
4. Progress through the stages and defeat the boss to win the game.

## Tips

- It is recommended that this game is run on full screen.
- The hero character will do one more PUNCH damage for every stage cleared.
- Yellow door stages are farming stages and spawn increasing numbers of enemies.
- The black door contains the boss enemy, SATAN.
- Higher ranked gear increases Gear Attack damage once equipped (try to get a legendary gear!).
- Playing manually as the human player may result in more consistent wins.

## Example Gameplay

```text
=========================================== STAGE 1 ===========================================
HERO  25/25  Sword (Unique)
LEGOLAS  15/15
---------------------------------------------- VS ---------------------------------------------
                                                   JAKE THE SKELETON  5/5  Dagger (Rare)
                                                   EMMA THE SKELETON  5/5
===============================================================================================
It is HERO's turn...
1 - Standard attack (PUNCH)
2 - Do nothing
3 - Use item
4 - Equip gear
5 - Gear attack
What do you want to do?
```

## Interface Breakdown

#### Stage Header

```text
=========================================== STAGE 1 ===========================================
```

Displays the current **stage number**, which determines enemy encounters and progression.

#### Character Status Line

```text
HERO  25/25  Sword (Unique)
LEGOLAS  15/15
```

Displays:

- Character name
- Current HP / Maximum HP
- Equipped gear (if any)
- Gear rarity

#### Battle Divider

```text
---------------------------------------------- VS ---------------------------------------------
```

Visually separates the friendly and enemy parties.

#### Enemy Party Status

```text
                                                   JAKE THE SKELETON  5/5  Dagger (Rare)
                                                   EMMA THE SKELETON  5/5
```

Displays the enemy party using the same format, right-aligned to clearly distinguish enemies from allies.

#### Turn Indicator and Action Menu

```text
It is HERO's turn...
1 - Standard attack (PUNCH)
2 - Do nothing
3 - Use item
4 - Equip gear
5 - Gear attack
What do you want to do?
```

Displays:

- The character in turn
- Available actions for that character
- A prompt for player input
