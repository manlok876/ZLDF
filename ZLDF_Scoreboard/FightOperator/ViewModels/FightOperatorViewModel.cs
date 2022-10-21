using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Diagnostics;
using System.IO;
using System.DirectoryServices;
using Prism.Mvvm;
using Prism.Commands;
using ZLDF.Classes;
using ZLDF.Classes.Logging;
using ZLDF.Scoreboard.Scoreboard.Views;
using ZLDF.Scoreboard.Scoreboard.ViewModels;

namespace ZLDF.Scoreboard.FightOperator.ViewModels
{
	internal class FightOperatorViewModel : BindableBase
	{
		#region Fights

		private Duel _currentDuel;
		public Duel CurrentDuel
		{
			get
			{
				return _currentDuel;
			}
			private set
			{
				if (_currentDuel is not null)
				{
					_currentDuel.PropertyChanged -= ScoreChangedListener;
					CurDuelLogger?.CloseLogFile();
				}

				InitTimerFromDuel(value);

				SetProperty(ref _currentDuel!, value);

				_currentDuel!.PropertyChanged += ScoreChangedListener;

				CurDuelLogger = new FileLogger(GetLogPathForDuel(_currentDuel));
			}
		}

		public List<Duel> _duels = new List<Duel>();
		public IEnumerable<Duel> AllDuels
		{
			get
			{
				return new List<Duel>(_duels);
			}
		}

		static public Duel CreateEmptyDuel()
		{
			Duel dummyDuel = new Duel();
			dummyDuel.Init(new Fighter(), new Fighter());
			return dummyDuel;
		}

		protected void AddDuelToList(Duel newDuel)
		{
			if (_duels.Contains(newDuel))
			{
				return;
			}
			_duels.Add(newDuel);
			newDuel.State = EventState.Scheduled;
			RaisePropertyChanged(nameof(AllDuels));
		}

		protected void AddDuelsToList(IEnumerable<Duel> newDuels)
		{
			foreach (Duel newDuel in newDuels)
			{
				if (_duels.Contains(newDuel))
				{
					continue;
				}
				_duels.Add(newDuel);
				newDuel.State = EventState.Scheduled;
			}
			RaisePropertyChanged(nameof(AllDuels));
		}

		protected void ClearFightsList()
		{
			_duels.Clear();
			RaisePropertyChanged(nameof(AllDuels));
		}

		public Duel? GetNextUnfinishedFight(bool bLoop = true)
		{
			if (_duels.Count < 1)
			{
				return null;
			}

			int fightsFromStart = _duels.Count;
			int fightsFromEnd = 0;

			if (CurrentDuel is not null)
			{
				int currentFightIdx = _duels.IndexOf(CurrentDuel);
				if (currentFightIdx >= 0 && currentFightIdx < _duels.Count)
				{
					fightsFromStart = currentFightIdx;
					fightsFromEnd = (_duels.Count - 1) - currentFightIdx;
				}
			}

			List<Duel> reorderedDuels = new List<Duel>();
			if (fightsFromEnd > 0)
			{
				reorderedDuels.AddRange(_duels.GetRange(_duels.Count - fightsFromEnd, fightsFromEnd));
			}
			if (bLoop && fightsFromStart > 0)
			{
				reorderedDuels.AddRange(_duels.GetRange(0, fightsFromStart));
			}

			foreach (Duel duel in reorderedDuels)
			{
				if (!duel.IsOver)
				{
					return duel;
				}
			}

			return null;
		}

		public Duel? GetNextFight(bool bLoop = true)
		{
			if (_duels.Count < 1)
			{
				return null;
			}

			if (CurrentDuel is null)
			{
				return _duels[0];
			}

			int nextFightIdx = _duels.IndexOf(CurrentDuel) + 1;
			if (nextFightIdx == -1 || nextFightIdx >= _duels.Count)
			{
				nextFightIdx = 0;
			}

			return _duels[nextFightIdx];
		}

