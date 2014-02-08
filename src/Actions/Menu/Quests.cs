using System;

using MastermindRPG.GUI;
using MastermindRPG.Threads;

namespace MastermindRPG.Actions
{
    /// <summary>
    /// Displays the information for currently obtained quests
    /// </summary>
    class Quests
    {
        /// <summary>
        /// The quest ID
        /// </summary>
        private static int quest;

        /// <summary>
        /// Opens the quest menu
        /// </summary>
        public static void Action()
        {
            ConsoleTools.DrawDesign(ConsoleTools.quests);

            // If the player has no qusts, tell them then exit
            if (Adventure.Human.QuestItems.Size == 0)
            {
                ConsoleTools.Draw(8, 1, "---");
                ConsoleTools.Draw(29, 1, "---");
                ConsoleTools.Draw(19, 10, 0);
                ConsoleTools.Draw(23, 10, 0);
                ConsoleTools.Draw(2, 5, "You have no quests right now.");
                ConsoleTools.Draw(2, 6, "Press any key to return.");
                Console.ReadKey();
                return;
            }

            ConsoleKey key;

            Refresh();

            do
            {
                System.Threading.Thread.Sleep(50);
                key = KeyInput.Key;

                if (key == ConsoleKey.LeftArrow && quest != 0)
                    quest--;
                else if (key == ConsoleKey.RightArrow && quest < Adventure.Human.QuestItems.Size - 1)
                    quest++;
                else
                    continue;

                Refresh();
            }
            while (key != ConsoleKey.Escape);
        }

        /// <summary>
        /// NPC names
        /// </summary>
        private static readonly string[] names = { "Ann", "Blake", "Cynthia", "Drake", "Elise", "Frank" };

        /// <summary>
        /// Quest Descriptions
        /// </summary>
        private static readonly string[,] questInfo =
        {
            {"There is one thing you could help me   ", "with actually. I seem to have left my", "backpack behind. Could you retrieve it", "for me? I'm terribly forgetful.    " },
            {"Well, if you come across a gem in one  ", "of the areas around town, could you  ", "pick it up for me? I'm trying to see  ", "what kind of gems are around here. " },
            {"Actually, I could use some spare tokens", "from the tournament. They are        ", "usually left around after matches. If ", "you could get me some, that'd help." },
            {"A favor? Well, you could find me some  ", "books from the surrounding area.     ", "I'd like something to read. Could you ", "do this?                           " },
            {"It's not quite a need, but if you find ", "any sheet music, could you bring it  ", "to me?                                ", "                                   " },
            {"Not necessarily for myself, but quite  ", "a few people have lost their wallets.", "If you find one, could you bring it   ", "here so it may be returned?        " },
        };

        /// <summary>
        /// Displays the quest information
        /// </summary>
        private static void Refresh()
        {
            ConsoleTools.Draw(8, 1, "       ");
            ConsoleTools.Draw(8, 1, names[Adventure.Human.QuestItems[quest].QuestId]);
            ConsoleTools.Draw(29, 1, "  ");
            ConsoleTools.Draw(29, 1, Adventure.Human.QuestItems[quest].Area);
            for (int x = 0; x < 4; ++x)
            {
                ConsoleTools.Draw(2, 5 + x, questInfo[Adventure.Human.QuestItems[quest].QuestId, x]);
            }
            ConsoleTools.Draw(19, 10, quest + 1);
            ConsoleTools.Draw(23, 10, Adventure.Human.QuestItems.Size);
        }
    }
}
