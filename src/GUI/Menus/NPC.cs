using System;
using System.IO;
using System.Reflection;

using MastermindRPG.Creatures;
using MastermindRPG.Data;
using MastermindRPG.GUI;
using MastermindRPG.Items;
using MastermindRPG.Threads;

namespace MastermindRPG
{
    /// <summary>
    /// Talks to the NPC in the given building,
    /// providing quests if the player meets
    /// the progress requirements
    /// 
    /// The data for this class is in the other
    /// part of it (MastermindRPG.NPCData)
    /// </summary>
    class NPC
    {
        # region constants

        /// <summary>
        /// NPC names
        /// </summary>
        private static readonly string[] names = 
        { 
            Constants.StringValue("nPC0Name"), 
            Constants.StringValue("nPC1Name"), 
            Constants.StringValue("nPC2Name"), 
            Constants.StringValue("nPC3Name"), 
            Constants.StringValue("nPC4Name"), 
            Constants.StringValue("nPC5Name") 
        };

        /// <summary>
        /// The messages the NPCs say
        /// </summary>
        private static readonly string[, , ,] messages = new string[,,,]
        {
            # region Ann
            {
                { // Miscellaneous
                    {Constants.StringValue("nPC0TextIntroLine0"), Constants.StringValue("nPC0TextIntroLine1"),Constants.StringValue("nPC0TextIntroLine2"),Constants.StringValue("nPC0TextIntroLine3")},
                    {Constants.StringValue("nPC0TextReminderLine0"), Constants.StringValue("nPC0TextReminderLine1"), Constants.StringValue("nPC0TextReminderLine2"), Constants.StringValue("nPC0TextReminderLine3")},
                    {Constants.StringValue("nPC0TextCompleteLine0"), Constants.StringValue("nPC0TextCompleteLine1"), Constants.StringValue("nPC0TextCompleteLine2"), Constants.StringValue("nPC0TextCompleteLine3")},
                    {"","","",""}
                },
                { // Quests
                    {Constants.StringValue("nPC0TextQuestLine0"), Constants.StringValue("nPC0TextQuestLine1"), Constants.StringValue("nPC0TextQuestLine2"), Constants.StringValue("nPC0TextQuestLine3")},
                    {Constants.StringValue("nPC0TextAcceptLine0"), Constants.StringValue("nPC0TextAcceptLine1"), Constants.StringValue("nPC0TextAcceptLine2"), Constants.StringValue("nPC0TextAcceptLine3")},
                    {Constants.StringValue("nPC0TextRejectLine0"), Constants.StringValue("nPC0TextRejectLine1"), Constants.StringValue("nPC0TextRejectLine2"), Constants.StringValue("nPC0TextRejectLine3")},
                    {Constants.StringValue("nPC0TextNothingLine0"), Constants.StringValue("nPC0TextNothingLine1"), Constants.StringValue("nPC0TextNothingLine2"), Constants.StringValue("nPC0TextNothingLine3")}
                },
                { // News
                    {Constants.StringValue("nPC0TextNewsLine0"), Constants.StringValue("nPC0TextNewsLine1"), Constants.StringValue("nPC0TextNewsLine2"), Constants.StringValue("nPC0TextNewsLine3")},
                    {Constants.StringValue("nPC0TextInterestingLine0"), Constants.StringValue("nPC0TextInterestingLine1"), Constants.StringValue("nPC0TextInterestingLine2"), Constants.StringValue("nPC0TextInterestingLine3")},
                    {Constants.StringValue("nPC0TextBoringLine0"), Constants.StringValue("nPC0TextBoringLine1"), Constants.StringValue("nPC0TextBoringLine2"), Constants.StringValue("nPC0TextBoringLine3")}, 
                    {"","","",""}
                }
            },
            # endregion Ann
            # region Blake
            {
                { // Miscellaneous
                    {Constants.StringValue("nPC1TextIntroLine0"), Constants.StringValue("nPC1TextIntroLine1"),Constants.StringValue("nPC1TextIntroLine2"),Constants.StringValue("nPC1TextIntroLine3")},
                    {Constants.StringValue("nPC1TextReminderLine0"), Constants.StringValue("nPC1TextReminderLine1"), Constants.StringValue("nPC1TextReminderLine2"), Constants.StringValue("nPC1TextReminderLine3")},
                    {Constants.StringValue("nPC1TextCompleteLine0"), Constants.StringValue("nPC1TextCompleteLine1"), Constants.StringValue("nPC1TextCompleteLine2"), Constants.StringValue("nPC1TextCompleteLine3")},
                    {"","","",""}
                },
                { // Quests
                    {Constants.StringValue("nPC1TextQuestLine0"), Constants.StringValue("nPC1TextQuestLine1"), Constants.StringValue("nPC1TextQuestLine2"), Constants.StringValue("nPC1TextQuestLine3")},
                    {Constants.StringValue("nPC1TextAcceptLine0"), Constants.StringValue("nPC1TextAcceptLine1"), Constants.StringValue("nPC1TextAcceptLine2"), Constants.StringValue("nPC1TextAcceptLine3")},
                    {Constants.StringValue("nPC1TextRejectLine0"), Constants.StringValue("nPC1TextRejectLine1"), Constants.StringValue("nPC1TextRejectLine2"), Constants.StringValue("nPC1TextRejectLine3")},
                    {Constants.StringValue("nPC1TextNothingLine0"), Constants.StringValue("nPC1TextNothingLine1"), Constants.StringValue("nPC1TextNothingLine2"), Constants.StringValue("nPC1TextNothingLine3")}
                },
                { // News
                    {Constants.StringValue("nPC1TextNewsLine0"), Constants.StringValue("nPC1TextNewsLine1"), Constants.StringValue("nPC1TextNewsLine2"), Constants.StringValue("nPC1TextNewsLine3")},
                    {Constants.StringValue("nPC1TextInterestingLine0"), Constants.StringValue("nPC1TextInterestingLine1"), Constants.StringValue("nPC1TextInterestingLine2"), Constants.StringValue("nPC1TextInterestingLine3")},
                    {Constants.StringValue("nPC1TextBoringLine0"), Constants.StringValue("nPC1TextBoringLine1"), Constants.StringValue("nPC1TextBoringLine2"), Constants.StringValue("nPC1TextBoringLine3")}, 
                    {"","","",""}
                }
            },
            # endregion Blake
            # region Cynthia
            {
                { // Miscellaneous
                    {Constants.StringValue("nPC2TextIntroLine0"), Constants.StringValue("nPC2TextIntroLine1"),Constants.StringValue("nPC2TextIntroLine2"),Constants.StringValue("nPC2TextIntroLine3")},
                    {Constants.StringValue("nPC2TextReminderLine0"), Constants.StringValue("nPC2TextReminderLine1"), Constants.StringValue("nPC2TextReminderLine2"), Constants.StringValue("nPC2TextReminderLine3")},
                    {Constants.StringValue("nPC2TextCompleteLine0"), Constants.StringValue("nPC2TextCompleteLine1"), Constants.StringValue("nPC2TextCompleteLine2"), Constants.StringValue("nPC2TextCompleteLine3")},
                    {"","","",""}
                },
                { // Quests
                    {Constants.StringValue("nPC2TextQuestLine0"), Constants.StringValue("nPC2TextQuestLine1"), Constants.StringValue("nPC2TextQuestLine2"), Constants.StringValue("nPC2TextQuestLine3")},
                    {Constants.StringValue("nPC2TextAcceptLine0"), Constants.StringValue("nPC2TextAcceptLine1"), Constants.StringValue("nPC2TextAcceptLine2"), Constants.StringValue("nPC2TextAcceptLine3")},
                    {Constants.StringValue("nPC2TextRejectLine0"), Constants.StringValue("nPC2TextRejectLine1"), Constants.StringValue("nPC2TextRejectLine2"), Constants.StringValue("nPC2TextRejectLine3")},
                    {Constants.StringValue("nPC2TextNothingLine0"), Constants.StringValue("nPC2TextNothingLine1"), Constants.StringValue("nPC2TextNothingLine2"), Constants.StringValue("nPC2TextNothingLine3")}
                },
                { // News
                    {Constants.StringValue("nPC2TextNewsLine0"), Constants.StringValue("nPC2TextNewsLine1"), Constants.StringValue("nPC2TextNewsLine2"), Constants.StringValue("nPC2TextNewsLine3")},
                    {Constants.StringValue("nPC2TextInterestingLine0"), Constants.StringValue("nPC2TextInterestingLine1"), Constants.StringValue("nPC2TextInterestingLine2"), Constants.StringValue("nPC2TextInterestingLine3")},
                    {Constants.StringValue("nPC2TextBoringLine0"), Constants.StringValue("nPC2TextBoringLine1"), Constants.StringValue("nPC2TextBoringLine2"), Constants.StringValue("nPC2TextBoringLine3")}, 
                    {"","","",""}
                }
            },
            # endregion Cynthia
            # region Drake
            {
                { // Miscellaneous
                    {Constants.StringValue("nPC3TextIntroLine0"), Constants.StringValue("nPC3TextIntroLine1"),Constants.StringValue("nPC3TextIntroLine2"),Constants.StringValue("nPC3TextIntroLine3")},
                    {Constants.StringValue("nPC3TextReminderLine0"), Constants.StringValue("nPC3TextReminderLine1"), Constants.StringValue("nPC3TextReminderLine2"), Constants.StringValue("nPC3TextReminderLine3")},
                    {Constants.StringValue("nPC3TextCompleteLine0"), Constants.StringValue("nPC3TextCompleteLine1"), Constants.StringValue("nPC3TextCompleteLine2"), Constants.StringValue("nPC3TextCompleteLine3")},
                    {"","","",""}
                },
                { // Quests
                    {Constants.StringValue("nPC3TextQuestLine0"), Constants.StringValue("nPC3TextQuestLine1"), Constants.StringValue("nPC3TextQuestLine2"), Constants.StringValue("nPC3TextQuestLine3")},
                    {Constants.StringValue("nPC3TextAcceptLine0"), Constants.StringValue("nPC3TextAcceptLine1"), Constants.StringValue("nPC3TextAcceptLine2"), Constants.StringValue("nPC3TextAcceptLine3")},
                    {Constants.StringValue("nPC3TextRejectLine0"), Constants.StringValue("nPC3TextRejectLine1"), Constants.StringValue("nPC3TextRejectLine2"), Constants.StringValue("nPC3TextRejectLine3")},
                    {Constants.StringValue("nPC3TextNothingLine0"), Constants.StringValue("nPC3TextNothingLine1"), Constants.StringValue("nPC3TextNothingLine2"), Constants.StringValue("nPC3TextNothingLine3")}
                },
                { // News
                    {Constants.StringValue("nPC3TextNewsLine0"), Constants.StringValue("nPC3TextNewsLine1"), Constants.StringValue("nPC3TextNewsLine2"), Constants.StringValue("nPC3TextNewsLine3")},
                    {Constants.StringValue("nPC3TextInterestingLine0"), Constants.StringValue("nPC3TextInterestingLine1"), Constants.StringValue("nPC3TextInterestingLine2"), Constants.StringValue("nPC3TextInterestingLine3")},
                    {Constants.StringValue("nPC3TextBoringLine0"), Constants.StringValue("nPC3TextBoringLine1"), Constants.StringValue("nPC3TextBoringLine2"), Constants.StringValue("nPC3TextBoringLine3")}, 
                    {"","","",""}
                }
            },
            # endregion Drake
            # region Elise
            { 
                { // Miscellaneous
                    {Constants.StringValue("nPC4TextIntroLine0"), Constants.StringValue("nPC4TextIntroLine1"),Constants.StringValue("nPC4TextIntroLine2"),Constants.StringValue("nPC4TextIntroLine3")},
                    {Constants.StringValue("nPC4TextReminderLine0"), Constants.StringValue("nPC4TextReminderLine1"), Constants.StringValue("nPC4TextReminderLine2"), Constants.StringValue("nPC4TextReminderLine3")},
                    {Constants.StringValue("nPC4TextCompleteLine0"), Constants.StringValue("nPC4TextCompleteLine1"), Constants.StringValue("nPC4TextCompleteLine2"), Constants.StringValue("nPC4TextCompleteLine3")},
                    {"","","",""}
                },
                { // Quests
                    {Constants.StringValue("nPC4TextQuestLine0"), Constants.StringValue("nPC4TextQuestLine1"), Constants.StringValue("nPC4TextQuestLine2"), Constants.StringValue("nPC4TextQuestLine3")},
                    {Constants.StringValue("nPC4TextAcceptLine0"), Constants.StringValue("nPC4TextAcceptLine1"), Constants.StringValue("nPC4TextAcceptLine2"), Constants.StringValue("nPC4TextAcceptLine3")},
                    {Constants.StringValue("nPC4TextRejectLine0"), Constants.StringValue("nPC4TextRejectLine1"), Constants.StringValue("nPC4TextRejectLine2"), Constants.StringValue("nPC4TextRejectLine3")},
                    {Constants.StringValue("nPC4TextNothingLine0"), Constants.StringValue("nPC4TextNothingLine1"), Constants.StringValue("nPC4TextNothingLine2"), Constants.StringValue("nPC4TextNothingLine3")}
                },
                { // News
                    {Constants.StringValue("nPC4TextNewsLine0"), Constants.StringValue("nPC4TextNewsLine1"), Constants.StringValue("nPC4TextNewsLine2"), Constants.StringValue("nPC4TextNewsLine3")},
                    {Constants.StringValue("nPC4TextInterestingLine0"), Constants.StringValue("nPC4TextInterestingLine1"), Constants.StringValue("nPC4TextInterestingLine2"), Constants.StringValue("nPC4TextInterestingLine3")},
                    {Constants.StringValue("nPC4TextBoringLine0"), Constants.StringValue("nPC4TextBoringLine1"), Constants.StringValue("nPC4TextBoringLine2"), Constants.StringValue("nPC4TextBoringLine3")}, 
                    {"","","",""}
                }
            },
            # endregion 
            # region Frank
            {
                { // Miscellaneous
                    {Constants.StringValue("nPC5TextIntroLine0"), Constants.StringValue("nPC5TextIntroLine1"),Constants.StringValue("nPC5TextIntroLine2"),Constants.StringValue("nPC5TextIntroLine3")},
                    {Constants.StringValue("nPC5TextReminderLine0"), Constants.StringValue("nPC5TextReminderLine1"), Constants.StringValue("nPC5TextReminderLine2"), Constants.StringValue("nPC5TextReminderLine3")},
                    {Constants.StringValue("nPC5TextCompleteLine0"), Constants.StringValue("nPC5TextCompleteLine1"), Constants.StringValue("nPC5TextCompleteLine2"), Constants.StringValue("nPC5TextCompleteLine3")},
                    {"","","",""}
                },
                { // Quests
                    {Constants.StringValue("nPC5TextQuestLine0"), Constants.StringValue("nPC5TextQuestLine1"), Constants.StringValue("nPC5TextQuestLine2"), Constants.StringValue("nPC5TextQuestLine3")},
                    {Constants.StringValue("nPC5TextAcceptLine0"), Constants.StringValue("nPC5TextAcceptLine1"), Constants.StringValue("nPC5TextAcceptLine2"), Constants.StringValue("nPC5TextAcceptLine3")},
                    {Constants.StringValue("nPC5TextRejectLine0"), Constants.StringValue("nPC5TextRejectLine1"), Constants.StringValue("nPC5TextRejectLine2"), Constants.StringValue("nPC5TextRejectLine3")},
                    {Constants.StringValue("nPC5TextNothingLine0"), Constants.StringValue("nPC5TextNothingLine1"), Constants.StringValue("nPC5TextNothingLine2"), Constants.StringValue("nPC5TextNothingLine3")}
                },
                { // News
                    {Constants.StringValue("nPC5TextNewsLine0"), Constants.StringValue("nPC5TextNewsLine1"), Constants.StringValue("nPC5TextNewsLine2"), Constants.StringValue("nPC5TextNewsLine3")},
                    {Constants.StringValue("nPC5TextInterestingLine0"), Constants.StringValue("nPC5TextInterestingLine1"), Constants.StringValue("nPC5TextInterestingLine2"), Constants.StringValue("nPC5TextInterestingLine3")},
                    {Constants.StringValue("nPC5TextBoringLine0"), Constants.StringValue("nPC5TextBoringLine1"), Constants.StringValue("nPC5TextBoringLine2"), Constants.StringValue("nPC5TextBoringLine3")}, 
                    {"","","",""}
                }
            }
            # endregion Frank
        };

