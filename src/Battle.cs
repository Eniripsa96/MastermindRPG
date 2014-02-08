using System;

using MastermindRPG.AI.Othello;
using MastermindRPG.Creatures;
using MastermindRPG.Data;
using MastermindRPG.Data.Structures.List;
using MastermindRPG.GUI;
using MastermindRPG.GUI.Menus;
using MastermindRPG.Threads;

namespace MastermindRPG
{
    /// <summary>
    /// Handles the controls for a battle
    /// </summary>
    class Battle
    {
        # region constants

        /// <summary>
        /// Turn enum
        /// </summary>
        public enum Turn
        {
            player = 1,
            enemy = 2
        };

        /// <summary>
        /// Direction constants
        /// </summary>
        private static readonly int[] up = { 0, -1 };
        private static readonly int[] down = { 0, 1 };
        private static readonly int[] left = { -1, 0 };
        private static readonly int[] right = { 1, 0 };

        /// <summary>
        /// Names of the actions (to display)
        /// </summary>
        private static readonly string[] actionNames = 
        {
            Constants.StringValue("placeTokenDisplayName"),
            Constants.StringValue("skillBetrayDisplayName"), 
            Constants.StringValue("skillSacrificeDisplayName"), 
            Constants.StringValue("skillDoubleStrikeDisplayName"), 
            Constants.StringValue("skillNegotiateDisplayName"), 
            Constants.StringValue("skillHealDisplayName"), 
            Constants.StringValue("skillRewindDisplayName"), 
            Constants.StringValue("skillVoidDisplayName"), 
            Constants.StringValue("skillOfferingDisplayName"), 
            Constants.StringValue("skillDominateDisplayName") 
        };

        # endregion constants

        # region fields

        /// <summary>
        /// AP for the human and AI
        /// </summary>
        private static int[] ap;

        /// <summary>
        /// Whether or not to update AP
        /// </summary>
        private static Boolean changeAp;

        /// <summary>
        /// Position of the cursor
        /// </summary>
        private static int horizontalPosition;
        private static int verticalPosition;

        /// <summary>
        /// The current player performing an action
        /// 1 = Human
        /// 2 = AI
        /// </summary>
        private static Turn turn;

        /// <summary>
        /// The currently selected action
        /// </summary>
        private static int action;

        /// <summary>
        /// The number of extra turns the human has
        /// </summary>
        private static int extraTurns;

        /// <summary>
        /// The human that is battling
        /// </summary>
        private static Human human;

        /// <summary>
        /// The enemy that is battling
        /// </summary>
        private static Enemy enemy;

        /// <summary>
        /// The battlefield being used
        /// </summary>
        private static Battlefield battlefield;

        # endregion fields

        # region properties

        /// <summary>
        /// Returns the horizontal position
        /// </summary>
        public static int X
        {
            get { return horizontalPosition; }
            set { horizontalPosition = value; }
        }
        
        /// <summary>
        /// Returns the vertical position
        /// </summary>
        public static int Y
        {
            get { return verticalPosition; }
            set { verticalPosition = value; }
        }

        /// <summary>
        /// Returns the current turn
        /// </summary>
        public static int CurrentTurn
        {
            get { return (turn == Turn.player ? 1 : 2); }
        }

        /// <summary>
        /// The horizontal coordinate for drawing the cursor
        /// </summary>
        private static int xPos
        {
            get { return horizontalPosition * 4 + 14; }
        }

        /// <summary>
        /// The vertical coordinate for drawing the cursor
        /// </summary>
        private static int yPos
        {
            get { return verticalPosition * 2 + 4; }
        }

        /// <summary>
        /// Returns the ap for the current player
        /// </summary>
        public static int Ap
        {
            get { return ap[CurrentTurn - 1]; }
            set { if (changeAp) ap[CurrentTurn - 1] = value; }
        }

        /// <summary>
        /// Property for the human object
        /// </summary>
        public static Human Player
        {
            get { return human; }
        }

        /// <summary>
        /// Property for the enemy
        /// </summary>
        public static Enemy Foe
        {
            get { return enemy; }
        }

