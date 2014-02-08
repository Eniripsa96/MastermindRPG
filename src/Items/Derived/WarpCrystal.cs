using MastermindRPG.Data;

namespace MastermindRPG.Items.Derived
{
    /// <summary>
    /// Warping item that returns user to town
    /// </summary>
    class WarpCrystal : Warping
    {
        /// <summary>
        /// Warp Crystal Constructor
        /// 
        /// Sets parameters in order to teleport to town
        /// </summary>
        /// <param name="quantity">number of crystals</param>
        public WarpCrystal(int quantity)
            : base(quantity, Constants.IntValue("warpCrystalCapacity"), new int[] { 0, 0, 0, 16, 6 })
        { }

        /// <summary>
        /// ToString override
        /// </summary>
        /// <returns>ID:Quantity</returns>
        public override string ToString()
        {
            return "3:" + quantity;
        }
    }
}
