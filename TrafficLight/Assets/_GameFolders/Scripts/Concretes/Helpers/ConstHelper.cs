namespace TrafficLight.Helpers
{
    public static class ConstHelper
    {
        public static string TrafficDataPathKey { get; }
        
        static ConstHelper()
        {
            TrafficDataPathKey = "TrafficLightData";
        }
    }
}