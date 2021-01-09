using FourCharacterPhrase.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using System.Timers;

namespace FourCharacterPhrase.Blazor.Pages
{
    public class IndexBase : ComponentBase
    {
        public BordEntity Bord { get; set; } = new BordEntity();

        public string validationMessage = "";

        public string Message { get; set; } = "";


        protected override void OnInitialized()
        {
            //Bord.SetData();
        }

        public async Task StartGame()
        {
            await Bord.SetData();

            StateHasChanged();
        }

        protected void BordClick(CellEntity cell)
        {
            Bord.Click(cell);

            Bord.PostCells();

            if (Bord.IsCompleted() == true)
            {
                Message = "Completed";
            }

            StateHasChanged();
        }

        protected string GetCss(CellStatus cellStatus)
        {
            switch (cellStatus)
            {
                case CellStatus.None:
                    return "btn btn-default";
                case CellStatus.Selecting:
                    return "btn btn-info";
                case CellStatus.Completed:
                    return "btn btn-warning";
                default:
                    return "btn btn-default";
            }
            
        }

        protected async void OnValidSubmit()
        {
            validationMessage = "Success!";
            await StartGame();
        }

        protected void OnInvalidSubmit()
        {
            validationMessage = "Failed!";
        }
    }
}

