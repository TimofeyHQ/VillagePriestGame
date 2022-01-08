namespace VillagePriestGame.Core
{
    public class Characteristic
    {
        public int MaxValue { get; private set; }
        public int CurrentValue { get; private set; }

        public Characteristic(int maxValue, int currentValue = 0)
        {
            if (currentValue < 0)
                currentValue = 0;
            if (currentValue >= maxValue)
            currentValue = maxValue;
            if (maxValue >= 0)
            {
                MaxValue = maxValue;
                CurrentValue = currentValue;
            }
            else
            throw new System.IndexOutOfRangeException ($"Characteristic's maximum value must be natural number or zero.");
        }

        public override string ToString()
        {
            return $"{CurrentValue} of {MaxValue}";
        }
        
        static public Characteristic operator ++ (Characteristic operand) 
        {
            if (operand.CurrentValue + 1 >= operand.MaxValue)
                return new Characteristic(operand.MaxValue, operand.MaxValue);
            else
                return new Characteristic(operand.MaxValue, operand.CurrentValue + 1);
        }

        static public Characteristic operator -- (Characteristic operand)
        {
            if (operand.CurrentValue - 1 <= 0)
                return new Characteristic(operand.MaxValue, 0);
            else
                return new Characteristic(operand.MaxValue, operand.CurrentValue - 1);
        }

        static public Characteristic operator + (Characteristic operand1, int operand2)
        {
            if (operand1.CurrentValue + operand2 >= operand1.MaxValue)
                return new Characteristic(operand1.MaxValue, operand1.MaxValue);
            else 
                return new Characteristic(operand1.MaxValue, operand1.CurrentValue + operand2);
        }
        

        static public Characteristic operator - (Characteristic operand1, int operand2)
        {
            if (operand1.CurrentValue - operand2 <= 0)
                return new Characteristic(operand1.MaxValue, 0);
            else 
                return new Characteristic(operand1.MaxValue, operand1.CurrentValue - operand2);
        }

        static public Characteristic operator * (Characteristic operand1, int operand2)
        {
            if (operand1.CurrentValue * operand2 >= operand1.MaxValue)
                return new Characteristic(operand1.MaxValue, operand1.MaxValue);
            else 
                return new Characteristic(operand1.MaxValue, operand1.CurrentValue * operand2);
        }

        static public Characteristic operator / (Characteristic operand1, int operand2)
        {
            if (operand1.CurrentValue / operand2 <= 1)
                return new Characteristic(operand1.MaxValue, 1);
            else 
                return new Characteristic(operand1.MaxValue, operand1.CurrentValue / operand2);
        }

        static public Characteristic operator % (Characteristic operand1, int operand2)
        {
            return new Characteristic(operand1.MaxValue, operand1.CurrentValue % operand2);
        }

        static public bool operator < (Characteristic operand1, Characteristic operand2)
        {
            if (operand1.MaxValue == operand2.MaxValue)
                return operand1.CurrentValue < operand2.CurrentValue;
            else
                return (operand1.CurrentValue / operand1.MaxValue) < 
                (operand2.CurrentValue / operand2.MaxValue);
        }
        
        static public bool operator > (Characteristic operand1, Characteristic operand2)
        {
            if (operand1.MaxValue == operand2.MaxValue)
                return operand1.CurrentValue > operand2.CurrentValue;
            else
                return (operand1.CurrentValue / operand1.MaxValue) > 
                (operand2.CurrentValue / operand2.MaxValue);
        }

        static public bool operator >= (Characteristic operand1, Characteristic operand2)
        {
            if (operand1.MaxValue == operand2.MaxValue)
                return operand1.CurrentValue >= operand2.CurrentValue;
            else
                return (operand1.CurrentValue / operand1.MaxValue) >= 
                (operand2.CurrentValue / operand2.MaxValue);
        }

        static public bool operator <= (Characteristic operand1, Characteristic operand2)
        {
            if (operand1.MaxValue == operand2.MaxValue)
                return operand1.CurrentValue <= operand2.CurrentValue;
            else
                return (operand1.CurrentValue / operand1.MaxValue) <= 
                (operand2.CurrentValue / operand2.MaxValue);
        }
        static public bool operator <= (Characteristic operand1, int operand2)
        {
                return operand1.CurrentValue <= operand2;
        }

        static public bool operator >= (Characteristic operand1, int operand2)
        {
                return operand1.CurrentValue >= operand2;
        }
        
        static public bool operator < (Characteristic operand1, int operand2)
        {
                return operand1.CurrentValue < operand2;
        }

        static public bool operator > (Characteristic operand1, int operand2)
        {
                return operand1.CurrentValue > operand2;
        }

        public decimal GetPercent()
        {
            return (decimal)this.CurrentValue / (decimal)this.MaxValue;
        }
    };
}