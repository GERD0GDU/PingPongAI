using System.ComponentModel;
using System.Runtime.CompilerServices;
using PingPongAI.AI;

namespace PingPongAI.App
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public Array AgentTypeItems{ get; } =
            Enum.GetValues(typeof(AgentTypes));

        private bool _isRunning = false;
        public bool IsRunning
        {
            get => _isRunning;
            set
            {
                _isRunning = value;
                OnPropertyChanged();
            }
        }

        private AgentTypes _leftAgentType = AgentTypes.RuleBased;
        public AgentTypes LeftAgentType
        {
            get => _leftAgentType;
            set
            {
                _leftAgentType = value;
                OnPropertyChanged();
            }
        }

        private AgentTypes _rightAgentType = AgentTypes.Human;
        public AgentTypes RightAgentType
        {
            get => _rightAgentType;
            set
            {
                _rightAgentType = value;
                OnPropertyChanged();
            }
        }

        private bool _isTrainingEnabled = false;
        public bool IsTrainingEnabled
        {
            get => _isTrainingEnabled;
            set
            {
                _isTrainingEnabled = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged = null;

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

}