		public ICommand MoveToNextFightCommand { get; private set; }
		public void MoveToNextFight()
		{
			if (_duels.Count < 1)
			{
				AddDuelToList(CreateEmptyDuel());
			}

			Duel? nextDuel = GetNextFight();

			if (nextDuel is null)
			{
				MoveToFight(_duels[0]);
				return;
			}

			MoveToFight(nextDuel);
		}

		public ICommand MoveToFightCommand { get; private set; }
		public void MoveToFight(Duel targetFight)
		{
			if (!_duels.Contains(targetFight))
			{
				// TODO: log warning?
				return;
			}
			if (!CurrentDuel?.IsOver ?? false)
			{
				CurrentDuel!.State = EventState.Paused;
			}
			// TODO: log fight changed event
			CurrentDuel = targetFight;
		}
		public ICommand RestartFightCommand { get; private set; }
		public void RestartFight()
		{
			ResetScore();
			ResetTime();
		}
		public ICommand PostponeFightCommand { get; private set; }
		public void PostponeFight()
		{
			StopFightTimer();
			if (CurrentDuel.State == EventState.InProgress)
			{
				CurrentDuel.State = EventState.Paused;
			}
			MoveToNextFight();
		}
		public ICommand FinishFightCommand { get; private set; }
		public void FinishFight()
		{
			StopFightTimer();
			CurrentDuel.State = EventState.Finished;
			AddDuelToList(CreateEmptyDuel());
			MoveToNextFight();
		}

		#endregion // Fights

		public Fighter FirstFighter
		{
			get
			{
				return CurrentDuel.FirstFighter;
			}
		}
		public Fighter SecondFighter
		{
			get
			{
				return CurrentDuel.SecondFighter;
			}
		}

		private string _arenaText;
		public string ArenaText
		{
			get
			{
				return _arenaText;
			}
			set
			{
				SetProperty(ref _arenaText, value);
			}
		}

		#region Positioning

		private bool _bIsFlipped = false;
		public bool IsFlipped
		{
			get { return _bIsFlipped; }
			set
			{
				SetProperty(ref _bIsFlipped, value);
			}
		}

		public ICommand SwapSidesCommand { get; private set; }
		public void SwapSides() => IsFlipped = !IsFlipped;

		#endregion // Positioning

		#region Scoreboard

		private ScoreboardViewModel? _scoreboardVM;
		public ScoreboardViewModel? ScoreboardVM
		{
			get { return _scoreboardVM; }
			private set
			{
				SetProperty(ref _scoreboardVM, value);
			}
		}
		private ScoreboardView? _scoreboardWindow;

		public bool IsScoreboardOpened()
		{
			return _scoreboardWindow != null;
		}

		public ICommand CloseScoreboardCommand { get; private set; }
		public void CloseScoreboard()
		{
			if (_scoreboardWindow != null)
			{
				_scoreboardWindow.Close();
				_scoreboardWindow = null;
			}
		}

		public ICommand OpenScoreboardCommand { get; private set; }
		public void OpenScoreboard()
		{
			if (_scoreboardWindow != null)
			{
				_scoreboardWindow.Close();
				_scoreboardWindow = null;
			}

			if (ScoreboardVM == null)
			{
				ScoreboardVM = new ScoreboardViewModel(this);
			}

			_scoreboardWindow = new ScoreboardView();
			_scoreboardWindow.DataContext = ScoreboardVM;
			_scoreboardWindow.Show();
		}

		public ICommand MaximizeScoreboardCommand { get; private set; }
		public void MaximizeScoreboard()
		{
			if (_scoreboardWindow != null)
			{
				_scoreboardWindow.WindowState = System.Windows.WindowState.Maximized;
			}
		}

		#endregion // Scoreboard

		#region Scores

		public float FirstFighterScore
		{
			get
			{
				return CurrentDuel.FirstFighterScore;
			}
		}
		public float SecondFighterScore
		{
			get
			{
				return CurrentDuel.SecondFighterScore;
			}
		}