        /// <summary>
        /// The choices a player can choose from
        /// </summary>
        private static readonly string[, ,] choices = new string[,,] 
        {
            { // Introduction
                { Constants.StringValue("nPCChoiceIntro0"), Constants.StringValue("nPCChoiceIntro1"), Constants.StringValue("nPCChoiceIntro2") },
                { Constants.StringValue("nPCChoiceReminder0"), "", "" },
                { Constants.StringValue("nPCChoiceComplete0"), "", "" },
                {"","",""}
            },
            { // Quests
                { Constants.StringValue("nPCChoiceQuest0"), Constants.StringValue("nPCChoiceQuest1"), "" },
                { Constants.StringValue("nPCChoiceAccept0"), "", "" },
                { Constants.StringValue("nPCChoiceReject0"), "", "" },
                { Constants.StringValue("nPCChoiceNothing0"), "", "" }
            },
            { // News
                { Constants.StringValue("nPCChoiceNews0"), Constants.StringValue("nPCChoiceNews1"), "" },
                { Constants.StringValue("nPCChoiceInteresting0"), "", "" },
                { Constants.StringValue("nPCChoiceBoring0"), "", ""},
                {"","",""}
            }
        };

        # endregion constants

        private static int message;
        private static int[] topic;
        private static int building;