        /// <summary>
        /// Property for the battlefield
        /// </summary>
        public static Battlefield Field
        {
            get { return battlefield; }
        }

        /// <summary>
        /// Property for extraTurns
        /// </summary>
        public static int ExtraTurns
        {
            get { return extraTurns; }
            set { extraTurns = value; }
        }

        # endregion properties

        # region control methods

        /// <summary>
        /// Executes a fight between the given combatants
        /// </summary>
        /// <param name="humanPtr">The human</param>
        /// <param name="enemyPtr">The enemy</param>
        /// <returns></returns>
        public static int Fight(Human humanPtr, Enemy enemyPtr)
        {
            // Initialize a battlefield and the battle data
            battlefield = new Battlefield();
            ap = new int[2];
            human = humanPtr;
            enemy = enemyPtr;
            horizontalPosition = 0;
            verticalPosition = 0;
            action = 0;

            Draw();

            ConsoleKey key;
            
            do
            {
                int[] direction;

                System.Threading.Thread.Sleep(25);
                key = KeyInput.Key;
                if (key == ConsoleKey.UpArrow)
                    direction = up;
                else if (key == ConsoleKey.RightArrow)
                    direction = right;
                else if (key == ConsoleKey.LeftArrow)
                    direction = left;
                else if (key == ConsoleKey.DownArrow)
                    direction = down;
                else
                {
                    if (key == ConsoleKey.H)
                    {
                        Help();
                        Draw();
                    }
                    else if (key == ConsoleKey.I && enemy.Level != 999)
                    {
                        bool? usedItem = Inventory();
                        Draw();
                        if (usedItem == null)
                            return -1;
                        else if (usedItem == true)
                            AiTurn();
                    }
                    else if (key == ConsoleKey.Escape && enemy.Level == 999)
                        return 1;
                    else if (key == ConsoleKey.Tab && extraTurns == 0)
                    {
                        action = GetAction(action + 1);
                        RefreshBattleStats();
                    }
                    else if (key == ConsoleKey.Spacebar)
                    {
                        Act();
                        if (battlefield.CountTokens(1) == 0)
                            return 0;
                        else if (battlefield.CountTokens(2) == 0)
                            return human.Health;
                    }
                    else
                        CheckForShortcut(key.ToString().Replace("D", ""));
                    continue;
                }

                // Move the player if a
                // directional key was pressed
                MoveCursor(direction);
            }
            while (human.Health > 0 && enemy.Health > 0);

            if (human.Health <= 0)
                return 0;
            return human.Health;
        }

        /// <summary>
        /// Checks if the pressed key is a shortcut and applies it if applicable
        /// </summary>
        /// <param name="key">pressed key name</param>
        private static void CheckForShortcut(string key)
        {
            try
            {
                // Try to parse the key
                int shortcut = int.Parse(key);

                // Offset the shortcuts to line up with the actions
                shortcut = (shortcut + 9) % 10;

                int i = GetAction(shortcut);
                // Apply the shortcut
                if (shortcut == GetAction(shortcut))
                    action = shortcut;
                RefreshBattleStats();
            }
            catch (Exception) 
            { }
        }

        /// <summary>
        /// Performs the player action
        /// </summary>
        private static void Act()
        {
            turn = Turn.player;
            if (human.UseSkill(action))
            {
                if (extraTurns == 0 && enemy.Health > 0)
                {
                    Ap++;
                    AiTurn();
                }
                RefreshBattleStats();
            }
            battlefield.Draw();
            DrawCursor();
        }

        /// <summary>
        /// Gets the first valid action starting
        /// from the given ID recursively
        /// </summary>
        /// <param name="newAction"></param>
        private static int GetAction(int newAction)
        {
            if (newAction % 10 == 0)
                return 0;
            if (human.Skills[newAction - 1])
                return newAction;
            return GetAction(newAction + 1);
        }

        # endregion control methods

        # region AI

