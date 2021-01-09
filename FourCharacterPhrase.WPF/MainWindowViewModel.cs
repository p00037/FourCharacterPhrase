using ExtensionsLibrary;
using FourCharacterPhrase.Shared;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace FourCharacterPhrase.WPF
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ReactiveProperty<BordEntity> Bord { get; set; } = new ReactiveProperty<BordEntity>(new BordEntity());

        public ReactiveCommand BordClickCommand { get; } = new ReactiveCommand();

        public MainWindowViewModel()
        {
            BordClickCommand.Subscribe(x => BordClick(x));

            Bord.Value.SetData();
        }

        private void BordClick(object entity)
        {
            Bord.Value.Click((CellEntity)entity);

            Bord.Value = Bord.Value.DeepCopy();

            if (Bord.Value.IsCompleted() == true)
            {
                MessageBox.Show("Completed");
            }
        }
    }
}
