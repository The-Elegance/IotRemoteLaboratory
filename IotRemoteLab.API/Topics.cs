namespace IotRemoteLab.API
{
    public static class Topics
    {
        // teriminal data from stand (all output)
        public const string TerminalDataFrom = "/lab/stand/+/serial/in";
        // terminal data from user (commands)
        public const string TerminalDataTo = "/lab/stand/+/serial/out";
        // button with led state (0/1) | Port - id of button
        public const string LedButtonState = "/lab/stand/+/gpio/led/#";
        // button without led state (0/1) | Port - id of button
        public const string ButtonNoLedState = "/lab/stand/+/gpio/button/#";
        // code complied output
        public const string DebugCodeOutput = "/lab/stand/+/debug/upload";
        // led on/off -> (0/1)
        public const string LedState = "/lab/stand/+/led";
        // webcamera on/off -> (0/1)
        public const string Webcamera = "/lab/stand/+/webcamera";

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
            ];
        }
    }
}
