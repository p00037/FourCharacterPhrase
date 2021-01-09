using FourCharacterPhrase.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace FourCharacterPhrase.Blazor.Pages
{
    public class MirrorBase : ComponentBase
    {
        public List<CellEntity> Cells { get; set; } = new List<CellEntity>();

        public string Name { get; set; } = "";

        private Timer timer;

        protected override void OnInitialized()
        {
            SetTimmer();
        }

        private void SetTimmer()
        {
            // Create a timer with a two second interval.
            timer = new Timer(1000);

            // Hook up the Elapsed event for the timer. 
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private async void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Cells = await WebApiService.GetRequest<List<CellEntity>, string>($"Cells?name={Name}");

            Console.WriteLine(Cells.First().Value);
            StateHasChanged();
        }

        protected string GetCss(CellStatus cellStatus)
        {
            switch (cellStatus)
            {
                case CellStatus.None:
                    return "btn-lg btn-default";
                case CellStatus.Selecting:
                    return "btn-lg btn-info";
                case CellStatus.Completed:
                    return "btn-lg btn-warning";
                default:
                    return "btn-lg btn-default";
            }

        }
    }
}