        /// <summary>
        /// Perform a move for the computer
        /// </summary>
        private static void AiTurn()
        {
            // Allow the user to see their move
            // Display that it is the AI's turn
            // and that pressing a key will make
            // the AI move.
            battlefield.Draw();
            
            do
            {
                // Set the turn to the AI's turn
                turn = Turn.enemy;
                
                // Get the "best" move for the AI
                Move move = AiScripts.Run();

                // If the AI could make a move, 
                // do it
                if (move != null)
                {
                    ConsoleTools.Draw(0, 16, "AI's Turn:");
                    ConsoleTools.Draw(0, 17, "Press any");
                    ConsoleTools.Draw(0, 18, "key to have");
                    ConsoleTools.Draw(0, 19, "the AI make");
                    ConsoleTools.Draw(0, 20, "its move.");
                    ConsoleTools.Pause();

                    horizontalPosition = move.Location % 10;
                    verticalPosition = move.Location / 10;
                    enemy.UseSkill(move.Action);
                    while (extraTurns != 0)
                    {
                        move = AiScripts.Run(move.Action);
                        horizontalPosition = move.Location % 10;
                        verticalPosition = move.Location / 10;
                        enemy.UseSkill(move.Action);
                    }
                    ConsoleTools.Draw(54, 4, "The AI Used");
                    ConsoleTools.Draw(53, 5, "            ");
                    ConsoleTools.Draw(53, 5, actionNames[move.Action]);
                    Ap++;
                }

                // Otherwise, see if the human could make a move
                // If neither could make a move and both still
                // have tokens, reset the board.
                else
                {
                    turn = Turn.player;
                    if (AiScripts.FindPossibleMoves(1, 1).Size == 0 || battlefield.CountTokens(1) == 0)
                    {
                        if (battlefield.CountTokens(1) == 0 || battlefield.CountTokens(2) == 0)
                            break;
                        Reset();
                    }
                    break;
                }
                DrawCursor();
                turn = Turn.player;
            }
            // While the human cannot make a move, make the AI move again
            while (AiScripts.FindPossibleMoves(1).Size == 0);

            // Clear the AI's turn message
            ConsoleTools.Draw(0, 16, "           ");
            ConsoleTools.Draw(0, 17, "         ");
            ConsoleTools.Draw(0, 18, "           ");
            ConsoleTools.Draw(0, 19, "           ");
            ConsoleTools.Draw(0, 20, "         ");
            MoveCursor(new int[] { 0, 0 });
        }

        # endregion ai

        # region graphics

        /// <summary>
        /// Moves the cursor with the given direction
        /// </summary>
        /// <param name="direction">direction to move the cursor</param>
        private static void MoveCursor(int[] direction)
        {
            if (direction[0] != 0 && horizontalPosition + direction[0] >= 0 && horizontalPosition + direction[0] < 10)
                horizontalPosition += direction[0];
            else if (direction[1] != 0 && verticalPosition + direction[1] >= 0 && verticalPosition + direction[1] < 10)
                verticalPosition += direction[1];

            // Don't redraw the battlefield when not necessary (reduce flicker)
            else return;

            battlefield.Draw();
            DrawCursor();
        }

        /// <summary>
        /// Draws the cursor at the current location
        /// </summary>
        private static void DrawCursor()
        {
            ConsoleTools.Draw(xPos, yPos, " ", Constants.ColorValue("battleCursorColor"), battlefield.GetColor(10 * Y + X));
        }

        /// <summary>
        /// Displays the battle stats on the console window
        /// </summary>
        private static void RefreshBattleStats()
        {
            Turn t = turn;
            turn = Turn.player;
            // Human stats
            ConsoleTools.Draw(7, 6, "   ");
            ConsoleTools.Draw(7, 6, human.Level);
            ConsoleTools.Draw(7, 8, "   ");
            ConsoleTools.Draw(7, 8, human.Health);
            ConsoleTools.Draw(7, 10, "   ");
            ConsoleTools.Draw(7, 10, Ap);
            ConsoleTools.Draw(2, 14, "    ");
            ConsoleTools.Draw(3, 14, battlefield.CountTokens(1));

            turn = Turn.enemy;
            // Enemy stats
            ConsoleTools.Draw(60, 14, "   ");
            ConsoleTools.Draw(60, 14, enemy.Level);
            ConsoleTools.Draw(60, 16, "    ");
            ConsoleTools.Draw(60, 16, enemy.Health);
            ConsoleTools.Draw(60, 18, "   ");
            ConsoleTools.Draw(60, 18, Ap);
            ConsoleTools.Draw(55, 22, "    ");
            ConsoleTools.Draw(55, 22, battlefield.CountTokens(2));

            turn = t;

            // Current action
            ConsoleTools.Draw(35, 1, "             ");
            ConsoleTools.Draw(35, 1, actionNames[action]);
        }

