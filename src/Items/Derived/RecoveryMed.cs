using MastermindRPG.Data;

namespace MastermindRPG.Items.Derived
{
    /// <summary>
    /// Recovery Med Item, heals the player for moderate amounts
    /// </summary>
    class RecoveryMed : Potion
    {
        /// <summary>
        /// RecoveryLow Constructor
        /// </summary>
        /// <param name="quantity">Initial quantity</param>
        public RecoveryMed(int quantity) 
            : base(
            quantity, 
            Constants.IntValue("recoveryMedBattleHealAmount"), 
            Constants.IntValue("recoveryMedFieldHealAmount"), 
            Constants.IntValue("recoveryMedCapacity"))
        { }

        /// <summary>
        /// ToString override
        /// </summary>
        /// <returns>ID:Quantity</returns>
        public override string ToString()
        {
            return "1:" + quantity;
        }
    }
}
