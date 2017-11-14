using Chapter30_CalculatorUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter30_FuelEconomy
{
    public class FuelEconomyViewModel : BindableBase
    {
        public FuelEconomyViewModel()
        {
            InitializeFuelEcoTypes();
        }
        private List<FuelEconomyType> fuelEconTypes;
        public List<FuelEconomyType> FuelEconTypes
        {
            get
            {
                return this.fuelEconTypes;
            }
        }
        private void InitializeFuelEcoTypes()
        {
            var t1 = new FuelEconomyType
            {
                Id = "lpk",
                Text = "L/100 km",
                DistanceText = "Distance (kilometers)",
                FuelText = "Fuel used (liters)"
            };
            var t2 = new FuelEconomyType
            {
                Id = "mpg",
                Text = "Miles per gallon",
                DistanceText = "Distance (miles)",
                FuelText = "Fuel used (gallons)"
            };
            this.fuelEconTypes = new List<FuelEconomyType>() { t1, t2 };
        }
        private FuelEconomyType selectFuelEconomyType;
        public FuelEconomyType SelectFuelEconomyType
        {
            get
            {
                return this.selectFuelEconomyType;  
            }
            set
            {
                SetProperty(ref selectFuelEconomyType, value, "SelectFuelEconomyType");
            }
        }
        private string fuel;
        public string Fuel
        {
            get { return this.fuel; }
            set { SetProperty(ref fuel, value); }
        }
        private string distance;
        public string Distance
        {
            get { return distance; }
            set { SetProperty(ref distance, value); }
        }

        private string result;
        public string Result
        {
            get { return result; }
            set { SetProperty(ref result, value); }
        }
    }
}
