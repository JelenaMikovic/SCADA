namespace scada_back.Services
{
    public class SimulationService
    {
        public static double CalculateValue(string address)
        {
            if (address == "SIN") return Sinus();
            else if (address == "COS") return Cosinus();
            else if (address == "RAMP") return Ramp();
            else return -1000;
        }

        private static double Sinus()
        {
            return 100 * Math.Sin((double)DateTime.Now.Second / 60 * Math.PI);
        }

        private static double Cosinus()
        {
            return 100 * Math.Cos((double)DateTime.Now.Second / 60 * Math.PI);
        }

        private static double Ramp()
        {
            return 100 * DateTime.Now.Second / 60;
        }
    }
}