        /// <summary>
        /// Talks to the NPC at the given building
        /// </summary>
        /// <param name="building">building ID</param>
        /// <param name="human">human data</param>
        public static void Talk(int b, ref Human human)
        {
            building = b;
            
            message = 0;
            topic = new int[2];
            
            ConsoleTools.DrawDesign(ConsoleTools.npc);
            ConsoleTools.Draw(14, 1, names[building]);

            ConsoleKey key;

            for (int x = 0; x < 3; ++x)
            {

                DrawMessages();

                do
                {
                    System.Threading.Thread.Sleep(50);
                    key = KeyInput.Key;

                    if (key == ConsoleKey.UpArrow && message != 0)
                        message--;
                    else if (key == ConsoleKey.DownArrow && message < 2 - x)
                        message++;
                    else
                        continue;

                    DrawMessages();
                }
                while (key != ConsoleKey.Spacebar);

                if (x == 2)
                    break;

                topic[x] = message + 1;
                message = 0;

                // Player said goodbye
                if (topic[0] == 3)
                    break;

                // Special Quest Cases
                if (topic[0] == 1)
                {
                    int status = QuestState(human);

                    // If the player already has a quest from the
                    // NPC, apply the appropriate action.
                    if (status >= 0)
                    {
                        x = 1;
                        topic[0] = 0;
                        topic[1] = status + 1;

                        // Reward the player for turning in a quest
                        if (topic[0] == 0 && topic[1] == 2)
                            foreach (QuestItem quest in human.QuestItems)
                                if (quest.QuestId % 6 == building)
                                    human.Reward(quest);
                    }
                    else if (!QuestAvailable(human))
                    {
                        topic[1] = 3;
                        x = 1;
                    }
                    else if (topic[1] == 1)
                        human.GetQuest(new QuestItem(building, human.progress + 1));
                }
            }
        }

