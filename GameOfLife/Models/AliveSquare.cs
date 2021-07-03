using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace GameOfLife.Models
{
    public class AliveSquare : ObservableObject
    {
        #region Privat firelds

        private bool _isSelected;

        #endregion

        #region Constructor

        public AliveSquare()
        {
            ClickedBtnCommand = new RelayCommand(ClickedBtn);
        }

        #endregion

        #region Commands

        public ICommand ClickedBtnCommand { get; }

        #endregion

        #region Public properties

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                RaisePropertyChanged(nameof(IsSelected));
            }
        }

        #endregion

        #region Private methods

        private void ClickedBtn()
        {
            IsSelected = !IsSelected;
        }

        #endregion

    }
}