		internal void ScoreChangedListener(object? sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(CurrentDuel.FirstFighterScore))
			{
				RaisePropertyChanged(nameof(FirstFighterScore));
			}
			else if (e.PropertyName == nameof(CurrentDuel.SecondFighterScore))
			{
				RaisePropertyChanged(nameof(SecondFighterScore));
			}
		}

		public ICommand IncreaseFighterScoreCommand { get; private set; }
		public void IncreaseFighterScore(Fighter fighter)
		{
			CurrentDuel.AddFighterScore(fighter, 1);
			string FighterNum = fighter == FirstFighter ? "First" : "Second";
			LogDuelEvent(new StringMessage($"{FighterNum} fighter: +1 point"));
		}

		public ICommand DecreaseFighterScoreCommand { get; private set; }
		public void DecreaseFighterScore(Fighter fighter)
		{
			CurrentDuel.AddFighterScore(fighter, -1);
			string FighterNum = fighter == FirstFighter ? "First" : "Second";
			LogDuelEvent(new StringMessage($"{FighterNum} fighter: -1 point"));
		}
		protected void ResetScore()
		{
			CurrentDuel.FirstFighterScore = 0;
			CurrentDuel.SecondFighterScore = 0;
			LogDuelEvent(new StringMessage("Score reset"));
		}

		#endregion // Scores

		#region Time

		private CountdownTimer _fightTimer;
		public CountdownTimer FightTimer
		{
			get { return _fightTimer; }
			private set
			{
				SetProperty(ref _fightTimer, value);
			}
		}

		private void FightTick(object? sender, EventArgs e)
		{
			UpdateFightRemainingTime();
		}

		protected void UpdateFightRemainingTime()
		{
			if (CurrentDuel is null)
			{
				return;
			}
			CurrentDuel.RemainingTime = FightTimer.RemainingTime;
		}

		protected void InitTimerFromDuel(Duel duel)
		{
			StopFightTimer();
			FightTimer.TotalTime = duel.TotalTime;
			FightTimer.RemainingTime = duel.RemainingTime;
		}

		public ICommand StartStopCommand { get; private set; }
		public void StartStop()
		{
			if (FightTimer.IsEnabled)
			{
				StopFightTimer();
			}
			else
			{
				StartFightTimer();
			}
		}

		public void StartFightTimer()
		{
			if (FightTimer.IsEnabled)
			{
				return;
			}

			if (!CurrentDuel.HasStarted)
			{
				LogDuelEvent(new StringMessage("Began duel"));
			}

			if (!CurrentDuel.IsOver)
			{
				CurrentDuel.State = EventState.InProgress;
			}
			// TODO: check if we can continue fight
			FightTimer.Start();

			LogDuelEvent(new StringMessage("Started fight time"));
		}

		public void StopFightTimer()
		{
			if (!FightTimer.IsEnabled)
			{
				return;
			}
			FightTimer.Stop();

			LogDuelEvent(new StringMessage("Paused fight time"));
		}

		public void IncreaseRemainingTime(TimeSpan timeSpan)
		{
			FightTimer.RemainingTime += timeSpan;
			UpdateFightRemainingTime();
			LogDuelEvent(new StringMessage($"Fight time increased by {timeSpan.TotalSeconds} seconds"));
		}

		public void DecreaseRemainingTime(TimeSpan timeSpan)
		{
			FightTimer.RemainingTime -= timeSpan;
			UpdateFightRemainingTime();
			LogDuelEvent(new StringMessage($"Fight time decreased by {-timeSpan.TotalSeconds} seconds"));
		}

		public ICommand AddSecondsCommand { get; private set; }
		public void AddSeconds(float? seconds)
		{
			if (!seconds.HasValue)
			{
				return;
			}
			else if (seconds < 0)
			{
				DecreaseRemainingTime(TimeSpan.FromSeconds(-seconds.Value));
			}
			else if (seconds > 0)
			{
				IncreaseRemainingTime(TimeSpan.FromSeconds(seconds.Value));
			}
		}

