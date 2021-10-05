using LunarRover.Commands;
using LunarRover.Objects;

namespace LunarRover.Invokers
{
    public class VehicleInvoker
    {

        private Dictionary<string, ICommand> _commands = new();

        public VehicleInvoker AddCommand<TCommand>(string CommandKey, ScenarioContext context) where TCommand : class, ICommand
        {
            _commands.Remove(CommandKey);

            if (Activator.CreateInstance(typeof(TCommand), context) is ICommand command)
                _commands.Add(CommandKey, command);

            return this;
        }

        public VehicleInvoker AddCommand<TCommand>(string CommandKey, TCommand command) where TCommand : class, ICommand
        {
            _commands.Remove(CommandKey);

            _commands.Add(CommandKey, command);

            return this;
        }

        public void RunCommand(string commandKey)
        {
            if (_commands.TryGetValue(commandKey, out ICommand? command))
                command?.Execute();
            else
                return;
        }

        public void RunCommandList(IList<string> listOfCommands)
        {
            foreach(var command in listOfCommands)
                RunCommand(command);
        }

        public Dictionary<string, ICommand> GetCommands() => _commands;
    }
}
