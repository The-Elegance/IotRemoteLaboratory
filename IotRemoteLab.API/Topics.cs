namespace IotRemoteLab.API
{
    public static class Topics
    {
        public const string Base = "/lab/stand/+/";

        // teriminal data from stand (all output)
        public const string TerminalDataFrom = Base + "serial/in";
        // terminal data from user (commands)
        public const string TerminalDataTo = Base + "serial/out";
        // button with led state (0/1) | Port - id of button
        public const string LedButtonState = Base + "gpio/led/#";
        // button without led state (0/1) | Port - id of button
        public const string ButtonNoLedState = Base + "gpio/button/#";
        // code complied output
        public const string DebugCodeOutput = Base + "debug/upload";
        // led on/off -> (0/1)
        public const string LedState = Base + "led";
        // webcamera on/off -> (0/1)
        public const string Webcamera = Base + "webcamera";
        // uart type -> 1,2,3,4
        public const string UartType = Base + "uart";

        public static string[] ToArray() 
        {
            return [
                TerminalDataFrom,
                TerminalDataTo,
                LedButtonState,
                ButtonNoLedState,
                DebugCodeOutput,
                LedState,
                Webcamera,
                UartType
            ];
        }
    }
}
