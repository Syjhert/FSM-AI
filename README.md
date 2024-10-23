# Application of DFA

This project is a strategic turn-based game taking place on a dynamic grid. The player navigates through the arena filled with walls while trying to outsmart an enemy AI.

Gameplay Mechanics:
- The arena will have a boundary and randomly generated walls that cannot be passed through the player and enemy.
- The player will spawn on a random cell in the grid that is not a wall.
- The player can move from one cell vertically or horizontally per turn. Moving to a wall or the arena boundary will not make the player move but take up the turn.
- The player can attack instead of moving for a turn that shades the cells around the player with a movement cost of 3 or less.
- The enemy will spawn in the arena after the player makes their 3rd turn.
- The enemy AI has a total of 5 states:
  - Spawned: The state right after their spawn, does nothing
  - Idle: When the enemy does not detect the player YET. Moves randomly or does not move at all per turn.
  - Pursuing: When the enemy detects the player in its 8-cell ranged Line of Sight and is not obstructed by a wall. It will try to move towards the player but can stumble (not move) which gives the player a chance to run away.
  - Attacking: The enemy will attack this turn and shades the cells around its attack range just like the player but now with a movement cost of 2 or less (lesser range than the player).
  - Dead: The enemy's health drops to zero and cannot change to other states.

Other Information:
- The project is coded in C# Windows Form in Visual Studio.
- The arena is made using DataGridView and for each turn (or other specific instances) the game clears the content of the grid and replaces it with new content.
- The form shows the enemy's state after the result of the previous turn.
- The game's update function order is: enemy state change > enemy move > game-over screen check > player move > enemy damage check > player damage check
- The enemy's state changes first per turn but the damage check happens last. This results in the enemy making a move before it has the Dead state despite having zero health on that turn.
- The form has a checkbox in the bottom left to show the enemy Line of Sight (detection range for Pursuing State).

Deterministic Finite Automaton (DFA):
![Automata Application DFA](https://github.com/user-attachments/assets/8a87e539-f576-40c0-854f-dc7552681f3f)