        /// <summary>
        /// Displays the help for the battle system
        /// </summary>
        public static void Help()
        {
            int page = 0;
            while (page != -1)
            {
                if (page == 0)
                    ConsoleTools.DrawDesign(ConsoleTools.battleHelp1);
                else if (page == 1)
                    ConsoleTools.DrawDesign(ConsoleTools.battleHelp2);
                else
                    ConsoleTools.DrawDesign(ConsoleTools.battleHelp3);

                ConsoleKey input;
                do
                {
                    System.Threading.Thread.Sleep(50);
                    input = KeyInput.Key;
                }
                while ((input != ConsoleKey.LeftArrow || page == 0) 
                    && (input != ConsoleKey.RightArrow || page == 2) 
                    && input != ConsoleKey.Escape);

                if (input == ConsoleKey.LeftArrow)
                    page--;
                if (input == ConsoleKey.RightArrow)
                    page++;
                if (input == ConsoleKey.Escape)
                    page = -1;
            }
        }

        /// <summary>
        /// Accesses the inventory
        /// </summary>
        /// <returns>null if smokebomb was used, true if another item was used, or false otherwise</returns>
        public static Boolean? Inventory()
        {
            string result = "";
            InventoryMenu menu = new InventoryMenu();

            do
            {
                // If an input did not exit the loop,
                // execute it.
                if (result.Length != 0)
                {
                    int id = int.Parse(result);
                    if (!Adventure.Human.UseItem(id))
                    {
                        ConsoleTools.DrawDesign(ConsoleTools.cannotUseItem);
                        ConsoleTools.DrawDesign(ConsoleTools.inventory);
                    }
                    else if (id == 5)
                        return null;
                    else
                        return true;
                }

                result = menu.Show();
            }
            while (!result.Equals(""));
            return false;
        }

        /// <summary>
        /// Initializes the battle GUI
        /// </summary>
        private static void Draw()
        {
            if (human.Level == 999)
            {
                ConsoleTools.DrawDesign(ConsoleTools.battlefieldPractice);
                ConsoleTools.Draw(14, 24, "Practice Battle - Press Escape to stop");
            }
            else
                ConsoleTools.DrawDesign(ConsoleTools.battlefield);
            battlefield.Draw();
            RefreshBattleStats();
            DrawCursor();
        }

        # endregion graphics

        # region stats

        /// <summary>
        /// Consumes the ap for a skill used by the current
        /// player. This is invoked by the skill classes
        /// with their costs
        /// </summary>
        /// <param name="amount"></param>
        public static void ConsumeAp(int amount)
        {
            Ap -= amount;
        }

        /// <summary>
        /// Applies damage depending on what turn it is and if it affects the user or not
        /// </summary>
        /// <param name="amount">damage</param>
        /// <param name="self">self-inflicting</param>
        public static void Damage(int amount, Boolean self)
        {
            if (self)
                if (turn == Turn.player)
                    human.Damage(amount);
                else
                    enemy.Damage(amount);
            else
                if (turn == Turn.player)
                    enemy.Damage(amount);
                else
                    human.Damage(amount);
        }

        /// <summary>
        /// Resets the board to default configuration
        /// and sets the action points for both players
        /// back to zero (to avoid instant-knockouts at
        /// the start)
        /// </summary>
        public static void Reset()
        {
            battlefield.Reset();
            Turn t = turn;
            turn = Turn.player;
            Ap = 0;
            turn = Turn.enemy;
            Ap = 0;
            turn = t;
        }

        /// <summary>
        /// Sets the ap activity to the given state
        /// </summary>
        /// <param name="state">state of ap updating</param>
        public static void ApEnabled(Boolean state)
        {
            changeAp = state;
        }

        # endregion stats
    }
}
