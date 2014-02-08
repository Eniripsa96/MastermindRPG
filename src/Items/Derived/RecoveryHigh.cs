using MastermindRPG.Data;

namespace MastermindRPG.Items.Derived
{
    /// <summary>
    /// Recovery High Item, heals the player for large amounts
    /// </summary>
    class RecoveryHigh : Potion
    {
        /// <summary>
        /// Recovery High Constructor
        /// </summary>
        /// <param name="quantity">Initial quantity</param>
        public RecoveryHigh(int quantity) 
            : base(
            quantity, 
            Constants.IntValue("recoveryHighBattleHealAmount"), 
            Constants.IntValue("recoveryHighFieldHealAmount"), 
            Constants.IntValue("recoveryHighCapacity"))
        { }

        /// <summary>
        /// ToString override
        /// </summary>
        /// <returns>ID:Quantity</returns>
        public override string ToString()
        {
            return "2:" + quantity;
        }
    }
}
