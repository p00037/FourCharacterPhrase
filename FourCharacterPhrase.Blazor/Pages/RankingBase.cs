using FourCharacterPhrase.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace FourCharacterPhrase.Blazor.Pages
{
    public class RankingBase : ComponentBase
    {
        protected List<AnswerNumberEntity> AnswerNumberEntitys = new List<AnswerNumberEntity>();

        private Timer timer;

        protected override void OnInitialized()
        {
            SetTimmer();
        }

        public async Task ResetDataAsync()
        {
            await WebApiService.DeleteRequest("AnswerNumber", "");
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
            AnswerNumberEntitys = await WebApiService.GetRequest<List<AnswerNumberEntity>, string>("AnswerNumber");
            StateHasChanged();
        }
    }
}