		public ICommand ResetTimeCommand { get; private set; }
		protected void ResetTime()
		{
			FightTimer.Stop();
			FightTimer.Reset();
			UpdateFightRemainingTime();

			LogDuelEvent(new StringMessage("Fight time reset"));
		}

		#endregion // Time

		#region Colors

		private Color _firstFighterColor;
		public Color FirstFighterColor
		{
			get
			{
				return _firstFighterColor;
			}
			set
			{
				SetProperty(ref _firstFighterColor, value);
			}
		}

		private Color _secondFighterColor;
		public Color SecondFighterColor
		{
			get
			{
				return _secondFighterColor;
			}
			set
			{
				SetProperty(ref _secondFighterColor, value);
			}
		}

		public Color GetColorForFighter(Fighter? fighter)
		{
			if (fighter == FirstFighter)
			{
				return FirstFighterColor;
			}
			else if (fighter == SecondFighter)
			{
				return SecondFighterColor;
			}
			else
			{
				return Colors.White;
			}
		}

		#endregion Colors

		#region Sound

		public ICommand SetFightEndSoundCommand { get; private set; }
		public void SetFightEndSound(string filePath)
		{
			//w.sound = new SoundPlayer((String)filePath);
			//w.sound.Load();
		}

		#endregion // Sound

		#region Logging

		private FileLogger GlobalLogger { get; set; }

		protected void LogGlobal(Message message)
		{
			GlobalLogger.Log(message);
		}

		private FileLogger? CurDuelLogger { get; set; }

		public static string GetLogPathForDuel(Duel duel)
		{
			return $"Logs/Duel_{duel.Id}_" +
				$"({duel.FirstFighter.LastName}_{duel.FirstFighter.FirstName.First()}_vs_" +
				$"{duel.SecondFighter.LastName}_{duel.SecondFighter.FirstName.First()})" +
				$".txt";
		}

		protected void LogDuelEvent(Message message)
		{
			CurDuelLogger?.Log(message);
		}

		#endregion // Logging

		public ICommand ExitAppCommand { get; private set; }
		public void ExitApp()
		{
			CloseScoreboard();
		}

		public FightOperatorViewModel()
		{
			Directory.CreateDirectory("Logs");
			GlobalLogger = new FileLogger($"Logs/FOVM_Log_{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}.txt");

			ArenaText = "Ристалище 1";

			FirstFighterColor = Colors.Red;
			SecondFighterColor = Colors.Blue;

			_fightTimer = new CountdownTimer(DispatcherPriority.Send);
			_fightTimer.TickRate = 0.01;
			_fightTimer.Tick += FightTick;

			AddDuelToList(CreateEmptyDuel());
			MoveToNextFight();

			SwapSidesCommand = new DelegateCommand(SwapSides);

			CloseScoreboardCommand = new DelegateCommand(CloseScoreboard);
			OpenScoreboardCommand = new DelegateCommand(OpenScoreboard);
			MaximizeScoreboardCommand = new DelegateCommand(MaximizeScoreboard);

			IncreaseFighterScoreCommand = new DelegateCommand<Fighter>(IncreaseFighterScore);
			DecreaseFighterScoreCommand = new DelegateCommand<Fighter>(DecreaseFighterScore);

			StartStopCommand = new DelegateCommand(StartStop);
			AddSecondsCommand = new DelegateCommand<float?>(AddSeconds);
			ResetTimeCommand = new DelegateCommand(ResetTime);

			MoveToNextFightCommand = new DelegateCommand(MoveToNextFight);
			MoveToFightCommand = new DelegateCommand<Duel>(MoveToFight);
			RestartFightCommand = new DelegateCommand(RestartFight);
			PostponeFightCommand = new DelegateCommand(PostponeFight);
			FinishFightCommand = new DelegateCommand(FinishFight);
		}
	}
}
