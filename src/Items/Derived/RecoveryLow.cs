using MastermindRPG.Data;

namespace MastermindRPG.Items.Derived
{
    /// <summary>
    /// Recovery Low Item, heals the player for small amounts
    /// </summary>
    class RecoveryLow : Potion
    {
        /// <summary>
        /// RecoveryLow Constructor
        /// </summary>
        /// <param name="quantity">Initial quantity</param>
        public RecoveryLow(int quantity) 
            : base(
            quantity, 
            Constants.IntValue("recoveryLowBattleHealAmount"), 
            Constants.IntValue("recoveryHighFieldHealAmount"), 
            Constants.IntValue("recoveryLowCapacity"))
        { }

        /// <summary>
        /// ToString override
        /// </summary>
        /// <returns>ID:Quantity</returns>
        public override string ToString()
        {
            return "0:" + quantity;
        }
    }
}
