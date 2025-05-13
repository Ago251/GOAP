namespace Ecosystem.AI.GOAP.Utility
{
    public static class DictionaryKeys
    {
        public static string NearObject(string obj) => $"Has_{obj}_Near";

        public static string InActionRange(string obj) => $"{obj}_InActionRange";

        public static string Hunger() => "Hunger";

        public static string Thirsty() => "Thirsty";

        public static string IsNearOppositeGender() => "IsNearOppositeGender";

        public static string ChildTotal() => "ChildTotal";

        public static string Status() => "Status";

        public static string HasObjective() => "HasObjective";
    }
}
