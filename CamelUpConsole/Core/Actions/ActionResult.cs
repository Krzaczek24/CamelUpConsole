namespace CamelUpConsole.Core.Actions
{
    internal class ActionResult
    {
        public Result Result { get; set; } = new Result();

        private ActionResult(Result result)
        {
            Result = result;
        }

        public static ActionResult FallBackResult => new ActionResult(new Result() { FallBack = true });
    }
}
