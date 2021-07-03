

using System.Collections.Generic;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GameOfLife.Models;
using GameOfLife.Services;

namespace GameOfLife.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        #region Private fields

        private Calculation _calc;
        private Timer _timer;

        #endregion

        #region Constructor

        public MainViewModel()
        {
            _timer = new Timer(1000);
            _timer.Elapsed += (sender, args) => _calc.Calculate();

            StartPlayCommand = new RelayCommand(StartPlay, ()=>(!_timer.Enabled));
            StopPlayCommand = new RelayCommand(StopPlay, () => (_timer.Enabled));
            ClearPlayCommand = new RelayCommand(ClearPlay, () => (!_timer.Enabled));
            NextStepCommand = new RelayCommand(NextStep, () => (!_timer.Enabled));

            LifeGrid = GidGenerator();
            _calc = new Calculation(LifeGrid);
            
        }

        #endregion

        #region Commands

        public RelayCommand StartPlayCommand { get; }
        public RelayCommand StopPlayCommand { get; }
        public RelayCommand ClearPlayCommand { get; }
        public RelayCommand NextStepCommand { get; }

        #endregion

        #region Public properties

        public string Title => "Game of Life";

        public int WindowWidth => 900;

        public int WindowHeight => 700;

        public List<List<AliveSquare>> LifeGrid { get; }

        #endregion

        #region Privat methods

        private void StartPlay()
        {
            if (_calc.CountAlive() < 1)
            {
                MessageBox.Show("You need setup the first generation");
                return;
            }
            
            _timer.Start();
            StartPlayCommand.RaiseCanExecuteChanged();
            StopPlayCommand.RaiseCanExecuteChanged();
            ClearPlayCommand.RaiseCanExecuteChanged();
            NextStepCommand.RaiseCanExecuteChanged();
        }

        private void ClearPlay()
        {
            _calc.ClearSpace();
        }

        private void StopPlay()
        {
            _timer.Stop();
            StartPlayCommand.RaiseCanExecuteChanged();
            StopPlayCommand.RaiseCanExecuteChanged();
            ClearPlayCommand.RaiseCanExecuteChanged();
            NextStepCommand.RaiseCanExecuteChanged();
        }

        private void NextStep()
        {
            if (_calc.CountAlive() < 1)
            {
                MessageBox.Show("You need setup the first generation");
                return;
            }
            _calc.Calculate();
        }

        private List<List<AliveSquare>> GidGenerator()
        {
            var rows = new List<List<AliveSquare>>();

            int width = WindowWidth / 18;
            int height = (WindowHeight- 18*5) / 18 ;

            for (int i = 0; i < height; i++)
            {
                var columns = new List<AliveSquare>();
                for (int x = 0; x < width; x++)
                {
                    columns.Add(new AliveSquare());
                }
                rows.Add(columns);
            }
            return rows;
        }
        
        #endregion

    }
}