        /// <summary>
        /// Draws the messages in the GUI
        /// </summary>
        private static void DrawMessages()
        {
            for (int x = 0; x < 4; ++x)
            {
                ConsoleTools.Draw(2, 3 + x, "                                      ");
                ConsoleTools.Draw(2, 3 + x, messages[building, topic[0], topic[1], x]);
                if (x >= 3)
                    continue;
                ConsoleTools.Draw(2, 8 + x, "                                      ");
                if (x == message)
                    ConsoleTools.Draw(2, 8 + x, choices[topic[0], topic[1], x]);
                else
                    ConsoleTools.Draw(2, 8 + x, choices[topic[0], topic[1], x], ConsoleColor.DarkGray);
            }
        }

        /// <summary>
        /// Returns the status of the current quest of the current NPC
        /// </summary>
        /// <param name="building">building id</param>
        /// <param name="human">human object</param>
        /// <returns>-1 = none; 0 = incomplete; 1 = complete</returns>
        private static int QuestState(Human human)
        {
            foreach (QuestItem quest in human.QuestItems)
                if (quest.QuestId % 6 == building)
                    return (quest.Owned ? 1 : 0);
            return -1;
        }

        /// <summary>
        /// Determines whether a quest is available at the given building with the given human's progress
        /// </summary>
        /// <param name="building">building ID</param>
        /// <param name="human">human object</param>
        /// <returns>Quest is Available</returns>
        private static Boolean QuestAvailable(Human human)
        {
            if (building == 0 && (human.progress % 6 == 0 || human.progress % 6 == 2))
                return true;
            if (building == 1 && (human.progress % 6 == 3 || human.progress % 6 == 4))
                return true;
            if (building == 2 && (human.progress % 9 == 0 || human.progress % 9 == 1 || human.progress % 9 == 4))
                return true;
            if (building == 3 && (human.progress % 9 == 2 || human.progress % 9 == 3 || human.progress % 9 == 7))
                return true;
            if (building == 4 && (human.progress % 6 == 1 || human.progress % 6 == 5))
                return true;
            if (building == 5 && (human.progress % 9 == 5 || human.progress % 9 == 6 || human.progress % 9 == 8))
                return true;
            return false;
        }
    }
}
